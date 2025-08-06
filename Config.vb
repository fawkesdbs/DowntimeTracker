Imports Microsoft.Data.SqlClient
Imports System.IO
Imports System.Configuration

Public Module Config

    ''' <summary>
    ''' Securely retrieves the connection string from the App.config file.
    ''' </summary>
    Public Function GetConnectionString() As String
        Return ConfigurationManager.ConnectionStrings("ServerConnection").ConnectionString
    End Function

    ''' <summary>
    ''' Parses the config.txt file to get station names, mirroring parse_station_info_file().
    ''' </summary>
    ''' <returns>A list of station names.</returns>
    Public Function ParseStationInfoFile() As List(Of String)
        Dim stationNames As New List(Of String)
        ' Define possible locations for config.txt.
        Dim possiblePaths As String() = {
            Path.Combine("..", "StationInfo", "config.txt"),
            Path.Combine(Application.StartupPath, "config.txt")
        }

        For Each path As String In possiblePaths
            If File.Exists(path) Then
                Dim currentSection As String = ""
                For Each line As String In File.ReadLines(path)
                    Dim trimmedLine = line.Trim()
                    ' Check for section headers.
                    If String.IsNullOrEmpty(trimmedLine) OrElse trimmedLine.StartsWith("#") Then
                        If trimmedLine.Contains("Stations on PC") Then
                            currentSection = "stations"
                        Else
                            currentSection = "" ' Reset on other comments or blank lines
                        End If
                        Continue For
                    End If

                    If currentSection = "stations" Then
                        stationNames.Add(trimmedLine) ' Add station names to the list.
                    End If
                Next
                Exit For ' Stop after finding the first valid config file.
            End If
        Next

        ' If no stations were found, add a default value.
        If stationNames.Count = 0 Then
            stationNames.Add("Unknown")
        End If

        Return stationNames
    End Function

    ''' <summary>
    ''' Fetches operators from the database, mirroring get_operators_from_db().
    ''' </summary>
    ''' <returns>A dictionary mapping operator_id to operator_name.</returns>
    Public Function GetOperatorsFromDB() As Dictionary(Of String, String)
        Dim operators As New Dictionary(Of String, String)
        Dim query As String = "SELECT operator_id, operator_name FROM operators" '

        Try
            Using conn As New SqlConnection(GetConnectionString())
                Using cmd As New SqlCommand(query, conn)
                    conn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim operatorId = reader("operator_id").ToString()
                            Dim operatorName = reader("operator_name").ToString()
                            operators.Add(operatorId, operatorName)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' In a real application, you would show this error to the user.
            Console.WriteLine($"Error loading operators from database: {ex.Message}") '
        End Try

        Return operators
    End Function

    ''' <summary>
    ''' Fetches events from the database, mirroring get_events_from_db().
    ''' </summary>
    ''' <returns>A dictionary mapping event_name to event_category.</returns>
    Public Function GetEventsFromDB() As Dictionary(Of String, String)
        Dim events As New Dictionary(Of String, String)
        Dim query As String = "SELECT event_name, event_category FROM events" '

        Try
            Using conn As New SqlConnection(GetConnectionString())
                Using cmd As New SqlCommand(query, conn)
                    conn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim eventName = reader("event_name").ToString()
                            Dim eventCategory = reader("event_category").ToString()
                            events.Add(eventName, eventCategory)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine($"Error loading events from database: {ex.Message}") '
        End Try

        Return events
    End Function

    ''' <summary>
    ''' Gets the absolute path to a resource, equivalent to resource_path().
    ''' </summary>
    Public Function ResourcePath(ByVal relativePath As String) As String
        ' Application.StartupPath gives the directory of the .exe file
        Return Path.Combine(Application.StartupPath, relativePath)
    End Function

End Module