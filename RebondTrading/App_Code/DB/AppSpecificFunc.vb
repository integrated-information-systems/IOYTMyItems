Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Diagnostics
Imports System.Net
Imports System.Net.Mail
Imports System.Reflection

Namespace Models

    Public Class AppSpecificFunc
        Public Shared Sub ShootEmailOnLowParlevel(ItemCode As String)
            Try
                Dim ResultDataTable As New DataTable

                Dim CQ As New CustomQuery
                CQ._DB = "Custom"
                Dim CustomQueryParameters As New Dictionary(Of String, String)
                Dim InputQuery1 As String = String.Empty
                InputQuery1 = "Select T1.ItemCode, T1.ItemDescription, T1.ParLevel,  ISNULL(T3.ItemBalance,0)  as 'ItemBalance' from ItemMaster T1 " &
                        "  OUTER APPLY(SELECT Sum(StockInOut) as 'ItemBalance' FROM StockMovement " &
                        "  WHERE T1.ItemCode=StockMovement.ItemCode) AS T3"
                Dim Conditionlist1 As New List(Of String)

                Conditionlist1.Add(" ISNULL(T3.ItemBalance,0)<=T1.ParLevel  ")
                Conditionlist1.Add(" T1.ItemCode=@ItemCode  ")
                CustomQueryParameters.Add("@ItemCode", ItemCode)
                If Conditionlist1.Count > 0 Then
                    Dim CondiString1 As String = String.Join(" AND ", Conditionlist1)

                    InputQuery1 = InputQuery1 & " WHERE " & CondiString1
                End If

                '**********************Query Builder Function *****************
                CQ._InputQuery = InputQuery1 & " Order By T1.ItemCode  ASC"
                CQ._Parameters = CustomQueryParameters

                ResultDataTable = CURD.CustomQueryGetData(CQ)

                If Not IsNothing(ResultDataTable) Then
                    If ResultDataTable.Rows.Count > 0 Then


                        Dim TableItems As String = "<table>"

                        TableItems = TableItems & "<tr><th>Item Code</th><th>Item Description</th><th>ParLevel</th><th>Item Balance</th></tr>"
                        For Each DR As DataRow In ResultDataTable.Rows
                            TableItems = TableItems & "<tr><td>" & DR.Item(0).ToString & "</td><td>" & DR.Item(1).ToString & "</td><td>" & DR.Item(2).ToString & "</td><td>" & DR.Item(3).ToString & "</td></tr>"
                        Next



                        TableItems = TableItems & "</table>"

                        Dim AllUsers = Membership.GetAllUsers
                        Dim AllUsersEmailList As New List(Of String)
                        For Each Usr As MembershipUser In AllUsers



                            Dim mm As New MailMessage("iismediafire@gmail.com", Usr.Email)

                            mm.Subject = "MyItems - Items below ParLevel"

                            Dim Message As String = String.Empty
                            Message = "Dear " & Usr.UserName

                            Message = Message & "<br/><br/>Please refer the below for Items which are below the ParLevel<br/>"
                            '    If HeaderID <> String.Empty Then
                            '        Message = Message & "<br/> New " & MailType & " :" & HeaderID & " has been uploded by " & UploadedBy.Trim
                            '    Else
                            '        Message = Message & "<br/> New " & MailType & " has been uploded by " & UploadedBy.Trim
                            '    End If
                            Message = Message & TableItems
                            Message = Message & "<br/>Thanks."

                            mm.Body = Message
                            mm.IsBodyHtml = True
                            '    'Dim Filepath As String = Server.MapPath("~/Content/FeedBack.pdf")
                            '    'Dim attachment = New System.Net.Mail.Attachment(Filepath)
                            '    'mm.Attachments.Add(attachment)
                            Dim smtp As New SmtpClient()
                            smtp.Host = "smtp.gmail.com"
                            smtp.EnableSsl = True
                            Dim NetworkCred As New NetworkCredential()
                            NetworkCred.UserName = "iismediafire@gmail.com"
                            NetworkCred.Password = "99@lokyang"
                            smtp.UseDefaultCredentials = True
                            smtp.Credentials = NetworkCred
                            smtp.Port = 587

                            'Add this line to bypass the certificate validation
                            'The remote certificate Is invalid according to the validation procedure.

                            System.Net.ServicePointManager.ServerCertificateValidationCallback = Function(s As Object, certificate As System.Security.Cryptography.X509Certificates.X509Certificate, chain As System.Security.Cryptography.X509Certificates.X509Chain, sslPolicyErrors As System.Net.Security.SslPolicyErrors) True

                            smtp.Send(mm)

                        Next
                    End If
                End If
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
            End Try
        End Sub
        Public Shared Function GetBatchNos(Optional prefix As String = "") As DataTable
            Dim ReturnResult As New DataTable
            Try
                Dim CQ As New CustomQuery
                CQ._DB = "Custom"
                Dim CustomQueryParameters As New Dictionary(Of String, String)
                Dim Conditionlist1 As New List(Of String)
                Dim InputQuery1 As String = " Select BatchNo from GoodsReceiptItems WHERE BatchNo  LIKE '%'+ @prefix + '%' UNION Select BatchNo from GoodsIssueItems WHERE BatchNo LIKE '%'+ @prefix + '%' "


                If prefix <> String.Empty Then
                    'Conditionlist1.Add("BatchNo LIKE '%'+ @prefix + '%' ")
                    CustomQueryParameters.Add("@prefix", prefix)
                End If


                If Conditionlist1.Count > 0 Then
                    Dim CondiString1 As String = String.Join(" AND ", Conditionlist1)
                    InputQuery1 = InputQuery1 & " WHERE " & CondiString1
                End If
                CQ._InputQuery = InputQuery1
                CQ._Parameters = CustomQueryParameters

                ReturnResult = CURD.CustomQueryGetData(CQ)
                Return ReturnResult
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return Nothing
            End Try
        End Function
        Public Shared Function GetItemCodes(Optional prefix As String = "") As DataTable
            Dim ReturnResult As New DataTable
            Try
                Dim CQ As New CustomQuery
                CQ._DB = "Custom"
                Dim CustomQueryParameters As New Dictionary(Of String, String)
                Dim Conditionlist1 As New List(Of String)
                Dim InputQuery1 As String = " Select Idkey, ItemDescription, ItemCode from ItemMaster"
                Conditionlist1.Add(" Active=@Active  ")
                CustomQueryParameters.Add("@Active", 1)

                If prefix <> String.Empty Then
                    Conditionlist1.Add("ItemCode LIKE '%'+ @prefix + '%' ")
                    CustomQueryParameters.Add("@prefix", prefix)
                End If


                If Conditionlist1.Count > 0 Then
                    Dim CondiString1 As String = String.Join(" AND ", Conditionlist1)
                    InputQuery1 = InputQuery1 & " WHERE " & CondiString1
                End If
                CQ._InputQuery = InputQuery1
                CQ._Parameters = CustomQueryParameters

                ReturnResult = CURD.CustomQueryGetData(CQ)
                Return ReturnResult
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return Nothing
            End Try
        End Function
        Public Shared Function GetLocationCodes(Optional prefix As String = "") As DataTable
            Dim ReturnResult As New DataTable
            Try
                Dim CQ As New CustomQuery
                CQ._DB = "Custom"
                Dim CustomQueryParameters As New Dictionary(Of String, String)
                Dim Conditionlist1 As New List(Of String)
                Dim InputQuery1 As String = " Select Idkey, Location from LocationMaster"
                Conditionlist1.Add(" Active=@Active  ")
                CustomQueryParameters.Add("@Active", 1)

                If prefix <> String.Empty Then
                    Conditionlist1.Add("Location LIKE '%'+ @prefix + '%' ")
                    CustomQueryParameters.Add("@prefix", prefix)
                End If


                If Conditionlist1.Count > 0 Then
                    Dim CondiString1 As String = String.Join(" AND ", Conditionlist1)
                    InputQuery1 = InputQuery1 & " WHERE " & CondiString1
                End If
                CQ._InputQuery = InputQuery1
                CQ._Parameters = CustomQueryParameters

                ReturnResult = CURD.CustomQueryGetData(CQ)
                Return ReturnResult
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return Nothing
            End Try
        End Function
        Public Shared Function GetItemCategories() As DataTable
            Dim ReturnResult As DataTable = Nothing
            Try

                Dim ItemCategoryMasterObj As New ItemCategoryMaster



                ' INPUT FOR Query Builder
                Dim SQB As New SelectQuery
                SQB._InputTable = ItemCategoryMasterObj
                SQB._DB = "Custom"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = False
                SQB._Conditions = Nothing
                SQB._OrderBy = " Category ASC"
                Dim ResultDataTable As DataTable = CURD.SelectAllData(SQB)
                If Not IsNothing(ResultDataTable) Then
                    If ResultDataTable.Rows.Count > 0 Then
                        Return ResultDataTable
                    End If
                End If
                Return ReturnResult
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return Nothing
            End Try
        End Function
        Public Shared Function GetActiveAllMobileLocations(ByRef ResultLocationLines() As MobLocation) As Boolean
            Try
                Dim Result As Boolean = True
                Dim ResultDataTable As New DataTable
                '**********************Query Builder Function *****************
                Dim LocationMasterTable As New LocationMaster

                'Filter Values
                LocationMasterTable.Active = "1"

                'Query Conditions List
                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values

                ConditionsGrp1.Add("Active=@Active")

                QryConditions.Add(" AND ", ConditionsGrp1)

                ' INPUT FOR Query Builder
                Dim SQB As New SelectQuery
                SQB._InputTable = LocationMasterTable
                SQB._DB = "Custom"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True
                SQB._Conditions = QryConditions

                'ResultDataTable = CURD.SelectSpecificFieldsData(SQB)
                ResultDataTable = CURD.SelectAllData(SQB)
                '**********************Query Builder Function *****************
                If ResultDataTable.Rows.Count > 0 Then
                    ReDim ResultLocationLines(ResultDataTable.Rows.Count - 1)
                    Dim i As Integer = 0

                    For Each Drow As DataRow In ResultDataTable.Rows
                        Dim NewMobLocationObj As New MobLocation
                        DataRowToObject(NewMobLocationObj, Drow)

                        If IsNothing(NewMobLocationObj.Location) Then
                            NewMobLocationObj.Location = "-"
                        ElseIf NewMobLocationObj.Location = String.Empty Then
                            NewMobLocationObj.Location = "-"
                        End If

                        ResultLocationLines(i) = NewMobLocationObj
                        i = i + 1
                    Next

                End If
                Return Result
            Catch ex As Exception
                WriteLog(ex)
                Return False
            End Try
        End Function
        Public Shared Function GetActiveAllMobileItems(ByRef ResultItemsLines() As MobItem) As Boolean
            Try
                Dim Result As Boolean = True
                Dim ResultDataTable As New DataTable
                '**********************Query Builder Function *****************
                Dim ItemMasterTable As New ItemMaster

                Dim CQ As New CustomQuery
                CQ._DB = "Custom"
                Dim CustomQueryParameters As New Dictionary(Of String, String)
                Dim InputQuery1 As String = String.Empty
                InputQuery1 = "Select T1.*, ISNULL(T3.ItemBalance,0)  as 'ItemBalance' from ItemMaster T1 " &
                "  OUTER APPLY(SELECT Sum(StockInOut) as 'ItemBalance' FROM StockMovement " &
                "  WHERE T1.ItemCode=StockMovement.ItemCode) AS T3"
                Dim Conditionlist1 As New List(Of String)


                Conditionlist1.Add(" T1.Active=@Active ")
                CustomQueryParameters.Add("@Active", "1")




                If Conditionlist1.Count > 0 Then
                    Dim CondiString1 As String = String.Join(" AND ", Conditionlist1)

                    InputQuery1 = InputQuery1 & " WHERE " & CondiString1
                End If

                '**********************Query Builder Function *****************
                CQ._InputQuery = InputQuery1 & " Order By T1.ItemCode  ASC"
                CQ._Parameters = CustomQueryParameters

                ResultDataTable = CURD.CustomQueryGetData(CQ)

                '**********************Query Builder Function *****************
                If ResultDataTable.Rows.Count > 0 Then
                    ReDim ResultItemsLines(ResultDataTable.Rows.Count - 1)
                    Dim i As Integer = 0

                    For Each Drow As DataRow In ResultDataTable.Rows
                        Dim NewMobItemsObj As New MobItem
                        DataRowToObject(NewMobItemsObj, Drow)

                        If IsNothing(NewMobItemsObj.ItemCode) Then
                            NewMobItemsObj.ItemCode = "-"
                        ElseIf NewMobItemsObj.ItemCode = String.Empty Then
                            NewMobItemsObj.ItemCode = "-"
                        End If
                        If IsNothing(NewMobItemsObj.ItemDescription) Then
                            NewMobItemsObj.ItemDescription = "-"
                        ElseIf NewMobItemsObj.ItemDescription = String.Empty Then
                            NewMobItemsObj.ItemDescription = "-"
                        End If
                        If IsNothing(NewMobItemsObj.UOM) Then
                            NewMobItemsObj.UOM = "-"
                        ElseIf NewMobItemsObj.UOM = String.Empty Then
                            NewMobItemsObj.UOM = "-"
                        End If
                        If IsNothing(NewMobItemsObj.ItemBalance) Then
                            NewMobItemsObj.ItemBalance = "0"
                        ElseIf NewMobItemsObj.ItemBalance = String.Empty Then
                            NewMobItemsObj.ItemBalance = "0"
                        End If
                        If IsNothing(NewMobItemsObj.ParLevel) Then
                            NewMobItemsObj.ParLevel = "0"
                        ElseIf NewMobItemsObj.ParLevel = String.Empty Then
                            NewMobItemsObj.ParLevel = "0"
                        End If
                        ResultItemsLines(i) = NewMobItemsObj
                        i = i + 1
                    Next

                End If
                Return Result
            Catch ex As Exception
                WriteLog(ex)
                Return False
            End Try
        End Function
        Public Shared Function GetActiveAllMobileCustomers(ByRef ResultCustomersLines() As MobCustomer) As Boolean
            Try
                Dim Result As Boolean = True
                Dim ResultDataTable As New DataTable
                '**********************Query Builder Function *****************
                Dim CustomerMasterTable As New CustomerMaster

                'Filter Values
                CustomerMasterTable.Active = "1"

                'Query Conditions List
                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values

                ConditionsGrp1.Add("Active=@Active")

                QryConditions.Add(" AND ", ConditionsGrp1)

                ' INPUT FOR Query Builder
                Dim SQB As New SelectQuery
                SQB._InputTable = CustomerMasterTable
                SQB._DB = "Custom"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True
                SQB._Conditions = QryConditions

                'ResultDataTable = CURD.SelectSpecificFieldsData(SQB)
                ResultDataTable = CURD.SelectAllData(SQB)
                '**********************Query Builder Function *****************
                If ResultDataTable.Rows.Count > 0 Then
                    ReDim ResultCustomersLines(ResultDataTable.Rows.Count - 1)
                    Dim i As Integer = 0

                    For Each Drow As DataRow In ResultDataTable.Rows
                        Dim NewMobCustomerObj As New MobCustomer
                        DataRowToObject(NewMobCustomerObj, Drow)

                        If IsNothing(NewMobCustomerObj.CustomerCode) Then
                            NewMobCustomerObj.CustomerCode = "-"
                        ElseIf NewMobCustomerObj.CustomerCode = String.Empty Then
                            NewMobCustomerObj.CustomerCode = "-"
                        End If
                        If IsNothing(NewMobCustomerObj.CustomerName) Then
                            NewMobCustomerObj.CustomerName = "-"
                        ElseIf NewMobCustomerObj.CustomerName = String.Empty Then
                            NewMobCustomerObj.CustomerName = "-"
                        End If
                        'If IsNothing(NewMobPOItemsObj.DocNum) Then
                        '    NewMobPOItemsObj.DocNum = "-"
                        'ElseIf NewMobPOObj.DocNum = String.Empty Then
                        '    NewMobPOObj.DocNum = "-"
                        'End If
                        'If IsNothing(NewMobPOObj.DocEntry) Then
                        '    NewMobPOObj.DocEntry = "-"
                        'ElseIf NewMobPOObj.DocEntry = String.Empty Then
                        '    NewMobPOObj.DocEntry = "-"
                        'End If
                        ResultCustomersLines(i) = NewMobCustomerObj
                        i = i + 1
                    Next

                End If
                Return Result
            Catch ex As Exception
                WriteLog(ex)
                Return False
            End Try
        End Function
        Public Shared Function GetActiveAllMobileVendors(ByRef ResultVendorsLines() As MobVendor) As Boolean
            Try
                Dim Result As Boolean = True
                Dim ResultDataTable As New DataTable
                '**********************Query Builder Function *****************
                Dim SupplierMasterTable As New SupplierMaster

                'Filter Values
                SupplierMasterTable.Active = "1"

                'Query Conditions List
                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values

                ConditionsGrp1.Add("Active=@Active")

                QryConditions.Add(" AND ", ConditionsGrp1)

                ' INPUT FOR Query Builder
                Dim SQB As New SelectQuery
                SQB._InputTable = SupplierMasterTable
                SQB._DB = "Custom"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True
                SQB._Conditions = QryConditions

                'ResultDataTable = CURD.SelectSpecificFieldsData(SQB)
                ResultDataTable = CURD.SelectAllData(SQB)
                '**********************Query Builder Function *****************
                If ResultDataTable.Rows.Count > 0 Then
                    ReDim ResultVendorsLines(ResultDataTable.Rows.Count - 1)
                    Dim i As Integer = 0

                    For Each Drow As DataRow In ResultDataTable.Rows
                        Dim NewMobVendorObj As New MobVendor
                        DataRowToObject(NewMobVendorObj, Drow)

                        If IsNothing(NewMobVendorObj.SupplierCode) Then
                            NewMobVendorObj.SupplierCode = "-"
                        ElseIf NewMobVendorObj.SupplierCode = String.Empty Then
                            NewMobVendorObj.SupplierCode = "-"
                        End If
                        If IsNothing(NewMobVendorObj.SupplierName) Then
                            NewMobVendorObj.SupplierName = "-"
                        ElseIf NewMobVendorObj.SupplierName = String.Empty Then
                            NewMobVendorObj.SupplierName = "-"
                        End If
                        'If IsNothing(NewMobPOItemsObj.DocNum) Then
                        '    NewMobPOItemsObj.DocNum = "-"
                        'ElseIf NewMobPOObj.DocNum = String.Empty Then
                        '    NewMobPOObj.DocNum = "-"
                        'End If
                        'If IsNothing(NewMobPOObj.DocEntry) Then
                        '    NewMobPOObj.DocEntry = "-"
                        'ElseIf NewMobPOObj.DocEntry = String.Empty Then
                        '    NewMobPOObj.DocEntry = "-"
                        'End If
                        ResultVendorsLines(i) = NewMobVendorObj
                        i = i + 1
                    Next

                End If
                Return Result
            Catch ex As Exception
                WriteLog(ex)
                Return False
            End Try
        End Function
        Public Shared Function GetNextIdentityNo(ByRef IdentityNo As Integer, ByVal InitialNo As Integer, ByVal Obj As Object) As Boolean
            Try
                Dim ReturnResult As Boolean = True

                Dim ErrMsg As String = String.Empty
                Dim SQ As New SelectQuery

                ''Where Condition parameter
                'Obj.SubmittedToSAP = "Y"

                SQ._InputTable = Obj
                SQ._DB = "Custom"
                SQ._HasInBetweenConditions = False
                SQ._HasWhereConditions = False

                ''Query Conditions List
                'Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                ''Query Condition Groups
                'Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                ''Query Conditions values
                'ConditionsGrp1.Add("SubmittedToSAP=@SubmittedToSAP")

                'QryConditions.Add(" AND ", ConditionsGrp1)

                'SQ._Conditions = QryConditions


                Dim ResultDataTable As New DataTable
                ResultDataTable = CURD.SelectAllData(SQ)




                If ResultDataTable.Rows.Count > 0 Then
                    IdentityNo = ResultDataTable.Compute("Max(Idkey)", "") + 1
                Else
                    IdentityNo = InitialNo
                End If
                Return ReturnResult
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return False
            End Try
        End Function
        Public Shared Function IsIdentityNoNotAlreadyUsed(ByVal IdentityNo As Integer, ByVal Obj As Object) As Boolean
            Try
                Dim ReturnResult As Boolean = False

                Dim ErrMsg As String = String.Empty
                Dim SQ As New SelectQuery

                ''Where Condition parameter
                Obj.Idkey = IdentityNo

                SQ._InputTable = Obj
                SQ._DB = "Custom"
                SQ._HasInBetweenConditions = False
                SQ._HasWhereConditions = True

                'Query Conditions List
                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values
                ConditionsGrp1.Add("Idkey=@Idkey")

                QryConditions.Add(" AND ", ConditionsGrp1)

                SQ._Conditions = QryConditions


                Dim ResultDataTable As New DataTable
                ResultDataTable = CURD.SelectAllData(SQ)




                If ResultDataTable.Rows.Count > 0 Then
                    ReturnResult = False
                Else
                    ReturnResult = True
                End If
                Return ReturnResult
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return False
            End Try
        End Function

        Public Shared Sub DataRowToObject(ByRef Obj As Object, ByVal DR As DataRow)
            Try
                Dim props As Type = Obj.GetType

                Dim Drow As DataRow = DR
                For Each member As PropertyInfo In props.GetProperties
                    If Drow.Table.Columns.Contains(member.Name) Then
                        If Not IsDBNull(Drow(member.Name)) Then
                            member.SetValue(Obj, Drow(member.Name).ToString, Nothing)
                        End If
                    End If

                Next

            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
            End Try
        End Sub
        Public Shared Sub PageControlsToObject(ByVal FooterRow As Control, ByRef Obj As Object)
            Try


                Dim props As Type = Obj.GetType

                For Each member As PropertyInfo In props.GetProperties
                    If Not IsNothing(FooterRow.FindControl(member.Name)) Then
                        Select Case (FooterRow.FindControl(member.Name).GetType())
                            Case GetType(TextBox)
                                Dim TBox As TextBox = TryCast(FooterRow.FindControl(member.Name), TextBox)
                                member.SetValue(Obj, TBox.Text, Nothing)
                            Case GetType(DropDownList)
                                Dim DDL As DropDownList = TryCast(FooterRow.FindControl(member.Name), DropDownList)

                                member.SetValue(Obj, DDL.SelectedValue, Nothing)
                            Case GetType(Label)
                                Dim Lbl As Label = TryCast(FooterRow.FindControl(member.Name), Label)
                                member.SetValue(Obj, Lbl.Text, Nothing)
                            Case GetType(RadioButtonList)
                                Dim RdoList As RadioButtonList = TryCast(FooterRow.FindControl(member.Name), RadioButtonList)
                                If RdoList.SelectedIndex > -1 Then
                                    member.SetValue(Obj, RdoList.SelectedItem.Value, Nothing)
                                End If
                        End Select
                    End If
                Next
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
            End Try
        End Sub
        Public Shared Sub GetStringListFromDataTable(ByVal InputDataTable As DataTable, ByVal ColumnName As String, ByRef ListofString As List(Of String))
            Try
                For Each Drow As DataRow In InputDataTable.Rows
                    If Not IsDBNull(Drow(ColumnName)) Then
                        ListofString.Add(Drow(ColumnName).ToString)
                    End If
                Next
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
            End Try
        End Sub
        Public Shared Function GetLocalCustomers(CustomerCodeOrName As String, Optional prefix As String = "") As DataTable
            Dim ReturnResult As New DataTable
            Try
                Dim CQ As New CustomQuery
                CQ._DB = "Custom"
                Dim CustomQueryParameters As New Dictionary(Of String, String)
                Dim Conditionlist1 As New List(Of String)
                Dim InputQuery1 As String = " Select Idkey, CustomerName+'-'+CustomerCode as 'CustomerName', CustomerCode from CustomerMaster"
                Conditionlist1.Add(" Active=@Active  ")
                CustomQueryParameters.Add("@Active", 1)
                If CustomerCodeOrName = "CustomerCode" Then
                    If prefix <> String.Empty Then
                        Conditionlist1.Add("CustomerCode LIKE '%'+ @prefix + '%' ")
                        CustomQueryParameters.Add("@prefix", prefix)
                    End If
                ElseIf CustomerCodeOrName = "CustomerName" Then
                    If prefix <> String.Empty Then
                        Conditionlist1.Add("CustomerName LIKE '%'+ @prefix + '%' ")
                        CustomQueryParameters.Add("@prefix", prefix)
                    End If
                End If

                If Conditionlist1.Count > 0 Then
                    Dim CondiString1 As String = String.Join(" AND ", Conditionlist1)
                    InputQuery1 = InputQuery1 & " WHERE " & CondiString1
                End If
                CQ._InputQuery = InputQuery1
                CQ._Parameters = CustomQueryParameters

                ReturnResult = CURD.CustomQueryGetData(CQ)
                Return ReturnResult
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Shared Function GetLocalItems(ItemCodeOrName As String, Optional prefix As String = "") As DataTable
            Dim ReturnResult As New DataTable
            Try
                Dim CQ As New CustomQuery
                CQ._DB = "Custom"
                Dim CustomQueryParameters As New Dictionary(Of String, String)
                Dim Conditionlist1 As New List(Of String)
                Dim InputQuery1 As String = " Select Idkey, ItemName+'-'+ItemCode as 'ItemName', ItemCode from ItemMaster"
                Conditionlist1.Add(" Active=@Active  ")
                CustomQueryParameters.Add("@Active", 1)
                If ItemCodeOrName = "ItemCode" Then
                    If prefix <> String.Empty Then
                        Conditionlist1.Add("ItemCode LIKE '%'+ @prefix + '%' ")
                        CustomQueryParameters.Add("@prefix", prefix)
                    End If
                ElseIf ItemCodeOrName = "ItemName" Then
                    If prefix <> String.Empty Then
                        Conditionlist1.Add("ItemName LIKE '%'+ @prefix + '%' ")
                        CustomQueryParameters.Add("@prefix", prefix)
                    End If
                End If

                If Conditionlist1.Count > 0 Then
                    Dim CondiString1 As String = String.Join(" AND ", Conditionlist1)
                    InputQuery1 = InputQuery1 & " WHERE " & CondiString1
                End If
                CQ._InputQuery = InputQuery1
                CQ._Parameters = CustomQueryParameters

                ReturnResult = CURD.CustomQueryGetData(CQ)
                Return ReturnResult
            Catch ex As Exception
                Return Nothing
            End Try
        End Function


        Public Shared Sub MakeFormReadOnly(ByRef InputForm As Control, ByVal Obj As Object, ByVal SetReadOnly As Boolean)
            Try


                Dim props As Type = Obj.GetType

                For Each member As PropertyInfo In props.GetProperties
                    If Not IsNothing(InputForm.FindControl(member.Name)) Then
                        If SetReadOnly = True Then
                            Select Case (InputForm.FindControl(member.Name).GetType())
                                Case GetType(TextBox)
                                    Dim TBox As TextBox = TryCast(InputForm.FindControl(member.Name), TextBox)
                                    TBox.Enabled = False
                                Case GetType(DropDownList)
                                    Dim DDL As DropDownList = TryCast(InputForm.FindControl(member.Name), DropDownList)
                                    DDL.Enabled = False
                                Case GetType(Label)
                                    Dim Lbl As Label = TryCast(InputForm.FindControl(member.Name), Label)
                                    Lbl.Enabled = False
                                Case GetType(RadioButtonList)
                                    Dim RdoList As RadioButtonList = TryCast(InputForm.FindControl(member.Name), RadioButtonList)
                                    RdoList.Enabled = False
                            End Select
                        Else
                            Select Case (InputForm.FindControl(member.Name).GetType())
                                Case GetType(TextBox)
                                    Dim TBox As TextBox = TryCast(InputForm.FindControl(member.Name), TextBox)
                                    TBox.Enabled = True
                                Case GetType(DropDownList)
                                    Dim DDL As DropDownList = TryCast(InputForm.FindControl(member.Name), DropDownList)
                                    DDL.Enabled = True
                                Case GetType(Label)
                                    Dim Lbl As Label = TryCast(InputForm.FindControl(member.Name), Label)
                                    Lbl.Enabled = True
                                Case GetType(RadioButtonList)
                                    Dim RdoList As RadioButtonList = TryCast(InputForm.FindControl(member.Name), RadioButtonList)
                                    RdoList.Enabled = True
                            End Select
                        End If

                    End If
                Next
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
            End Try
        End Sub
        Public Shared Function GetStatusValue(ByVal InputStatus As String) As Integer
            Dim ReturnResult As Integer = -1
            Try
                InputStatus = InputStatus.Replace(" ", "_")
                InputStatus = "Status_" & InputStatus
                For Each Status As DocStatus In System.Enum.GetValues(GetType(DocStatus))
                    If Status.ToString.IndexOf(InputStatus) > -1 Then
                        ReturnResult = Status
                    End If
                Next
                Return ReturnResult
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return ReturnResult

            End Try

        End Function
        Public Shared Function GetStatusName(ByRef InputValue As DocStatus) As String
            Dim ReturnResult As String = String.Empty
            Try
                ReturnResult = InputValue.ToString.Replace("Status_", "")
                ReturnResult = ReturnResult.Replace("_", " ")
                Return ReturnResult
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return ReturnResult
            End Try
        End Function

        Public Shared Sub ObjectToPageControls(ByRef FooterRow As Control, ByVal Obj As Object)
            Try


                Dim props As Type = Obj.GetType

                For Each member As PropertyInfo In props.GetProperties
                    If Not IsNothing(FooterRow.FindControl(member.Name)) Then
                        If Not IsNothing(member.GetValue(Obj, Nothing)) Then
                            Select Case (FooterRow.FindControl(member.Name).GetType())
                                Case GetType(TextBox)
                                    Dim TBox As TextBox = TryCast(FooterRow.FindControl(member.Name), TextBox)
                                    TBox.Text = member.GetValue(Obj, Nothing).ToString
                                Case GetType(DropDownList)
                                    Dim DDL As DropDownList = TryCast(FooterRow.FindControl(member.Name), DropDownList)
                                    DDL.SelectedIndex = DDL.Items.IndexOf(DDL.Items.FindByValue(member.GetValue(Obj, Nothing).ToString))
                                Case GetType(Label)
                                    Dim Lbl As Label = TryCast(FooterRow.FindControl(member.Name), Label)
                                    Lbl.Text = member.GetValue(Obj, Nothing).ToString
                                Case GetType(RadioButtonList)
                                    Dim RdoList As RadioButtonList = TryCast(FooterRow.FindControl(member.Name), RadioButtonList)
                                    RdoList.SelectedIndex = RdoList.Items.IndexOf(RdoList.Items.FindByValue(member.GetValue(Obj, Nothing).ToString))
                            End Select
                        Else
                            Select Case (FooterRow.FindControl(member.Name).GetType())
                                Case GetType(TextBox)
                                    Dim TBox As TextBox = TryCast(FooterRow.FindControl(member.Name), TextBox)
                                    TBox.Text = String.Empty
                                Case GetType(DropDownList)
                                    Dim DDL As DropDownList = TryCast(FooterRow.FindControl(member.Name), DropDownList)
                                    DDL.SelectedIndex = 0
                                Case GetType(Label)
                                    Dim Lbl As Label = TryCast(FooterRow.FindControl(member.Name), Label)
                                    Lbl.Text = String.Empty
                                Case GetType(RadioButtonList)
                                    Dim RdoList As RadioButtonList = TryCast(FooterRow.FindControl(member.Name), RadioButtonList)
                                    RdoList.SelectedIndex = -1
                            End Select
                        End If
                    End If
                Next
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
            End Try
        End Sub
        Public Shared Sub BindGridData(ByVal GridDataTable As DataTable, ByRef InputGridView As GridView)
            Try


                If GridDataTable.Rows.Count > 0 Then
                    InputGridView.DataSource = GridDataTable
                    InputGridView.DataBind()
                Else

                    Dim TempDataTable As DataTable = GridDataTable.Clone
                    TempDataTable.Rows.Add(TempDataTable.NewRow())
                    InputGridView.DataSource = TempDataTable
                    InputGridView.DataBind()

                    AppSpecificFunc.GridNoDataFound(InputGridView)
                End If

            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
            End Try
        End Sub
        Public Shared Sub GridNoDataFound(ByRef InputGridView As GridView)
            Try

                Dim TotalColumns As Integer = InputGridView.Columns.Count
                InputGridView.Rows(0).Cells.Clear()
                InputGridView.Rows(0).Cells.Add(New TableCell())
                InputGridView.Rows(0).Cells(0).ColumnSpan = TotalColumns
                InputGridView.Rows(0).Cells(0).Text = "No Record Found"
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
            End Try
        End Sub
        Public Shared Sub GridRowToObject(ByVal FooterRow As GridViewRow, ByRef Obj As Object)
            Try


                Dim props As Type = Obj.GetType

                For Each member As PropertyInfo In props.GetProperties
                    If Not IsNothing(FooterRow.FindControl(member.Name)) Then
                        Select Case (FooterRow.FindControl(member.Name).GetType())
                            Case GetType(TextBox)
                                Dim TBox As TextBox = TryCast(FooterRow.FindControl(member.Name), TextBox)
                                member.SetValue(Obj, TBox.Text, Nothing)
                            Case GetType(FreeTextBoxControls.FreeTextBox)
                                Dim TBox As FreeTextBoxControls.FreeTextBox = TryCast(FooterRow.FindControl(member.Name), FreeTextBoxControls.FreeTextBox)
                                member.SetValue(Obj, TBox.Text, Nothing)
                            Case GetType(CheckBox)
                                Dim ChkBox As CheckBox = TryCast(FooterRow.FindControl(member.Name), CheckBox)
                                If ChkBox.Checked = True Then
                                    member.SetValue(Obj, "Y", Nothing)
                                Else
                                    member.SetValue(Obj, "N", Nothing)
                                End If
                            Case GetType(DropDownList)
                                Dim DDL As DropDownList = TryCast(FooterRow.FindControl(member.Name), DropDownList)
                                member.SetValue(Obj, DDL.SelectedValue, Nothing)
                            Case GetType(RadioButton)
                                Dim RDO As RadioButton = TryCast(FooterRow.FindControl(member.Name), RadioButton)
                                If RDO.Checked = True Then
                                    member.SetValue(Obj, "Y", Nothing)
                                Else
                                    member.SetValue(Obj, "N", Nothing)
                                End If
                            Case GetType(Label)
                                Dim Lbl As Label = TryCast(FooterRow.FindControl(member.Name), Label)
                                member.SetValue(Obj, Lbl.Text, Nothing)
                        End Select
                    End If
                Next
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
            End Try
        End Sub
        Public Shared Sub ObjectToDataRow(ByVal Obj As Object, ByRef DRow As DataRow)
            Try


                Dim props As Type = Obj.GetType

                For Each member As PropertyInfo In props.GetProperties
                    If Not IsNothing(member.GetValue(Obj, Nothing)) Then
                        DRow(member.Name) = member.GetValue(Obj, Nothing)
                    End If
                Next
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
            End Try
        End Sub
        Public Shared Sub DataTableToObject(ByRef Obj As Object, ByVal DT As DataTable, Optional ByVal Index As Integer = 0)
            Try
                Dim props As Type = Obj.GetType
                If DT.Rows.Count > 0 Then
                    Dim Drow As DataRow = DT.Rows(Index)
                    For Each member As PropertyInfo In props.GetProperties
                        If DT.Columns.Contains(member.Name) Then
                            If Not IsDBNull(Drow(member.Name)) Then
                                member.SetValue(Obj, Drow(member.Name).ToString, Nothing)
                            End If
                        End If
                    Next
                End If
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
            End Try
        End Sub

        Public Shared Function WriteLog(ByRef ex As Exception) As ErrLog
            Dim EL As New ErrLog
            Dim St As StackTrace = New StackTrace(ex, True)
            EL.errMSg = ex.Message
            If Not IsNothing(ex.InnerException) Then
                EL.InnerException = ex.InnerException.Message
            End If
            Dim FrameCount As Integer = St.FrameCount
            For i As Integer = 0 To FrameCount - 1
                If Not IsNothing(St.GetFrame(i).GetFileName) Then
                    EL.FileName = St.GetFrame(i).GetFileName.ToString
                    EL.LineNumber = St.GetFrame(i).GetFileLineNumber.ToString
                End If

            Next
            EL.CreatedOn = Format(CDate(DateTime.Now), "yyyy-MM-dd HH:mm:ss")
            CURD.InsertData(EL, True)
            Return EL
        End Function


    End Class
End Namespace

