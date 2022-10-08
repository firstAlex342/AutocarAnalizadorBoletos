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
                LLenarCheckedListBox(CheckedListBox1, rangoInicial, rangoFinal)
            End If


            If (TextBox3.TextLength > 0 And TextBox4.TextLength > 0) Then
                rangoInicial = Int32.Parse(TextBox3.Text)
                rangoFinal = Int32.Parse(TextBox4.Text)
                ValidarRangos(rangoInicial, rangoFinal)
                LLenarCheckedListBox(CheckedListBox2, rangoInicial, rangoFinal)
            End If

            If (TextBox5.TextLength > 0 And TextBox6.TextLength > 0) Then
                rangoInicial = Int32.Parse(TextBox5.Text)
                rangoFinal = Int32.Parse(TextBox6.Text)
                ValidarRangos(rangoInicial, rangoFinal)
                LLenarCheckedListBox(CheckedListBox3, rangoInicial, rangoFinal)
            End If

            If (TextBox7.TextLength > 0 And TextBox8.TextLength > 0) Then
                rangoInicial = Int32.Parse(TextBox7.Text)
                rangoFinal = Int32.Parse(TextBox8.Text)
                ValidarRangos(rangoInicial, rangoFinal)
                LLenarCheckedListBox(CheckedListBox4, rangoInicial, rangoFinal)
            End If

            If (TextBox9.TextLength > 0 And TextBox10.TextLength > 0) Then
                rangoInicial = Int32.Parse(TextBox9.Text)
                rangoFinal = Int32.Parse(TextBox10.Text)
                ValidarRangos(rangoInicial, rangoFinal)
                LLenarCheckedListBox(CheckedListBox5, rangoInicial, rangoFinal)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub LLenarCheckedListBox(ByRef miChk As CheckedListBox, ByVal rangoInicial As Integer, ByVal rangoFinal As Integer)
        Dim generatorFajillas As New GeneradorFajillas(rangoInicial, rangoFinal)
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

    End Sub


    Private Sub ValidarRangos(ByVal rangIni As Integer, ByVal rangFini As Integer)
        If rangFini < rangIni Then
            Throw New ArgumentException(rangIni.ToString() & "debe ser menor a " & rangFini.ToString())
        End If
    End Sub
End Class
