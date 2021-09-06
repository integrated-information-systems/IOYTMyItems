<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CustMaster.aspx.vb" Inherits="RebondTrading.CustMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"
EnablePageMethods = "true">
</asp:ScriptManager>
    <h2>Customer Master</h2>
<asp:ValidationSummary ID="HeaderValidationSummary" runat="server" ValidationGroup="HeaderValidation" />
<div class="MainContent">
    <div class="FormHeader">
        <div class="twocolleft50" >
        <!-- Panel added to set the default enter button hitting setup --> 
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnAddOrUpdate">         
            <div class="Table">                               
                <div class="Row">
                    <div class="Cell"><asp:Label ID="labelForCustomerCode" runat="server" AssociatedControlID="CustomerCode" Text="Customer Code" ></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="CustomerCode" runat="server" MaxLength="20" CssClass="textEntry"  ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CustomerCodeRequired" ControlToValidate="CustomerCode" ValidationGroup="HeaderValidation" runat="server" ErrorMessage="Customer Code Required"></asp:RequiredFieldValidator>
                    </div>
                </div>       
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForCustomerName" runat="server" AssociatedControlID="CustomerName" Text="Customer Name" ></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="CustomerName" runat="server" MaxLength="120" CssClass="textEntry" ></asp:TextBox> 
                    </div>
                </div>    
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForRegistrationDate" runat="server" AssociatedControlID="RegistrationDate" Text="Registration Date" ></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="RegistrationDate" runat="server" CssClass="textDateEntry" MaxLength="10"   ></asp:TextBox> 
                        <act:CalendarExtender ID="RegistrationDateCalendarExtender" TargetControlID="RegistrationDate" runat="server" Format="dd-MM-yyyy" />
                        <asp:CustomValidator  id="ValidRegistrationDateRequired"  runat="server"  ValidationGroup="HeaderValidation" OnServerValidate="ValidateRegistrationDate"   ErrorMessage="Valid Date required" Display="Dynamic">*
                                                 </asp:CustomValidator>
                    </div>
                </div>     
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForAddressLine1" runat="server" AssociatedControlID="AddressLine1" Text="Address Line 1" ></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="AddressLine1" runat="server" MaxLength="100" CssClass="textEntry" ></asp:TextBox> 
                    </div>
                </div>  
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForAddressLine2" runat="server" AssociatedControlID="AddressLine2" Text="Address Line 2" ></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="AddressLine2" runat="server" MaxLength="100" CssClass="textEntry" ></asp:TextBox> 
                    </div>
                </div>    
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForAddressLine3" runat="server" AssociatedControlID="AddressLine3" Text="Address Line 3" ></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="AddressLine3" runat="server" MaxLength="100" CssClass="textEntry" ></asp:TextBox> 
                    </div>
                </div> 
	            <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForAddressLine4" runat="server" AssociatedControlID="AddressLine4" Text="Address Line 4" ></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="AddressLine4" runat="server" MaxLength="100" CssClass="textEntry" ></asp:TextBox> 
                    </div>
                </div> 
	            <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForAddressLine5" runat="server" AssociatedControlID="AddressLine5" Text="Address Line 5" ></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="AddressLine5" runat="server" MaxLength="100" CssClass="textEntry" ></asp:TextBox> 
                    </div>
                </div>     
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForActive" runat="server" AssociatedControlID="Active" Text="Active"></asp:Label></div>
                    <div class="Cell"><asp:CheckBox runat="server" ID="Active" /></div>
                </div>  
                <div class="Row">
                    <div class="Cell">&nbsp;</div>
                    <div class="Cell">
                        <asp:Button ID="btnAddOrUpdate" runat="server" Text="Add"  CssClass="Action-Button"   ValidationGroup="HeaderValidation" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear"  CssClass="Action-Button"  />
                    </div>
                </div>                                
            </div>
            </asp:Panel>
        </div>
        <div class="twocolright50" >
        <!-- Panel added to set the default enter button hitting setup --> 
        <asp:Panel ID="Panel2" runat="server" DefaultButton="btnSearch">         
            <div class="Table">
                                            
                <div class="Row">
                    <div class="Cell"><asp:Label ID="labelForSearchCustomerCode" runat="server" AssociatedControlID="SearchCustomerCode" Text="Search Customer Code" ></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="SearchCustomerCode" runat="server" MaxLength="20" CssClass="textEntry300" ></asp:TextBox>                       
                    </div>
                </div>    
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForSearchCustomerName" runat="server" AssociatedControlID="SearchCustomerName" Text="Search Customer Name" ></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="SearchCustomerName" runat="server" MaxLength="20" CssClass="textEntry300" ></asp:TextBox>                       
                    </div>
                </div>                  
                <div class="Row">
                    <div class="Cell">&nbsp;</div>
                    <div class="Cell">
                        <asp:Button ID="btnSearch" runat="server" Text="Search"  CssClass="Action-Button"  />
                        <asp:Button ID="btnSearchClear" runat="server" Text="Clear"  CssClass="Action-Button"  />
                    </div>
                </div>                                             
            </div>
            </asp:Panel>
        </div>  
     </div>


    <div class="GridViewSalesPersons">
        <center>
            <asp:GridView ID="CustomerMasterListGrid"
             AllowSorting="False"
             AllowPaging="True" 
             runat="server"
             CellPadding="4"
             ForeColor="#333333"
             AutoGenerateColumns="False"
             ShowHeaderWhenEmpty="True"
             GridLines="None"
             EmptyDataText="No Record found"
             CssClass="ItemGrid"     
             PageSize="20"
             DataKeyNames="Idkey"             
             >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                <Columns>
                    <asp:BoundField DataField="CustomerCode" HeaderText="Customer Code" />                                 
                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />    
                    <asp:BoundField DataField="RegistrationDate" HeaderText="Registration Date" DataFormatString="{0:dd-MM-yyyy}" />  
                    <asp:BoundField DataField="Active" HeaderText="Active" />
                    <asp:CommandField ShowSelectButton="true" HeaderText="Select" /> 
                </Columns>
            </asp:GridView>
        </center>
     </div>

</div>
<div id="dialog" title="Message"><asp:Label runat="server" Text="" ID="LblShowMsg"></asp:Label></div>
</asp:Content>

