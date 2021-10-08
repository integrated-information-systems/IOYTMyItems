<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SkuMaster.aspx.vb" Inherits="RebondTrading.SkuMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Item Master</h2>
<asp:ValidationSummary ID="HeaderValidationSummary" runat="server" ValidationGroup="HeaderValidation" />
<div class="MainContent">
    <div class="FormHeader">
        <div class="twocolleft50" >
        <!-- Panel added to set the default enter button hitting setup --> 
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnAddOrUpdate">         
            <div class="Table">                               
                <div class="Row">
                    <div class="Cell"><asp:Label ID="labelForItemCode" runat="server" AssociatedControlID="ItemCode" Text="Item Code" ></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="ItemCode" runat="server" MaxLength="20" CssClass="textEntry"  ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ItemCodeRequired" ControlToValidate="ItemCode" ValidationGroup="HeaderValidation" runat="server" ErrorMessage="Item Code Required">*</asp:RequiredFieldValidator>
                    </div>
                </div>       
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForItemDescription" runat="server" AssociatedControlID="ItemDescription" Text="Item Description" ></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="ItemDescription" runat="server" MaxLength="120" CssClass="textEntry" ></asp:TextBox> 
                    </div>
                </div>  
                <div class="Row">
                <div class="Cell" ><asp:Label ID="lblForCategory" runat="server" Text="Category" AssociatedControlID="Category"></asp:Label></div>
                <div class="Cell"><asp:DropDownList runat="server" ID="Category"    ></asp:DropDownList> 
                    <asp:CustomValidator  id="ValidCategoryRequired"  runat="server"  ValidationGroup="HeaderValidation" OnServerValidate="ValidateCategory"   ErrorMessage="Valid Category required" Display="Dynamic">*</asp:CustomValidator>   
                </div>                                                               
            </div>                   
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForUOM" runat="server" AssociatedControlID="UOM" Text="UOM" ></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="UOM" runat="server" MaxLength="100" CssClass="textEntry" ></asp:TextBox> 
                    </div>
                </div>        
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForParLevel" runat="server" AssociatedControlID="ParLevel" Text="Par Level" ></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="ParLevel" runat="server" MaxLength="100" CssClass="textEntry" ></asp:TextBox> 
                        <asp:CustomValidator ID="ParLevelValidator" runat="server" ErrorMessage="Valid ParLevel Required"  ValidationGroup="HeaderValidation"    OnServerValidate="ValidateParLevel" >*</asp:CustomValidator>
                    </div>
                </div>                        
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForActive" runat="server" AssociatedControlID="Active" Text="Active"></asp:Label></div>
                    <div class="Cell"><asp:CheckBox runat="server" ID="Active" /></div>
                </div>                
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForFileAttachment1" runat="server" AssociatedControlID="FileAttachment1" Text="Image 1"></asp:Label></div>
                    <div class="Cell"><asp:FileUpload id="FileAttachment1" runat="server" /> 
                          <asp:CustomValidator ID="FileAttachment1Validator" runat="server" ErrorMessage="Valid ParLevel Required"  ValidationGroup="HeaderValidation"    OnServerValidate="ValidateFileAttachment1" >*</asp:CustomValidator>
                    </div>                    
                </div>
                <div class="Row">
                    <div class="Cell">&nbsp;</div>
                    <div class="Cell">
                        <asp:Image ID="Image1" runat="server"  ImageUrl='http://sapb1viewer.xyz:81/MyItems/Business/LoadImage.aspx?FileName=NoImg.png'   alt="image" Width="100" Height="100" /></div>
                </div>
                 <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForFileAttachment2" runat="server" AssociatedControlID="FileAttachment2" Text="Image 2"></asp:Label></div>
                    <div class="Cell"><asp:FileUpload id="FileAttachment2" runat="server" /> </div>
                </div>
                    <div class="Row">
                    <div class="Cell">&nbsp;</div>
                    <div class="Cell">
                        <asp:Image ID="Image2" runat="server"  ImageUrl='http://sapb1viewer.xyz:81/MyItems/Business/LoadImage.aspx?FileName=NoImg.png'   alt="image" Width="100" Height="100" /></div>
                </div>
                 <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForFileAttachment3" runat="server" AssociatedControlID="FileAttachment3" Text="Image 3"></asp:Label></div>
                    <div class="Cell"><asp:FileUpload id="FileAttachment3" runat="server" /> </div>
                </div>
                <div class="Row">
                    <div class="Cell">&nbsp;</div>
                    <div class="Cell">
                        <asp:Image ID="Image3" runat="server"  ImageUrl='http://sapb1viewer.xyz:81/MyItems/Business/LoadImage.aspx?FileName=NoImg.png'   alt="image" Width="100" Height="100" /></div>
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
                    <div class="Cell"><asp:Label ID="labelForSearchItemCode" runat="server" AssociatedControlID="SearchItemCode" Text="Search Item Code" ></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="SearchItemCode" runat="server" MaxLength="20" CssClass="textEntry300" ></asp:TextBox>                       
                    </div>
                </div>    
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForSearchItemDescription" runat="server" AssociatedControlID="SearchItemDescription" Text="Search Item Description" ></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="SearchItemDescription" runat="server" MaxLength="20" CssClass="textEntry300" ></asp:TextBox>                       
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
            <asp:GridView ID="ItemMasterListGrid"
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
                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />                                 
                    <asp:BoundField DataField="ItemDescription" HeaderText="Item Description" />   
                    <asp:BoundField DataField="ParLevel" HeaderText="Par Level" />   
                    <asp:BoundField DataField="ItemBalance" HeaderText="Item Balance" />   
                    <asp:BoundField DataField="Active" HeaderText="Active" />
                    <asp:CommandField ShowSelectButton="true" HeaderText="Select" /> 
                </Columns>
            </asp:GridView>
        </center>
     </div>

</div>
<div id="dialog" title="Message"><asp:Label runat="server" Text="" ID="LblShowMsg"></asp:Label></div>
</asp:Content>

