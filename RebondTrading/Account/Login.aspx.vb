Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString("ReturnUrl"))
        If User.Identity.Name <> Nothing Then
            Response.Redirect("~")
        End If
    End Sub

     

    Private Sub LoginUser_LoggedIn(sender As Object, e As System.EventArgs) Handles LoginUser.LoggedIn        
        Session("CurrentLoggedIn") = LoginUser.UserName
    End Sub

    Private Sub LoginUser_LoggingIn(sender As Object, e As System.Web.UI.WebControls.LoginCancelEventArgs) Handles LoginUser.LoggingIn
        
       
    End Sub
End Class