Imports RebondTrading.Models
Imports System.Threading

Public Class Register
    Inherits System.Web.UI.Page

    Private Sub Register_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Try
            BindSalesPersons()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            RegisterUser.ContinueDestinationPageUrl = Request.QueryString("ReturnUrl")
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub

    Protected Sub RegisterUser_CreatedUser(ByVal sender As Object, ByVal e As EventArgs) Handles RegisterUser.CreatedUser
        Try

            Dim UserName As TextBox = RegisterUserWizardStep.ContentTemplateContainer.FindControl("Username")
            Dim UserProfile As ProfileBase = System.Web.Profile.ProfileBase.Create(UserName.Text)
            'Dim SalesPersonLst As ListBox = RegisterUserWizardStep.ContentTemplateContainer.FindControl("SalesPersonLst")
            'Dim ScsValues As String = String.Empty
            'Dim ScsValuesList As List(Of String) = New List(Of String)
            'Dim index() As Integer = SalesPersonLst.GetSelectedIndices

            'For Each scsIndex In index
            '    ScsValuesList.Add(SalesPersonLst.Items(scsIndex).Value)
            '    'ScsValues = ScsValues & SalesPersonLst.Items(scsIndex).Value & "|"
            'Next
            'ScsValues = String.Join("|", ScsValuesList.ToArray)

            'UserProfile.SetPropertyValue("SalesPersons", ScsValues)
            'UserProfile.Save()

            If Roles.RoleExists("normal-user") Then
                Roles.AddUserToRole(UserName.Text.ToString, "normal-user")
            End If
            ShowMessage("User Added Sucessfully")
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub ShowMessage(ByVal Message As String)
        LblShowMsg.Text = Message
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GPMS", "$(document).ready(function () { ShowMessage(); });", True)
    End Sub
    Protected Sub ContinueButton_Click(sender As Object, e As System.EventArgs)
        Try


            Response.Redirect("~/Account/Register.aspx")
        Catch ex As Exception When Not TypeOf ex Is ThreadAbortException
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub CreateUserButton_Click(sender As Object, e As System.EventArgs)

    End Sub
    Protected Sub BindSalesPersons()
        'Try
        '    Dim SalesPersonLst As ListBox = RegisterUserWizardStep.ContentTemplateContainer.FindControl("SalesPersonLst")
        '    SalesPersonLst.Items.Clear()
        '    Dim SalesPersonTable As DataTable = AppSpecificFunc.GetAllSalesPersons
        '    SalesPersonLst.DataSource = SalesPersonTable
        '    SalesPersonLst.DataTextField = "SlpName"
        '    SalesPersonLst.DataValueField = "SlpCode"
        '    SalesPersonLst.DataBind()
        'Catch ex As Exception
        '    AppSpecificFunc.WriteLog(ex)
        'End Try
    End Sub
End Class