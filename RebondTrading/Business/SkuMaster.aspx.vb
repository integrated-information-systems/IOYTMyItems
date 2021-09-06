Imports RebondTrading.Models
Imports System.IO
Public Class SkuMaster
    Inherits System.Web.UI.Page
#Region "Form Event Handlers"
    Private Sub SkuMaster_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            BindItemCategory()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
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
                Dim InputObj As New ItemMaster
                InputObj.ItemCode = ItemCode.Text
                InputObj.ItemDescription = ItemDescription.Text
                InputObj.UOM = UOM.Text
                InputObj.Active = CBool(Active.Checked)
                InputObj.ParLevel = ParLevel.Text
                InputObj.Category = Category.SelectedItem.Value

                Dim File1SavePath As String = String.Empty
                Dim File2SavePath As String = String.Empty
                Dim File3SavePath As String = String.Empty

                If FileAttachment1.HasFile Then
                    Dim ext As String = System.IO.Path.GetExtension(FileAttachment1.PostedFile.FileName)
                    File1SavePath = Server.MapPath("~") & "\" & System.Configuration.ConfigurationManager.AppSettings("UploadPath").ToString & "\" & ItemCode.Text & "_Image1_" & DateAndTime.Now.Ticks & ext
                    Dim file1Name As String = Path.GetFileName(File1SavePath)
                    FileAttachment1.PostedFile.SaveAs(File1SavePath)
                    InputObj.Image1 = file1Name

                Else
                    InputObj.Image1 = Nothing
                End If
                If FileAttachment2.HasFile Then
                    Dim ext As String = System.IO.Path.GetExtension(FileAttachment2.PostedFile.FileName)
                    File2SavePath = Server.MapPath("~") & "\" & System.Configuration.ConfigurationManager.AppSettings("UploadPath").ToString & "\" & ItemCode.Text & "_Image2_" & DateAndTime.Now.Ticks & "." & ext
                    Dim file2Name As String = Path.GetFileName(File2SavePath)
                    FileAttachment2.PostedFile.SaveAs(File2SavePath)
                    InputObj.Image2 = file2Name
                Else
                    InputObj.Image2 = Nothing
                End If
                If FileAttachment3.HasFile Then
                    Dim ext As String = System.IO.Path.GetExtension(FileAttachment3.PostedFile.FileName)
                    File3SavePath = Server.MapPath("~") & "\" & System.Configuration.ConfigurationManager.AppSettings("UploadPath").ToString & "\" & ItemCode.Text & "_Image2_" & DateAndTime.Now.Ticks & "." & ext
                    Dim file3Name As String = Path.GetFileName(File3SavePath)
                    FileAttachment3.PostedFile.SaveAs(File3SavePath)
                    InputObj.Image3 = file3Name
                Else
                    InputObj.Image3 = Nothing
                End If

                Dim FilterObj As New ItemMaster

                If ItemMasterListGrid.SelectedIndex > -1 Then
                    InputObj.UpdatedBy = User.Identity.Name
                    FilterObj.Idkey = ItemMasterListGrid.DataKeys(ItemMasterListGrid.SelectedIndex).Value

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
                        ShowMessage("Item Code already exist")
                    End If
                Else
                    'Dim PrimaryKey As Integer = 0

                    'AppSpecificFunc.GetNextIdentityNo(PrimaryKey, 1, New ItemMaster)
                    'While (AppSpecificFunc.IsIdentityNoNotAlreadyUsed(PrimaryKey, New ItemMaster) = False)
                    '    AppSpecificFunc.GetNextIdentityNo(PrimaryKey, 1, New ItemMaster)
                    'End While
                    'InputObj.Idkey = PrimaryKey

                    InputObj.Idkey = Nothing

                    InputObj.CreatedBy = User.Identity.Name
                    If (CURD.InsertData(InputObj, True)) Then
                        ShowMessage("Added Successfully")
                        ClearForm()
                    Else
                        ShowMessage("Item Code already exist")
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
            ItemMasterListGrid.SelectedIndex = -1
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub

