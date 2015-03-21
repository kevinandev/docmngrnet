<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="DocMngr.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>System login</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmLogin" runat="server" style="vertical-align: middle; display: block;">
    <div style="width: 512px; height: 262px; margin-top: 200px; padding-left: 30px; padding-top: 120px;
        margin-left: auto; margin-right: auto" class="loginBody">
        <table width="90%">
            <col width="30%" />
            <tbody>
                <tr>
                    <td style="font-weight:bolder">
                        Tên đăng nhập
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtUserName" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td  style="font-weight:bolder">
                        Mật khẩu
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"  Width="100%" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkRememberPassword" runat="server" /> Ghi nhớ mật khẩu
                    </td>
                    <td align="right">
                        <asp:Button ID="btnLogin" runat="server" Text="Đăng nhập" 
                            onclick="loginClick" />
                    </td>
                </tr>
                <tr>
                    <td style="color:Red">
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </td>
                   
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
