Public Class Form3
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Try

            Dim ctx As New Database1Entities()
            Dim selectResul As IList(Of Dotacion) = (From s In ctx.Dotacion
                                                     Where s.serie = TextBox1.Text And s.folio = TextBox2.Text And s.activo = True
                                                     Select s).ToList()
            Dim dotacionAux = selectResul.First()


            Dim dataSetResultados As New DataSet()
            dataSetResultados = GenerarDataSet()

            Dim lista As IList(Of String) = dotacionAux.contenidoDotacion.Split(New Char() {"]"c}, StringSplitOptions.RemoveEmptyEntries)
            Dim totalBoletos As Integer = 0
            lista.ToList().ForEach(Sub(item)
                                       Dim cadenaSinCorchetes As String = item.Trim(New Char() {"["c})
                                       Dim cadenasParaFilaDataGridView As String() = cadenaSinCorchetes.Split(New Char() {"-"c}, StringSplitOptions.RemoveEmptyEntries)

                                       Dim newRow As DataRow = dataSetResultados.Tables("resumenDotaciones").NewRow()
                                       newRow("Serie") = cadenasParaFilaDataGridView(0)
                                       newRow("FolioInicial") = Int32.Parse(cadenasParaFilaDataGridView(1))
                                       newRow("FolioFinal") = Int32.Parse(cadenasParaFilaDataGridView(2))
                                       dataSetResultados.Tables("resumenDotaciones").Rows.Add(newRow)

                                       'cuento cuantos boletos hay
                                       totalBoletos = totalBoletos + Int32.Parse(cadenasParaFilaDataGridView(2)) - Int32.Parse(cadenasParaFilaDataGridView(1)) + 1
                                   End Sub)

            dataSetResultados.Tables("resumenDotaciones").AcceptChanges()
            dataSetResultados.AcceptChanges()
            PublicarResumen(dataSetResultados)
            TextBox3.Text = totalBoletos.ToString()

            TextBox1.Enabled = False
            TextBox2.Enabled = False
            Button1.Enabled = False
            Button2.Enabled = True
        Catch ex As Exception
            'Se arregla el form para que regrese al estado inicial
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox1.Enabled = True
            TextBox2.Enabled = True
            TextBox3.Clear()
            DataGridView1.DataSource = DBNull.Value
            Button1.Enabled = True
            Button2.Enabled = False

            MessageBox.Show(ex.Message)
        End Try

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
        columna2.ColumnName = "FolioInicial"
        columna2.ReadOnly = False
        columna2.Unique = False
        columna2.AutoIncrement = False
        columna2.Caption = "Folio inicial"
        miTabla.Columns.Add(columna2)

        Dim columna3 As New DataColumn()
        columna3.DataType = System.Type.GetType("System.String")
        columna3.ColumnName = "ColumnaVacia"
        columna3.ReadOnly = False
        columna3.Unique = False
        columna3.AutoIncrement = False
        columna3.Caption = "Columna vacia"
        miTabla.Columns.Add(columna3)

        Dim columna4 As New DataColumn()
        columna4.DataType = System.Type.GetType("System.Int32")
        columna4.ColumnName = "FolioFinal"
        columna4.ReadOnly = False
        columna4.Unique = False
        columna4.AutoIncrement = False
        columna4.Caption = "Folio final"
        miTabla.Columns.Add(columna4)



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
        Me.DataGridView1.Columns("FolioInicial").SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView1.Columns("ColumnaVacia").SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView1.Columns("FolioFinal").SortMode = DataGridViewColumnSortMode.NotSortable
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim result As DialogResult = MessageBox.Show("desea elimiar registro en pantalla de la BD", "Pregunta importante", MessageBoxButtons.YesNo)

        Try
            If result = DialogResult.Yes Then
                'Se elimina registro de la BD
                'Dim ctx As New Database1Entities()
                'Dim selectResul As IList(Of Dotacion) = (From s In ctx.Dotacion
                '                                         Where s.serie = TextBox1.Text And s.folio = TextBox2.Text And s.activo = True
                '                                         Select s).ToList()
                'Dim dotacionAux = selectResul.First()
                'dotacionAux.fechaModificacion = DateTime.Now
                'dotacionAux.activo = False
                'ctx.SaveChanges()

                Dim xelement2 As XElement = XElement.Load("Dotaciones.xml")
                Dim lista = (From nm In xelement2.Elements("Dotacion")
                             Select nm).ToList()
                For Each addEle As XElement In lista
                    If addEle.Element("serie") = TextBox1.Text And addEle.Element("folio") = TextBox2.Text Then
                        addEle.Remove()
                    End If
                Next

                xelement2.Save("Dotaciones.xml")

            End If

            'Se arregla el form para que regrese al estado inicial
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox1.Enabled = True
            TextBox2.Enabled = True
            TextBox3.Clear()
            DataGridView1.DataSource = DBNull.Value
            Button1.Enabled = True
            Button2.Enabled = False
            Button3.Enabled = True

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Dim xelement2 As XElement = XElement.Load("Dotaciones.xml")
            Dim listaDeSelect = (From nm In xelement2.Elements("Dotacion")
                                 Where nm.Element("serie") = TextBox1.Text And nm.Element("folio") = TextBox2.Text And CInt(nm.Element("activo")) = 1
                                 Select nm).ToList()

            Dim elementoBuscado = listaDeSelect.First()
            Dim dataSetResultados As New DataSet()
            dataSetResultados = GenerarDataSet()

            Dim lista As IList(Of String) = elementoBuscado.Element("contenidoDotacion").Value.Split(New Char() {"]"c}, StringSplitOptions.RemoveEmptyEntries)
            Dim totalBoletos As Integer = 0
            lista.ToList().ForEach(Sub(item)
                                       Dim cadenaSinCorchetes As String = item.Trim(New Char() {"["c})
                                       Dim cadenasParaFilaDataGridView As String() = cadenaSinCorchetes.Split(New Char() {"-"c}, StringSplitOptions.RemoveEmptyEntries)

                                       Dim newRow As DataRow = dataSetResultados.Tables("resumenDotaciones").NewRow()
                                       newRow("Serie") = cadenasParaFilaDataGridView(0)
                                       newRow("FolioInicial") = Int32.Parse(cadenasParaFilaDataGridView(1))
                                       newRow("FolioFinal") = Int32.Parse(cadenasParaFilaDataGridView(2))
                                       dataSetResultados.Tables("resumenDotaciones").Rows.Add(newRow)

                                       'cuento cuantos boletos hay
                                       totalBoletos = totalBoletos + Int32.Parse(cadenasParaFilaDataGridView(2)) - Int32.Parse(cadenasParaFilaDataGridView(1)) + 1
                                   End Sub)

            dataSetResultados.Tables("resumenDotaciones").AcceptChanges()
            dataSetResultados.AcceptChanges()
            PublicarResumen(dataSetResultados)
            TextBox3.Text = totalBoletos.ToString()

            TextBox1.Enabled = False
            TextBox2.Enabled = False
            Button3.Enabled = False
            Button2.Enabled = True

        Catch ex As Exception
            'Se arregla el form para que regrese al estado inicial
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox1.Enabled = True
            TextBox2.Enabled = True
            TextBox3.Clear()
            DataGridView1.DataSource = DBNull.Value
            Button1.Enabled = True
            Button2.Enabled = False
            Button3.Enabled = True
            MessageBox.Show(ex.Message)
        End Try

    End Sub
End Class