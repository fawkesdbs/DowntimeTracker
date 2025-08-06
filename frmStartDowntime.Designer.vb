<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStartDowntime
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
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.lbxScannedOperators = New System.Windows.Forms.ListBox()
        Me.txtOperatorId = New System.Windows.Forms.TextBox()
        Me.lblOperatorCount = New System.Windows.Forms.Label()
        Me.lblScanPrompt = New System.Windows.Forms.Label()
        Me.lblStation = New System.Windows.Forms.Label()
        Me.lblSelectedEvent = New System.Windows.Forms.Label()
        Me.btnChooseEvent = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(219, 256)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 13
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(107, 256)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(75, 23)
        Me.btnSubmit.TabIndex = 12
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'lbxScannedOperators
        '
        Me.lbxScannedOperators.FormattingEnabled = True
        Me.lbxScannedOperators.Location = New System.Drawing.Point(109, 79)
        Me.lbxScannedOperators.Name = "lbxScannedOperators"
        Me.lbxScannedOperators.Size = New System.Drawing.Size(178, 95)
        Me.lbxScannedOperators.TabIndex = 11
        '
        'txtOperatorId
        '
        Me.txtOperatorId.Location = New System.Drawing.Point(149, 55)
        Me.txtOperatorId.Name = "txtOperatorId"
        Me.txtOperatorId.Size = New System.Drawing.Size(100, 20)
        Me.txtOperatorId.TabIndex = 10
        '
        'lblOperatorCount
        '
        Me.lblOperatorCount.AutoSize = True
        Me.lblOperatorCount.Location = New System.Drawing.Point(146, 177)
        Me.lblOperatorCount.Name = "lblOperatorCount"
        Me.lblOperatorCount.Size = New System.Drawing.Size(104, 13)
        Me.lblOperatorCount.TabIndex = 9
        Me.lblOperatorCount.Text = "No operators added."
        '
        'lblScanPrompt
        '
        Me.lblScanPrompt.AutoSize = True
        Me.lblScanPrompt.Location = New System.Drawing.Point(140, 36)
        Me.lblScanPrompt.Name = "lblScanPrompt"
        Me.lblScanPrompt.Size = New System.Drawing.Size(98, 13)
        Me.lblScanPrompt.TabIndex = 8
        Me.lblScanPrompt.Text = "Scan Operator IDs:"
        '
        'lblStation
        '
        Me.lblStation.AutoSize = True
        Me.lblStation.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStation.Location = New System.Drawing.Point(103, 10)
        Me.lblStation.Name = "lblStation"
        Me.lblStation.Size = New System.Drawing.Size(191, 20)
        Me.lblStation.TabIndex = 7
        Me.lblStation.Text = "Station: [StationName]"
        '
        'lblSelectedEvent
        '
        Me.lblSelectedEvent.AutoSize = True
        Me.lblSelectedEvent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSelectedEvent.Location = New System.Drawing.Point(116, 231)
        Me.lblSelectedEvent.Name = "lblSelectedEvent"
        Me.lblSelectedEvent.Size = New System.Drawing.Size(164, 15)
        Me.lblSelectedEvent.TabIndex = 14
        Me.lblSelectedEvent.Text = "Selected Downtime Event: None"
        Me.lblSelectedEvent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnChooseEvent
        '
        Me.btnChooseEvent.Location = New System.Drawing.Point(156, 198)
        Me.btnChooseEvent.Name = "btnChooseEvent"
        Me.btnChooseEvent.Size = New System.Drawing.Size(75, 23)
        Me.btnChooseEvent.TabIndex = 15
        Me.btnChooseEvent.Text = "Choose Downtime Event"
        Me.btnChooseEvent.UseVisualStyleBackColor = True
        '
        'frmStartDowntime
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(397, 288)
        Me.Controls.Add(Me.btnChooseEvent)
        Me.Controls.Add(Me.lblSelectedEvent)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.lbxScannedOperators)
        Me.Controls.Add(Me.txtOperatorId)
        Me.Controls.Add(Me.lblOperatorCount)
        Me.Controls.Add(Me.lblScanPrompt)
        Me.Controls.Add(Me.lblStation)
        Me.Name = "frmStartDowntime"
        Me.Text = "Start Downtime"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnCancel As Button
    Friend WithEvents btnSubmit As Button
    Friend WithEvents lbxScannedOperators As ListBox
    Friend WithEvents txtOperatorId As TextBox
    Friend WithEvents lblOperatorCount As Label
    Friend WithEvents lblScanPrompt As Label
    Friend WithEvents lblStation As Label
    Friend WithEvents lblSelectedEvent As Label
    Friend WithEvents btnChooseEvent As Button
End Class
