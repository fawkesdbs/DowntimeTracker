Imports Microsoft.Data.SqlClient
Imports System.Configuration

Public Class OperatorMovementLogger
    Implements IDisposable

    ' Private fields for dependencies
    Private ReadOnly _timeSync As TimeSync
    Private ReadOnly _conn As SqlConnection

    ''' <summary>
    ''' Initializes the logger and opens a database connection. Based on __init__ from the Python file.
    ''' </summary>
    Public Sub New(timeSync As TimeSync)
        _timeSync = timeSync
        ' Get the connection string from our shared Config module
        Dim connString = ConfigurationManager.ConnectionStrings("ServerConnection").ConnectionString
        _conn = New SqlConnection(connString)
        _conn.Open()
    End Sub

    ''' <summary>
    ''' Gets the current synchronized time. Equivalent to _now().
    ''' </summary>
    Private Function GetNow() As DateTime
        Return _timeSync.GetNow()
    End Function

    ''' <summary>
    ''' Logs a sign-in or sign-out event. Based on log_event().
    ''' </summary>
    ''' <returns>False if the operator is already in the target state, otherwise True.</returns>
    Public Function LogEvent(operatorName As String, station As String, state As String) As Boolean
        Try
            ' Check if operator is already in the target state
            Dim checkQuery = "SELECT TOP 1 state FROM operator_events WHERE operator = @operator ORDER BY event_time DESC"
            Using checkCmd As New SqlCommand(checkQuery, _conn)
                checkCmd.Parameters.AddWithValue("@operator", operatorName)
                Dim lastState = checkCmd.ExecuteScalar()
                If lastState IsNot Nothing AndAlso lastState.ToString() = state Then
                    Console.WriteLine($"[OperatorMovement] '{operatorName}' already '{state}'. Skipping.") '
                    Return False '
                End If
            End Using

            ' If not, insert the new event
            Dim insertQuery As String = "INSERT INTO operator_events (operator, station, event_time, state) VALUES (@operator, @station, @event_time, @state)"
            Using insertCmd As New SqlCommand(insertQuery, _conn)
                insertCmd.Parameters.AddWithValue("@operator", operatorName)
                insertCmd.Parameters.AddWithValue("@station", station)
                insertCmd.Parameters.AddWithValue("@event_time", GetNow())
                insertCmd.Parameters.AddWithValue("@state", state)
                Return insertCmd.ExecuteNonQuery() > 0
            End Using
        Catch ex As Exception
            UIHelpers.ShowError("Database Error", $"Failed to log operator event: {ex.Message}")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Builds and returns a map of {operator: station} for everyone whose latest event is 'Sign In'.
    ''' Based on get_current_station_map().
    ''' </summary>
    Public Function GetCurrentStationMap() As Dictionary(Of String, String)
        Dim stationMap As New Dictionary(Of String, String)
        ' This query finds the latest event for each operator and joins to get the station
        ' for those whose latest event is a 'Sign In'.
        Dim query As String = "
            WITH LatestEvents AS (
                SELECT operator, MAX(event_time) AS latest_time
                FROM operator_events
                GROUP BY operator
            )
            SELECT e.operator, e.station
            FROM operator_events e
            JOIN LatestEvents le ON e.operator = le.operator AND e.event_time = le.latest_time
            WHERE e.state = 'Sign In'"

        Try
            Using cmd As New SqlCommand(query, _conn)
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        stationMap.Add(reader("operator").ToString(), reader("station").ToString())
                    End While
                End Using
            End Using
        Catch ex As Exception
            UIHelpers.ShowError("Database Error", $"Failed to get current operator stations: {ex.Message}")
        End Try

        Return stationMap
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean

    ' This is the public method called to release resources, equivalent to Python's 'close()'
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    ' This method handles the actual cleanup.
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' Dispose managed resources like the database connection
                _conn?.Close()
                _conn?.Dispose()
            End If
        End If
        disposedValue = True
    End Sub
#End Region
End Class