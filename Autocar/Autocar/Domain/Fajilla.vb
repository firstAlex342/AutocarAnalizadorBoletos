Public Class Fajilla
    Private _folioInicial As Integer
    Private _folioFinal As Integer
    Private _misBoletos As List(Of Boleto)

    '-----------------Constructor
    Public Sub New(ByVal folioInicial As Integer, ByVal folioFinal As Integer)
        Me.FolioInicial = folioInicial
        Me.FolioFinal = folioFinal
        Me.MisBoletos = New List(Of Boleto)
    End Sub

    Public Function MostrarInfo() As String
        Dim s As String
        s = "Folio inicial = " & FolioInicial.ToString() & " Folio final = " & FolioFinal.ToString()
        Return s
    End Function

    Public Overrides Function ToString() As String  '-----overrides en vez de overloads porque???
        Return (FolioInicial.ToString() & "     " & FolioFinal.ToString())
    End Function

    Public Sub AddBoleto(ByRef miBoleto As Boleto)
        Me.MisBoletos.Add(miBoleto)
    End Sub

    Public Sub InicializarFolioInicialYFolioFinal()
        Me.FolioInicial = (Me.MisBoletos.First()).MiFolio
        Me.FolioFinal = (Me.MisBoletos.Last()).MiFolio

    End Sub

    '----------------Properties
    Public Property FolioInicial As Integer
        Get
            Return _folioInicial
        End Get
        Set(value As Integer)
            _folioInicial = value
        End Set
    End Property

    Public Property FolioFinal As Integer
        Get
            Return _folioFinal
        End Get
        Set(value As Integer)
            _folioFinal = value
        End Set
    End Property

    Public Property MisBoletos As List(Of Boleto)
        Get
            Return _misBoletos
        End Get

        Set(value As List(Of Boleto))
            _misBoletos = value
        End Set
    End Property
End Class
