Public Class AppState

    ' Private fields for dependencies
    Private ReadOnly _downtimeLogger As DowntimeLogger
    Private ReadOnly _movementLogger As OperatorMovementLogger

    ' Maps an operator name to a dictionary containing their active downtime "id"
    Private _activeDowntimes As Dictionary(Of String, Dictionary(Of String, String))

    ''' <summary>
    ''' Initializes the application state. Based on __init__ from app_state.py.
    ''' </summary>
    Public Sub New(downtimeLogger As DowntimeLogger, movementLogger As OperatorMovementLogger)
        _downtimeLogger = downtimeLogger
        _movementLogger = movementLogger
        _activeDowntimes = New Dictionary(Of String, Dictionary(Of String, String))
    End Sub

    ''' <summary>
    ''' On startup, scans today's log for open downtimes. Based on load_active_downtimes_from_log().
    ''' </summary>
    Public Sub LoadActiveDowntimesFromLog()
        Dim dateStr As String = _downtimeLogger.GetNow().ToString("yyyy-MM-dd") '
        Dim rawLog = _downtimeLogger.LoadLog(dateStr) '

        _activeDowntimes.Clear() '
        For Each entry In rawLog
            ' If end_time is DBNull, the downtime is active
            If entry.ContainsKey("end_time") AndAlso entry("end_time") Is DBNull.Value Then '
                Dim operatorName = entry("operator").ToString()
                Dim eventId = entry("id").ToString()
                If Not String.IsNullOrEmpty(operatorName) AndAlso Not String.IsNullOrEmpty(eventId) Then
                    _activeDowntimes(operatorName) = New Dictionary(Of String, String) From {
                        {"id", eventId},
                        {"date_str", dateStr}
                    } '
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Checks which of the given operators are already in an active downtime. Based on can_start_downtime().
    ''' </summary>
    Public Function CanStartDowntime(operators As List(Of String)) As List(Of String)
        Return operators.Where(Function(op) _activeDowntimes.ContainsKey(op)).ToList() '
    End Function

    ''' <summary>
    ''' Starts a downtime for the given operators. Based on start_downtime().
    ''' </summary>
    Public Sub StartDowntime(station As String, reason As String, operators As List(Of String))
        Dim ids As List(Of String) = _downtimeLogger.LogDowntimeStart(station, operators, reason) '
        Dim dateStr As String = _downtimeLogger.GetNow().ToString("yyyy-MM-dd")

        ' Loop through both lists simultaneously, like Python's zip()
        For i As Integer = 0 To operators.Count - 1
            Dim op = operators(i)
            Dim eventId = ids(i)
            _activeDowntimes(op) = New Dictionary(Of String, String) From {
                {"id", eventId},
                {"date_str", dateStr}
            } '
        Next
    End Sub

    ''' <summary>
    ''' Stops downtime for the given list of operators. Based on stop_downtime().
    ''' </summary>
    Public Sub StopDowntime(operators As List(Of String))
        Dim downtimeIds As New List(Of String)
        For Each op As String In operators
            If _activeDowntimes.ContainsKey(op) Then
                downtimeIds.Add(_activeDowntimes(op)("id")) '
            End If
        Next

        If downtimeIds.Count > 0 Then
            _downtimeLogger.LogDowntimeStop(downtimeIds) '
        End If

        ' Remove the operators from the active state
        For Each op As String In operators
            _activeDowntimes.Remove(op) '
        Next
    End Sub

    ''' <summary>
    ''' Returns a processed list of today's downtime log entries for UI display. Based on get_daily_log().
    ''' </summary>
    Public Function GetDailyLog() As List(Of Dictionary(Of String, Object))
        Dim dateStr = _downtimeLogger.GetNow().ToString("yyyy-MM-dd") '
        Dim rawLog = _downtimeLogger.LoadLog(dateStr) '
        Dim processed As New List(Of Dictionary(Of String, Object))

        For Each entry As Dictionary(Of String, Object) In rawLog
            Try
                Dim pEntry As New Dictionary(Of String, Object)
                pEntry("timestamp") = Convert.ToDateTime(entry("start_time")) '
                pEntry("category") = entry("category") '
                pEntry("event") = entry("downtime") '
                pEntry("operator") = entry("operator") '
                pEntry("station") = entry("station") '

                If entry("end_time") Is DBNull.Value Then
                    pEntry("status") = "Live" '
                    pEntry("end_time") = Nothing
                    pEntry("duration_minutes") = (_downtimeLogger.GetNow() - Convert.ToDateTime(pEntry("timestamp"))).TotalMinutes
                Else
                    pEntry("status") = "✔️" '
                    pEntry("end_time") = Convert.ToDateTime(entry("end_time"))
                    pEntry("duration_minutes") = Convert.ToDouble(entry("duration_minutes"))
                End If

                processed.Add(pEntry) '
            Catch ex As Exception
                Console.WriteLine($"[AppState] Skipping invalid log entry: {ex.Message}") '
            End Try
        Next

        ' The log is already sorted by the SQL query, but we can ensure it here to match Python's explicit sort
        Return processed.OrderBy(Function(entry) CType(entry("timestamp"), DateTime)).ToList() '
    End Function
End Class