Namespace Models
    Public Class ComplexDeleteQuery
        Private InputTable As Object()

        Public Property _InputTable() As Object
            Get
                Return InputTable
            End Get
            Set(ByVal value As Object)
                InputTable = value
            End Set
        End Property
        Public Property _HasWhereConditions() As Boolean
        Public Property _Conditions() As Dictionary(Of String, List(Of String))
        Public Property _HasInBetweenConditions() As Boolean
        Public Property _InBetweenCondition() As String
        Public Property _DB() As String
    End Class
End Namespace

