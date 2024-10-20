Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            LimpiarCkeckedListBox()

            Dim rangoInicial As Integer
            Dim rangoFinal As Integer

            If (TextBox1.TextLength > 0 And TextBox2.TextLength > 0) Then ' Si existe algo escrit en ambos textboxes...
                rangoInicial = Int32.Parse(TextBox1.Text)  'Convertir el primero a numero, en caso de que no se pueda lanzar excepcion
                rangoFinal = Int32.Parse(TextBox2.Text)     'Convertir el segundo a numero, en caso de que no se pueda lanzar excepcion
                ValidarRangos(rangoInicial, rangoFinal)  'Verifica que el rango Final sea mayor o igual que rango final, si no se puede lanzaar excepcion
                LLenarCheckedListBox(CheckedListBox1, rangoInicial, rangoFinal, TextBox11.Text)
            End If


            If (TextBox3.TextLength > 0 And TextBox4.TextLength > 0) Then
                rangoInicial = Int32.Parse(TextBox3.Text)
                rangoFinal = Int32.Parse(TextBox4.Text)
                ValidarRangos(rangoInicial, rangoFinal)
                LLenarCheckedListBox(CheckedListBox2, rangoInicial, rangoFinal, TextBox12.Text)
            End If

            If (TextBox5.TextLength > 0 And TextBox6.TextLength > 0) Then
                rangoInicial = Int32.Parse(TextBox5.Text)
                rangoFinal = Int32.Parse(TextBox6.Text)
                ValidarRangos(rangoInicial, rangoFinal)
                'LLenarCheckedListBox(CheckedListBox3, rangoInicial, rangoFinal)
            End If

            If (TextBox7.TextLength > 0 And TextBox8.TextLength > 0) Then
                rangoInicial = Int32.Parse(TextBox7.Text)
                rangoFinal = Int32.Parse(TextBox8.Text)
                ValidarRangos(rangoInicial, rangoFinal)
                'LLenarCheckedListBox(CheckedListBox4, rangoInicial, rangoFinal)
            End If

            If (TextBox9.TextLength > 0 And TextBox10.TextLength > 0) Then
                rangoInicial = Int32.Parse(TextBox9.Text)
                rangoFinal = Int32.Parse(TextBox10.Text)
                ValidarRangos(rangoInicial, rangoFinal)
                'LLenarCheckedListBox(CheckedListBox5, rangoInicial, rangoFinal)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub LLenarCheckedListBox(ByRef miChk As CheckedListBox, ByVal rangoInicial As Integer, ByVal rangoFinal As Integer, ByVal serieBoleto As String)
        Dim generatorFajillas As New GeneradorFajillas(rangoInicial, rangoFinal, serieBoleto)
        Dim listaFajillas As New List(Of Fajilla)
        listaFajillas = generatorFajillas.GenerarFajillas()

        Dim i As Integer
        i = 0
        While (i < listaFajillas.Count)
            miChk.Items.Add(listaFajillas(i))
            i = i + 1
        End While
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            LimpiarCkeckedListBox()
            LimpiarTextBoxes()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub LimpiarCkeckedListBox()
        CheckedListBox1.Items.Clear()
        CheckedListBox2.Items.Clear()
        CheckedListBox3.Items.Clear()
        CheckedListBox4.Items.Clear()
        CheckedListBox5.Items.Clear()
    End Sub


    Private Sub LimpiarTextBoxes()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox9.Clear()
        TextBox10.Clear()

        TextBox11.Clear()
        TextBox12.Clear()

    End Sub


    Private Sub ValidarRangos(ByVal rangIni As Integer, ByVal rangFini As Integer)
        If rangFini < rangIni Then
            Throw New ArgumentException(rangIni.ToString() & "debe ser menor a " & rangFini.ToString())
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim fajillaUno As New Fajilla(0, 0)
        Dim fajillaDos As New Fajilla(0, 0)
        Dim fajillaUnoIncompleto As Boolean
        Dim fajillaDosIncompleto As Boolean
        Dim listaFajillasAGuardar As New List(Of Fajilla)
        Dim i As Integer = 0

        fajillaUnoIncompleto = False
        fajillaDosIncompleto = False

        Try
            If CheckedListBox1.Items.Count >= 1 Then  'si tiene varios items
                fajillaUno = CheckedListBox1.Items.Item(0) 'examina si el prime item esta incompleto
                If fajillaUno.MisBoletos.Count < 100 Then
                    fajillaUnoIncompleto = True 'esta incompleto
                End If
            Else
                Throw New Exception("La chklistbox1 debe tener al menos 1 item")
            End If


            If CheckedListBox2.Items.Count >= 1 Then
                fajillaDos = CheckedListBox2.Items.Item(0)
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

                    If CheckedListBox1.Items.Count > 1 Then  'en caso que hayan mas fajillas en CheckedListBox1...
                        Dim fajillaAux As New Fajilla(0, 0)
                        fajillaAux = CheckedListBox1.Items.Item(1)
                        s = s & "[" & fajillaAux.MisBoletos(0).MiSerie.ToString() & "-" & fajillaAux.FolioInicial.ToString() & "-"
                        fajillaAux = CheckedListBox1.Items.Item(CheckedListBox1.Items.Count - 1)
                        s = s & fajillaAux.FolioFinal.ToString() & "]"
                    End If

                    If CheckedListBox2.Items.Count > 1 Then 'en caso que hayan mas fajillas en CheckedListBox2...
                        Dim fajillaAux As New Fajilla(0, 0)
                        fajillaAux = CheckedListBox2.Items.Item(1)
                        s = s & "[" & fajillaAux.MisBoletos(0).MiSerie.ToString() & "-" & fajillaAux.FolioInicial.ToString() & "-"
                        fajillaAux = CheckedListBox2.Items.Item(CheckedListBox2.Items.Count - 1)
                        s = s & fajillaAux.FolioFinal.ToString() & "]"
                    End If


                    MessageBox.Show(s)
                    Dim form2 As New Form2(s)
                    form2.ShowDialog()
                Else
                    If fajillaDos.MisBoletos.Count < fajillaUno.MisBoletos.Count Then
                        'fajilla dos es menor
                        Dim s As String
                        s = "[" & fajillaDos.MisBoletos(0).MiSerie.ToString() & "-" & fajillaDos.FolioInicial.ToString() & "-" & fajillaDos.FolioFinal.ToString() & "]"
                        s = s & "[" & fajillaUno.MisBoletos(0).MiSerie.ToString() & "-" & fajillaUno.FolioInicial.ToString() & "-" & fajillaUno.FolioFinal.ToString() & "]"

                        '-----------------se repite procedimiento
                        If CheckedListBox1.Items.Count > 1 Then  'en caso que hayan mas fajillas en CheckedListBox1...
                            Dim fajillaAux As New Fajilla(0, 0)
                            fajillaAux = CheckedListBox1.Items.Item(1)
                            s = s & "[" & fajillaAux.MisBoletos(0).MiSerie.ToString() & "-" & fajillaAux.FolioInicial.ToString() & "-"
                            fajillaAux = CheckedListBox1.Items.Item(CheckedListBox1.Items.Count - 1)
                            s = s & fajillaAux.FolioFinal.ToString() & "]"
                        End If

                        If CheckedListBox2.Items.Count > 1 Then 'en caso que hayan mas fajillas en CheckedListBox2...
                            Dim fajillaAux As New Fajilla(0, 0)
                            fajillaAux = CheckedListBox2.Items.Item(1)
                            s = s & "[" & fajillaAux.MisBoletos(0).MiSerie.ToString() & "-" & fajillaAux.FolioInicial.ToString() & "-"
                            fajillaAux = CheckedListBox2.Items.Item(CheckedListBox2.Items.Count - 1)
                            s = s & fajillaAux.FolioFinal.ToString() & "]"
                        End If


                        MessageBox.Show(s)
                        Dim form2 As New Form2(s)
                        form2.ShowDialog()
                    Else
                        'son iguales
                        Dim s As String
                        s = "[" & fajillaUno.MisBoletos(0).MiSerie.ToString() & "-" & fajillaUno.FolioInicial.ToString() & "-" & fajillaUno.FolioFinal.ToString() & "]"
                        s = s & "[" & fajillaDos.MisBoletos(0).MiSerie.ToString() & "-" & fajillaDos.FolioInicial.ToString() & "-" & fajillaDos.FolioFinal.ToString() & "]"

                        If CheckedListBox1.Items.Count > 1 Then  'en caso que hayan mas fajillas en CheckedListBox1...
                            Dim fajillaAux As New Fajilla(0, 0)
                            fajillaAux = CheckedListBox1.Items.Item(1)
                            s = s & "[" & fajillaAux.MisBoletos(0).MiSerie.ToString() & "-" & fajillaAux.FolioInicial.ToString() & "-"
                            fajillaAux = CheckedListBox1.Items.Item(CheckedListBox1.Items.Count - 1)
                            s = s & fajillaAux.FolioFinal.ToString() & "]"
                        End If

                        If CheckedListBox2.Items.Count > 1 Then 'en caso que hayan mas fajillas en CheckedListBox2...
                            Dim fajillaAux As New Fajilla(0, 0)
                            fajillaAux = CheckedListBox2.Items.Item(1)
                            s = s & "[" & fajillaAux.MisBoletos(0).MiSerie.ToString() & "-" & fajillaAux.FolioInicial.ToString() & "-"
                            fajillaAux = CheckedListBox2.Items.Item(CheckedListBox2.Items.Count - 1)
                            s = s & fajillaAux.FolioFinal.ToString() & "]"
                        End If

                        MessageBox.Show(s)
                        Dim form2 As New Form2(s)
                        form2.ShowDialog()
                    End If
                End If

            Else
                If fajillaUnoIncompleto = True And fajillaDosIncompleto = False Then
                    Dim respuesta As String

                    Dim fajillaAux As New Fajilla(0, 0)
                    fajillaAux = CheckedListBox1.Items.Item(0)

                    respuesta = "[" & fajillaAux.MisBoletos(0).MiSerie.ToString() & "-" & fajillaAux.FolioInicial.ToString() & "-"  'extraifo follio inicial de primer fajilla en checkBox1
                    fajillaAux = CheckedListBox1.Items.Item(CheckedListBox1.Items.Count - 1)
                    respuesta = respuesta & fajillaAux.FolioFinal.ToString() & "]"  'extraifo follio final de ultima fajilla en checkbox1

                    fajillaAux = CheckedListBox2.Items.Item(0)
                    respuesta = respuesta & "[" & fajillaAux.MisBoletos(0).MiSerie.ToString() & "-" & fajillaAux.FolioInicial.ToString() & "-"  'extraifo follio inicial de primer fajilla en checkBox2
                    fajillaAux = CheckedListBox2.Items.Item(CheckedListBox2.Items.Count - 1)
                    respuesta = respuesta & fajillaAux.FolioFinal.ToString() & "]" 'extraifo follio final de ultima fajilla en checkbox2

                    MessageBox.Show(respuesta)
                    Dim form2 As New Form2(respuesta)
                    form2.ShowDialog()

                Else
                    If fajillaUnoIncompleto = False And fajillaDosIncompleto = True Then
                        Dim respuesta As String

                        Dim fajillaAux As New Fajilla(0, 0)
                        fajillaAux = CheckedListBox2.Items.Item(0)

                        'obtengo todo los que compone las segunda dotacion
                        respuesta = "[" & fajillaAux.MisBoletos(0).MiSerie & "-" & fajillaAux.FolioInicial.ToString() & "-"
                        fajillaAux = CheckedListBox2.Items.Item(CheckedListBox2.Items.Count - 1)
                        respuesta = respuesta & fajillaAux.FolioFinal.ToString() & "]"

                        'ahora obtengo todo lo que compojne las primera dotacion
                        fajillaAux = CheckedListBox1.Items.Item(0)
                        respuesta = respuesta & "[" & fajillaAux.MisBoletos(0).MiSerie & "-" & fajillaAux.FolioInicial.ToString() & "-"
                        fajillaAux = CheckedListBox1.Items.Item(CheckedListBox1.Items.Count - 1)
                        respuesta = respuesta & fajillaAux.FolioFinal.ToString() & "]"

                        MessageBox.Show(respuesta)

                        Dim form2 As New Form2(respuesta)
                        form2.ShowDialog()

                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim form3 As New Form3()
        form3.ShowDialog()
    End Sub
End Class
