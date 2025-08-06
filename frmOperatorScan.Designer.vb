<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOperatorScan
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
        Me.lblStation = New System.Windows.Forms.Label()
        Me.lblScanPrompt = New System.Windows.Forms.Label()
        Me.lblOperatorCount = New System.Windows.Forms.Label()
        Me.txtOperatorId = New System.Windows.Forms.TextBox()
        Me.lbxScannedOperators = New System.Windows.Forms.ListBox()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblStation
        '
        Me.lblStation.AutoSize = True
        Me.lblStation.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStation.Location = New System.Drawing.Point(103, 10)
        Me.lblStation.Name = "lblStation"
        Me.lblStation.Size = New System.Drawing.Size(191, 20)
        Me.lblStation.TabIndex = 0
        Me.lblStation.Text = "Station: [StationName]"
        '
        'lblScanPrompt
        '
        Me.lblScanPrompt.AutoSize = True
        Me.lblScanPrompt.Location = New System.Drawing.Point(140, 36)
        Me.lblScanPrompt.Name = "lblScanPrompt"
        Me.lblScanPrompt.Size = New System.Drawing.Size(116, 26)
        Me.lblScanPrompt.TabIndex = 1
        Me.lblScanPrompt.Text = "Scan Operator IDs to..." & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'lblOperatorCount
        '
        Me.lblOperatorCount.AutoSize = True
        Me.lblOperatorCount.Location = New System.Drawing.Point(146, 177)
        Me.lblOperatorCount.Name = "lblOperatorCount"
        Me.lblOperatorCount.Size = New System.Drawing.Size(104, 13)
        Me.lblOperatorCount.TabIndex = 2
        Me.lblOperatorCount.Text = "No operators added."
        '
        'txtOperatorId
        '
        Me.txtOperatorId.Location = New System.Drawing.Point(149, 55)
        Me.txtOperatorId.Name = "txtOperatorId"
        Me.txtOperatorId.Size = New System.Drawing.Size(100, 20)
        Me.txtOperatorId.TabIndex = 3
        '
        'lbxScannedOperators
        '
        Me.lbxScannedOperators.FormattingEnabled = True
        Me.lbxScannedOperators.Location = New System.Drawing.Point(109, 79)
        Me.lbxScannedOperators.Name = "lbxScannedOperators"
        Me.lbxScannedOperators.Size = New System.Drawing.Size(178, 95)
        Me.lbxScannedOperators.TabIndex = 4
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(109, 196)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(75, 23)
        Me.btnSubmit.TabIndex = 5
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(212, 196)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'frmOperatorScan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(397, 228)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.lbxScannedOperators)
        Me.Controls.Add(Me.txtOperatorId)
        Me.Controls.Add(Me.lblOperatorCount)
        Me.Controls.Add(Me.lblScanPrompt)
        Me.Controls.Add(Me.lblStation)
        Me.Name = "frmOperatorScan"
        Me.Text = "frmOperatorScan"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblStation As Label
    Friend WithEvents lblScanPrompt As Label
    Friend WithEvents lblOperatorCount As Label
    Friend WithEvents txtOperatorId As TextBox
    Friend WithEvents lbxScannedOperators As ListBox
    Friend WithEvents btnSubmit As Button
    Friend WithEvents btnCancel As Button
End Class
