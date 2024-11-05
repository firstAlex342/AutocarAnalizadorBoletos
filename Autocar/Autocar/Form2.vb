Imports System.IO
Imports System.Xml

Public Class Form2
    Private _textoAinsertar As String

    Public Sub New(ByVal cadenaAInsertar As String)
        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        TextoAInsertar = cadenaAInsertar
        GenerarDataGridView()
    End Sub


    Public Property TextoAInsertar As String
        Get
            Return _textoAinsertar
        End Get
        Set(value As String)
            _textoAinsertar = value
        End Set
    End Property

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'https://docs.devart.com/dotconnect/db2/Tutorial_EF.html
        'http://joseluisgarciab.blogspot.com/2013/10/agregar-una-base-de-datos-sqlcompact-un.html
        'https://learn.microsoft.com/en-us/dotnet/visual-basic/programming-guide/language-features/constants-enums/constant-and-literal-data-types
        'https://www.dotnetperls.com/split-vbnet
        'https://learn.microsoft.com/en-us/dotnet/api/system.stringsplitoptions?view=net-8.0
        Try

            'For Each item As String In cad
            '    MessageBox.Show(item)
            'Next

            Button1.Enabled = False 'deshabilito el boton para que no molesten
            Dim cadenasSinCocheteCierre As String() = Me.TextoAInsertar.Split(New Char() {"]"c}, StringSplitOptions.RemoveEmptyEntries)
            Dim primeraCadena As String = cadenasSinCocheteCierre(0)
            primeraCadena = primeraCadena.Trim(New Char() {"["c})
            Dim elementosPrimeraCadena As String() = primeraCadena.Split(New Char() {"-"c}, StringSplitOptions.RemoveEmptyEntries)
            'MessageBox.Show("Serie primer boleto " & elementosPrimeraCadena(0) & "Folio primer boleto " & elementosPrimeraCadena(1))

            Dim horaActual As DateTime
            horaActual = DateTime.Now



            Dim ctx As New Database1Entities()
            Dim nuevaDotacion As Dotacion = New Dotacion()
            nuevaDotacion.serie = elementosPrimeraCadena(0)
            nuevaDotacion.folio = elementosPrimeraCadena(1)
            nuevaDotacion.contenidoDotacion = Me.TextoAInsertar
            nuevaDotacion.fechaAlta = horaActual
            nuevaDotacion.fechaModificacion = horaActual
            nuevaDotacion.activo = 1

            ctx.Dotacion.Add(nuevaDotacion)
            ctx.SaveChanges()

            'Se arregla el form para que regrese al estado inicial
            Me.TextoAInsertar = String.Empty
            DataGridView1.DataSource = DBNull.Value
            TextBox1.Clear()
            Button1.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)


            Me.TextoAInsertar = String.Empty
            DataGridView1.DataSource = DBNull.Value
            TextBox1.Clear()
            Button1.Enabled = False
        End Try

    End Sub


    Public Function GenerarDataSet() As DataSet
        Dim miTabla As New DataTable("resumenDotaciones")

        Dim columna1 As New DataColumn()
        columna1.DataType = System.Type.GetType("System.Int32")
        columna1.ColumnName = "Orden"
        columna1.ReadOnly = False
        columna1.Unique = False
        columna1.AutoIncrement = False
        columna1.Caption = "Orden"
        miTabla.Columns.Add(columna1)

        Dim columna2 As New DataColumn()
        columna2.DataType = System.Type.GetType("System.String")
        columna2.ColumnName = "Serie"
        columna2.ReadOnly = False
        columna2.Unique = False
        columna1.AutoIncrement = False
        columna2.Caption = "Serie"
        miTabla.Columns.Add(columna2)

        Dim columna3 As New DataColumn()
        columna3.DataType = System.Type.GetType("System.Int32")
        columna3.ColumnName = "FolioInicial"
        columna3.ReadOnly = False
        columna3.Unique = False
        columna3.AutoIncrement = False
        columna3.Caption = "Folio inicial"
        miTabla.Columns.Add(columna3)

        Dim columna4 As New DataColumn()
        columna4.DataType = System.Type.GetType("System.Int32")
        columna4.ColumnName = "FolioFinal"
        columna4.ReadOnly = False
        columna4.Unique = False
        columna4.AutoIncrement = False
        columna4.Caption = "Folio final"
        miTabla.Columns.Add(columna4)

        Dim column5 As New DataColumn()
        column5.DataType = System.Type.GetType("System.Int32")
        column5.ColumnName = "NumBoletosEnFajilla"
        column5.ReadOnly = False
        column5.Unique = False
        column5.AutoIncrement = False
        column5.Caption = "Numero de boletos en fajilla"
        miTabla.Columns.Add(column5)

        Dim miDataSet As New DataSet()
        miDataSet.Tables.Add(miTabla)
        miDataSet.EnforceConstraints = False
        miDataSet.AcceptChanges()

        Return miDataSet
    End Function

    Public Sub GenerarDataGridView()
        Dim miDataSet As New DataSet()
        miDataSet = GenerarDataSet() 'genera dataset vacio

        Dim lista As IList(Of String) = Me.TextoAInsertar.Split(New Char() {"]"c}, StringSplitOptions.RemoveEmptyEntries)
        Dim totalBoletos As Integer = 0
        lista.ToList().ForEach(Sub(item)
                                   Dim cadenaSinCorchetes As String = item.Trim(New Char() {"["c})
                                   Dim cadenasParaFilaDataGridView As String() = cadenaSinCorchetes.Split(New Char() {"-"c}, StringSplitOptions.RemoveEmptyEntries)
                                   Dim newRow As DataRow = miDataSet.Tables("resumenDotaciones").NewRow()
                                   newRow("Serie") = cadenasParaFilaDataGridView(0)
                                   newRow("FolioInicial") = Int32.Parse(cadenasParaFilaDataGridView(1))
                                   newRow("FolioFinal") = Int32.Parse(cadenasParaFilaDataGridView(2))
                                   newRow("NumBoletosEnFajilla") = Int32.Parse(cadenasParaFilaDataGridView(2)) - Int32.Parse(cadenasParaFilaDataGridView(1)) + 1
                                   miDataSet.Tables("resumenDotaciones").Rows.Add(newRow)

                                   'cuento cuantos boletos hay
                                   totalBoletos = totalBoletos + Int32.Parse(cadenasParaFilaDataGridView(2)) - Int32.Parse(cadenasParaFilaDataGridView(1)) + 1
                               End Sub)

        TextBox1.Text = totalBoletos.ToString()
        miDataSet.Tables("resumenDotaciones").AcceptChanges()
        miDataSet.AcceptChanges()
        PublicarResumen(miDataSet)
    End Sub

    Private Sub PublicarResumen(ByRef dataSet As DataSet)
        Me.DataGridView1.DataSource = dataSet
        Me.DataGridView1.DataMember = "resumenDotaciones"

        'https://stackoverflow.com/questions/3965304/disable-sorting-when-clicking-datagridview-column-header
        'Hago que la flecha que esta sobre la columna que provoca el ordenamiento desaparezca
        Me.DataGridView1.Columns("Orden").SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView1.Columns("Serie").SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView1.Columns("FolioInicial").SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView1.Columns("FolioFinal").SortMode = DataGridViewColumnSortMode.NotSortable
        Me.DataGridView1.Columns("NumBoletosEnFajilla").SortMode = DataGridViewColumnSortMode.NotSortable
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Try
            Dim empNM As XNamespace = "urn:lst-emp:emp"

            'Dim xDoc As New XDocument(New XDeclaration("1.0", "UTF-16", Nothing),
            '            New XElement(empNM + "Employees",
            '            New XElement("Employee",
            '            New XComment("Only 3 elements for demo purposes"),
            '            New XElement("EmpId", "5"),
            '            New XElement("Name", "Kimmy"),
            '            New XElement("Sex", "Female"))))

            'crea el arbol employees (vacio)
            '            Dim xDoc As New XDocument(New XDeclaration("1.0", "UTF-16", Nothing),
            'New XElement(empNM + "Employees"))

            '            Dim sw As New StringWriter()
            '            Dim xWrite As XmlWriter = XmlWriter.Create(sw)
            '            xDoc.Save(xWrite)
            '            xWrite.Close()
            '            xDoc.Save("Algo.xml")

            'Cargar archivo completo y mostrar
            'Dim xRead As XmlReader = XmlReader.Create("Algo.xml")
            'Dim xEle As XElement = XElement.Load(xRead)
            'MessageBox.Show(xEle)

            'Anexa elementos
            'Dim xEle As XElement = XElement.Load("Algo.xml")
            'xEle.Add(New XElement("Employee",
            '                  New XElement("EmpId", 8),
            '                  New XElement("Name", "andres iniesta"),
            '                   New XElement("Sex", "Male")))

            'xEle.Save("Algo.xml")

            'busca elemento
            'Dim xelement2 As XElement = XElement.Load("Algo.xml")
            'Dim name = From nm In xelement2.Elements("Employee")
            '           Where CInt(nm.Element("EmpId")) = 7
            '           Select nm

            'For Each xElemento As XElement In name
            '    MessageBox.Show(xElemento)
            'Next xElemento

            Button2.Enabled = False 'deshabilito el boton para que no molesten
            Dim cadenasSinCocheteCierre As String() = Me.TextoAInsertar.Split(New Char() {"]"c}, StringSplitOptions.RemoveEmptyEntries)
            Dim primeraCadena As String = cadenasSinCocheteCierre(0)
            primeraCadena = primeraCadena.Trim(New Char() {"["c})
            Dim elementosPrimeraCadena As String() = primeraCadena.Split(New Char() {"-"c}, StringSplitOptions.RemoveEmptyEntries)

            Dim horaActual As DateTime
            horaActual = DateTime.Now

            'busca si elemento ya esta repetido
            Dim xelement2 As XElement = XElement.Load("Dotaciones.xml")
            Dim name = From nm In xelement2.Elements("Dotacion")     '!****ojo ve que no hay select en esta consulta***** !!!!!!
                       Where CStr(nm.Element("serie")) = elementosPrimeraCadena(0) And CStr(nm.Element("folio")) = elementosPrimeraCadena(1)
                       Select nm
            If name.Count() >= 1 Then
                'Ya esta este elemento en el archivo
                Throw New Exception("Esta serie / folio ya esta en el archivo como campo llave, no se realizo inserción en archivo XML")
            Else
                'Anexa elementos
                'Dim xEle As XElement = XElement.Load("Dotaciones.xml")
                xelement2.Add(New XElement("Dotacion",
                                  New XElement("serie", elementosPrimeraCadena(0)),
                                  New XElement("folio", elementosPrimeraCadena(1)),
                                   New XElement("contenidoDotacion", Me.TextoAInsertar),
                                   New XElement("fechaAlta", horaActual),
                                   New XElement("fechaModificacion", horaActual),
                                   New XElement("activo", 1)))
                xelement2.Save("Dotaciones.xml")
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub
End Class