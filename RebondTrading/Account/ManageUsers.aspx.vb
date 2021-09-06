Imports RebondTrading.Models
Imports System.Drawing

Public Class ManageUsers
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            If Not IsPostBack Then
                BindUsers()
                BindSalesPersons()
            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub BindUsers()
        Try
            UsersList.DataSource = Membership.GetAllUsers
            UsersList.DataBind()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub BindSalesPersons()
        'Try
        '    Dim SalesPersonTable As DataTable = AppSpecificFunc.GetAllSalesPersons
        '    SalesPersonLst.DataSource = SalesPersonTable
        '    SalesPersonLst.DataTextField = "SlpName"
        '    SalesPersonLst.DataValueField = "SlpCode"
        '    SalesPersonLst.DataBind()
        'Catch ex As Exception
        '    AppSpecificFunc.WriteLog(ex)
        'End Try
    End Sub
    Protected Sub UsersList_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Try


            Dim UserName As String = UsersList.Rows(e.RowIndex).Cells(1).Text

            If UserName.ToLower = "admin" Then
                lblMsg.Text = String.Format("You Cannot Delete {0} User", UserName)
                lblMsg.ForeColor = Color.Green
                lblMsg.Visible = True
                BindUsers()
                UsersList.SelectedIndex = -1
            Else
                Dim SelectedUser As MembershipUser = Membership.GetUser(UserName)
                If Not IsNothing(SelectedUser) Then
                    ' Temporarily commented on 3:42 PM 10/Mar/2015
                    SelectedUser.IsApproved = False
                    'Membership.UpdateUser(SelectedUser)
                    Membership.DeleteUser(SelectedUser.UserName)
                End If
                'lblMsg.Text = String.Format("User ""{0}""  deleted Successfully", UserName)
                'lblMsg.ForeColor = Color.Green
                'lblMsg.Visible = True
                ShowMessage(String.Format("User ""{0}""  deleted Successfully", UserName))
                BindUsers()
                UsersList.SelectedIndex = -1
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub

    Private Sub UsersList_DataBinding(sender As Object, e As System.EventArgs) Handles UsersList.DataBinding

    End Sub
    Protected Sub UsersList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles UsersList.RowDataBound
        Try


            If e.Row.RowIndex > -1 Then
                Dim MUser As MembershipUser = Membership.GetUser(e.Row.Cells(1).Text)
                If MUser.IsApproved = False Then

                    e.Row.BackColor = Color.Red
                    e.Row.ForeColor = Color.White
                    'e.Row.Cells(0).Text = String.Empty

                Else
                    Dim lb As LinkButton = e.Row.Cells(0).Controls(0)
                    lb.OnClientClick = "return confirm('Are you certain you want to delete?');"
                End If

            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub UsersList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles UsersList.SelectedIndexChanged
        Try




            UserName.Text = UsersList.Rows(UsersList.SelectedIndex).Cells(1).Text
            Dim MUser As MembershipUser = Membership.GetUser(UserName.Text)
            Dim UserProfile As ProfileBase = System.Web.Profile.ProfileBase.Create(UserName.Text)

            'Dim SalesPersonList As String = UserProfile.GetPropertyValue("SalesPersons").ToString
            'SalesPersonLst.ClearSelection()

            'Dim SplittedSalesPersons() As String = SalesPersonList.Split("|")

            'For Each SalesPersonValue In SplittedSalesPersons
            '    Dim index As Integer = SalesPersonLst.Items.IndexOf(SalesPersonLst.Items.FindByValue(SalesPersonValue))
            '    If index <> -1 Then
            '        SalesPersonLst.Items(index).Selected = True
            '    End If
            'Next

            Email.Text = MUser.Email
            BindUsers()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try

    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try


            Dim MUser As MembershipUser = Membership.GetUser(UserName.Text)

            If Password.Text <> String.Empty Then
                MUser.UnlockUser()
                MUser.ChangePassword(MUser.ResetPassword(), Password.Text)
            End If

            MUser.Email = Email.Text
            ''MUser.IsApproved = True
            Membership.UpdateUser(MUser)


            'Dim UserProfile As ProfileBase = System.Web.Profile.ProfileBase.Create(UserName.Text)
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

            ClearForm()
            'lblMsg.Visible = True
            'lblMsg.Text = "Updated Successfully"
            'lblMsg.ForeColor = Color.Green
            BindUsers()
            ShowMessage("User information updated Sucessfully")
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Private Sub ClearForm()
        Try


            UsersList.SelectedIndex = -1
            UserName.Text = String.Empty
            Email.Text = String.Empty
            Password.Text = String.Empty
            ConfirmPassword.Text = String.Empty
            'SalesPersonLst.ClearSelection()
            Abbreviation.Text = String.Empty
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub ShowMessage(ByVal Message As String)
        LblShowMsg.Text = Message
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GPMS", "$(document).ready(function () { ShowMessage(); });", True)
    End Sub

End Class