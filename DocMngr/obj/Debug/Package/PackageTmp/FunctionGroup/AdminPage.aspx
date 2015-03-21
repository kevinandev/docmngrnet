<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="FunctionGroup.AdminPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div>
    <asp:ImageButton ID="BtnRoleConfig" runat="server" 
        ImageUrl="~/imgs/user_group.png" BorderStyle="Solid" BorderWidth="1px" 
        CssClass="adminPage" ToolTip="Quản lý role" onclick="BtnRoleConfig_Click"/>
    <asp:ImageButton ID="BtnFunction" runat="server" 
        ImageUrl="~/imgs/panel_window_menu.png" BorderStyle="Solid" BorderWidth="1px" 
        CssClass="adminPage" ToolTip="Chức năng hệ thống" onclick="BtnFunction_Click" />
    <asp:ImageButton ID="BtnMenuConfig" runat="server" 
        ImageUrl="~/imgs/kmenuedit.png" BorderStyle="Solid" BorderWidth="1px" 
        CssClass="adminPage" ToolTip="Quản lý Menu" onclick="BtnMenuConfig_Click" />
    <asp:ImageButton ID="BtnUserConfig" runat="server" ImageUrl="~/imgs/user.png" BorderStyle="Solid" BorderWidth="1px" CssClass="adminPage" ToolTip="Quản lý User" />
    <asp:ImageButton ID="BtnUnitConfig" runat="server" ImageUrl="~/imgs/view_tree.png" BorderStyle="Solid" BorderWidth="1px" CssClass="adminPage" ToolTip="Quản lý đơn vị" />
</div>
</asp:Content>
