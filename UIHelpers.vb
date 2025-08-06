Imports System.Windows.Forms

Public Module UIHelpers

    ''' <summary>
    ''' Displays an error message box. Based on show_error()
    ''' </summary>
    Public Sub ShowError(title As String, message As String)
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    ''' <summary>
    ''' Displays an informational message box. Based on show_info()
    ''' </summary>
    Public Sub ShowInfo(title As String, message As String)
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ''' <summary>
    ''' Displays a warning message box. Based on show_warning()
    ''' </summary>
    Public Sub ShowWarning(title As String, message As String)
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    ''' <summary>
    ''' Centers a popup form on its parent form. Based on center_top_popup()
    ''' </summary>
    Public Sub CenterTopPopup(parent As Form, popup As Form, Optional yOffset As Integer = 0)
        Dim parentX = parent.Location.X
        Dim parentY = parent.Location.Y
        Dim parentWidth = parent.Width

        ' Calculate the centered X position
        Dim x = parentX + (parentWidth / 2) - (popup.Width / 2)
        ' Set Y position to the top of the parent plus an offset
        Dim y = parentY + yOffset

        popup.StartPosition = FormStartPosition.Manual
        popup.Location = New Point(x, y)
    End Sub

End Module