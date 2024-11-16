Imports System.IO
Imports System.Xml
Public Class FormRaiz
    Private formulario1 As Form1



    'https://stackoverflow.com/questions/32724504/vb-net-form-inside-a-panel-resize
    Private Sub AbrirFormulario(ByVal formHijo As Form)
        If Me.PanelContenedor.Controls.Count > 0 Then
            Me.PanelContenedor.Controls.Clear()
        End If


        Me.PanelContenedor.Controls.Add(formHijo)
        Me.PanelContenedor.Tag = formHijo
        formHijo.Show()
    End Sub

    Private Sub LimpiarCkeckedListBox()
        formulario1.CheckedListBox1.Items.Clear()
        formulario1.CheckedListBox2.Items.Clear()
        formulario1.CheckedListBox3.Items.Clear()
        formulario1.CheckedListBox4.Items.Clear()
        formulario1.CheckedListBox5.Items.Clear()
    End Sub


    Private Sub LimpiarTextBoxes()
        formulario1.TextBox1.Clear()
        formulario1.TextBox2.Clear()
        formulario1.TextBox3.Clear()
        formulario1.TextBox4.Clear()
        formulario1.TextBox5.Clear()
        formulario1.TextBox6.Clear()
        formulario1.TextBox7.Clear()
        formulario1.TextBox8.Clear()
        formulario1.TextBox9.Clear()
        formulario1.TextBox10.Clear()

        formulario1.TextBox11.Clear()
        formulario1.TextBox12.Clear()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try

            formulario1 = New Form1 With {.TopLevel = False, .AutoSize = False}
            If (Me.PanelContenedor.Controls.Count > 0) Then
                Me.PanelContenedor.Controls.RemoveAt(0)
            End If

            formulario1.MdiParent = Me
            formulario1.Show()
            Me.PanelContenedor.Controls.Add(formulario1)
            formulario1.Dock = DockStyle.Fill

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim fajillaUno As New Fajilla(0, 0)
        Dim fajillaDos As New Fajilla(0, 0)
        Dim fajillaUnoIncompleto As Boolean
        Dim fajillaDosIncompleto As Boolean
        Dim listaFajillasAGuardar As New List(Of Fajilla)
        Dim i As Integer = 0

        fajillaUnoIncompleto = False
        fajillaDosIncompleto = False

        Try
            If formulario1.CheckedListBox1.Items.Count >= 1 Then  'si tiene varios items
                fajillaUno = formulario1.CheckedListBox1.Items.Item(0) 'examina si el prime item esta incompleto
                If fajillaUno.MisBoletos.Count < 100 Then
                    fajillaUnoIncompleto = True 'esta incompleto
                End If
            Else
                Throw New Exception("La chklistbox1 debe tener al menos 1 item")
            End If


            If formulario1.CheckedListBox2.Items.Count >= 1 Then
                fajillaDos = formulario1.CheckedListBox2.Items.Item(0)
                If fajillaDos.MisBoletos.Count < 100 Then
                    fajillaDosIncompleto = True
                End If
            Else
                Throw New Exception("La chklistbox2 debe tener al menos 1 item")
            End If


            '----ver cual es mayor
            If fajillaUnoIncompleto = True And fajillaDosIncompleto = True Then
                If (fajillaUno.MisBoletos.Count) < (fajillaDos.MisBoletos.Count) Then
                    'fajilla uno es menor

                    Dim s As String
                    s = "[" & fajillaUno.MisBoletos(0).MiSerie.ToString() & "-" & fajillaUno.FolioInicial.ToString() & "-" & fajillaUno.FolioFinal.ToString() & "]"
                    s = s & "[" & fajillaDos.MisBoletos(0).MiSerie.ToString() & "-" & fajillaDos.FolioInicial.ToString() & "-" & fajillaDos.FolioFinal.ToString() & "]"

                    If formulario1.CheckedListBox1.Items.Count > 1 Then  'en caso que hayan mas fajillas en CheckedListBox1...
                        Dim fajillaAux As New Fajilla(0, 0)
                        fajillaAux = formulario1.CheckedListBox1.Items.Item(1)
                        s = s & "[" & fajillaAux.MisBoletos(0).MiSerie.ToString() & "-" & fajillaAux.FolioInicial.ToString() & "-"
                        fajillaAux = formulario1.CheckedListBox1.Items.Item(formulario1.CheckedListBox1.Items.Count - 1)
                        s = s & fajillaAux.FolioFinal.ToString() & "]"
                    End If

                    If formulario1.CheckedListBox2.Items.Count > 1 Then 'en caso que hayan mas fajillas en CheckedListBox2...
                        Dim fajillaAux As New Fajilla(0, 0)
                        fajillaAux = formulario1.CheckedListBox2.Items.Item(1)
                        s = s & "[" & fajillaAux.MisBoletos(0).MiSerie.ToString() & "-" & fajillaAux.FolioInicial.ToString() & "-"
                        fajillaAux = formulario1.CheckedListBox2.Items.Item(formulario1.CheckedListBox2.Items.Count - 1)
                        s = s & fajillaAux.FolioFinal.ToString() & "]"
                    End If

                    'comenzar a abrir formulario mdi hijo
                    MessageBox.Show(s)
                    formulario1 = Nothing
                    Dim form2 As New Form2(s)
                    form2.TopLevel = False
                    form2.AutoSize = False
                    If (Me.PanelContenedor.Controls.Count > 0) Then
                        Me.PanelContenedor.Controls.RemoveAt(0)
                    End If
                    form2.MdiParent = Me
                    form2.Show()
                    Me.PanelContenedor.Controls.Add(form2)
                    form2.Dock = DockStyle.Fill
                Else
                    If fajillaDos.MisBoletos.Count < fajillaUno.MisBoletos.Count Then
                        'fajilla dos es menor
                        Dim s As String
                        s = "[" & fajillaDos.MisBoletos(0).MiSerie.ToString() & "-" & fajillaDos.FolioInicial.ToString() & "-" & fajillaDos.FolioFinal.ToString() & "]"
                        s = s & "[" & fajillaUno.MisBoletos(0).MiSerie.ToString() & "-" & fajillaUno.FolioInicial.ToString() & "-" & fajillaUno.FolioFinal.ToString() & "]"

                        '-----------------se repite procedimiento
                        If formulario1.CheckedListBox1.Items.Count > 1 Then  'en caso que hayan mas fajillas en CheckedListBox1...
                            Dim fajillaAux As New Fajilla(0, 0)
                            fajillaAux = formulario1.CheckedListBox1.Items.Item(1)
                            s = s & "[" & fajillaAux.MisBoletos(0).MiSerie.ToString() & "-" & fajillaAux.FolioInicial.ToString() & "-"
                            fajillaAux = formulario1.CheckedListBox1.Items.Item(formulario1.CheckedListBox1.Items.Count - 1)
                            s = s & fajillaAux.FolioFinal.ToString() & "]"
                        End If

                        If formulario1.CheckedListBox2.Items.Count > 1 Then 'en caso que hayan mas fajillas en CheckedListBox2...
                            Dim fajillaAux As New Fajilla(0, 0)
                            fajillaAux = formulario1.CheckedListBox2.Items.Item(1)
                            s = s & "[" & fajillaAux.MisBoletos(0).MiSerie.ToString() & "-" & fajillaAux.FolioInicial.ToString() & "-"
                            fajillaAux = formulario1.CheckedListBox2.Items.Item(formulario1.CheckedListBox2.Items.Count - 1)
                            s = s & fajillaAux.FolioFinal.ToString() & "]"
                        End If

                        'comenzar a abrir formulario mdi hijo
                        MessageBox.Show(s)
                        formulario1 = Nothing
                        Dim form2 As New Form2(s)
                        form2.TopLevel = False
                        form2.AutoSize = False
                        If (Me.PanelContenedor.Controls.Count > 0) Then
                            Me.PanelContenedor.Controls.RemoveAt(0)
                        End If
                        form2.MdiParent = Me
                        form2.Show()
                        Me.PanelContenedor.Controls.Add(form2)
                        form2.Dock = DockStyle.Fill
                    Else
                        'son iguales
                        Dim s As String
                        s = "[" & fajillaUno.MisBoletos(0).MiSerie.ToString() & "-" & fajillaUno.FolioInicial.ToString() & "-" & fajillaUno.FolioFinal.ToString() & "]"
                        s = s & "[" & fajillaDos.MisBoletos(0).MiSerie.ToString() & "-" & fajillaDos.FolioInicial.ToString() & "-" & fajillaDos.FolioFinal.ToString() & "]"

                        If formulario1.CheckedListBox1.Items.Count > 1 Then  'en caso que hayan mas fajillas en CheckedListBox1...
                            Dim fajillaAux As New Fajilla(0, 0)
                            fajillaAux = formulario1.CheckedListBox1.Items.Item(1)
                            s = s & "[" & fajillaAux.MisBoletos(0).MiSerie.ToString() & "-" & fajillaAux.FolioInicial.ToString() & "-"
                            fajillaAux = formulario1.CheckedListBox1.Items.Item(formulario1.CheckedListBox1.Items.Count - 1)
                            s = s & fajillaAux.FolioFinal.ToString() & "]"
                        End If

                        If formulario1.CheckedListBox2.Items.Count > 1 Then 'en caso que hayan mas fajillas en CheckedListBox2...
                            Dim fajillaAux As New Fajilla(0, 0)
                            fajillaAux = formulario1.CheckedListBox2.Items.Item(1)
                            s = s & "[" & fajillaAux.MisBoletos(0).MiSerie.ToString() & "-" & fajillaAux.FolioInicial.ToString() & "-"
                            fajillaAux = formulario1.CheckedListBox2.Items.Item(formulario1.CheckedListBox2.Items.Count - 1)
                            s = s & fajillaAux.FolioFinal.ToString() & "]"
                        End If

                        'comenzar a abrir formulario mdi hijo
                        MessageBox.Show(s)
                        formulario1 = Nothing
                        Dim form2 As New Form2(s)
                        form2.TopLevel = False
                        form2.AutoSize = False
                        If (Me.PanelContenedor.Controls.Count > 0) Then
                            Me.PanelContenedor.Controls.RemoveAt(0)
                        End If
                        form2.MdiParent = Me
                        form2.Show()
                        Me.PanelContenedor.Controls.Add(form2)
                        form2.Dock = DockStyle.Fill
                    End If
                End If

            Else
                If fajillaUnoIncompleto = True And fajillaDosIncompleto = False Then
                    Dim respuesta As String

                    Dim fajillaAux As New Fajilla(0, 0)
                    fajillaAux = formulario1.CheckedListBox1.Items.Item(0)

                    respuesta = "[" & fajillaAux.MisBoletos(0).MiSerie.ToString() & "-" & fajillaAux.FolioInicial.ToString() & "-"  'extraifo follio inicial de primer fajilla en checkBox1
                    fajillaAux = formulario1.CheckedListBox1.Items.Item(formulario1.CheckedListBox1.Items.Count - 1)
                    respuesta = respuesta & fajillaAux.FolioFinal.ToString() & "]"  'extraifo follio final de ultima fajilla en checkbox1

                    fajillaAux = formulario1.CheckedListBox2.Items.Item(0)
                    respuesta = respuesta & "[" & fajillaAux.MisBoletos(0).MiSerie.ToString() & "-" & fajillaAux.FolioInicial.ToString() & "-"  'extraifo follio inicial de primer fajilla en checkBox2
                    fajillaAux = formulario1.CheckedListBox2.Items.Item(formulario1.CheckedListBox2.Items.Count - 1)
                    respuesta = respuesta & fajillaAux.FolioFinal.ToString() & "]" 'extraifo follio final de ultima fajilla en checkbox2

                    'comenzar a abrir formulario mdi hijo
                    MessageBox.Show(respuesta)
                    formulario1 = Nothing
                    Dim form2 As New Form2(respuesta)
                    form2.TopLevel = False
                    form2.AutoSize = False
                    If (Me.PanelContenedor.Controls.Count > 0) Then
                        Me.PanelContenedor.Controls.RemoveAt(0)
                    End If
                    form2.MdiParent = Me
                    form2.Show()
                    Me.PanelContenedor.Controls.Add(form2)
                    form2.Dock = DockStyle.Fill

                Else
                    If fajillaUnoIncompleto = False And fajillaDosIncompleto = True Then
                        Dim respuesta As String

                        Dim fajillaAux As New Fajilla(0, 0)
                        fajillaAux = formulario1.CheckedListBox2.Items.Item(0)

                        'obtengo todo los que compone las segunda dotacion
                        respuesta = "[" & fajillaAux.MisBoletos(0).MiSerie & "-" & fajillaAux.FolioInicial.ToString() & "-"
                        fajillaAux = formulario1.CheckedListBox2.Items.Item(formulario1.CheckedListBox2.Items.Count - 1)
                        respuesta = respuesta & fajillaAux.FolioFinal.ToString() & "]"

                        'ahora obtengo todo lo que compojne las primera dotacion
                        fajillaAux = formulario1.CheckedListBox1.Items.Item(0)
                        respuesta = respuesta & "[" & fajillaAux.MisBoletos(0).MiSerie & "-" & fajillaAux.FolioInicial.ToString() & "-"
                        fajillaAux = formulario1.CheckedListBox1.Items.Item(formulario1.CheckedListBox1.Items.Count - 1)
                        respuesta = respuesta & fajillaAux.FolioFinal.ToString() & "]"

                        'comenzar a abrir formulario mdi hijo
                        MessageBox.Show(respuesta)
                        formulario1 = Nothing
                        Dim form2 As New Form2(respuesta)
                        form2.TopLevel = False
                        form2.AutoSize = False
                        If (Me.PanelContenedor.Controls.Count > 0) Then
                            Me.PanelContenedor.Controls.RemoveAt(0)
                        End If
                        form2.MdiParent = Me
                        form2.Show()
                        Me.PanelContenedor.Controls.Add(form2)
                        form2.Dock = DockStyle.Fill

                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            LimpiarCkeckedListBox()
            LimpiarTextBoxes()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            formulario1 = Nothing

            Dim formulario3 As Form3
            formulario3 = New Form3()
            formulario3.TopLevel = False
            formulario3.AutoSize = False
            If (Me.PanelContenedor.Controls.Count > 0) Then
                Me.PanelContenedor.Controls.RemoveAt(0)
            End If

            formulario3.MdiParent = Me
            formulario3.Show()
            Me.PanelContenedor.Controls.Add(formulario3)
            formulario3.Dock = DockStyle.Fill

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            formulario1 = Nothing

            Dim formulario4 As New Form4()
            formulario4.TopLevel = False
            formulario4.AutoSize = False

            If (Me.PanelContenedor.Controls.Count > 0) Then
                Me.PanelContenedor.Controls.RemoveAt(0)
            End If

            formulario4.MdiParent = Me
            formulario4.Show()
            Me.PanelContenedor.Controls.Add(formulario4)
            formulario4.Dock = DockStyle.Fill

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            formulario1 = Nothing
            If (Me.PanelContenedor.Controls.Count > 0) Then
                Me.PanelContenedor.Controls.RemoveAt(0)
            End If


            Dim result As DialogResult = MessageBox.Show("¿desea generar archivo de almacenamiento?", "Pregunta importante", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                Dim empNM As XNamespace = "urn:lst-emp:emp"

                Dim xDoc As New XDocument(New XDeclaration("1.0", "UTF-16", Nothing),
                              New XElement(empNM + "Dotaciones.xml"))

                Dim sw As New StringWriter()
                Dim xWrite As XmlWriter = XmlWriter.Create(sw)
                xDoc.Save(xWrite)
                xWrite.Close()
                xDoc.Save("Dotaciones.xml")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


End Class