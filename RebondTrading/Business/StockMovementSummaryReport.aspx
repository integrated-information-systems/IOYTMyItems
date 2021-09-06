<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="StockMovementSummaryReport.aspx.vb" Inherits="RebondTrading.StockMovementSummaryReport" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"
EnablePageMethods = "true">
</asp:ScriptManager>
<h2>Item Balance Report</h2>

        <div class="FormHeader"> 
        <div class="twocolleft50" > 
            <div class="Table"  >                
                <%--<div class="Row">
                    <div class="Cell"><asp:Label ID="Label1"  runat="server" Text="Doc Date From"></asp:Label></div>
                    <div class="Cell"> <asp:TextBox ID="DocFromDate" runat="server" CssClass="textDateEntry" MaxLength="10"></asp:TextBox> 
                        <act:CalendarExtender ID="CalendarExtender1" TargetControlID="DocFromDate" runat="server" Format="yyyy-MM-dd" />
                        </div>
                </div>--%>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForLocationFrom"  runat="server" Text="Location From"></asp:Label></div>
                    <div class="Cell"> <asp:TextBox ID="LocationFrom" runat="server"  ></asp:TextBox>     
                         <act:AutoCompleteExtender ServiceMethod="GetLocations"
                        MinimumPrefixLength="1"
                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"  
                        TargetControlID="LocationFrom" CompletionListElementID="LocationFromAutoCompleteContainer"
                        ID="LocationFromAutoCompleteExtender" runat="server" FirstRowSelected = "false"> </act:AutoCompleteExtender>
                        <div id="LocationFromAutoCompleteContainer"></div>
                        </div>
                </div>                
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForSkuFrom"  runat="server" Text="Item Code From"></asp:Label></div>
                    <div class="Cell"> <asp:TextBox ID="SkuFrom" runat="server"  ></asp:TextBox>   
                        <act:AutoCompleteExtender ServiceMethod="GetSkus"
                        MinimumPrefixLength="1"
                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"  
                        TargetControlID="SkuFrom" CompletionListElementID="SkuFromAutoCompleteContainer"
                        ID="SkuFromAutoCompleteExtender" runat="server" FirstRowSelected = "false"> </act:AutoCompleteExtender>
                        <div id="SkuFromAutoCompleteContainer"></div>
                        </div>
                </div>    
                <%--<div class="Row">
                    <div class="Cell"><asp:Label ID="lblForBatchNo"  runat="server" Text="Batch No"></asp:Label></div>
                    <div class="Cell"> <asp:TextBox ID="BatchNo" runat="server"  ></asp:TextBox>  
                        <act:AutoCompleteExtender ServiceMethod="GetBatchNos"
                        MinimumPrefixLength="1"
                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"  
                        TargetControlID="BatchNo" CompletionListElementID="BatchNoAutoCompleteContainer"
                        ID="BatchNoAutoCompleteExtender" runat="server" FirstRowSelected = "false"> </act:AutoCompleteExtender>
                        <div id="BatchNoAutoCompleteContainer"></div>
                        </div>
                </div>   --%>
            </div>
        </div>
        <div class="Twocolleft50">
            <div class="Table">                    
                      <%--<div class="Row">
                            <div class="Cell"><asp:Label ID="Label2"  runat="server" Text="To"></asp:Label></div>
                            <div class="Cell"> <asp:TextBox ID="DocToDate" runat="server" CssClass="textDateEntry" MaxLength="10"></asp:TextBox> 
                                <act:CalendarExtender ID="CalendarExtender2" TargetControlID="DocToDate" runat="server" Format="yyyy-MM-dd" />
                                </div>
                        </div> --%>
                        <div class="Row">
                            <div class="Cell"><asp:Label ID="lblForLocationTo"  runat="server" Text="To"></asp:Label></div>
                            <div class="Cell"> <asp:TextBox ID="LocationTo" runat="server"  ></asp:TextBox>  
                                <act:AutoCompleteExtender ServiceMethod="GetLocations"
                        MinimumPrefixLength="1"
                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"  
                        TargetControlID="LocationTo" CompletionListElementID="LocationToAutoCompleteContainer"
                        ID="LocationToAutoCompleteExtender" runat="server" FirstRowSelected = "false"> </act:AutoCompleteExtender>
                        <div id="LocationToAutoCompleteContainer"></div>
                                </div>
                        </div> 
                        <div class="Row">
                            <div class="Cell"><asp:Label ID="lblForSkuTo"  runat="server" Text="To"></asp:Label></div>
                            <div class="Cell"> <asp:TextBox ID="SkuTo" runat="server"  ></asp:TextBox>      
                        <act:AutoCompleteExtender ServiceMethod="GetSkus"
                        MinimumPrefixLength="1"
                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"  
                        TargetControlID="SkuTo" CompletionListElementID="SkuToAutoCompleteContainer"
                        ID="SkuToAutoCompleteExtender" runat="server" FirstRowSelected = "false"> </act:AutoCompleteExtender>
                        <div id="SkuToAutoCompleteContainer"></div>
                                </div>
                        </div>
                       
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
        <center>
               <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True" BestFitPage="True" PrintMode="ActiveX" />
            </center>
</asp:Content>
