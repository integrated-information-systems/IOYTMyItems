Namespace Models
    Public Class JoinObj
        Private _InputObj As Object
        Private _Join As String
        Private _Condition As String
        Public Property InputObj() As Object
            Get
                If IsNothing(_InputObj) Then
                    Return Nothing
                Else
                    Return _InputObj
                End If

            End Get
            Set(ByVal value As Object)
                _InputObj = value
            End Set
        End Property
        Public Property Join() As Object
            Get
                If IsNothing(_Join) Then
                    Return Nothing
                Else
                    Return _Join
                End If

            End Get
            Set(ByVal value As Object)
                _Join = value
            End Set
        End Property
        Public Property Condition() As Object
            Get
                If IsNothing(_Condition) Then
                    Return Nothing
                Else
                    Return _Condition
                End If

            End Get
            Set(ByVal value As Object)
                _Condition = value
            End Set
        End Property
    End Class
End Namespace
