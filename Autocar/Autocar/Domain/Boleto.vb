Public Class Boleto
    Private _miSerie As String
    Private _miFolio As Integer
    '------tips para probar clase
    'Dim miBoleto As New Boleto("I34", 516700)
    'System.Console.WriteLine(miBoleto.EsBoletoFinal().ToString())


    '-----Constructor
    Public Sub New(ByVal serie As String, ByVal folio As Integer)
        Me.MiSerie = serie
        Me.MiFolio = folio
    End Sub

    '----Methods
    Public Function EsBoletoFinal() As Boolean
        If Me.MiFolio >= 100 Then
            Dim folioFormatoCadena As String
            folioFormatoCadena = Me.MiFolio.ToString()

            Dim ultimoDigito As Char
            Dim penultimoDigito As Char

            ultimoDigito = folioFormatoCadena(folioFormatoCadena.Length - 1)
            penultimoDigito = folioFormatoCadena(folioFormatoCadena.Length - 2)
            If ultimoDigito = "0"c And penultimoDigito = "0"c Then
                Return True
            Else
                Return False
            End If
        End If

        'Aqui falta considerar en caso de   que el boleto final de una fajilla sea 99 en vez de 100
        Return False
    End Function


    '---Properties
    Public Property MiSerie As String
        Set(value As String)
            _miSerie = value
        End Set

        Get
            Return _miSerie
        End Get
    End Property

    Public Property MiFolio As Integer
        Set(value As Integer)
            _miFolio = value
        End Set

        Get
            Return (_miFolio)
        End Get
    End Property
End Class
