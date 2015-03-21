<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateRole.aspx.cs" Inherits="DocMngr.CreateRole" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Object Name"></asp:Label>
        <asp:TextBox ID="TxtRoleName" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="BtnSubmit" runat="server" Text="Create role" OnClick="BtnSubmit_Click" />
        <asp:Button ID="BtnAddToAdminRole" runat="server" Text="AddToAdmin" 
            onclick="BtnAddToAdminRole_Click" />
    </div>
    </form>
</body>
</html>
