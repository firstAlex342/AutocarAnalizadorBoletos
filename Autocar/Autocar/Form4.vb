Public Class Form4
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim miDataSet = GenerarDataSet()

        Dim xelement2 As XElement = XElement.Load("Dotaciones.xml")
        Dim lista = (From nm In xelement2.Elements("Dotacion")
                     Select nm).ToList()

        lista.ForEach(Sub(item)
                          Dim newRow As DataRow = miDataSet.Tables("resumenDotaciones").NewRow()
                          newRow("Serie") = item.Element("serie").Value
                          newRow("Folio") = CInt(item.Element("folio").Value)
                          newRow("ContenidoDotacion") = item.Element("contenidoDotacion").Value
                          newRow("FechaAlta") = item.Element("fechaAlta").Value
                          newRow("FechaModificacion") = item.Element("fechaModificacion").Value
                          newRow("Activo") = CInt(item.Element("activo").Value)
                          newRow("NumBoletosEnFajilla") = CalculaNumBoletosDe(item.Element("contenidoDotacion").Value)
                          miDataSet.Tables("resumenDotaciones").Rows.Add(newRow)
                      End Sub)

        PublicarResumen(miDataSet)

        'lista.ToList().ForEach(Sub(item)
        '                           Dim cadenaSinCorchetes As String = item.Trim(New Char() {"["c})
        '                           Dim cadenasParaFilaDataGridView As String() = cadenaSinCorchetes.Split(New Char() {"-"c}, StringSplitOptions.RemoveEmptyEntries)
        '                           Dim newRow As DataRow = miDataSet.Tables("resumenDotaciones").NewRow()
        '                           newRow("Serie") = cadenasParaFilaDataGridView(0)
        '                           newRow("FolioInicial") = Int32.Parse(cadenasParaFilaDataGridView(1))
        '                           newRow("FolioFinal") = Int32.Parse(cadenasParaFilaDataGridView(2))
        '                           newRow("NumBoletosEnFajilla") = Int32.Parse(cadenasParaFilaDataGridView(2)) - Int32.Parse(cadenasParaFilaDataGridView(1)) + 1
        '                           miDataSet.Tables("resumenDotaciones").Rows.Add(newRow)

        '                           'cuento cuantos boletos hay
        '                           totalBoletos = totalBoletos + Int32.Parse(cadenasParaFilaDataGridView(2)) - Int32.Parse(cadenasParaFilaDataGridView(1)) + 1
        '                       End Sub)


        'busca elemento
        'Dim xelement2 As XElement = XElement.Load("Algo.xml")
        'Dim name = From nm In xelement2.Elements("Employee")
        '           Where CInt(nm.Element("EmpId")) = 7
        '           Select nm

        'For Each xElemento As XElement In name
        '    MessageBox.Show(xElemento)
        'Next xElemento
    End Sub


    Public Function GenerarDataSet() As DataSet
        Dim miTabla As New DataTable("resumenDotaciones")

        Dim columna1 As New DataColumn()
        columna1.DataType = System.Type.GetType("System.String")
        columna1.ColumnName = "Serie"
        columna1.ReadOnly = False
        columna1.Unique = False
        columna1.AutoIncrement = False
        columna1.Caption = "Serie"
        miTabla.Columns.Add(columna1)

        Dim columna2 As New DataColumn()
        columna2.DataType = System.Type.GetType("System.Int32")
        columna2.ColumnName = "Folio"
        columna2.ReadOnly = False
        columna2.Unique = False
        columna2.AutoIncrement = False
        columna2.Caption = "Folio"
        miTabla.Columns.Add(columna2)

        Dim columna3 As New DataColumn()
        columna3.DataType = System.Type.GetType("System.String")
        columna3.ColumnName = "ContenidoDotacion"
        columna3.ReadOnly = False
        columna3.Unique = False
        columna3.AutoIncrement = False
        columna3.Caption = "Contenido dotacion"
        miTabla.Columns.Add(columna3)

        Dim columna4 As New DataColumn()
        columna4.DataType = System.Type.GetType("System.DateTime")
        columna4.ColumnName = "FechaAlta"
        columna4.ReadOnly = False
        columna4.Unique = False
        columna4.AutoIncrement = False
        columna4.Caption = "Fecha alta"
        miTabla.Columns.Add(columna4)

        Dim columna5 As New DataColumn()
        columna5.DataType = System.Type.GetType("System.DateTime")
        columna5.ColumnName = "FechaModificacion"
        columna5.ReadOnly = False
        columna5.Unique = False
        columna5.AutoIncrement = False
        columna5.Caption = "Fecha modificacion"
        miTabla.Columns.Add(columna5)


        Dim columna6 As New DataColumn()
        columna6.DataType = System.Type.GetType("System.Int32")
        columna6.ColumnName = "Activo"
        columna6.ReadOnly = False
        columna6.Unique = False
        columna6.AutoIncrement = False
        columna6.Caption = "Activo"
        miTabla.Columns.Add(columna6)

        Dim columna7 As New DataColumn()
        columna7.DataType = System.Type.GetType("System.Int32")
        columna7.ColumnName = "NumBoletosEnFajilla"
        columna7.ReadOnly = False
        columna7.Unique = False
        columna7.AutoIncrement = False
        columna7.Caption = "Numero boletos en fajilla"
        miTabla.Columns.Add(columna7)



        Dim miDataSet As New DataSet()
        miDataSet.Tables.Add(miTabla)
        miDataSet.EnforceConstraints = False
        miDataSet.AcceptChanges()

        Return miDataSet
    End Function

    Private Sub PublicarResumen(ByRef dataSet As DataSet)
        Me.DataGridView1.DataSource = dataSet
        Me.DataGridView1.DataMember = "resumenDotaciones"

        'https://stackoverflow.com/questions/3965304/disable-sorting-when-clicking-datagridview-column-header
        'Hago que la flecha que esta sobre la columna que provoca el ordenamiento desaparezca
        Me.DataGridView1.Columns("Serie").SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView1.Columns("Folio").SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView1.Columns("ContenidoDotacion").SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView1.Columns("FechaAlta").SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView1.Columns("FechaModificacion").SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView1.Columns("Activo").SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView1.Columns("NumBoletosEnFajilla").SortMode = DataGridViewColumnSortMode.NotSortable
    End Sub


    Private Function CalculaNumBoletosDe(ByVal contenidoDotacion As String) As Integer
        Dim respuesta As Integer = 0

        Dim lista As IList(Of String) = contenidoDotacion.Split(New Char() {"]"c}, StringSplitOptions.RemoveEmptyEntries)
        Dim totalBoletos As Integer = 0

        lista.ToList().ForEach(Sub(item)
                                   Dim cadenaSinCorchetes As String = item.Trim(New Char() {"["c})
                                   Dim cadenasParaFilaDataGridView As String() = cadenaSinCorchetes.Split(New Char() {"-"c}, StringSplitOptions.RemoveEmptyEntries)
                                   totalBoletos = totalBoletos + Int32.Parse(cadenasParaFilaDataGridView(2)) - Int32.Parse(cadenasParaFilaDataGridView(1)) + 1
                               End Sub)

        CalculaNumBoletosDe = totalBoletos
    End Function
End Class