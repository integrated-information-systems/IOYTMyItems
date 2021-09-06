<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="GoodsReceipt.aspx.vb" Inherits="RebondTrading.GoodsReceipt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"
EnablePageMethods = "true">
</asp:ScriptManager>
<h2>Item Receipt</h2>
<div class="MainContent">
    <div class="FormHeader"> 
        <div class="twocolleft25" > 
            <div class="Table"  >
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblFilterDate"  runat="server" Text="Date"></asp:Label></div>
                    <div class="Cell"> <asp:TextBox ID="FilterDate" runat="server" CssClass="textDateEntry" MaxLength="10"   ></asp:TextBox> 
                        <act:CalendarExtender ID="FilterDateCalendarExtender" TargetControlID="FilterDate" runat="server" Format="dd-MM-yyyy" />
                        <asp:CustomValidator  id="ValidFilterDateRequired"  runat="server"  ValidationGroup="HeaderValidation" OnServerValidate="ValidateFilterDate"   ErrorMessage="Valid Date required" Display="Dynamic">*
                                                 </asp:CustomValidator>
                        </div>
                </div>
 
            </div>
        </div>
        <div class="twocolleft50" > 
            <div class="Table"  >
 

        </div>
    </div>
</div>
     <div class="FormHeader">
            <div class="Table">
                <div class="Row">
                    <div class="Cell">
                        <asp:Button ID="btnSearch" runat="server" Text="Search"  CssClass="Action-Button"   ValidationGroup="HeaderValidation" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear"  CssClass="Action-Button"  /></div>
                </div>
            </div>
        </div>
        <div class="GridViewItems">
        <center>
         <asp:GridView ID="GoodsReceiptMasterGrid"
             AllowSorting="True"
             AllowPaging="True" 
             runat="server"
             CellPadding="4"
             ForeColor="#333333"
             AutoGenerateColumns="False"
             ShowHeaderWhenEmpty="True"
             GridLines="None"
             EmptyDataText="No Record found"
             CssClass="ItemGrid"     
              PageSize="25"
             >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                <Columns>
                    
                    <asp:BoundField DataField="SupplierCode" HeaderText="Supplier Code"  ReadOnly="True"   />     
                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code"  ReadOnly="True"   />                                                
                    <asp:BoundField DataField="BatchNo" HeaderText="BatchNo" ReadOnly="True" />                                        
                    <asp:BoundField DataField="Qty" HeaderText="Qty" ReadOnly="True" />
                    <asp:BoundField DataField="UOM" HeaderText="UOM" ReadOnly="True"  />   
                    <asp:BoundField DataField="Location" HeaderText="Location" ReadOnly="True"  />        
                    <asp:BoundField DataField="UnitPrice" HeaderText="UnitPrice" ReadOnly="True"  />        
                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" ReadOnly="True"  />        
                    <asp:BoundField DataField="ReceivedBy" HeaderText="Received By" ReadOnly="True"  />        
                    <%--<asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnView" runat="server" CommandArgument='<%# Eval("IdKey")%>' CommandName="View" Text="View" />
                        </ItemTemplate>
                    </asp:TemplateField>    --%>            
                </Columns>
                </asp:GridView>
                <% If GoodsReceiptMasterGrid.Rows(0).Cells.Count > 1 Then%>
                <i>You are viewing page
                <%=GoodsReceiptMasterGrid.PageIndex + 1%>
                of
                <%=GoodsReceiptMasterGrid.PageCount%>
                </i>
                <% End If%>
                </center>
</div>
</div>
</asp:Content>
