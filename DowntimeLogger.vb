Imports System.Data
Imports Microsoft.Data.SqlClient
Imports System.Configuration

Public Class DowntimeLogger
    Implements IDisposable

    ' Private fields for dependencies
    Private ReadOnly _timeSync As TimeSync
    Private ReadOnly _conn As SqlConnection
    Private ReadOnly _events As Dictionary(Of String, String)

    ''' <summary>
    ''' Initializes the logger, connects to the database. Based on __init__ from downtime_logger.py.
    ''' </summary>
    Public Sub New(timeSync As TimeSync, events As Dictionary(Of String, String))
        _timeSync = timeSync
        _events = events ' Store the events dictionary passed from the main form
        ' Get the connection string from our shared Config module
        Dim connString = ConfigurationManager.ConnectionStrings("ServerConnection").ConnectionString
        _conn = New SqlConnection(connString)
        _conn.Open() ' Open the connection and keep it open for the lifetime of the object
    End Sub

    ''' <summary>
    ''' Gets the current synchronized time from the TimeSync object.
    ''' </summary>
    Public Function GetNow() As DateTime
        Return _timeSync.GetNow()
    End Function

    ''' <summary>
    ''' Loads all downtime entries for a specified date. Based on load_log().
    ''' </summary>
    Public Function LoadLog(dateStr As String) As List(Of Dictionary(Of String, Object))
        Dim results As New List(Of Dictionary(Of String, Object))
        Dim query As String = "SELECT id, station, operator, downtime, category, start_time, end_time, duration_minutes FROM downtime_logs WHERE CAST(start_time AS DATE) = @date ORDER BY start_time ASC"

        Try
            Using cmd As New SqlCommand(query, _conn)
                cmd.Parameters.AddWithValue("@date", dateStr)
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim row As New Dictionary(Of String, Object)
                        For i As Integer = 0 To reader.FieldCount - 1
                            row.Add(reader.GetName(i), reader.GetValue(i))
                        Next
                        results.Add(row)
                    End While
                End Using
            End Using
        Catch ex As Exception
            UIHelpers.ShowError("Database Error", $"Failed to load downtime log: {ex.Message}")
        End Try

        Return results
    End Function

    ''' <summary>
    ''' Logs the start of a downtime event. Based on log_downtime_start().
    ''' </summary>
    Public Function LogDowntimeStart(station As String, operators As List(Of String), downtime As String) As List(Of String)
        Dim generatedIds As New List(Of String)
        Dim now As DateTime = GetNow()
        ' Get the category from the events dictionary, or use "Unknown" if not found
        Dim category As String = If(_events.ContainsKey(downtime), _events(downtime), "Unknown")

        Dim query As String = "INSERT INTO downtime_logs (id, station, operator, downtime, category, start_time) VALUES (@id, @station, @operator, @downtime, @category, @start_time)"

        For Each op As String In operators
            Dim entryId As String = Guid.NewGuid().ToString()
            Try
                Using cmd As New SqlCommand(query, _conn)
                    cmd.Parameters.AddWithValue("@id", entryId)
                    cmd.Parameters.AddWithValue("@station", station)
                    cmd.Parameters.AddWithValue("@operator", op)
                    cmd.Parameters.AddWithValue("@downtime", downtime)
                    cmd.Parameters.AddWithValue("@category", category)
                    cmd.Parameters.AddWithValue("@start_time", now)
                    cmd.ExecuteNonQuery()
                    generatedIds.Add(entryId)
                End Using
            Catch ex As Exception
                UIHelpers.ShowError("Database Error", $"Failed to log start of downtime for {op}: {ex.Message}")
            End Try
        Next

        Return generatedIds
    End Function

    ''' <summary>
    ''' Logs the end of one or more downtime events. Based on log_downtime_stop().
    ''' </summary>

    Public Function LogDowntimeStop(downtimeIds As List(Of String)) As List(Of String)
        Dim updatedIds As New List(Of String)
        If downtimeIds Is Nothing OrElse downtimeIds.Count = 0 Then Return updatedIds

        Dim now As DateTime = GetNow()
        Dim downtimesToStop As New List(Of Tuple(Of String, DateTime))

        Try
            ' Step 1: READ all necessary data robustly.
            Dim parameterNames As New List(Of String)
            For i = 0 To downtimeIds.Count - 1
                parameterNames.Add($"@id{i}")
            Next
            Dim selectQuery As String = $"SELECT id, start_time FROM downtime_logs WHERE id IN ({String.Join(",", parameterNames)}) AND end_time IS NULL"

            Using selectCmd As New SqlCommand(selectQuery, _conn)
                For i = 0 To downtimeIds.Count - 1
                    selectCmd.Parameters.AddWithValue(parameterNames(i), downtimeIds(i))
                Next

                Using reader As SqlDataReader = selectCmd.ExecuteReader()
                    ' Get the column indexes *before* the loop. This is the most robust way.
                    Dim idOrdinal As Integer = reader.GetOrdinal("id")
                    Dim startTimeOrdinal As Integer = reader.GetOrdinal("start_time")

                    While reader.Read()
                        ' Use the ordinals to read the data.
                        Dim currentId = reader.GetString(idOrdinal)
                        Dim currentStartTime = reader.GetDateTime(startTimeOrdinal)
                        downtimesToStop.Add(Tuple.Create(currentId, currentStartTime))
                    End While
                End Using ' Reader is fully closed here.
            End Using

            ' Step 2: WRITE all the updates now that the reader is closed.
            Dim updateQuery As String = "UPDATE downtime_logs SET end_time = @end_time, duration_minutes = @duration WHERE id = @id"
            For Each dt In downtimesToStop
                Dim id = dt.Item1
                Dim startTime = dt.Item2
                Dim durationMinutes = (now - startTime).TotalSeconds / 60

                Using updateCmd As New SqlCommand(updateQuery, _conn)
                    updateCmd.Parameters.AddWithValue("@end_time", now)
                    updateCmd.Parameters.AddWithValue("@duration", Math.Round(durationMinutes, 2))
                    updateCmd.Parameters.AddWithValue("@id", id)
                    If updateCmd.ExecuteNonQuery() > 0 Then
                        updatedIds.Add(id)
                    End If
                End Using
            Next

        Catch ex As Exception
            UIHelpers.ShowError("Database Error", $"Failed to execute stop downtime operation: {ex.Message}")
        End Try

        Return updatedIds
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