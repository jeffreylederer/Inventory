<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportView.aspx.cs" Inherits="Inventory.Reports.ReportView" MasterPageFile="Site1.Master" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" %>


<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

        <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
        <rsweb:ReportViewer ID="rv1" runat="server" AsyncRendering="false"
        Width="600px" Height="1000px">
            <LocalReport ReportPath="Reports/BowlsInventory.rdlc" >
            </LocalReport>
        </rsweb:ReportViewer>
</asp:Content> 
