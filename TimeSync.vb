Imports System.Net.Http
Imports System.Threading
Imports Newtonsoft.Json

Public Class TimeSync
    Implements IDisposable

    ' Private fields to hold the sync state
    Private _syncTime As Nullable(Of DateTime)
    Private _localReference As Nullable(Of DateTime)
    Private ReadOnly _lockObject As New Object()
    Private ReadOnly _resyncInterval As TimeSpan
    Private ReadOnly _stopEvent As New ManualResetEvent(False)
    Private _syncThread As Thread

    ''' <summary>
    ''' Initializes the time synchronizer, mirroring the Python __init__ method.
    ''' </summary>
    Public Sub New(Optional resyncIntervalMinutes As Integer = 30)
        _resyncInterval = TimeSpan.FromMinutes(resyncIntervalMinutes)
    End Sub

    ''' <summary>
    ''' This new method handles the initial async setup.
    ''' </summary>
    Public Async Function InitializeAsync() As Task
        ' Perform the initial sync asynchronously
        Await SyncAsync()

        ' Start the background thread AFTER the first sync is complete
        _syncThread = New Thread(AddressOf ResyncLoop)
        _syncThread.IsBackground = True
        _syncThread.Start()
    End Function

    ''' <summary>
    ''' Fetches the current time from the online API asynchronously. Mirrors the sync() method.
    ''' </summary>
    Public Async Function SyncAsync() As Task
        Try
            Using client As New HttpClient()
                ' The API URL from the Python script
                Dim response = Await client.GetAsync("https://timeapi.io/api/Time/current/zone?timeZone=Africa/Johannesburg")
                response.EnsureSuccessStatusCode() ' Throws an exception if the HTTP request fails

                Dim jsonString = Await response.Content.ReadAsStringAsync()
                ' Use Newtonsoft.Json to parse the response into our helper class
                Dim timeData = JsonConvert.DeserializeObject(Of TimeApiResponse)(jsonString)

                ' Lock shared variables to ensure thread safety during update
                SyncLock _lockObject
                    _syncTime = timeData.dateTime
                    _localReference = DateTime.Now
                End SyncLock

                Console.WriteLine($"[TimeSync] Synced at {_localReference}, online time: {_syncTime}")
            End Using
        Catch ex As Exception
            Console.WriteLine($"[TimeSync] Sync failed: {ex.Message}")
        End Try
    End Function

    ''' <summary>
    ''' The background thread's main loop, which calls SyncAsync periodically. Mirrors _resync_loop().
    ''' </summary>
    Private Sub ResyncLoop()
        ' This loop continues until the _stopEvent is signaled (e.g., when Dispose is called)
        While Not _stopEvent.WaitOne(_resyncInterval)
            SyncAsync().GetAwaiter().GetResult()
        End While
    End Sub

    ''' <summary>
    ''' Gets the current synchronized time. Mirrors get_now().
    ''' </summary>
    Public Function GetNow() As DateTime
        SyncLock _lockObject
            ' If sync hasn't occurred yet, return the local system time as a fallback
            If Not _syncTime.HasValue OrElse Not _localReference.HasValue Then
                Return DateTime.Now
            End If

            ' Calculate the current time based on the last sync and the elapsed local time
            Dim delta As TimeSpan = DateTime.Now - _localReference.Value
            Return _syncTime.Value + delta
        End SyncLock
    End Function

    ''' <summary>
    ''' Signals the background thread to stop and waits for it to finish. Mirrors stop().
    ''' This is called as part of the IDisposable pattern.
    ''' </summary>
    Public Sub StopSync()
        _stopEvent.Set() ' Signal the event to break the ResyncLoop
        _syncThread?.Join() ' Wait for the background thread to terminate gracefully
    End Sub

    ' Helper class to model the expected JSON response from the API
    Private Class TimeApiResponse
        Public Property dateTime As DateTime
    End Class

#Region "IDisposable Support"
    Private disposedValue As Boolean

    ' This method handles the cleanup of resources.
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' Dispose managed state (managed objects) like the thread and event.
                StopSync()
                _stopEvent.Dispose()
            End If
        End If
        disposedValue = True
    End Sub

    ' This is the public method called to release resources.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class