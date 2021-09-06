Imports RebondTrading.Models

Public Class ItmCategoryMaster
    Inherits System.Web.UI.Page

#Region "Form Event Handlers"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack() Then
                ClearForm()
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
                Dim InputObj As New ItemCategoryMaster
                InputObj.Category = Category.Text
                InputObj.Active = CBool(Active.Checked)

                Dim FilterObj As New ItemCategoryMaster

                If CategoryMasterListGrid.SelectedIndex > -1 Then
                    InputObj.UpdatedBy = User.Identity.Name
                    FilterObj.Idkey = CategoryMasterListGrid.DataKeys(CategoryMasterListGrid.SelectedIndex).Value

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
                        ShowMessage("Category already exist")
                    End If
                Else

                    'Dim PrimaryKey As Integer = 0

                    'AppSpecificFunc.GetNextIdentityNo(PrimaryKey, 1, New ItemCategoryMaster)
                    'While (AppSpecificFunc.IsIdentityNoNotAlreadyUsed(PrimaryKey, New ItemCategoryMaster) = False)
                    '    AppSpecificFunc.GetNextIdentityNo(PrimaryKey, 1, New ItemCategoryMaster)
                    'End While
                    'InputObj.Idkey = PrimaryKey

                    InputObj.Idkey = Nothing
                    InputObj.CreatedBy = User.Identity.Name
                    If (CURD.InsertData(InputObj, True)) Then
                        ShowMessage("Added Successfully")
                        ClearForm()
                    Else
                        ShowMessage("Category already exist")
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
            CategoryMasterListGrid.SelectedIndex = -1
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub

#End Region
#Region "Form Related functions"

    Protected Sub ClearSearchForm()
        Try
            Category.Text = String.Empty
            LoadData()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub ClearForm()
        Try
            Category.Text = String.Empty

            Active.Checked = True
            btnAddOrUpdate.Text = "Add"
            CategoryMasterListGrid.SelectedIndex = -1

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Function LoadData() As DataTable
        Dim ResultDataTable As New DataTable
        Try
            Dim CategoryMasterObj As New ItemCategoryMaster
            Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))


            'Query Condition Groups
            Dim ConditionsGrp1 As List(Of String) = New List(Of String)


            'Query Conditions values
            If SearchCategory.Text <> String.Empty Then
                CategoryMasterObj.Category = SearchCategory.Text
                ConditionsGrp1.Add("Category  LIKE '%'+ @Category + '%' ")
            End If


            'Query Conditions values
            If ConditionsGrp1.Count > 0 Then
                QryConditions.Add(" AND ", ConditionsGrp1)
            End If




            ' INPUT FOR Query Builder
            Dim SQB As New SelectQuery
            SQB._InputTable = CategoryMasterObj
            SQB._DB = "Custom"
            SQB._HasInBetweenConditions = False
            If ConditionsGrp1.Count > 0 Then
                SQB._HasWhereConditions = True
            Else
                SQB._HasWhereConditions = False
            End If

            SQB._Conditions = QryConditions
            SQB._OrderBy = " Category ASC"
            ResultDataTable = CURD.SelectAllData(SQB)
            AppSpecificFunc.BindGridData(ResultDataTable, CategoryMasterListGrid)
            Return ResultDataTable
        Catch ex As Exception
            Return Nothing
        End Try
    End Function




#End Region
#Region "Event Handlers - Grid View"
    Private Sub SkuMasterListGrid_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles CategoryMasterListGrid.PageIndexChanging
        Try
            CategoryMasterListGrid.PageIndex = e.NewPageIndex
            ClearForm()
            LoadData()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub

    Private Sub CategoryMasterListGrid_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CategoryMasterListGrid.SelectedIndexChanged
        Try
            If CategoryMasterListGrid.SelectedIndex > -1 Then
                btnAddOrUpdate.Text = "Update"

                Dim CategoryMasterObj As New ItemCategoryMaster
                Dim IdKey As String = CategoryMasterListGrid.DataKeys(CategoryMasterListGrid.SelectedIndex).Value
                CategoryMasterObj.Idkey = IdKey

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
                SQB._InputTable = CategoryMasterObj
                SQB._DB = "Custom"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True

                SQB._Conditions = QryConditions
                Dim ResultDataTable As New DataTable

                ResultDataTable = CURD.SelectAllData(SQB)
                If ResultDataTable.Rows.Count > 0 Then
                    AppSpecificFunc.DataTableToObject(CategoryMasterObj, ResultDataTable)
                    Category.Text = CategoryMasterObj.Category

                    Active.Checked = CBool(CategoryMasterObj.Active)
                End If
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Private Sub CategoryMasterListGrid_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles CategoryMasterListGrid.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.Cells(1).Text = "True" Then
                    e.Row.Cells(1).Text = "Yes"
                Else
                    e.Row.Cells(1).Text = "No"
                End If

            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
#End Region

End Class