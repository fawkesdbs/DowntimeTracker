<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.cmbStation = New System.Windows.Forms.ComboBox()
        Me.btnSummary = New System.Windows.Forms.Button()
        Me.btnSignIn = New System.Windows.Forms.Button()
        Me.btnSignOut = New System.Windows.Forms.Button()
        Me.btnStartDT = New System.Windows.Forms.Button()
        Me.btnStopDT = New System.Windows.Forms.Button()
        Me.grpDowntime = New System.Windows.Forms.GroupBox()
        Me.pnlLogs = New System.Windows.Forms.Panel()
        Me.dgvLog = New System.Windows.Forms.DataGridView()
        Me.TimeCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CategoryCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DowntimeCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OperatorCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StatusCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblLogSummary = New System.Windows.Forms.Label()
        Me.btnToggle = New System.Windows.Forms.Button()
        Me.grpDowntime.SuspendLayout()
        Me.pnlLogs.SuspendLayout()
        CType(Me.dgvLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblStation
        '
        Me.lblStation.AutoSize = True
        Me.lblStation.Location = New System.Drawing.Point(12, 10)
        Me.lblStation.Name = "lblStation"
        Me.lblStation.Size = New System.Drawing.Size(46, 13)
        Me.lblStation.TabIndex = 0
        Me.lblStation.Text = "Station: "
        '
        'cmbStation
        '
        Me.cmbStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStation.FormattingEnabled = True
        Me.cmbStation.Location = New System.Drawing.Point(58, 6)
        Me.cmbStation.Name = "cmbStation"
        Me.cmbStation.Size = New System.Drawing.Size(121, 21)
        Me.cmbStation.TabIndex = 1
        '
        'btnSummary
        '
        Me.btnSummary.Location = New System.Drawing.Point(365, 5)
        Me.btnSummary.Name = "btnSummary"
        Me.btnSummary.Size = New System.Drawing.Size(75, 23)
        Me.btnSummary.TabIndex = 2
        Me.btnSummary.Text = "Operator Summary"
        Me.btnSummary.UseVisualStyleBackColor = True
        '
        'btnSignIn
        '
        Me.btnSignIn.BackColor = System.Drawing.Color.Green
        Me.btnSignIn.ForeColor = System.Drawing.Color.White
        Me.btnSignIn.Location = New System.Drawing.Point(446, 5)
        Me.btnSignIn.Name = "btnSignIn"
        Me.btnSignIn.Size = New System.Drawing.Size(40, 23)
        Me.btnSignIn.TabIndex = 3
        Me.btnSignIn.Text = "In"
        Me.btnSignIn.UseVisualStyleBackColor = False
        '
        'btnSignOut
        '
        Me.btnSignOut.BackColor = System.Drawing.Color.Red
        Me.btnSignOut.ForeColor = System.Drawing.Color.White
        Me.btnSignOut.Location = New System.Drawing.Point(492, 5)
        Me.btnSignOut.Name = "btnSignOut"
        Me.btnSignOut.Size = New System.Drawing.Size(40, 23)
        Me.btnSignOut.TabIndex = 4
        Me.btnSignOut.Text = "Out"
        Me.btnSignOut.UseVisualStyleBackColor = False
        '
        'btnStartDT
        '
        Me.btnStartDT.Location = New System.Drawing.Point(15, 22)
        Me.btnStartDT.Name = "btnStartDT"
        Me.btnStartDT.Size = New System.Drawing.Size(240, 100)
        Me.btnStartDT.TabIndex = 5
        Me.btnStartDT.Text = "START Downtime"
        Me.btnStartDT.UseVisualStyleBackColor = True
        '
        'btnStopDT
        '
        Me.btnStopDT.Location = New System.Drawing.Point(261, 22)
        Me.btnStopDT.Name = "btnStopDT"
        Me.btnStopDT.Size = New System.Drawing.Size(240, 100)
        Me.btnStopDT.TabIndex = 6
        Me.btnStopDT.Text = "STOP Downtime"
        Me.btnStopDT.UseVisualStyleBackColor = True
        '
        'grpDowntime
        '
        Me.grpDowntime.Controls.Add(Me.btnStartDT)
        Me.grpDowntime.Controls.Add(Me.btnStopDT)
        Me.grpDowntime.Location = New System.Drawing.Point(15, 33)
        Me.grpDowntime.Name = "grpDowntime"
        Me.grpDowntime.Size = New System.Drawing.Size(517, 144)
        Me.grpDowntime.TabIndex = 7
        Me.grpDowntime.TabStop = False
        Me.grpDowntime.Text = "Downtime Control"
        '
        'pnlLogs
        '
        Me.pnlLogs.Controls.Add(Me.dgvLog)
        Me.pnlLogs.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlLogs.Location = New System.Drawing.Point(0, 21)
        Me.pnlLogs.Name = "pnlLogs"
        Me.pnlLogs.Size = New System.Drawing.Size(544, 200)
        Me.pnlLogs.TabIndex = 8
        Me.pnlLogs.Visible = False
        '
        'dgvLog
        '
        Me.dgvLog.AllowUserToAddRows = False
        Me.dgvLog.AllowUserToDeleteRows = False
        Me.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLog.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TimeCol, Me.CategoryCol, Me.DowntimeCol, Me.OperatorCol, Me.StatusCol})
        Me.dgvLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLog.Location = New System.Drawing.Point(0, 0)
        Me.dgvLog.Name = "dgvLog"
        Me.dgvLog.ReadOnly = True
        Me.dgvLog.Size = New System.Drawing.Size(544, 200)
        Me.dgvLog.TabIndex = 2
        '
        'TimeCol
        '
        Me.TimeCol.HeaderText = "Time"
        Me.TimeCol.Name = "TimeCol"
        Me.TimeCol.ReadOnly = True
        '
        'CategoryCol
        '
        Me.CategoryCol.HeaderText = "Category"
        Me.CategoryCol.Name = "CategoryCol"
        Me.CategoryCol.ReadOnly = True
        '
        'DowntimeCol
        '
        Me.DowntimeCol.HeaderText = "Downtime"
        Me.DowntimeCol.Name = "DowntimeCol"
        Me.DowntimeCol.ReadOnly = True
        '
        'OperatorCol
        '
        Me.OperatorCol.HeaderText = "Operator"
        Me.OperatorCol.Name = "OperatorCol"
        Me.OperatorCol.ReadOnly = True
        '
        'StatusCol
        '
        Me.StatusCol.HeaderText = "Status"
        Me.StatusCol.Name = "StatusCol"
        Me.StatusCol.ReadOnly = True
        '
        'lblLogSummary
        '
        Me.lblLogSummary.Location = New System.Drawing.Point(118, 187)
        Me.lblLogSummary.Name = "lblLogSummary"
        Me.lblLogSummary.Size = New System.Drawing.Size(414, 23)
        Me.lblLogSummary.TabIndex = 10
        Me.lblLogSummary.Text = "No downtimes logged today."
        Me.lblLogSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnToggle
        '
        Me.btnToggle.Location = New System.Drawing.Point(15, 187)
        Me.btnToggle.Name = "btnToggle"
        Me.btnToggle.Size = New System.Drawing.Size(97, 23)
        Me.btnToggle.TabIndex = 9
        Me.btnToggle.Text = "▼ Show Full Log"
        Me.btnToggle.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(544, 221)
        Me.Controls.Add(Me.lblLogSummary)
        Me.Controls.Add(Me.btnToggle)
        Me.Controls.Add(Me.pnlLogs)
        Me.Controls.Add(Me.grpDowntime)
        Me.Controls.Add(Me.btnSignOut)
        Me.Controls.Add(Me.btnSignIn)
        Me.Controls.Add(Me.btnSummary)
        Me.Controls.Add(Me.cmbStation)
        Me.Controls.Add(Me.lblStation)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Downtime Tracker"
        Me.grpDowntime.ResumeLayout(False)
        Me.pnlLogs.ResumeLayout(False)
        CType(Me.dgvLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblStation As Label
    Friend WithEvents cmbStation As ComboBox
    Friend WithEvents btnSummary As Button
    Friend WithEvents btnSignIn As Button
    Friend WithEvents btnSignOut As Button
    Friend WithEvents btnStartDT As Button
    Friend WithEvents btnStopDT As Button
    Friend WithEvents grpDowntime As GroupBox
    Friend WithEvents pnlLogs As Panel
    Friend WithEvents dgvLog As DataGridView
    Friend WithEvents TimeCol As DataGridViewTextBoxColumn
    Friend WithEvents CategoryCol As DataGridViewTextBoxColumn
    Friend WithEvents DowntimeCol As DataGridViewTextBoxColumn
    Friend WithEvents OperatorCol As DataGridViewTextBoxColumn
    Friend WithEvents StatusCol As DataGridViewTextBoxColumn
    Friend WithEvents lblLogSummary As Label
    Friend WithEvents btnToggle As Button
End Class
