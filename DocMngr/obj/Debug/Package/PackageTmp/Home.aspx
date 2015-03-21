<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="DocMngr.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
<table>
<tr>
<td>
    <asp:Panel ID="Panel1" runat="server" Width="300px">
     
            <table width="100%">
            <tr><td>CÁC VĂN BẢN MỚI NHẤT</td></tr>
            <% for (int i = 0; i < dtVanBan.Rows.Count; i ++ )
               {
                   Session[Logic.VanBanTrongHoSoLogic.SESSION_SEC_ID] = dtVanBan.Rows[i]["ID"].ToString();
                    %>
            <tr><td>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Styles/dot.png" /><a href="ChiTietVBTrongHoSo.aspx "> <%= dtVanBan.Rows[i]["TrichYeu"].ToString() %>  </a> </td></tr>
            <%} 
                %>
            </table>  
    </asp:Panel>
    </td>
    <td>
    <asp:Panel ID="Panel2" runat="server" Width="400px">
      <table><tr><td class="style1"> 
                <asp:TextBox ID="txtKeyword" runat="server" Width="400px"></asp:TextBox></td><td> 
                    <asp:Button ID="Button1" runat="server" Text="Tìm kiếm" 
                        onclick="Button1_Click" />  </td></tr>
             <tr><td colspan="2">GIỚI THIỆU VỀ SỞ NỘI VỤ ABC</td></tr>
            
            </table>
    </asp:Panel> 
    </td>
    </tr>
    </table>
    </div>
</asp:Content>