#End Region
#Region "Form Related functions"
    Protected Sub BindItemCategory()
        Try
            Dim ItemCategoryDT As DataTable = AppSpecificFunc.GetItemCategories()

            Category.DataSource = ItemCategoryDT
            Category.DataTextField = "Category"
            Category.DataValueField = "Idkey"
            Category.DataBind()
            Category.Items.Insert(0, New ListItem("Select", "-1"))


        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub ClearSearchForm()
        Try
            ItemCode.Text = String.Empty
            ItemDescription.Text = String.Empty

            LoadData()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub ClearForm()
        Try
            ItemCode.Text = String.Empty
            ItemDescription.Text = String.Empty
            UOM.Text = String.Empty
            Active.Checked = True
            ParLevel.Text = "0"
            btnAddOrUpdate.Text = "Add"
            ItemMasterListGrid.SelectedIndex = -1
            Category.SelectedIndex = 0
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    'Protected Function LoadData() As DataTable
    '    Dim ResultDataTable As New DataTable
    '    Try
    '        Dim ItemMasterObj As New ItemMaster
    '        Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))


    '        'Query Condition Groups
    '        Dim ConditionsGrp1 As List(Of String) = New List(Of String)


    '        'Query Conditions values
    '        If SearchItemCode.Text <> String.Empty Then
    '            ItemMasterObj.ItemCode = SearchItemCode.Text
    '            ConditionsGrp1.Add("ItemCode  LIKE '%'+ @ItemCode + '%' ")
    '        End If


    '        'Query Conditions values
    '        If ConditionsGrp1.Count > 0 Then
    '            QryConditions.Add(" AND ", ConditionsGrp1)
    '        End If




    '        ' INPUT FOR Query Builder
    '        Dim SQB As New SelectQuery
    '        SQB._InputTable = ItemMasterObj
    '        SQB._DB = "Custom"
    '        SQB._HasInBetweenConditions = False
    '        If ConditionsGrp1.Count > 0 Then
    '            SQB._HasWhereConditions = True
    '        Else
    '            SQB._HasWhereConditions = False
    '        End If

    '        SQB._Conditions = QryConditions
    '        SQB._OrderBy = " ItemCode ASC"
    '        ResultDataTable = CURD.SelectAllData(SQB)
    '        AppSpecificFunc.BindGridData(ResultDataTable, ItemMasterListGrid)
    '        Return ResultDataTable
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function

    Protected Function LoadData() As DataTable
        Dim ResultDataTable As New DataTable
        Try
            Dim CQ As New CustomQuery
            CQ._DB = "Custom"
            Dim CustomQueryParameters As New Dictionary(Of String, String)
            Dim InputQuery1 As String = String.Empty
            InputQuery1 = "Select T1.*, ISNULL(T3.ItemBalance,0)  as 'ItemBalance' from ItemMaster T1 " &
                "  OUTER APPLY(SELECT Sum(StockInOut) as 'ItemBalance' FROM StockMovement " &
                "  WHERE T1.ItemCode=StockMovement.ItemCode) AS T3"
            Dim Conditionlist1 As New List(Of String)


            Conditionlist1.Add(" T1.ItemCode  LIKE '%'+ @ItemCode + '%' ")
            CustomQueryParameters.Add("@ItemCode", SearchItemCode.Text)


            Conditionlist1.Add(" T1.ItemDescription  LIKE '%'+ @ItemDescription + '%' ")
            CustomQueryParameters.Add("@ItemDescription", SearchItemDescription.Text)

            If Conditionlist1.Count > 0 Then
                Dim CondiString1 As String = String.Join(" AND ", Conditionlist1)

                InputQuery1 = InputQuery1 & " WHERE " & CondiString1
            End If

            '**********************Query Builder Function *****************
            CQ._InputQuery = InputQuery1 & " Order By T1.ItemCode  ASC"
            CQ._Parameters = CustomQueryParameters

            ResultDataTable = CURD.CustomQueryGetData(CQ)
            AppSpecificFunc.BindGridData(ResultDataTable, ItemMasterListGrid)
            Return ResultDataTable

        Catch ex As Exception
            Return Nothing
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Function


