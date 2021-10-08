Imports System.Globalization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports RebondTrading.Models

Public Class GoodsIssue
    Inherits System.Web.UI.Page

#Region "Form Event Handlers"
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            If Page.IsValid Then
                LoadItemsFromDataBase()
            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                Session("IsPostBack") = Server.UrlEncode(DateTime.Now).ToString
                InitialiseForm()
            Else
                Dim DeliveryPlannerLinesDataTable As DataTable = TryCast(ViewState("DeliveryPlannerLines"), DataTable)
                If Not IsNothing(DeliveryPlannerLinesDataTable) Then
                    If DeliveryPlannerLinesDataTable.Rows.Count <= 0 Then
                        AppSpecificFunc.GridNoDataFound(GoodsIssueMasterGrid)
                    End If
                End If
            End If


        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub

    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GoodsIssueMasterGrid.PageIndex = e.NewPageIndex
        LoadItemsFromDataBase()
    End Sub


#End Region
#Region "Form Related functions"
    Protected Sub InitialiseForm()
        Try
            FilterDate.Text = Format(CDate(DateTime.Now), "dd-MM-yyyy")
            LoadItemsFromDataBase()


        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Private Function LoadItemsFromDataBase() As DataTable
        Try
            Dim ResultDataTable As New DataTable


            Dim CQ As New CustomQuery

            CQ._DB = "Custom"
            Dim CustomQueryParameters As New Dictionary(Of String, String)
            Dim InputQuery1 As String = String.Empty
            InputQuery1 = " Select T1.*  from GoodsIssueItems T1 "
            Dim Conditionlist1 As New List(Of String)

            Dim ValidDate As New Date
            If Date.TryParseExact(FilterDate.Text, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, ValidDate) = True Then

            End If
            If FilterDate.Text.ToString <> String.Empty Then
                Conditionlist1.Add(" ( T1.IssuedDate>=@FromFilterDate AND T1.IssuedDate<=@ToFilterDate ) ")
                CustomQueryParameters.Add("@FromFilterDate", ValidDate.ToString("yyyy-MM-dd") & " 00:00:00")
                CustomQueryParameters.Add("@ToFilterDate", ValidDate.ToString("yyyy-MM-dd") & " 23:59:59")

            End If



            If Conditionlist1.Count > 0 Then
                Dim CondiString1 As String = String.Join(" AND ", Conditionlist1)

                InputQuery1 = InputQuery1 & " WHERE " & CondiString1
            End If

            '**********************Query Builder Function *****************
            CQ._InputQuery = InputQuery1 & " Order By Idkey  ASC"
            CQ._Parameters = CustomQueryParameters

            ResultDataTable = CURD.CustomQueryGetData(CQ)

            If Not IsNothing(ResultDataTable) Then
                ViewState("DeliveryPlannerLines") = ResultDataTable.Copy()
                AppSpecificFunc.BindGridData(ResultDataTable, GoodsIssueMasterGrid)
            End If
            Return ResultDataTable
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return Nothing
        End Try
    End Function
#End Region
#Region "Custom validation - Function"

    Protected Sub ValidateFilterDate(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles ValidFilterDateRequired.ServerValidate
        Try

            Dim ValidDate As New Date


            If FilterDate.Text <> String.Empty Then
                If Date.TryParseExact(FilterDate.Text, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, ValidDate) = False Then
                    args.IsValid = False
                End If
            Else
                args.IsValid = False
            End If


        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub

    Private Sub GoodsIssueMasterGrid_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GoodsIssueMasterGrid.RowCommand
        Try
            If e.CommandName = "Preview" Then
                Dim doc As New ReportDocument()

                Dim ReportFileName As String = String.Empty

                ReportFileName = Server.MapPath("~\RptFiles\DO.rpt")


                'Response.Write(ReportFileName)
                doc.Load(ReportFileName)
                doc.SetDatabaseLogon(ConfigurationManager.AppSettings("DB_Username"), ConfigurationManager.AppSettings("DB_Password"), ConfigurationManager.AppSettings("DB_Server"), ConfigurationManager.AppSettings("Company_DB"))
                doc.SetParameterValue("@idkey", e.CommandArgument)

                Dim exportOpts As ExportOptions = doc.ExportOptions

                exportOpts.ExportFormatType = ExportFormatType.PortableDocFormat

                exportOpts.ExportDestinationType = ExportDestinationType.DiskFile

                exportOpts.DestinationOptions = New DiskFileDestinationOptions()

                Dim diskOpts As New DiskFileDestinationOptions()

                Dim origin As New DateTime(1970, 1, 1, 0, 0, 0, 0)
                Dim Diff As New TimeSpan
                Diff = Now - origin
                Dim FileNameGenerated As String = Math.Floor(Diff.TotalSeconds)
                FileNameGenerated = "DO" & "_" & FileNameGenerated & ".pdf"
                'response.write(Server.MapPath("~/Reports/" & FileNameGenerated))
                CType(doc.ExportOptions.DestinationOptions, DiskFileDestinationOptions).DiskFileName = Server.MapPath("~/Reports/" & FileNameGenerated)
                'CType(doc.ExportOptions.DestinationOptions, DiskFileDestinationOptions).DiskFileName = "C:\inetpub\wwwroot\EarthScape\Reports\" + FileNameGenerated
                'export the report to PDF rather than displaying the report in a viewer

                doc.Export()

                Response.Clear()
                Response.AddHeader("content-disposition", "attachment; filename=" & FileNameGenerated)
                Response.WriteFile(Server.MapPath("~/Reports/" & FileNameGenerated))
                Response.ContentType = ""
                Response.End()

            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub





#End Region
End Class