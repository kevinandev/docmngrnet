<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateUser.aspx.cs" Inherits="DocMngr.CreateUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="frmCreateUser" runat="server">
    <div>
        <asp:Table ID="Table1" runat="server" Width="100%">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">UserName</asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">Password</asp:TableCell>
                <asp:TableCell  runat="server">
                    <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    <asp:Button ID="btnCreateUser" runat="server" Text="Create user" 
        onclick="btnCreateUser_Click" />
    <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </form>
</body>
</html>
