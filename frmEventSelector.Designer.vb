<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEventSelector
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblDowntime = New System.Windows.Forms.Label()
        Me.lbxEvents = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'lblDowntime
        '
        Me.lblDowntime.AutoSize = True
        Me.lblDowntime.Location = New System.Drawing.Point(137, 23)
        Me.lblDowntime.Name = "lblDowntime"
        Me.lblDowntime.Size = New System.Drawing.Size(121, 13)
        Me.lblDowntime.TabIndex = 0
        Me.lblDowntime.Text = "Select Downtime Event:"
        '
        'lbxEvents
        '
        Me.lbxEvents.FormattingEnabled = True
        Me.lbxEvents.Location = New System.Drawing.Point(56, 57)
        Me.lbxEvents.Name = "lbxEvents"
        Me.lbxEvents.Size = New System.Drawing.Size(282, 238)
        Me.lbxEvents.TabIndex = 1
        '
        'frmEventSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(394, 319)
        Me.Controls.Add(Me.lbxEvents)
        Me.Controls.Add(Me.lblDowntime)
        Me.Name = "frmEventSelector"
        Me.Text = "Select Downtime Event"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblDowntime As Label
    Friend WithEvents lbxEvents As ListBox
End Class
