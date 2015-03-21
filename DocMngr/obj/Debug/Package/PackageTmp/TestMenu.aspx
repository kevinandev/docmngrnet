<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestMenu.aspx.cs" Inherits="DocMngr.TestMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Site.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Menu ID="mnu1" runat="server" CssClass="menu" EnableViewState="false" IncludeStyleBlock="false" Orientation="Horizontal">
            <Items>
                <asp:MenuItem NavigateUrl="#" Text="Menu 1" Selectable="false">
                    <asp:MenuItem Text="Menu 11"></asp:MenuItem>
                    <asp:MenuItem Text="Menu 12"></asp:MenuItem>
                    <asp:MenuItem Text="Menu 13"></asp:MenuItem>
                    <asp:MenuItem Text="Menu 14"></asp:MenuItem>
                    <asp:MenuItem Text="Menu 15"></asp:MenuItem>
                </asp:MenuItem>
            </Items>
        </asp:Menu>
    </div>
    </form>
</body>
</html>
