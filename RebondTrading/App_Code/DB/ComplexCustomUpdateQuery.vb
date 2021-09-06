Imports Microsoft.VisualBasic
Namespace Models
    Public Class ComplexCustomUpdateQuery
        Private FilterTable As Object()
        Public Property _UpdateQuery() As String
        Public Property _FilterTable() As Object
            Get
                Return FilterTable
            End Get
            Set(ByVal value As Object)
                FilterTable = value
            End Set
        End Property
        Public Property _HasWhereConditions() As Boolean
        Public Property _Conditions() As Dictionary(Of String, List(Of String))
        Public Property _HasInBetweenConditions() As Boolean
        Public Property _InBetweenCondition() As String
        Public Property _DB() As String
    End Class
End Namespace

