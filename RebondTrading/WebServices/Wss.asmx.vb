Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports RebondTrading.Models
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Globalization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Web.Script.Serialization
Imports System.Web.Script.Services

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
<ScriptService>
Public Class Wss
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function JsonLogin(LoginDetails As MobLogin) As String
        Dim Result As New PostResults
        Dim JS As New JavaScriptSerializer
        Result.IsSuccess = False
        Try
            Dim Authenticated As Boolean = False
            Result.IsSuccess = Membership.ValidateUser(LoginDetails.UserName, LoginDetails.Password)

            Return JS.Serialize(Result).ToString
        Catch ex As Exception
            Return JS.Serialize(Result).ToString
        End Try
    End Function

    <WebMethod()>
    Public Function GetLocationsList() As String
        Dim JS As New JavaScriptSerializer
        Dim lines As MobLocation() = New MobLocation() {}
        Try


            AppSpecificFunc.GetActiveAllMobileLocations(lines)
            Return JS.Serialize(lines).ToString
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return JS.Serialize(lines).ToString
        End Try
    End Function
    <WebMethod()>
    Public Function GetItemsList() As String
        Dim JS As New JavaScriptSerializer
        Dim lines As MobItem() = New MobItem() {}
        Try
            AppSpecificFunc.GetActiveAllMobileItems(lines)
            Return JS.Serialize(lines).ToString
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return JS.Serialize(lines).ToString
        End Try
    End Function
    <WebMethod()>
    Public Function GetCustmersList() As String
        Dim JS As New JavaScriptSerializer
        Dim lines As MobCustomer() = New MobCustomer() {}
        Try
            AppSpecificFunc.GetActiveAllMobileCustomers(lines)
            Return JS.Serialize(lines).ToString
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return JS.Serialize(lines).ToString
        End Try
    End Function
    <WebMethod()>
    Public Function GetVendorsList() As String
        Dim JS As New JavaScriptSerializer
        Dim lines As MobVendor() = New MobVendor() {}
        Try
            AppSpecificFunc.GetActiveAllMobileVendors(lines)
            Return JS.Serialize(lines).ToString
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return JS.Serialize(lines).ToString
        End Try
    End Function
    <WebMethod()>
    Public Function FindItemCodeByBatchNoInGR(ByVal BatchNo As String) As MobGR

        Dim GR As New MobGR

        Try
            Dim ResultDataTable As New DataTable


            Dim CQ As New CustomQuery

            CQ._DB = "Custom"
            Dim CustomQueryParameters As New Dictionary(Of String, String)
            Dim InputQuery1 As String = String.Empty
            'InputQuery1 = " Select Top 1 *  From GoodsReceiptItems T1 "
            InputQuery1 = " SELECT TOP 1 SUM(QTY) as 'Qty', [ItemCode],[BatchNo],[UOM] ,[Location] FROM  ( "
            InputQuery1 = InputQuery1 & " SELECT [ItemCode],[BatchNo],[Qty],[UOM] ,[Location]  FROM GoodsReceiptItems "
            InputQuery1 = InputQuery1 & " UNION ALL "
            InputQuery1 = InputQuery1 & " SELECT [ItemCode],[BatchNo],[Qty]*-1,[UOM] ,[Location]  FROM GoodsIssueItems "
            InputQuery1 = InputQuery1 & " ) AS Table1  "

            Dim Conditionlist1 As New List(Of String)

            Conditionlist1.Add("BatchNo=@BatchNo")
            CustomQueryParameters.Add("@BatchNo", BatchNo)

            If Conditionlist1.Count > 0 Then
                Dim CondiString1 As String = String.Join(" AND ", Conditionlist1)

                InputQuery1 = InputQuery1 & " WHERE " & CondiString1
            End If




            '**********************Query Builder Function *****************
            CQ._InputQuery = InputQuery1 & "  Group by [ItemCode],[BatchNo],[UOM] ,[Location]  HAVING SUM(QTY) > 0  "
            CQ._Parameters = CustomQueryParameters

            ResultDataTable = CURD.CustomQueryGetData(CQ)

            If Not IsNothing(ResultDataTable) Then
                If ResultDataTable.Rows.Count > 0 Then
                    AppSpecificFunc.DataRowToObject(GR, ResultDataTable.Rows(0))
                End If
            End If

            Return GR
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return GR
        End Try
    End Function
    <WebMethod()>
    Public Function IsStockAvailable(ByVal ItemCode As String, ByVal BatchNo As String, ByVal Qty As String, ByVal UOM As String, ByVal Location As String) As PostResults
        Dim Results As New PostResults
        Results.IsSuccess = False
        Results.ErrMsg = "-"
        Results.IdKey = "-"
        Try
            Dim ResultDataTable As New DataTable


            Dim CQ As New CustomQuery

            CQ._DB = "Custom"
            Dim CustomQueryParameters As New Dictionary(Of String, String)
            Dim InputQuery1 As String = String.Empty
            InputQuery1 = " Select Sum(Qty)  From GoodsReceiptItems T1 "


            Dim Conditionlist1 As New List(Of String)
            Dim Conditionlist2 As New List(Of String)

            Conditionlist1.Add("T1.ItemCode=@ItemCode")
            Conditionlist2.Add("T2.ItemCode=@ItemCode")
            CustomQueryParameters.Add("@ItemCode", ItemCode)

            Conditionlist1.Add("T1.BatchNo=@BatchNo")
            Conditionlist2.Add("T2.BatchNo=@BatchNo")
            CustomQueryParameters.Add("@BatchNo", BatchNo)

            Conditionlist1.Add("T1.UOM=@UOM")
            Conditionlist2.Add("T2.UOM=@UOM")
            CustomQueryParameters.Add("@UOM", UOM)

            Conditionlist1.Add("T1.Location=@Location")
            Conditionlist2.Add("T2.Location=@Location")
            CustomQueryParameters.Add("@Location", Location)




            If Conditionlist1.Count > 0 Then
                Dim CondiString1 As String = String.Join(" AND ", Conditionlist1)

                InputQuery1 = InputQuery1 & " WHERE " & CondiString1
            End If


            InputQuery1 = InputQuery1 & " UNION ALL "

            InputQuery1 = InputQuery1 & " Select Sum(Qty)  From GoodsIssueItems T2 "

            If Conditionlist2.Count > 0 Then
                Dim CondiString2 As String = String.Join(" AND ", Conditionlist2)

                InputQuery1 = InputQuery1 & " WHERE " & CondiString2
            End If

            '**********************Query Builder Function *****************
            CQ._InputQuery = InputQuery1 & "  "
            CQ._Parameters = CustomQueryParameters

            ResultDataTable = CURD.CustomQueryGetData(CQ)

            If Not IsNothing(ResultDataTable) Then
                If ResultDataTable.Rows.Count > 0 Then
                    Dim GRQty As Integer = 0
                    Dim GIQty As Integer = 0
                    If Not IsDBNull(ResultDataTable.Rows(0).Item(0)) Then
                        GRQty = ResultDataTable.Rows(0).Item(0)
                    End If
                    If Not IsDBNull(ResultDataTable.Rows(1).Item(0)) Then
                        GIQty = ResultDataTable.Rows(1).Item(0)
                    End If

                    Dim AvailQty As Integer = GRQty + GIQty
                    If AvailQty >= Qty Then
                        Results.IsSuccess = True
                    Else
                        Results.IsSuccess = False
                        Results.ErrMsg = "Not Enough Qty"
                    End If
                End If
            End If

            Return Results
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Results.IsSuccess = False
            Results.ErrMsg = ex.Message
            Return Results
        End Try
    End Function
    <WebMethod()>
    Public Function WriteDataGoodsReceipt(ByVal SupplierCode As String, ByVal ItemCode As String, ByVal BatchNo As String, ByVal Qty As String, ByVal UOM As String, ByVal UnitPrice As String, ByVal Remarks As String, ByVal Location As String, ByVal ReceiptDate As String, ByVal UploadedBy As String) As PostResults
        Dim Results As New PostResults
        Results.IsSuccess = True
        Results.ErrMsg = "-"
        Results.IdKey = "-"
        Dim PrimaryKey As Integer = 0
        Try
            Using SqlCon As New SqlConnection(CURD.GetConnectionString)

                SqlCon.Open()

                Dim SqlTrans As SqlTransaction = SqlCon.BeginTransaction()
                Try


                    Dim ProceedNext As Boolean = True

                    Dim GR As New GoodsReceiptItems
                    GR.SupplierCode = SupplierCode.Trim
                    GR.ItemCode = ItemCode.Trim
                    GR.BatchNo = BatchNo.Trim
                    GR.Qty = Qty.Trim
                    GR.UOM = UOM.Trim
                    GR.Location = Location.Trim
                    GR.Remarks = Remarks.Trim
                    GR.ReceivedBy = UploadedBy.Trim
                    Dim ValidDate As New Date
                    If Date.TryParseExact(ReceiptDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, ValidDate) = True Then
                        Dim a As String = ValidDate.ToString("yyyy-MM-dd")
                        GR.ReceiptDate = a

                    End If

                    'CURD.InsertData(GR)

                    ProceedNext = CURD.InsertDataTransaction(GR, SqlCon, SqlTrans, PrimaryKey, True)
                    If ProceedNext = True Then
                        Dim SM As New StockMovement
                        SM.DocumentId = PrimaryKey
                        SM.DocumenType = "Stock In"
                        SM.LocationCode = Location.Trim
                        SM.ItemCode = ItemCode.Trim
                        SM.StockInOut = Qty.Trim
                        SM.UOM = UOM.Trim
                        SM.UnitPrice = UnitPrice.Trim
                        SM.Remarks = Remarks.Trim
                        SM.CreatedBy = UploadedBy.Trim

                        CURD.InsertDataTransaction(SM, SqlCon, SqlTrans, PrimaryKey, False)
                    Else
                        Throw New System.Exception(" Error in Good Receipt creation ")
                    End If

                    SqlTrans.Commit()
                Catch ex As Exception
                    SqlTrans.Rollback()
                    SqlTrans.Dispose()
                    AppSpecificFunc.WriteLog(ex)
                    Results.IsSuccess = False
                    Results.ErrMsg = ex.Message
                End Try

            End Using

            Return Results
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Results.IsSuccess = False
            Results.ErrMsg = ex.Message
            Return Results
        End Try
    End Function
    <WebMethod()>
    Public Function WriteDataGoodsIssue(ByVal CustomerCode As String, ByVal ItemCode As String, ByVal BatchNo As String, ByVal Qty As String, ByVal UOM As String, ByVal UnitPrice As String, ByVal Remarks As String, ByVal Location As String, ByVal ReceiptDate As String, ByVal UploadedBy As String) As PostResults
        Dim Results As New PostResults
        Results.IsSuccess = True
        Results.ErrMsg = "-"
        Results.IdKey = "-"
        Dim PrimaryKey As Integer = 0

        Try
            Using SqlCon As New SqlConnection(CURD.GetConnectionString)

                SqlCon.Open()

                Dim SqlTrans As SqlTransaction = SqlCon.BeginTransaction()
                Try
                    Dim ProceedNext As Boolean = True

                    Dim GI As New GoodsIssueItems
                    GI.CustomerCode = CustomerCode.Trim
                    GI.ItemCode = ItemCode.Trim
                    GI.BatchNo = BatchNo.Trim
                    GI.Qty = Qty.Trim
                    GI.UOM = UOM.Trim
                    GI.Location = Location.Trim
                    GI.Remarks = Remarks.Trim
                    GI.IssuedBy = UploadedBy.Trim
                    Dim ValidDate As New Date
                    If Date.TryParseExact(ReceiptDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, ValidDate) = True Then
                        Dim a As String = ValidDate.ToString("yyyy-MM-dd")
                        GI.IssuedDate = a

                    End If
                    'CURD.InsertData(GR)

                    ProceedNext = CURD.InsertDataTransaction(GI, SqlCon, SqlTrans, PrimaryKey, True)
                    If ProceedNext = True Then
                        Dim SM As New StockMovement
                        SM.DocumentId = PrimaryKey
                        SM.DocumenType = "Stock Out"
                        SM.LocationCode = Location.Trim
                        SM.ItemCode = ItemCode.Trim
                        SM.StockInOut = CDec(Qty.Trim) * -1
                        SM.UOM = UOM.Trim
                        SM.UnitPrice = UnitPrice.Trim
                        SM.Remarks = Remarks.Trim
                        SM.CreatedBy = UploadedBy.Trim
                        CURD.InsertDataTransaction(SM, SqlCon, SqlTrans, PrimaryKey, False)
                    Else
                        Throw New System.Exception(" Error in Good Receipt creation ")
                    End If

                    SqlTrans.Commit()

                    AppSpecificFunc.ShootEmailOnLowParlevel(ItemCode.Trim)

                Catch ex As Exception
                    SqlTrans.Rollback()
                    SqlTrans.Dispose()
                    AppSpecificFunc.WriteLog(ex)
                    Results.IsSuccess = False
                    Results.ErrMsg = ex.Message
                End Try
            End Using

            Return Results
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Results.IsSuccess = False
            Results.ErrMsg = ex.Message
            Return Results
        End Try
    End Function

    Public Function Base64ToImage(ByVal ProjectName As String, ByVal StartingName As String, ByVal Input As String) As String

        Dim origin As New DateTime(1970, 1, 1, 0, 0, 0, 0)
        Dim Diff As New TimeSpan
        Diff = Now - origin
        Dim FileNameGenerated As String = Math.Floor(Diff.TotalSeconds)
        ProjectName = ProjectName.Replace(" ", "_")
        ProjectName = ProjectName.Replace("+", "_")
        ProjectName = ProjectName.Replace("&", "_")
        FileNameGenerated = ProjectName & "_" & StartingName & "_" & FileNameGenerated & ".png"

        Dim img As System.Drawing.Image
        Dim MS As System.IO.MemoryStream = New System.IO.MemoryStream
        Dim b64 As String = Input.Replace(" ", "+")
        Dim b() As Byte

        'Converts the base64 encoded msg to image data
        b = Convert.FromBase64String(b64)
        MS = New System.IO.MemoryStream(b)

        'creates image
        img = System.Drawing.Image.FromStream(MS)
        Dim FilePath As String = Server.MapPath("~\Signatures\" & FileNameGenerated)
        img.Save(FilePath, System.Drawing.Imaging.ImageFormat.Png)
        Return FileNameGenerated
    End Function
End Class