#End Region
#Region "Event Handlers - Grid View"
    Private Sub SkuMasterListGrid_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles ItemMasterListGrid.PageIndexChanging
        Try
            ItemMasterListGrid.PageIndex = e.NewPageIndex
            ClearForm()
            LoadData()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub

    Private Sub SkuMasterListGrid_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ItemMasterListGrid.SelectedIndexChanged
        Try
            If ItemMasterListGrid.SelectedIndex > -1 Then
                btnAddOrUpdate.Text = "Update"

                Dim ItemMasterObj As New ItemMaster
                Dim IdKey As String = ItemMasterListGrid.DataKeys(ItemMasterListGrid.SelectedIndex).Value
                ItemMasterObj.Idkey = IdKey

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
                SQB._InputTable = ItemMasterObj
                SQB._DB = "Custom"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True

                SQB._Conditions = QryConditions
                Dim ResultDataTable As New DataTable

                ResultDataTable = CURD.SelectAllData(SQB)
                If ResultDataTable.Rows.Count > 0 Then
                    AppSpecificFunc.DataTableToObject(ItemMasterObj, ResultDataTable)
                    ItemCode.Text = ItemMasterObj.ItemCode
                    ItemDescription.Text = ItemMasterObj.ItemDescription
                    UOM.Text = ItemMasterObj.UOM
                    ParLevel.Text = ItemMasterObj.ParLevel
                    Category.SelectedIndex = Category.Items.IndexOf(Category.Items.FindByValue(ItemMasterObj.Category))
                    Active.Checked = CBool(ItemMasterObj.Active)

                    Dim file1Path As String = Server.MapPath("~") & "\" & System.Configuration.ConfigurationManager.AppSettings("UploadPath").ToString & "\"
                    Dim file1name As String = ItemMasterObj.Image1
                    file1Path = file1Path & file1name
                    Dim BaseURL As String = "http://sapb1viewer.xyz:81/MyItems/Business/"
                    If File.Exists(file1Path) Then
                        Image1.ImageUrl = BaseURL & "LoadImage.aspx?FileName=" & ItemMasterObj.Image1
                    Else
                        Image1.ImageUrl = BaseURL & "LoadImage.aspx?FileName=NoImg.png"
                    End If

                    Dim file2Path As String = Server.MapPath("~") & "\" & System.Configuration.ConfigurationManager.AppSettings("UploadPath").ToString & "\"
                    Dim file2name As String = ItemMasterObj.Image2
                    file2Path = file2Path & file2name
                    If File.Exists(file2Path) Then
                        Image2.ImageUrl = BaseURL & "LoadImage.aspx?FileName=" & ItemMasterObj.Image2
                    Else
                        Image2.ImageUrl = BaseURL & "LoadImage.aspx?FileName=NoImg.png"
                    End If

                    Dim file3Path As String = Server.MapPath("~") & "\" & System.Configuration.ConfigurationManager.AppSettings("UploadPath").ToString & "\"
                    Dim file3name As String = ItemMasterObj.Image3
                    file3Path = file3Path & file2name
                    If File.Exists(file3Path) Then
                        Image3.ImageUrl = BaseURL & "LoadImage.aspx?FileName=" & ItemMasterObj.Image3
                    Else
                        Image3.ImageUrl = BaseURL & "LoadImage.aspx?FileName=NoImg.png"
                    End If

                End If
                End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Private Sub ItemMasterListGrid_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles ItemMasterListGrid.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.Cells(4).Text = "True" Then
                    e.Row.Cells(4).Text = "Yes"
                Else
                    e.Row.Cells(4).Text = "No"
                End If
            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
#End Region
#Region "Custom validation - Function"
    Protected Sub ValidateCategory(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs)
        Try
            If Category.SelectedIndex <= 0 Then
                args.IsValid = False
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub ValidateParLevel(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles ParLevelValidator.ServerValidate
        Try

            'Dim ValidDate As New Date


            'If DocFromDate.Text <> String.Empty Then
            '    If Date.TryParseExact(DocFromDate.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, ValidDate) = False Then
            '        args.IsValid = False
            '    End If
            'Else
            '    args.IsValid = True
            'End If

            If Not IsNumeric(ParLevel.Text) Then
                args.IsValid = False
            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub

    Protected Sub ValidateFileAttachment1(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles FileAttachment1Validator.ServerValidate
        Try

            If FileAttachment1.HasFile Then
                Dim fileExtension As String = Path.GetExtension(FileAttachment1.FileName)
                Dim validExtension As List(Of String) = New List(Of String) From {".BMP", ".PNG", ".JPG", ".JPEG"}
                Dim intFileLength As Integer = Me.FileAttachment1.PostedFile.ContentLength
                If Not validExtension.Contains(fileExtension.ToUpper) Then
                    args.IsValid = False
                    FileAttachment1Validator.ErrorMessage = "Invalid File type found(Allowed only *.bmp, *.png, *.jpg, *.jpeg) - Image 1"
                ElseIf intFileLength > 5242880 Then
                    args.IsValid = False
                    FileAttachment1Validator.ErrorMessage = "Maximum size 5(MB) exceeded - Image 1"
                End If

            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub


#End Region
End Class