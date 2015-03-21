﻿<%@ Page Title="Thêm mới/xem hồ sơ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ThemMoiHoSo.aspx.cs" Inherits="Function.ThemMoiHoSo" %>
<%@ Register assembly="eWorld.UI" namespace="eWorld.UI" tagprefix="ew" %>
<%@ Register assembly="DTPicker" namespace="DTPicker" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
<script src="../Scripts/calendar-en.min.js" type="text/javascript"></script>
<link href="../Styles/calendar-blue.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function () {
        $("#<%=txtThoiGianBatDau.ClientID %>").dynDateTime({
            showsTime: true,
            ifFormat: "%d/%m/%Y",
            daFormat: "%l;%M %p, %e %m,  %Y",
            align: "BR",
            electric: false,
            singleClick: false,
            displayArea: ".siblings('.dtcDisplayArea')",
            button: ".next()"
        });
    });
</script>

<script type="text/javascript">
    $(document).ready(function () {
        $("#<%=txtThoiGiankT.ClientID %>").dynDateTime({
            showsTime: true,
            ifFormat: "%d/%m/%Y",
            daFormat: "%l;%M %p, %e %m,  %Y",
            align: "BR",
            electric: false,
            singleClick: false,
            displayArea: ".siblings('.dtcDisplayArea')",
            button: ".next()"
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div>
    <table>
    <tr><td colspan="4"><label ID="ldhead" runat="server"></label></td></tr>
    <tr>
    <td><span class="textrequire">*</span>  Cơ quan lưu trữ</td>
    <td colspan="3">
        <asp:DropDownList ID="ddlCoQuan" runat="server" Height="28px" Width="625px">
        </asp:DropDownList>
        </td>
    
    </tr>
    <tr>
    <td><span class="textrequire">*</span> Phông</td>
    <td colspan="3">
          <asp:DropDownList ID="ddlPhong" runat="server" Height="37px" Width="625px">
        </asp:DropDownList>
        </td>
   
    </tr>
     <tr>
    <td>Mục lục số</td>
    <td>
        <asp:DropDownList ID="ddlMucLuc" runat="server" Width="200px">
        </asp:DropDownList>
        </td>
    <td>Hộp số</td>
    <td>
        <asp:DropDownList ID="ddlHopSo" runat="server" Height="20px" Width="200px">
        </asp:DropDownList>
        </td>
    </tr>
       <tr>
    <td>Hồ sơ số:</td>
    <td>
        <asp:TextBox ID="txtHoSoSo" runat="server" Width="200px"></asp:TextBox>
        </td>
    <td>Ngôn ngữ</td>
    <td>
        <asp:DropDownList ID="ddlNgonNgu" runat="server" Height="20px" Width="200px">
        </asp:DropDownList>
        </td>
    </tr>
      <tr>
    <td>Ký hiệu thông tin</td>
    <td>
        <asp:TextBox ID="txtKyHieu" runat="server" Width="200px"></asp:TextBox>
        </td>
    <td></td>
    <td>
        
        </td>
    </tr>
    <tr>
    <td><span class="textrequire">*</span> Tiêu đề hồ sơ</td>
    <td colspan="3">
        <asp:TextBox ID="txtTieude" runat="server" Rows="5" TextMode="MultiLine" 
            Width="625px"></asp:TextBox>
        </td>
    
    </tr>
     <tr>
    <td>Chú giải</td>
    <td colspan="3">
        <asp:TextBox ID="txtGhiChu" runat="server" Rows="5" TextMode="MultiLine" 
            Width="625px"></asp:TextBox>
        </td>
    
    </tr>
    <tr>
    <td>Thời gian bắt đầu</td>
    <td>
       
          <asp:TextBox ID="txtThoiGianBatDau" 
            runat="server" ReadOnly = "true" Width="200px"></asp:TextBox>
        <img src="../Styles/calender.png" />
        </td>
    <td>Thời gian kết thúc</td>
    <td>
          <asp:TextBox ID="txtThoiGiankT" 
            runat="server" ReadOnly = "true" Width="200px"></asp:TextBox>
        <img src="../Styles/calender.png" />
        </td>
    </tr>
    <tr>
    <td>Bút tích</td>
    <td colspan="3">
        <asp:TextBox ID="txtButtich" runat="server" Width="625px"></asp:TextBox>
   
   </td>
   </tr>
   <tr>
    <td>Số lượng tờ</td>
    <td>
        <ew:NumericBox ID="txtSoluongto" runat="server" Width="200px">0</ew:NumericBox>
        </td>
    <td>Thời hạn bảo quản</td>
    <td>
      
          <asp:DropDownList ID="ddlThoiHan" runat="server" Width="200px">
        </asp:DropDownList>
        </td>
    </tr>
  <tr>
    <td>Chế độ SD</td>
    <td>
        
         <asp:DropDownList ID="ddlChedoSD" runat="server" Width="200px">
        </asp:DropDownList>
        
        </td>
    <td>Tình trạng vật lý</td>
    <td>
         
         <asp:DropDownList ID="ddlTinhTrang" runat="server" Width="200px">
        </asp:DropDownList>
        </td>
    </tr>
     <tr>
   <td></td>
    <td colspan="3">
        <asp:HiddenField ID="hidIdPhong" runat="server" />
   
        <asp:Button ID="btAddApprover" runat="server" Text="Thêm mới" Width="120px" 
            onclick="btAddApprover_Click"  />
        <asp:Button ID="btCancel" runat="server" Text="Quay lại" Width="120px" 
            onclick="btCancel_Click" />
        
   
   </td>
    </tr>
    </table>
    
    </div>

</asp:Content>
