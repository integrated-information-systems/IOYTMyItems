<%@ Page Title="Register" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="Register.aspx.vb" Inherits="RebondTrading.Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:CreateUserWizard ID="RegisterUser" runat="server" EnableViewState="False" 
        LoginCreatedUser="False" >
        <LayoutTemplate>
            <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <WizardSteps>
            <asp:CreateUserWizardStep ID="RegisterUserWizardStep" runat="server"  >
                <ContentTemplate>
                    <h2>
                       Add New User
                    </h2>     
                    <div class="MainContent">
                    <div class="FormHeader">    
                    
                    <p style="margin-left:2em;">
                        Passwords are required to be a minimum of <%= Membership.MinRequiredPasswordLength %> characters in length.
                    </p>
                    <span class="failureNotification">
                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="RegisterUserValidationGroup"/>
                    <div class="accountInfo">
                        <fieldset class="register">
                            <legend>Account Information</legend>
                            <p>
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                <asp:TextBox ID="UserName" runat="server" CssClass="textEntry" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                                     CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                                <asp:TextBox ID="Email" runat="server" CssClass="textEntry"  MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" 
                                     CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                                     CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                                <asp:TextBox ID="ConfirmPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" CssClass="failureNotification" Display="Dynamic" 
                                     ErrorMessage="Confirm Password is required." ID="ConfirmPasswordRequired" runat="server" 
                                     ToolTip="Confirm Password is required." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" 
                                     CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>
                            </p>
                          <%--  <p>
                          <asp:Label ID="SCLabel" runat="server" AssociatedControlID="ScsLst">Service Centres:</asp:Label>
                              <asp:ListBox ID="ScsLst" runat="server" AppendDataBoundItems="true" SelectionMode="Multiple"></asp:ListBox>
                              <asp:RequiredFieldValidator ID="ScsRequired" runat="server" ControlToValidate="ScsLst" CssClass="failureNotification" 
                            ErrorMessage="Please select  service centre." ToolTip="Please select  service centre." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                         </p>--%>
                          <p>
                          <%--<asp:Label ID="SalesPersonLabel" runat="server" AssociatedControlID="SalesPersonLst">Sales Persons:</asp:Label>
                              <asp:ListBox ID="SalesPersonLst" runat="server" AppendDataBoundItems="true" SelectionMode="Multiple"></asp:ListBox>--%>
                              <%--<asp:RequiredFieldValidator ID="SalesPersonRequired" runat="server" ControlToValidate="SalesPersonLst" CssClass="failureNotification" 
                            ErrorMessage="Please select Sales Person." ToolTip="Please select  Sales Person." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>--%>
                         </p>
                        </fieldset>
                        <p class="submitButton">
                            <asp:Button ID="CreateUserButton" runat="server" CommandName="MoveNext" Text="Create User" 
                                 ValidationGroup="RegisterUserValidationGroup" 
                                onclick="CreateUserButton_Click"/>
                        </p>
                    </div>
                    </div>
                    </div>       
                </ContentTemplate>
                <CustomNavigationTemplate>
                </CustomNavigationTemplate>
            </asp:CreateUserWizardStep>
<asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
    <ContentTemplate>
       <div class="accountInfo">
                        <fieldset class="register">
                            <legend> New User Added</legend>
                            <p class="SuccessMsg">New user account has been successfully created.</p>
                             <p class="submitButton">
                            <asp:Button ID="ContinueButton" runat="server" CausesValidation="False" 
                        CommandName="Continue" onclick="ContinueButton_Click" 
                        Text="Continue to Add New User" ValidationGroup="RegisterUser" />
                         </p>
                        </div>        
    </ContentTemplate>
            </asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
    <div id="dialog" title="Message"><asp:Label runat="server" Text="" ID="LblShowMsg"></asp:Label></div>
</asp:Content>