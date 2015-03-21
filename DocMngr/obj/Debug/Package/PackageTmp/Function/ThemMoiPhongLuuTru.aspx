<%@ Page Title="Thêm mới/xem phông lưu trữ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ThemMoiPhongLuuTru.aspx.cs" Inherits="Function.ThemMoiPhongLuuTru" %>
<%@ Register assembly="eWorld.UI" namespace="eWorld.UI" tagprefix="ew" %>
<%@ Register assembly="DTPicker" namespace="DTPicker" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
<script src="../Scripts/calendar-en.min.js" type="text/javascript"></script>
<link href="../Styles/calendar-blue.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function () {
        $("#<%=txtThoigian.ClientID %>").dynDateTime({
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
    <td><span class="textrequire">*</span> Cơ quan lưu trữ</td>
    <td colspan="3">
        <asp:DropDownList ID="ddlCoQuan" runat="server" Height="25px" Width="625px">
        </asp:DropDownList>
        </td>
    
    </tr>
    <tr>
    <td><span class="textrequire">*</span>Mã phông</td>
    <td>
        <asp:TextBox ID="txtMaPhong" runat="server" Width="200px"></asp:TextBox>
        </td>
    <td><span class="textrequire">*</span>Tên phông</td>
    <td>
        <asp:TextBox ID="txtTenPhong" runat="server" Width="200px"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td>Lịch sử hình thành</td>
    <td colspan="3">
        <asp:TextBox ID="txtLichsu" runat="server" Rows="5" TextMode="MultiLine" 
            Width="625px"></asp:TextBox>
        </td>
    
    </tr>
    <tr>
    <td>Thời hạn tài liệu</td>
    <td>
        <asp:TextBox ID="txtThoiHanTaiLieu" runat="server" Width="200px"></asp:TextBox>
        </td>
    <td>Tổng số tài liệu</td>
    <td>
        <ew:NumericBox ID="txtTongSoTaiLieu" runat="server" Width="200px">0</ew:NumericBox>
        </td>
    </tr>
    <tr>
    <td>Số tài liệu đã chỉnh lý</td>
    <td>
        <ew:NumericBox ID="txtTaiLieuChinhLy" runat="server" Width="200px">0</ew:NumericBox>
        </td>
    <td>Số tài liệu chưa chỉnh lý</td>
    <td>
        <ew:NumericBox ID="txtTaiLieuChuaChinhLy" runat="server" Width="200px">0</ew:NumericBox>
        </td>
    </tr>
    <tr>
    <td>Các nhóm tai liệu chủ yếu</td>
    <td colspan="3">
        <asp:TextBox ID="txtNhomTaiLieu" runat="server" Width="625px"></asp:TextBox>
   
   </td>
   </tr>
   <tr>
   <td>Các loại hình tài liệu khác</td>
    <td colspan="3">
        <asp:TextBox ID="txtTaiLieuKhac" runat="server" Width="625px"></asp:TextBox>
   
   </td>
    </tr>
  <tr>
    <td>Thời gian nhập tài liệu</td>
    <td>
        
        <asp:TextBox ID="txtThoigian" 
            runat="server" ReadOnly = "true" Width="200px"></asp:TextBox>
        <img src="../Styles/calender.png" />

        </td>
    <td><span class="textrequire">*</span>Ngôn ngữ</td>
    <td>
        <asp:DropDownList ID="ddlNgonNgu" runat="server" Height="23px" Width="200px">
        </asp:DropDownList>
        </td>
    </tr>
      <tr>
   <td>Công cụ tra cứu</td>
    <td colspan="3">
        <asp:TextBox ID="txtCongCu" runat="server" Width="625px"></asp:TextBox>
   
   </td>
    </tr>
     <tr>
   <td>Lập bản sao bảo hiểm</td>
    <td colspan="3">
        <asp:TextBox ID="txtBaoHiem" runat="server" Width="625px"></asp:TextBox>
   
   </td>
    </tr>
     <tr>
    <td>Ghi chú</td>
    <td colspan="3">
        <asp:TextBox ID="txtGhiChu" runat="server" Rows="5" TextMode="MultiLine" 
            Width="625px"></asp:TextBox>
        </td>
    
    </tr>
     <tr>
   <td></td>
    <td colspan="3">
        
   
        <asp:Button ID="btAddApprover" runat="server" Text="Thêm mới" Width="120px" 
            onclick="btAddApprover_Click" />
        <asp:Button ID="btCancel" runat="server" Text="Quay lại" Width="120px" 
            onclick="btCancel_Click" />
        
   
   </td>
    </tr>
    </table>
    
    </div>

</asp:Content>
