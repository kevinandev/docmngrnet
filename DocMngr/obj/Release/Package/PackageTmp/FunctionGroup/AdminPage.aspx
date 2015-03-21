<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AdminPage.aspx.cs" Inherits="FunctionGroup.AdminPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:ImageButton ID="BtnRoleConfig" runat="server" ImageUrl="~/imgs/user_group.png"
                        BorderStyle="Solid" BorderWidth="1px" CssClass="adminPage" ToolTip="Quản lý nhóm người dùng"
                        OnClick="BtnRoleConfig_Click" /><br />
                    Quản lý nhóm người dùng
                </td>
                <td>
                    <asp:ImageButton ID="BtnFunction" runat="server" ImageUrl="~/imgs/panel_window_menu.png"
                        BorderStyle="Solid" BorderWidth="1px" CssClass="adminPage" ToolTip="Quản lý phân quyền"
                        OnClick="BtnFunction_Click" /><br />
                    Quản lý phân quyền
                </td>
                <td>
                    <asp:ImageButton ID="BtnMenuConfig" runat="server" ImageUrl="~/imgs/kmenuedit.png"
                        BorderStyle="Solid" BorderWidth="1px" CssClass="adminPage" ToolTip="Quản lý Menu"
                        OnClick="BtnMenuConfig_Click" /><br />
                    Quản lý Menu
                </td>
                <td>
                    <asp:ImageButton ID="BtnUserConfig" runat="server" ImageUrl="~/imgs/user.png" BorderStyle="Solid"
                        BorderWidth="1px" CssClass="adminPage" ToolTip="Quản lý User" OnClick="BtnUserConfig_Click" /><br />
                    Quản lý User
                </td>
                <td>
                    <asp:ImageButton ID="BtnUnitConfig" runat="server" ImageUrl="~/imgs/view_tree.png"
                        BorderStyle="Solid" BorderWidth="1px" CssClass="adminPage" ToolTip="Quản lý phòng ban"
                        OnClick="BtnUnitConfig_Click" /><br />
                    Quản lý phòng ban
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
