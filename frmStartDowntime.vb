Public Class frmStartDowntime
    Private ReadOnly _operatorsMap As Dictionary(Of String, String)
    Private ReadOnly _eventsMap As Dictionary(Of String, String)
    Private _scannedOperatorNames As New List(Of String)

    Public ReadOnly Property ScannedOperators As List(Of String)
        Get
            Return _scannedOperatorNames
        End Get
    End Property
    Public SelectedEvent As String = ""

    Public Sub New(operatorsMap As Dictionary(Of String, String), eventsMap As Dictionary(Of String, String), stationName As String, Optional prefilledOperators As List(Of String) = Nothing)
        InitializeComponent()
        _operatorsMap = operatorsMap
        _eventsMap = eventsMap
        lblStation.Text = $"Station: {stationName}"

        If prefilledOperators IsNot Nothing Then
            _scannedOperatorNames.AddRange(prefilledOperators)
        End If
    End Sub

    Private Sub frmStartDowntime_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtOperatorId.Focus()
        ' Update the listbox if it was pre-filled.
        If _scannedOperatorNames.Any() Then
            UpdateListBox()
        End If
    End Sub

    Private Sub btnChooseEvent_Click(sender As Object, e As EventArgs) Handles btnChooseEvent.Click
        Using eventForm As New frmEventSelector(_eventsMap)
            If eventForm.ShowDialog(Me) = DialogResult.OK Then
                SelectedEvent = eventForm.SelectedEvent
                lblSelectedEvent.Text = $"Selected Downtime Event: {SelectedEvent}"
            End If
        End Using
    End Sub

    ' The Submit button is now much simpler. It just validates and closes.
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If _scannedOperatorNames.Count = 0 Then
            UIHelpers.ShowError("Missing Operators", "Please scan at least one operator.")
            Return
        End If
        If String.IsNullOrEmpty(SelectedEvent) Then
            UIHelpers.ShowError("Missing Event", "Please select a downtime event.")
            Return
        End If

        ' The form's job is done. It returns OK.
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

#Region "Operator Scanning Logic"
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub txtOperatorId_KeyDown(sender As Object, e As KeyEventArgs) Handles txtOperatorId.KeyDown
        If e.KeyCode = Keys.Enter Then AddOperator() : e.SuppressKeyPress = True
    End Sub
    Private Sub AddOperator()
        Dim opId = txtOperatorId.Text.Trim()
        If String.IsNullOrEmpty(opId) Then Return
        If _operatorsMap.ContainsKey(opId) Then
            Dim opName = _operatorsMap(opId)
            If Not _scannedOperatorNames.Contains(opName) Then
                _scannedOperatorNames.Add(opName)
                UpdateListBox()
            Else
                UIHelpers.ShowWarning("Duplicate", $"{opName} has already been scanned.")
            End If
        Else
            UIHelpers.ShowError("Invalid Operator", $"Operator ID '{opId}' was not recognized.")
        End If
        txtOperatorId.Clear()
        txtOperatorId.Focus()
    End Sub
    Private Sub lbxScannedOperators_DoubleClick(sender As Object, e As EventArgs) Handles lbxScannedOperators.DoubleClick
        If lbxScannedOperators.SelectedItem IsNot Nothing Then
            _scannedOperatorNames.Remove(lbxScannedOperators.SelectedItem.ToString())
            UpdateListBox()
        End If
    End Sub
    Private Sub UpdateListBox()
        lbxScannedOperators.DataSource = Nothing
        lbxScannedOperators.DataSource = _scannedOperatorNames
        lblOperatorCount.Text = $"{_scannedOperatorNames.Count} operators added."
    End Sub
#End Region
End Class