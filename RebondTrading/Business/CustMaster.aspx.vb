Imports System.Globalization
Imports RebondTrading.Models

Public Class CustMaster
    Inherits System.Web.UI.Page

#Region "Form Event Handlers"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack() Then
                Active.Checked = True
            End If
            LoadData()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            LoadData()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try
            ClearForm()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Private Sub btnAddOrUpdate_Click(sender As Object, e As EventArgs) Handles btnAddOrUpdate.Click
        Try
            If Page.IsValid Then
                Dim InputObj As New CustomerMaster
                InputObj.CustomerCode = CustomerCode.Text.Trim
                InputObj.CustomerName = CustomerName.Text.Trim
                InputObj.Active = CBool(Active.Checked)
                InputObj.AddressLine1 = AddressLine1.Text.Trim
                InputObj.AddressLine2 = AddressLine2.Text.Trim
                InputObj.AddressLine3 = AddressLine3.Text.Trim
                InputObj.AddressLine4 = AddressLine4.Text.Trim
                InputObj.AddressLine5 = AddressLine5.Text.Trim

                Dim ValidDate As New Date
                If Date.TryParseExact(RegistrationDate.Text, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, ValidDate) Then
                    InputObj.RegistrationDate = ValidDate.ToString("yyyy-MM-dd")
                End If



                Dim FilterObj As New CustomerMaster

                If CustomerMasterListGrid.SelectedIndex > -1 Then
                    InputObj.UpdatedBy = User.Identity.Name
                    FilterObj.Idkey = CustomerMasterListGrid.DataKeys(CustomerMasterListGrid.SelectedIndex).Value

                    Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                    'Query Condition Groups
                    Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                    'Query Conditions values

                    ConditionsGrp1.Add("IdKey=@Filter_IdKey")
                    QryConditions.Add(" AND ", ConditionsGrp1)

                    Dim UQ As New UpdateQuery
                    UQ._InputTable = InputObj
                    UQ._FilterTable = FilterObj
                    UQ._DB = "Custom"
                    UQ._Conditions = QryConditions
                    UQ._HasInBetweenConditions = False
                    UQ._HasWhereConditions = True

                    If CURD.UpdateData(UQ) Then
                        ShowMessage("Updated Successfully")
                        ClearForm()
                    Else
                        ShowMessage("Customer already exist")
                    End If
                Else
                    'Dim PrimaryKey As Integer = 0

                    'AppSpecificFunc.GetNextIdentityNo(PrimaryKey, 1, New CustomerMaster)
                    'While (AppSpecificFunc.IsIdentityNoNotAlreadyUsed(PrimaryKey, New CustomerMaster) = False)
                    '    AppSpecificFunc.GetNextIdentityNo(PrimaryKey, 1, New CustomerMaster)
                    'End While
                    'InputObj.Idkey = PrimaryKey


                    InputObj.Idkey = Nothing
                    InputObj.CreatedBy = User.Identity.Name
                    If (CURD.InsertData(InputObj, True)) Then
                        ShowMessage("Added Successfully")
                        ClearForm()
                    Else
                        ShowMessage("Customer already exist")
                    End If
                End If

            End If
            LoadData()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub ShowMessage(ByVal Message As String)
        Try
            LblShowMsg.Text = Message
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CBL", "$(document).ready(function () { ShowMessage(); });", True)
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Private Sub btnSearchClear_Click(sender As Object, e As EventArgs) Handles btnSearchClear.Click
        Try
            ClearSearchForm()
            LoadData()
            CustomerMasterListGrid.SelectedIndex = -1
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub

    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        CustomerMasterListGrid.PageIndex = e.NewPageIndex
        LoadData()
    End Sub


