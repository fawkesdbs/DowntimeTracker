Public Class frmEventSelector
    ' Private field to hold the events passed from the main form
    Private ReadOnly _eventsMap As Dictionary(Of String, String)

    ' Public property for the main form to get the result
    Public SelectedEvent As String = ""

    ''' <summary>
    ''' Constructor that accepts the dictionary of events.
    ''' </summary>
    Public Sub New(eventsMap As Dictionary(Of String, String))
        ' This call is required by the designer.
        InitializeComponent()

        _eventsMap = eventsMap
    End Sub

    ''' <summary>
    ''' This runs when the form is loaded, and it populates the ListBox.
    ''' This is the equivalent of the _build_selector() method.
    ''' </summary>
    Private Sub frmEventSelector_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Define the order of categories, as in the Python app.
        Dim categoryOrder = {"Production", "Inventory", "Quality", "Maintenance", "Planned", "Other"}

        ' Group events by category using LINQ. This is equivalent to _group_events().
        Dim groupedEvents = _eventsMap.GroupBy(Function(kvp) kvp.Value).ToDictionary(Function(g) g.Key, Function(g) g.Select(Function(kvp) kvp.Key).ToList())

        ' Populate the ListBox in the correct order.
        For Each category In categoryOrder
            If groupedEvents.ContainsKey(category) Then
                ' Add non-selectable category header and divider
                lbxEvents.Items.Add(category.ToUpper())
                lbxEvents.Items.Add(New String("-"c, 40))

                ' Add selectable event items
                For Each ev In groupedEvents(category)
                    lbxEvents.Items.Add($"    {ev}")
                Next
            End If
        Next
    End Sub

    ''' <summary>
    ''' Handles the user double-clicking an item. This is the equivalent of _on_select().
    ''' </summary>
    Private Sub lbxEvents_DoubleClick(sender As Object, e As EventArgs) Handles lbxEvents.DoubleClick
        If lbxEvents.SelectedItem Is Nothing Then Return

        Dim selectedValue = lbxEvents.SelectedItem.ToString()

        ' Ignore headers and dividers by checking for the leading spaces.
        If Not selectedValue.StartsWith("    ") Then Return

        ' Set the public property, signal success, and close the form.
        SelectedEvent = selectedValue.Trim()
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
End Class