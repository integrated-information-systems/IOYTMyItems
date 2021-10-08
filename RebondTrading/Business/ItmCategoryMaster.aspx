<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ItmCategoryMaster.aspx.vb" Inherits="RebondTrading.ItmCategoryMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Category Master</h2>
<asp:ValidationSummary ID="HeaderValidationSummary" runat="server" ValidationGroup="HeaderValidation" />
<div class="MainContent">
    <div class="FormHeader">
        <div class="twocolleft50" >
        <!-- Panel added to set the default enter button hitting setup --> 
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnAddOrUpdate">         
            <div class="Table">                               
                <div class="Row">
                    <div class="Cell"><asp:Label ID="labelForCategory" runat="server" AssociatedControlID="Category" Text="Category Code" ></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="Category" runat="server" MaxLength="20" CssClass="textEntry"  ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CategoryRequired" ControlToValidate="Category" ValidationGroup="HeaderValidation" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
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
                    <div class="Cell"><asp:Label ID="labelForSearchCategory" runat="server" AssociatedControlID="SearchCategory" Text="Search Category Code" ></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="SearchCategory" runat="server" MaxLength="20" CssClass="textEntry300" ></asp:TextBox>                       
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
            <asp:GridView ID="CategoryMasterListGrid"
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
                OnPageIndexChanging="OnPageIndexChanging"
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
                    <asp:BoundField DataField="Category" HeaderText="Category Code" />                                                                                     
                    <asp:BoundField DataField="Active" HeaderText="Active" />
                    <asp:CommandField ShowSelectButton="true" HeaderText="Select" /> 
                </Columns>
            </asp:GridView>
        </center>
     </div>

</div>
<div id="dialog" title="Message"><asp:Label runat="server" Text="" ID="LblShowMsg"></asp:Label></div>
</asp:Content>

