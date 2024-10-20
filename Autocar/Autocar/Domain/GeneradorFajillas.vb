Public Class GeneradorFajillas
    Private _rangoInicial As Integer
    Private _rangoFinal As Integer
    Private _serie As String

    '--------Constructor
    Public Sub New(ByVal rangoInicial As Integer, ByVal rangoFinal As Integer, ByVal serie As String)
        Me.RangoInicial = rangoInicial
        Me.RangoFinal = rangoFinal
        Me.Serie = serie
    End Sub


    Public Function GenerarFajillas() As List(Of Fajilla)
        Dim lista As New List(Of Fajilla)

        Dim i As Integer
        i = RangoInicial


        Dim miFajilla As New Fajilla(0, 0)
        While (i <= RangoFinal)
            Dim miBoleto As New Boleto(Me.Serie, i)
            miFajilla.AddBoleto(miBoleto)
            If miBoleto.EsBoletoFinal() Or miBoleto.MiFolio = RangoFinal Then
                miFajilla.InicializarFolioInicialYFolioFinal()
                lista.Add(miFajilla)
                miFajilla = New Fajilla(0, 0)
            End If

            i = i + 1
        End While

        Return lista
    End Function





    '--------------Properties
    Public Property RangoInicial As Integer
        Get
            Return _rangoInicial
        End Get
        Set(value As Integer)
            _rangoInicial = value
        End Set
    End Property

    Public Property RangoFinal As Integer
        Get
            Return _rangoFinal
        End Get
        Set(value As Integer)
            _rangoFinal = value
        End Set
    End Property

    Public Property Serie As String
        Get
            Return _serie
        End Get
        Set(value As String)
            _serie = value
        End Set
    End Property
End Class
