Imports RebondTrading.Models

Public Class StockMovementSummaryByBatchReport
    Inherits System.Web.UI.Page

#Region "Helper functions- Page Methods"
    <System.Web.Script.Services.ScriptMethod(),
System.Web.Services.WebMethod()>
    Public Shared Function GetSkus(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Try

            Dim DocumentNos As List(Of String) = New List(Of String)
            Dim ResultDataTable As DataTable

            ResultDataTable = AppSpecificFunc.GetItemCodes(prefixText)
            If ResultDataTable.Rows.Count > 0 Then
                AppSpecificFunc.GetStringListFromDataTable(ResultDataTable, "ItemCode", DocumentNos)
            End If
            Return DocumentNos
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return New List(Of String)
        End Try
    End Function
    <System.Web.Script.Services.ScriptMethod(),
System.Web.Services.WebMethod()>
    Public Shared Function GetLocations(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Try

            Dim DocumentNos As List(Of String) = New List(Of String)
            Dim ResultDataTable As DataTable

            ResultDataTable = AppSpecificFunc.GetLocationCodes(prefixText)
            If ResultDataTable.Rows.Count > 0 Then
                AppSpecificFunc.GetStringListFromDataTable(ResultDataTable, "Location", DocumentNos)
            End If
            Return DocumentNos
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return New List(Of String)
        End Try
    End Function
    <System.Web.Script.Services.ScriptMethod(),
System.Web.Services.WebMethod()>
    Public Shared Function GetBatchNos(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Try

            Dim DocumentNos As List(Of String) = New List(Of String)
            Dim ResultDataTable As DataTable

            ResultDataTable = AppSpecificFunc.GetBatchNos(prefixText)
            If ResultDataTable.Rows.Count > 0 Then
                AppSpecificFunc.GetStringListFromDataTable(ResultDataTable, "BatchNo", DocumentNos)
            End If
            Return DocumentNos
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return New List(Of String)
        End Try
    End Function
#End Region
#Region "Form Event Handlers"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub StockMoveRptforCarton_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            Me.CrystalReportViewer1.PDFOneClickPrinting = False

            'If IsPostBack Then
            'If Not IsNothing(Session("From_Date")) And Not IsNothing(Session("To_Date")) Then
            Dim ReportFileName As String = Server.MapPath("~\RptFiles\ItemBalanceBatch.rpt")



            Dim crReportDocument As New CrystalDecisions.Web.CrystalReportSource

            crReportDocument.Report.FileName = ReportFileName
            crReportDocument.ReportDocument.FileName = ReportFileName


            If Not IsNothing(Session("From_Date")) Then
                crReportDocument.ReportDocument.SetParameterValue("@From_Date", Session("From_Date").ToString)
            Else
                crReportDocument.ReportDocument.SetParameterValue("@From_Date", Nothing)
            End If

            If Not IsNothing(Session("To_Date")) Then
                crReportDocument.ReportDocument.SetParameterValue("@To_Date", Session("To_Date").ToString)
            Else
                crReportDocument.ReportDocument.SetParameterValue("@To_Date", Nothing)
            End If


            If Not IsNothing(Session("From_Loc")) Then
                crReportDocument.ReportDocument.SetParameterValue("@From_Loc", Session("From_Loc").ToString)
            Else
                crReportDocument.ReportDocument.SetParameterValue("@From_Loc", Nothing)
            End If

            If Not IsNothing(Session("To_Loc")) Then
                crReportDocument.ReportDocument.SetParameterValue("@To_Loc", Session("To_Loc").ToString)
            Else
                crReportDocument.ReportDocument.SetParameterValue("@To_Loc", Nothing)
            End If

            If Not IsNothing(Session("From_SKU")) Then
                crReportDocument.ReportDocument.SetParameterValue("@From_SKU", Session("From_SKU").ToString)
            Else
                crReportDocument.ReportDocument.SetParameterValue("@From_SKU", Nothing)
            End If

            If Not IsNothing(Session("To_SKU")) Then
                crReportDocument.ReportDocument.SetParameterValue("@To_SKU", Session("To_SKU").ToString)
            Else
                crReportDocument.ReportDocument.SetParameterValue("@To_SKU", Nothing)
            End If

            If Not IsNothing(Session("BatchNo")) Then
                crReportDocument.ReportDocument.SetParameterValue("@BatchNo", Session("BatchNo").ToString)
            Else
                crReportDocument.ReportDocument.SetParameterValue("@BatchNo", Nothing)
            End If

            Me.CrystalReportViewer1.ReportSource = crReportDocument
            Me.CrystalReportViewer1.EnableParameterPrompt = False
            Me.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None



            Me.CrystalReportViewer1.LogOnInfo(0).ConnectionInfo.ServerName = ConfigurationManager.AppSettings("DB_Server")
            Me.CrystalReportViewer1.LogOnInfo(0).ConnectionInfo.UserID = ConfigurationManager.AppSettings("DB_Username")
            Me.CrystalReportViewer1.LogOnInfo(0).ConnectionInfo.Password = ConfigurationManager.AppSettings("DB_Password")
            Me.CrystalReportViewer1.EnableDatabaseLogonPrompt = False

            'End If

            'End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            If Page.IsValid Then
                Dim ReportFileName As String = Server.MapPath("~\RptFiles\ItemBalanceBatch.rpt")



                Dim crReportDocument As New CrystalDecisions.Web.CrystalReportSource

                crReportDocument.Report.FileName = ReportFileName
                crReportDocument.ReportDocument.FileName = ReportFileName

                'If DocFromDate.Text <> String.Empty Then
                '    crReportDocument.ReportDocument.SetParameterValue("@From_Date", DocFromDate.Text.Trim)
                'Else
                crReportDocument.ReportDocument.SetParameterValue("@From_Date", Nothing)
                '    Session("From_Date") = DocFromDate.Text.Trim
                'End If

                'If DocToDate.Text <> String.Empty Then
                '    crReportDocument.ReportDocument.SetParameterValue("@To_Date", DocToDate.Text.Trim)
                'Else
                crReportDocument.ReportDocument.SetParameterValue("@To_Date", Nothing)
                '    Session("To_Date") = DocToDate.Text.Trim
                'End If

                If LocationFrom.Text <> String.Empty Then
                    crReportDocument.ReportDocument.SetParameterValue("@From_Loc", LocationFrom.Text.Trim)
                Else
                    crReportDocument.ReportDocument.SetParameterValue("@From_Loc", Nothing)
                    Session("From_Loc") = LocationFrom.Text.Trim
                End If

                If LocationTo.Text <> String.Empty Then
                    crReportDocument.ReportDocument.SetParameterValue("@To_Loc", LocationTo.Text.Trim)
                Else
                    crReportDocument.ReportDocument.SetParameterValue("@To_Loc", Nothing)
                    Session("To_Loc") = LocationTo.Text.Trim
                End If

                If SkuFrom.Text <> String.Empty Then
                    crReportDocument.ReportDocument.SetParameterValue("@From_SKU", SkuFrom.Text.Trim)
                Else
                    crReportDocument.ReportDocument.SetParameterValue("@From_SKU", Nothing)
                    Session("From_SKU") = SkuFrom.Text.Trim
                End If

                If SkuTo.Text <> String.Empty Then
                    crReportDocument.ReportDocument.SetParameterValue("@To_SKU", SkuTo.Text.Trim)
                Else
                    crReportDocument.ReportDocument.SetParameterValue("@To_SKU", Nothing)
                    Session("From_SKU") = SkuTo.Text.Trim
                End If



                If BatchNo.Text <> String.Empty Then
                    crReportDocument.ReportDocument.SetParameterValue("@BatchNo", BatchNo.Text.Trim)
                Else
                    crReportDocument.ReportDocument.SetParameterValue("@BatchNo", Nothing)
                    Session("BatchNo") = SkuTo.Text.Trim
                End If





                Me.CrystalReportViewer1.ReportSource = crReportDocument
                Me.CrystalReportViewer1.EnableParameterPrompt = False
                Me.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None



                Me.CrystalReportViewer1.LogOnInfo(0).ConnectionInfo.ServerName = ConfigurationManager.AppSettings("DB_Server")
                Me.CrystalReportViewer1.LogOnInfo(0).ConnectionInfo.UserID = ConfigurationManager.AppSettings("DB_Username")
                Me.CrystalReportViewer1.LogOnInfo(0).ConnectionInfo.Password = ConfigurationManager.AppSettings("DB_Password")
                Me.CrystalReportViewer1.EnableDatabaseLogonPrompt = False
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try

            'DocToDate.Text = String.Empty
            'DocFromDate.Text = String.Empty
            LocationFrom.Text = String.Empty
            LocationTo.Text = String.Empty
            SkuFrom.Text = String.Empty
            SkuTo.Text = String.Empty


        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub

#End Region

End Class