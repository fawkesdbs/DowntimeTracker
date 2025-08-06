Public Class frmOperatorScan
    Private ReadOnly _operatorsMap As Dictionary(Of String, String)
    Private _scannedOperatorNames As New List(Of String)

    Public ReadOnly Property ScannedOperators As List(Of String)
        Get
            Return _scannedOperatorNames
        End Get
    End Property

    ''' <summary>
    ''' Overloaded constructor that accepts a pre-filled list of operators.
    ''' </summary>
    Public Sub New(title As String, stationName As String, operatorsMap As Dictionary(Of String, String), Optional prefilledOperators As List(Of String) = Nothing)
        InitializeComponent()
        Me.Text = title
        lblStation.Text = $"Station: {stationName}"
        _operatorsMap = operatorsMap

        ' If a pre-filled list is provided, use it.
        If prefilledOperators IsNot Nothing Then
            _scannedOperatorNames.AddRange(prefilledOperators)
        End If
    End Sub

    Private Sub frmOperatorScan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtOperatorId.Focus()
        btnSubmit.DialogResult = DialogResult.OK
        btnCancel.DialogResult = DialogResult.Cancel

        ' Update the listbox if it was pre-filled.
        If _scannedOperatorNames.Any() Then
            UpdateListBox()
        End If
    End Sub

    Private Sub txtOperatorId_KeyDown(sender As Object, e As KeyEventArgs) Handles txtOperatorId.KeyDown
        If e.KeyCode = Keys.Enter Then
            AddOperator()
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub AddOperator()
        Dim opId = txtOperatorId.Text.Trim()
        If String.IsNullOrEmpty(opId) Then Return

        If _operatorsMap.ContainsKey(opId) Then
            Dim opName = _operatorsMap(opId)
            If Not _scannedOperatorNames.Contains(opName) Then
                _scannedOperatorNames.Add(opName)
                UpdateListBox()
            Else
                UIHelpers.ShowWarning("Duplicate", $"{opName} has already been scanned.")
            End If
        Else
            UIHelpers.ShowError("Invalid Operator", $"Operator ID '{opId}' was not recognized.")
        End If

        txtOperatorId.Clear()
        txtOperatorId.Focus()
    End Sub

    Private Sub lbxScannedOperators_DoubleClick(sender As Object, e As EventArgs) Handles lbxScannedOperators.DoubleClick
        If lbxScannedOperators.SelectedItem IsNot Nothing Then
            _scannedOperatorNames.Remove(lbxScannedOperators.SelectedItem.ToString())
            UpdateListBox()
        End If
    End Sub

    Private Sub UpdateListBox()
        lbxScannedOperators.DataSource = Nothing
        lbxScannedOperators.DataSource = _scannedOperatorNames
        lblOperatorCount.Text = $"{_scannedOperatorNames.Count} operators added."
    End Sub
End Class