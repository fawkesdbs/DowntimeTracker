Public Class frmOperatorSummary
    Private ReadOnly _dailyLog As List(Of Dictionary(Of String, Object))

    Public Sub New(dailyLog As List(Of Dictionary(Of String, Object)))
        InitializeComponent()
        _dailyLog = dailyLog
    End Sub

    Private Sub frmOperatorSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim operatorMinutes = New Dictionary(Of String, Double)

        For Each entry In _dailyLog
            Dim opName = entry("operator").ToString()
            Dim duration = Convert.ToDouble(entry("duration_minutes"))

            If operatorMinutes.ContainsKey(opName) Then
                operatorMinutes(opName) += duration
            Else
                operatorMinutes(opName) = duration
            End If
        Next

        dgvSummary.Rows.Clear()
        For Each op In operatorMinutes.Keys.OrderBy(Function(k) k)
            dgvSummary.Rows.Add(op, Math.Round(operatorMinutes(op), 1))
        Next
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class