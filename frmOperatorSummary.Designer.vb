<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOperatorSummary
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
        Me.dgvSummary = New System.Windows.Forms.DataGridView()
        Me.OperatorCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DowntimeCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnClose = New System.Windows.Forms.Button()
        CType(Me.dgvSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvSummary
        '
        Me.dgvSummary.AllowUserToAddRows = False
        Me.dgvSummary.AllowUserToDeleteRows = False
        Me.dgvSummary.AllowUserToResizeRows = False
        Me.dgvSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSummary.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.OperatorCol, Me.DowntimeCol})
        Me.dgvSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSummary.Location = New System.Drawing.Point(0, 0)
        Me.dgvSummary.Name = "dgvSummary"
        Me.dgvSummary.ReadOnly = True
        Me.dgvSummary.Size = New System.Drawing.Size(388, 286)
        Me.dgvSummary.TabIndex = 0
        '
        'OperatorCol
        '
        Me.OperatorCol.HeaderText = "Operator"
        Me.OperatorCol.Name = "OperatorCol"
        Me.OperatorCol.ReadOnly = True
        '
        'DowntimeCol
        '
        Me.DowntimeCol.HeaderText = "Total Downtime (min)"
        Me.DowntimeCol.Name = "DowntimeCol"
        Me.DowntimeCol.ReadOnly = True
        '
        'btnClose
        '
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnClose.Location = New System.Drawing.Point(0, 263)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(388, 23)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmOperatorSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(388, 286)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.dgvSummary)
        Me.Name = "frmOperatorSummary"
        Me.Text = "Operator Summary"
        CType(Me.dgvSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgvSummary As DataGridView
    Friend WithEvents btnClose As Button
    Friend WithEvents OperatorCol As DataGridViewTextBoxColumn
    Friend WithEvents DowntimeCol As DataGridViewTextBoxColumn
End Class
