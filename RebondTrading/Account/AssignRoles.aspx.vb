Imports RebondTrading.Models

Public Class AssignRoles
    Inherits System.Web.UI.Page

     
    Protected Sub btnAssignRoles_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAssignRoles.Click
        Try


            Dim selectedUser As String = String.Empty
            selectedUser = LstUserList.SelectedValue
            If selectedUser <> "0" Then
                For Each item As RepeaterItem In UsersRoleList.Items
                    Dim CurrentItem As CheckBox = item.FindControl("RoleCheckBox")
                    If CurrentItem.Checked = True And Not Roles.IsUserInRole(selectedUser, CurrentItem.Text) Then
                        Roles.AddUserToRole(selectedUser, CurrentItem.Text)
                    ElseIf CurrentItem.Checked = False And Roles.IsUserInRole(selectedUser, CurrentItem.Text) Then

                        Roles.RemoveUserFromRole(selectedUser, CurrentItem.Text)

                    End If

                Next
                'lblMsg.Text = String.Format("Roles  for user ""{0}""  applied Successfully", selectedUser)
                'lblMsg.ForeColor = Color.Green
                'lblMsg.Visible = True
                ShowMessage(String.Format("Roles  for user ""{0}""  applied Successfully", selectedUser))
            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            If Not IsPostBack Then
                LoadUserList()
                LoadRolesList()
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub LoadUserList()
        Try
            Dim Users As MembershipUserCollection = Membership.GetAllUsers()
            LstUserList.DataSource = Users
            LstUserList.DataBind()
            LstUserList.Items.Insert(0, New ListItem("-- Choose a User --", "0"))
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub LoadRolesList()
        Try
            Dim rolesArray As List(Of String)
            rolesArray = Roles.GetAllRoles().ToList
            UsersRoleList.DataSource = rolesArray
            UsersRoleList.DataBind()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub ShowMessage(ByVal Message As String)
        Try
            LblShowMsg.Text = Message
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GPMS", "$(document).ready(function () { ShowMessage(); });", True)
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub LstUserList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LstUserList.SelectedIndexChanged
        Try


            Dim selectedUser As String = String.Empty
            selectedUser = LstUserList.SelectedValue
            If selectedUser <> "0" Then
                For Each item As RepeaterItem In UsersRoleList.Items
                    Dim CurrentItem As CheckBox = item.FindControl("RoleCheckBox")
                    If Roles.IsUserInRole(selectedUser, CurrentItem.Text) Then
                        CurrentItem.Checked = True
                    ElseIf Not Roles.IsUserInRole(selectedUser, CurrentItem.Text) Then

                        CurrentItem.Checked = False

                    End If

                Next
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
End Class