#End Region
#Region "Form Related functions"

    Protected Sub ClearSearchForm()
        Try
            CustomerCode.Text = String.Empty
            CustomerName.Text = String.Empty
            LoadData()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub ClearForm()
        Try
            CustomerCode.Text = String.Empty
            CustomerName.Text = String.Empty
            RegistrationDate.Text = String.Empty
            AddressLine1.Text = String.Empty
            AddressLine2.Text = String.Empty
            AddressLine3.Text = String.Empty
            AddressLine4.Text = String.Empty
            AddressLine5.Text = String.Empty
            Active.Checked = True
            btnAddOrUpdate.Text = "Add"
            CustomerMasterListGrid.SelectedIndex = -1
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Function LoadData() As DataTable
        Dim ResultDataTable As New DataTable
        Try
            Dim ItemMasterObj As New CustomerMaster
            Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))


            'Query Condition Groups
            Dim ConditionsGrp1 As List(Of String) = New List(Of String)


            'Query Conditions values
            If SearchCustomerCode.Text <> String.Empty Then
                ItemMasterObj.CustomerCode = SearchCustomerCode.Text
                ConditionsGrp1.Add("CustomerCode  LIKE '%'+ @CustomerCode + '%' ")
            End If

            'Query Conditions values
            If SearchCustomerName.Text <> String.Empty Then
                ItemMasterObj.CustomerName = SearchCustomerName.Text
                ConditionsGrp1.Add("CustomerName  LIKE '%'+ @CustomerName + '%' ")
            End If

            'Query Conditions values
            If ConditionsGrp1.Count > 0 Then
                QryConditions.Add(" AND ", ConditionsGrp1)
            End If




            ' INPUT FOR Query Builder
            Dim SQB As New SelectQuery
            SQB._InputTable = ItemMasterObj
            SQB._DB = "Custom"
            SQB._HasInBetweenConditions = False
            If ConditionsGrp1.Count > 0 Then
                SQB._HasWhereConditions = True
            Else
                SQB._HasWhereConditions = False
            End If

            SQB._Conditions = QryConditions
            SQB._OrderBy = " CustomerCode ASC"
            ResultDataTable = CURD.SelectAllData(SQB)
            AppSpecificFunc.BindGridData(ResultDataTable, CustomerMasterListGrid)
            Return ResultDataTable
        Catch ex As Exception
            Return Nothing
        End Try
    End Function




#End Region
#Region "Custom validation - Function"

    Protected Sub ValidateRegistrationDate(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles ValidRegistrationDateRequired.ServerValidate
        Try

            Dim ValidDate As New Date


            If RegistrationDate.Text <> String.Empty Then
                If Date.TryParseExact(RegistrationDate.Text, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, ValidDate) = False Then
                    args.IsValid = False
                End If
            Else
                args.IsValid = False
            End If


        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub


    Private Sub CustomerMasterListGrid_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CustomerMasterListGrid.SelectedIndexChanged
        Try
            If CustomerMasterListGrid.SelectedIndex > -1 Then
                btnAddOrUpdate.Text = "Update"

                Dim CustomerMasterObj As New CustomerMaster
                Dim IdKey As String = CustomerMasterListGrid.DataKeys(CustomerMasterListGrid.SelectedIndex).Value
                CustomerMasterObj.Idkey = IdKey

                'Query Condition builder
                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)


                ''Query Conditions values
                ConditionsGrp1.Add("IdKey=@IdKey")

                'Query Conditions values

                QryConditions.Add(" AND ", ConditionsGrp1)


                ' INPUT FOR Query Builder
                Dim SQB As New SelectQuery
                SQB._InputTable = CustomerMasterObj
                SQB._DB = "Custom"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True

                SQB._Conditions = QryConditions
                Dim ResultDataTable As New DataTable

                ResultDataTable = CURD.SelectAllData(SQB)
                If ResultDataTable.Rows.Count > 0 Then
                    AppSpecificFunc.DataTableToObject(CustomerMasterObj, ResultDataTable)
                    CustomerCode.Text = CustomerMasterObj.CustomerCode
                    CustomerName.Text = CustomerMasterObj.CustomerName
                    RegistrationDate.Text = CustomerMasterObj.RegistrationDate

                    AddressLine1.Text = CustomerMasterObj.AddressLine1
                    AddressLine2.Text = CustomerMasterObj.AddressLine2
                    AddressLine3.Text = CustomerMasterObj.AddressLine3
                    AddressLine4.Text = CustomerMasterObj.AddressLine4
                    AddressLine5.Text = CustomerMasterObj.AddressLine5

                    Dim RegistrationDateValue As New Date
                    If Date.TryParse(RegistrationDate.Text, RegistrationDateValue) Then
                        RegistrationDate.Text = Format(RegistrationDateValue, "dd-MM-yyyy")
                    End If
                    Active.Checked = CBool(CustomerMasterObj.Active)
                End If
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try

    End Sub



#End Region
#Region "Event Handlers - Grid View"
    Private Sub CustomerMasterListGrid_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles CustomerMasterListGrid.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.Cells(3).Text = "True" Then
                    e.Row.Cells(3).Text = "Yes"
                Else
                    e.Row.Cells(3).Text = "No"
                End If
            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
#End Region
End Class