Imports System.Globalization
Imports RebondTrading.Models

Public Class SupMaster
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
                Dim InputObj As New SupplierMaster
                InputObj.SupplierCode = SupplierCode.Text
                InputObj.SupplierName = SupplierName.Text
                InputObj.Active = CBool(Active.Checked)
                Dim ValidDate As New Date
                If Date.TryParseExact(RegistrationDate.Text, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, ValidDate) Then
                    InputObj.RegistrationDate = ValidDate.ToString("yyyy-MM-dd")
                End If


                Dim FilterObj As New SupplierMaster

                If SupplierMasterListGrid.SelectedIndex > -1 Then
                    InputObj.UpdatedBy = User.Identity.Name
                    FilterObj.Idkey = SupplierMasterListGrid.DataKeys(SupplierMasterListGrid.SelectedIndex).Value

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
                        ShowMessage("Supplier already exist")
                    End If
                Else
                    'Dim PrimaryKey As Integer = 0

                    'AppSpecificFunc.GetNextIdentityNo(PrimaryKey, 1, New SupplierMaster)
                    'While (AppSpecificFunc.IsIdentityNoNotAlreadyUsed(PrimaryKey, New SupplierMaster) = False)
                    '    AppSpecificFunc.GetNextIdentityNo(PrimaryKey, 1, New SupplierMaster)
                    'End While
                    'InputObj.Idkey = PrimaryKey
                    InputObj.Idkey = Nothing
                    InputObj.CreatedBy = User.Identity.Name
                    If (CURD.InsertData(InputObj, True)) Then
                        ShowMessage("Added Successfully")
                        ClearForm()
                    Else
                        ShowMessage("Supplier already exist")
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
            SupplierMasterListGrid.SelectedIndex = -1
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub

    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        SupplierMasterListGrid.PageIndex = e.NewPageIndex
        LoadData()
    End Sub


#End Region
#Region "Form Related functions"

    Protected Sub ClearSearchForm()
        Try
            SupplierCode.Text = String.Empty
            SupplierName.Text = String.Empty
            LoadData()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub ClearForm()
        Try
            SupplierCode.Text = String.Empty
            SupplierName.Text = String.Empty
            RegistrationDate.Text = String.Empty
            Active.Checked = True
            btnAddOrUpdate.Text = "Add"
            SupplierMasterListGrid.SelectedIndex = -1
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Function LoadData() As DataTable
        Dim ResultDataTable As New DataTable
        Try
            Dim ItemMasterObj As New SupplierMaster
            Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))


            'Query Condition Groups
            Dim ConditionsGrp1 As List(Of String) = New List(Of String)


            'Query Conditions values
            If SearchSupplierCode.Text <> String.Empty Then
                ItemMasterObj.SupplierCode = SearchSupplierCode.Text
                ConditionsGrp1.Add("SupplierCode  LIKE '%'+ @SupplierCode + '%' ")
            End If

            'Query Conditions values
            If SearchSupplierName.Text <> String.Empty Then
                ItemMasterObj.SupplierName = SearchSupplierName.Text
                ConditionsGrp1.Add("SupplierName  LIKE '%'+ @SupplierName + '%' ")
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
            SQB._OrderBy = " SupplierCode ASC"
            ResultDataTable = CURD.SelectAllData(SQB)
            AppSpecificFunc.BindGridData(ResultDataTable, SupplierMasterListGrid)
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


    Private Sub SupplierMasterListGrid_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SupplierMasterListGrid.SelectedIndexChanged
        Try
            If SupplierMasterListGrid.SelectedIndex > -1 Then
                btnAddOrUpdate.Text = "Update"

                Dim SupplierMasterObj As New SupplierMaster
                Dim IdKey As String = SupplierMasterListGrid.DataKeys(SupplierMasterListGrid.SelectedIndex).Value
                SupplierMasterObj.Idkey = IdKey

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
                SQB._InputTable = SupplierMasterObj
                SQB._DB = "Custom"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True

                SQB._Conditions = QryConditions
                Dim ResultDataTable As New DataTable

                ResultDataTable = CURD.SelectAllData(SQB)
                If ResultDataTable.Rows.Count > 0 Then
                    AppSpecificFunc.DataTableToObject(SupplierMasterObj, ResultDataTable)
                    SupplierCode.Text = SupplierMasterObj.SupplierCode
                    SupplierName.Text = SupplierMasterObj.SupplierName
                    RegistrationDate.Text = SupplierMasterObj.RegistrationDate
                    Dim RegistrationDateValue As New Date
                    If Date.TryParse(RegistrationDate.Text, RegistrationDateValue) Then
                        RegistrationDate.Text = Format(RegistrationDateValue, "dd-MM-yyyy")
                    End If
                    Active.Checked = CBool(SupplierMasterObj.Active)
                End If
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try

    End Sub



#End Region
#Region "Event Handlers - Grid View"
    Private Sub SupplierMasterListGrid_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles SupplierMasterListGrid.RowDataBound
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