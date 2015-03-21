<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="DocMngr._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="font-size:30px; margin:15px" align="center" >
        <asp:Label ID="lblTitle" Text="Tiêu đề" runat="server"></asp:Label>
    </div>
    <div runat="server" id="divContent"></div>
</asp:Content>
