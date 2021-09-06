<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Forgot.aspx.vb" Inherits="RebondTrading.Forgot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <h2>
        Password recovery
    </h2>
                            <asp:PasswordRecovery  MailDefinition-BodyFileName="~/Account/PasswordRecoveryMailContent.html" ID="PasswordRecovery1" MailDefinition-IsBodyHtml="true" MailDefinition-Subject="Your new password" SuccessText="In a few moments, you will receive an email with the subject line 'Your New Password' that contains a new password." runat="server" Height="147px" Width="442px"  BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana"  >
        <UserNameTemplate>
            <table border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse">
                <tr>
                    <td style="width: 445px">
                        <table border="0" cellpadding="0" style="width: 442px; height: 147px">
                            <tr>
                                <td align="center" colspan="2">
                                    <strong><span ><%--Password Recovery--%></span></strong></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Enter your Username:</asp:Label></td>
                                <td style="width: 291px">
                                    <asp:TextBox ID="UserName" runat="server" Width="187px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                        ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="PasswordRecovery1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="color: red">
                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2" style="text-align: right">
                                    <asp:Button ID="SubmitButton" runat="server" CommandName="Submit" Text="Submit" ValidationGroup="PasswordRecovery1"
                                        Width="103px" />
                                    &nbsp; &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </UserNameTemplate>
        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
        <SuccessTextStyle Font-Bold="True" ForeColor="#5D7B9D" />   
        <TitleTextStyle  Font-Bold="True"  />
        <SubmitButtonStyle   BorderColor="#CCCCCC" BorderStyle="Solid"
            BorderWidth="1px"  />
    </asp:PasswordRecovery> 
                       
</asp:Content>
