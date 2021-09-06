Public Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' This helps to load javascript libraries while using RegisterStartupScript
        Page.Header.DataBind()
    End Sub

End Class