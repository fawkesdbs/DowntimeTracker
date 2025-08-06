Public Class Form1
    ' Declare class-level variables for core components
    Private _timeSync As TimeSync
    Private _downtimeLogger As DowntimeLogger
    Private _movementLogger As OperatorMovementLogger
    Private _appState As AppState

    ' Store the loaded operators and events
    Private _operators As Dictionary(Of String, String)
    Private _events As Dictionary(Of String, String)

    Public Sub New()
        ' Make the form completely transparent before it loads.
        Me.Opacity = 0

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' --- Step 1: Disable controls to prevent user interaction during load ---
        btnSignIn.Enabled = False
        btnSignOut.Enabled = False
        btnStartDT.Enabled = False
        btnStopDT.Enabled = False
        btnSummary.Enabled = False

        Try
            Debug.WriteLine("Form_Load started...")

            Debug.WriteLine("Loading icon...")
            Me.Icon = New Icon(Config.ResourcePath("assets/icon.ico"))

            Debug.WriteLine("Loading operators from database...")
            _operators = Config.GetOperatorsFromDB()

            Debug.WriteLine("Loading events from database...")
            _events = Config.GetEventsFromDB()

            Debug.WriteLine("Parsing station info file...")
            Dim stationNames = Config.ParseStationInfoFile()

            Debug.WriteLine($"Operators loaded: {_operators.Count}, Events loaded: {_events.Count}")
            If _operators.Count = 0 OrElse _events.Count = 0 Then
                UIHelpers.ShowError("Initialization Failed", "Could not load operators or events from the database. The application will now close.")
                Application.Exit()
                Return
            End If

            Debug.WriteLine("Initializing TimeSync...")
            _timeSync = New TimeSync()
            Await _timeSync.InitializeAsync()
            Debug.WriteLine("TimeSync initialized successfully.")

            Debug.WriteLine("Initializing Loggers...")
            _movementLogger = New OperatorMovementLogger(_timeSync)
            _downtimeLogger = New DowntimeLogger(_timeSync, _events)

            Debug.WriteLine("Initializing AppState...")
            _appState = New AppState(_downtimeLogger, _movementLogger)
            _appState.LoadActiveDowntimesFromLog()
            Debug.WriteLine("AppState initialized successfully.")

            ' --- UI Setup ---
            cmbStation.DataSource = stationNames
            If cmbStation.Items.Count > 0 Then
                cmbStation.SelectedIndex = 0
            End If

            UpdateLogDisplay()
            ResizeForm()
            Debug.WriteLine("Form_Load completed successfully.")

            Me.Opacity = 100

        Catch ex As Exception
            Debug.WriteLine($"CRITICAL ERROR in Form_Load: {ex.Message}")
            UIHelpers.ShowError("Form Load Error", "A critical error occurred while loading the application: " & vbCrLf & ex.Message)
            If Me.IsHandleCreated Then Me.Close() Else Application.Exit()
        Finally
            ' --- Step 3: Re-enable controls ---
            ' This code will run whether the load succeeded or failed,
            ' ensuring the UI is never left in a disabled state.
            btnSignIn.Enabled = True
            btnSignOut.Enabled = True
            btnStartDT.Enabled = True
            btnStopDT.Enabled = True
            btnSummary.Enabled = True
        End Try
    End Sub

    ' --- UI Update Logic (Equivalent to parts of CollapsibleLogFrame) ---
    Private Sub UpdateLogDisplay()
        Dim log = _appState.GetDailyLog()
        Dim station As String = If(cmbStation.SelectedItem IsNot Nothing, cmbStation.SelectedItem.ToString(), "")
        If String.IsNullOrEmpty(station) Then Return

        Dim stationLog = log.Where(Function(entry) entry("station").ToString() = station).ToList()

        If stationLog.Any() Then
            Dim lastEntry = stationLog.Last()
            lblLogSummary.Text = $"{Convert.ToDateTime(lastEntry("timestamp")):HH:mm:ss} | {lastEntry("event")} ({lastEntry("operator")})"
        Else
            lblLogSummary.Text = "No downtimes logged for this station today."
        End If

        If dgvLog.Visible Then
            dgvLog.Rows.Clear()
            For Each entry In stationLog
                Dim rowIndex = dgvLog.Rows.Add(
                    Convert.ToDateTime(entry("timestamp")).ToString("HH:mm:ss"),
                    entry("category").ToString(),
                    entry("event").ToString(),
                    entry("operator").ToString(),
                    entry("status").ToString()
                )
                If entry("status").ToString() = "Live" Then
                    dgvLog.Rows(rowIndex).DefaultCellStyle.BackColor = Color.LightGreen
                Else
                    dgvLog.Rows(rowIndex).DefaultCellStyle.BackColor = Color.LightYellow
                End If
            Next
        End If
    End Sub

    Private Sub ResizeForm()
        If pnlLogs.Visible Then
            Me.Height = 460
        Else
            Me.Height = 260
        End If
    End Sub

    ' --- Event Handlers for Buttons and Controls (Equivalent to methods in DowntimeTrackerUI) ---
    Private Sub btnToggleLog_Click(sender As Object, e As EventArgs) Handles btnToggle.Click
        pnlLogs.Visible = Not pnlLogs.Visible
        btnToggle.Text = If(pnlLogs.Visible, "▲ Hide Full Log", "▼ Show Full Log")
        UpdateLogDisplay()
        ResizeForm()
    End Sub

    Private Sub cmbStation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStation.SelectedIndexChanged
        UpdateLogDisplay()
    End Sub

    ' Equivalent to on_sign_in
    Private Sub btnSignIn_Click(sender As Object, e As EventArgs) Handles btnSignIn.Click
        Using signInForm As New frmOperatorScan("Sign In to Station", cmbStation.Text, _operators)
            UIHelpers.CenterTopPopup(Me, signInForm)
            If signInForm.ShowDialog(Me) = DialogResult.OK Then
                Dim scannedOps = signInForm.ScannedOperators
                If scannedOps.Count = 0 Then
                    UIHelpers.ShowWarning("No Operators", "No operators were scanned.")
                    Return
                End If

                Dim signedInOps As New List(Of String)
                Dim skippedOps As New List(Of String)

                For Each op In scannedOps
                    For Each entry In _appState.GetDailyLog().AsEnumerable().Reverse()
                        If entry("operator").ToString() = op AndAlso entry("event").ToString() = "Operator move" AndAlso entry("status").ToString() = "Live" Then
                            _appState.StopDowntime(New List(Of String) From {op})
                            Exit For
                        End If
                    Next

                    If _movementLogger.LogEvent(op, cmbStation.Text, "Sign In") Then
                        signedInOps.Add(op)
                    Else
                        Dim stationMap = _movementLogger.GetCurrentStationMap()
                        Dim lastStation As String = ""
                        If stationMap.TryGetValue(op, lastStation) Then
                            skippedOps.Add($"{op}: {lastStation}")
                        End If
                    End If
                Next

                If skippedOps.Any() Then
                    UIHelpers.ShowWarning("Already Signed In", $"The following operators were already signed in and were skipped:{vbCrLf}{String.Join(vbCrLf, skippedOps)}")
                End If
                If signedInOps.Any() Then
                    UIHelpers.ShowInfo("Sign In Complete", $"The following operators have signed in to {cmbStation.Text}:{vbCrLf}{String.Join(vbCrLf, signedInOps)}")
                End If

                UpdateLogDisplay()
            End If
        End Using
    End Sub

    ' Equivalent to on_sign_out
    Private Sub btnSignOut_Click(sender As Object, e As EventArgs) Handles btnSignOut.Click
        Dim operatorsToSignOut As List(Of String) = Nothing

        Do ' Start a loop that we can continue until the sign-out is successful or cancelled.

            ' Show the sign-out form. On the first pass, it will be empty.
            ' On subsequent passes, it will be pre-filled with the operators who still need to sign out.
            Using signOutForm As New frmOperatorScan("Sign Out of Station", cmbStation.Text, _operators, operatorsToSignOut)
                UIHelpers.CenterTopPopup(Me, signOutForm)
                If signOutForm.ShowDialog(Me) <> DialogResult.OK Then
                    Exit Do ' User cancelled, so exit the loop.
                End If
                operatorsToSignOut = signOutForm.ScannedOperators
            End Using

            If operatorsToSignOut Is Nothing OrElse operatorsToSignOut.Count = 0 Then
                UIHelpers.ShowWarning("No Operators", "No operators were scanned.")
                Exit Do
            End If

            ' Check for operators currently in downtime
            Dim operatorsInDowntime = _appState.CanStartDowntime(operatorsToSignOut)

            If operatorsInDowntime.Any() Then
                ' --- REDIRECTION LOGIC ---
                Dim opStr = String.Join(vbCrLf, operatorsInDowntime)
                UIHelpers.ShowError("Cannot Sign Out", $"The following operator(s) are in active downtime and must stop their downtime first:{vbCrLf}{opStr}")

                ' Open the Stop Downtime form, pre-filled with the conflicting operators
                Using stopDowntimeForm As New frmOperatorScan("Stop Downtime", cmbStation.Text, _operators, operatorsInDowntime)
                    UIHelpers.CenterTopPopup(Me, stopDowntimeForm)
                    If stopDowntimeForm.ShowDialog(Me) = DialogResult.OK Then
                        _appState.StopDowntime(stopDowntimeForm.ScannedOperators)
                        UIHelpers.ShowInfo("Downtime Ended", "Downtime has been stopped.")
                        UpdateLogDisplay()
                        ' The loop will now continue, showing the sign-out screen again with the original list.
                    Else
                        ' User cancelled the "Stop Downtime" part, so we exit the whole workflow.
                        Exit Do
                    End If
                End Using
            Else
                ' --- SUCCESS CASE ---
                ' If there were no operators in downtime, we can sign them out and exit the loop.
                For Each op In operatorsToSignOut
                    _movementLogger.LogEvent(op, cmbStation.Text, "Sign Out")
                Next

                UIHelpers.ShowInfo("Sign Out Complete", $"The following operators have signed out:{vbCrLf}{String.Join(vbCrLf, operatorsToSignOut)}")
                UpdateLogDisplay()
                Exit Do ' The workflow is complete.
            End If
        Loop
    End Sub

    ' Equivalent to on_start_downtime
    Private Sub btnStartDowntime_Click(sender As Object, e As EventArgs) Handles btnStartDT.Click
        Dim scannedOps As List(Of String) = Nothing
        Dim selectedEvent As String = ""

        Do ' Loop until the entire workflow is complete or cancelled.

            ' --- Step 1: Show the Start Downtime form ---
            Using startForm As New frmStartDowntime(_operators, _events, cmbStation.Text, scannedOps)
                UIHelpers.CenterTopPopup(Me, startForm)
                If startForm.ShowDialog(Me) <> DialogResult.OK Then
                    Exit Do ' User cancelled, so exit the workflow.
                End If
                ' Get the data from the form
                scannedOps = startForm.ScannedOperators
                selectedEvent = startForm.SelectedEvent
            End Using

            ' --- Step 2: Validate if operators are signed in ---
            Dim notSignedIn = scannedOps.Where(Function(op) Not _movementLogger.GetCurrentStationMap().ContainsKey(op)).ToList()

            If notSignedIn.Any() Then
                ' --- Step 3: If not signed in, redirect to the Sign In form ---
                Dim opStr = String.Join(vbCrLf, notSignedIn)
                UIHelpers.ShowInfo("Sign In Required", $"The following operator(s) must be signed in first:{vbCrLf}{opStr}")

                Using signInForm As New frmOperatorScan("Sign In to Station", cmbStation.Text, _operators, notSignedIn)
                    UIHelpers.CenterTopPopup(Me, signInForm)
                    If signInForm.ShowDialog(Me) = DialogResult.OK Then
                        ' The user confirmed the sign in, so log the events.
                        For Each opToSignIn In signInForm.ScannedOperators
                            _movementLogger.LogEvent(opToSignIn, cmbStation.Text, "Sign In")
                        Next
                        ' Continue the loop, which will re-show the Start Downtime form,
                        ' now with the original list of operators pre-filled.
                        Continue Do
                    Else
                        ' The user cancelled the sign-in part, so exit the entire workflow.
                        Exit Do
                    End If
                End Using
            End If

            ' --- Step 4: If everyone is signed in, start the downtime ---
            Dim alreadyInDowntime = _appState.CanStartDowntime(scannedOps)
            If alreadyInDowntime.Any() Then
                _appState.StopDowntime(alreadyInDowntime)
                UIHelpers.ShowInfo("Previous Downtime Ended", $"Previous downtime for the following operator(s) was automatically ended: {vbCrLf}{String.Join(vbCrLf, alreadyInDowntime)}")
            End If

            If selectedEvent = "Operator move" Then
                For Each op In scannedOps
                    Dim prevStation = _movementLogger.GetCurrentStationMap().Item(op)
                    _movementLogger.LogEvent(op, prevStation, "Sign Out")
                    _appState.StartDowntime(prevStation, selectedEvent, New List(Of String) From {op})
                Next
            Else
                _appState.StartDowntime(cmbStation.Text, selectedEvent, scannedOps)
            End If

            UIHelpers.ShowInfo("Downtime Started", $"Downtime '{selectedEvent}' started for:{vbCrLf}{String.Join(vbCrLf, scannedOps)}")
            UpdateLogDisplay()
            Exit Do ' The workflow is complete, exit the loop.

        Loop
    End Sub


    ' Equivalent to on_stop_downtime
    Private Sub btnStopDowntime_Click(sender As Object, e As EventArgs) Handles btnStopDT.Click
        Using stopForm As New frmOperatorScan("Stop Downtime", cmbStation.Text, _operators)
            UIHelpers.CenterTopPopup(Me, stopForm)
            If stopForm.ShowDialog(Me) = DialogResult.OK Then
                Dim scannedOps = stopForm.ScannedOperators
                If scannedOps.Count = 0 Then
                    UIHelpers.ShowWarning("No Operators", "No operators were scanned.")
                    Return
                End If

                _appState.StopDowntime(scannedOps)
                UIHelpers.ShowInfo("Downtime Ended", $"Downtime ended for:{vbCrLf}{String.Join(vbCrLf, scannedOps)}")
                UpdateLogDisplay()
            End If
        End Using
    End Sub

    ' Equivalent to show_operator_summary_popup
    Private Sub btnSummary_Click(sender As Object, e As EventArgs) Handles btnSummary.Click
        Using summaryForm As New frmOperatorSummary(_appState.GetDailyLog())
            UIHelpers.CenterTopPopup(Me, summaryForm)
            summaryForm.ShowDialog(Me)
        End Using
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        _timeSync?.Dispose()
        _downtimeLogger?.Dispose()
        _movementLogger?.Dispose()
    End Sub
End Class