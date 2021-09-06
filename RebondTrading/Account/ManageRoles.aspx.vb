Imports RebondTrading.Models
Imports System.Drawing

Public Class ManageRoles
    Inherits System.Web.UI.Page

    Protected Sub CheckExist(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        Try

            If Roles.RoleExists(txtRoleName.Text) Then
                e.IsValid = False
            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub RolesGrid_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Try


            Dim RoleNamelbl As Label = RolesGrid.Rows(e.RowIndex).Cells(1).FindControl("lblRoleName")
            Dim RoleName As String = RoleNamelbl.Text
            If RoleName.ToLower = "admin" Then
                ShowMessage(String.Format("You cannot delete {0} Role", RoleName))
            Else
                If Roles.RoleExists(RoleName) Then
                    Roles.DeleteRole(RoleName)
                End If
                ShowMessage(String.Format("Role ""{0}"" deleted Successfully", RoleName))
                LoadRoles()
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try

            If Not Roles.RoleExists(txtRoleName.Text) Then
                Roles.CreateRole(txtRoleName.Text)
                Clear()
                LoadRoles()
                ShowMessage("Role Successfully Added")
            End If
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
    Protected Sub LoadRoles()
        Try

            Dim rolesArray() As String
            rolesArray = Roles.GetAllRoles()
            RolesGrid.DataSource = rolesArray
            RolesGrid.DataBind()

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub Clear()
        Try
            txtRoleName.Text = String.Empty
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack Then
                lblMsg.Text = String.Empty
            End If
           
            LoadRoles()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub

    Protected Sub RolesGrid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles RolesGrid.RowDataBound
        Try
            If e.Row.RowIndex >= 0 Then
                If Not Roles.IsUserInRole(User.Identity.Name, "admin") Then
                    e.Row.Cells(0).Text = String.Empty
                End If
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub

End Class