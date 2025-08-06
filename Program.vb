Friend Module Program
    ''' <summary>
    ''' The main entry point for the application.
    ''' </summary>
    <STAThread>
    Friend Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        ' Add a global exception handler to catch any unhandled errors.
        AddHandler Application.ThreadException, AddressOf Application_ThreadException

        Try
            ' This is the original line that starts your app.
            Application.Run(New Form1())
        Catch ex As Exception
            ' This will catch any error during the startup of Form1.
            MessageBox.Show("A critical error occurred on startup: " & vbCrLf & ex.Message & vbCrLf & vbCrLf & "The application will now close.", "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' This subroutine will handle any UI thread errors that occur after the app has started.
    ''' </summary>
    Private Sub Application_ThreadException(sender As Object, e As Threading.ThreadExceptionEventArgs)
        MessageBox.Show("An unexpected error occurred: " & vbCrLf & e.Exception.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

End Module