<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AssignRoles.aspx.vb" Inherits="RebondTrading.AssignRoles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 114px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>
                    Assign Roles</h2>
                    <div class="MainContent">
<div class="FormHeader"> 

    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    <div style="Width:45%;" >
                        <fieldset class="assignRoles">
                         <legend>Assign User Roles</legend>
                         <p>
                         <asp:Label ID="Label1" runat="server" AssociatedControlID="LstUserList"> Select User:</asp:Label>
                         <asp:ListBox ID="LstUserList" runat="server" Height="23px" Width="173px" 
                    AutoPostBack="True" Rows="1">
                </asp:ListBox>
                         </p>
                      
                         <p> <asp:Label ID="Label2" runat="server" AssociatedControlID="UsersRoleList"> Roles:</asp:Label></p>
                          <ul>
                            <asp:Repeater ID="UsersRoleList" runat="server"> 
           <ItemTemplate>
          
           
           <li><asp:CheckBox runat="server" ID="RoleCheckBox" AutoPostBack="false" Text='<%# Container.DataItem %>' /></li>
           
           </ItemTemplate>     
             
              </asp:Repeater>
              </ul>
                         
                            
                          
                            
                           
              
                        
                         <p class="submitButton"><asp:Button ID="btnAssignRoles" runat="server" Text="Assign Roles" /></p>
                         </fieldset>
                         </div>
          <div id="dialog" title="Message"><asp:Label runat="server" Text="" ID="LblShowMsg"></asp:Label></div>    
</div>
</div>           
</asp:Content>
