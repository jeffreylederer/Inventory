﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Site1.Master" AutoEventWireup="true" CodeBehind="BowlsPictures.aspx.cs" Inherits="Inventory.Reports.BowlsPictures" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" %>


<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:Label runat="server" ForeColor="Red" ID="lblError" EnableViewState="False"></asp:Label>
    <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
    <rsweb:ReportViewer ID="rv1" runat="server" AsyncRendering="false" OnReportError="Rv1_ReportError"
                        Width="600px" Height="1000px">
        <LocalReport ReportPath="Reports/BowlsPictures.rdlc" >
        </LocalReport>
    </rsweb:ReportViewer>
</asp:Content> 
