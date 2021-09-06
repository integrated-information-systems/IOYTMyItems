Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Data
Imports System.Globalization

Namespace Models
    Public Class CommonFunc
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
        Public Shared Sub GridRowToObject(ByVal FooterRow As GridViewRow, ByRef Obj As Object)
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
        Public Shared Sub PageControlsToObject(ByVal FooterRow As Control, ByRef Obj As Object)
            Try


                Dim props As Type = Obj.GetType

                For Each member As PropertyInfo In props.GetProperties
                    If Not IsNothing(FooterRow.FindControl(member.Name)) Then
                        Select Case (FooterRow.FindControl(member.Name).GetType())
                            Case GetType(TextBox)
                                Dim TBox As TextBox = TryCast(FooterRow.FindControl(member.Name), TextBox)
                                If TBox.CssClass.Contains("DecimalValue") Then
                                    TBox.Text = TBox.Text.Replace(",", "")
                                End If
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
                            Case GetType(CheckBox)
                                Dim chkbox As CheckBox = TryCast(FooterRow.FindControl(member.Name), CheckBox)
                                If chkbox.Checked = True Then
                                    member.SetValue(Obj, "1", Nothing)
                                Else
                                    member.SetValue(Obj, "0", Nothing)
                                End If


                            Case GetType(FreeTextBoxControls.FreeTextBox)
                                Dim TBox As FreeTextBoxControls.FreeTextBox = TryCast(FooterRow.FindControl(member.Name), FreeTextBoxControls.FreeTextBox)
                                member.SetValue(Obj, TBox.Text, Nothing)
                        End Select
                    End If
                Next
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
            End Try
        End Sub
        Public Shared Sub ObjectToPageControls(ByRef FooterRow As Control, ByVal Obj As Object)
            Try


                Dim props As Type = Obj.GetType

                For Each member As PropertyInfo In props.GetProperties
                    If Not IsNothing(FooterRow.FindControl(member.Name)) Then
                        If Not IsNothing(member.GetValue(Obj, Nothing)) Then
                            Select Case (FooterRow.FindControl(member.Name).GetType())
                                Case GetType(TextBox)
                                    Dim TBox As TextBox = TryCast(FooterRow.FindControl(member.Name), TextBox)
                                    If TBox.CssClass.Contains("DecimalValue") Then
                                        Dim Value As Decimal = member.GetValue(Obj, Nothing).ToString
                                        TBox.Text = (Value).ToString("N", New CultureInfo("en-US"))
                                    Else
                                        TBox.Text = member.GetValue(Obj, Nothing).ToString
                                    End If
                                Case GetType(CheckBox)
                                    Dim ChkBox As CheckBox = TryCast(FooterRow.FindControl(member.Name), CheckBox)

                                    ChkBox.Checked = CBool(member.GetValue(Obj, Nothing))
                                Case GetType(FreeTextBoxControls.FreeTextBox)
                                    Dim TBox As FreeTextBoxControls.FreeTextBox = TryCast(FooterRow.FindControl(member.Name), FreeTextBoxControls.FreeTextBox)
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
                                Case GetType(FreeTextBoxControls.FreeTextBox)
                                    Dim TBox As FreeTextBoxControls.FreeTextBox = TryCast(FooterRow.FindControl(member.Name), FreeTextBoxControls.FreeTextBox)
                                    TBox.Text = String.Empty
                                Case GetType(DropDownList)
                                    Dim DDL As DropDownList = TryCast(FooterRow.FindControl(member.Name), DropDownList)
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
                                Case GetType(FreeTextBoxControls.FreeTextBox)
                                    Dim TBox As FreeTextBoxControls.FreeTextBox = TryCast(InputForm.FindControl(member.Name), FreeTextBoxControls.FreeTextBox)
                                    TBox.ReadOnly = True
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
                                Case GetType(FreeTextBoxControls.FreeTextBox)
                                    Dim TBox As FreeTextBoxControls.FreeTextBox = TryCast(InputForm.FindControl(member.Name), FreeTextBoxControls.FreeTextBox)
                                    TBox.ReadOnly = False
                            End Select
                        End If

                    End If
                Next
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
            End Try
        End Sub
        Public Shared Sub DataTableToObject(ByRef Obj As Object, ByVal DT As DataTable, Optional ByVal RowIndex As Integer = 0)
            Try
                Dim props As Type = Obj.GetType
                If DT.Rows.Count > 0 Then
                    Dim Drow As DataRow = DT.Rows(RowIndex)
                    For Each member As PropertyInfo In props.GetProperties
                        If Not IsDBNull(Drow(member.Name)) Then
                            member.SetValue(Obj, Drow(member.Name).ToString, Nothing)
                        End If
                    Next
                End If
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
            End Try
        End Sub
    End Class
End Namespace

