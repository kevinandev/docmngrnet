<%@ Page Title="Chi tiết văn bản trong hồ sơ" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="ChiTietVBTrongHoSo.aspx.cs" Inherits="Function.ChiTietVBTrongHoSo" %>

<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register Assembly="DTPicker" Namespace="DTPicker" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td colspan="4">
                <label id="ldhead" runat="server">
                </label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <b>I.Thông tin hồ sơ</b>
            </td>
        </tr>
        <tr>
            <td>
                <span class="textrequire">*</span> Cơ quan lưu trữ
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlCoQuan" runat="server" Height="28px" Width="625px" Enabled="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <span class="textrequire">*</span> Phông
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlPhong" runat="server" Height="37px" Width="625px" Enabled="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Mục lục số
            </td>
            <td>
                <asp:DropDownList ID="ddlMucLuc" runat="server" Width="200px" Enabled="False">
                </asp:DropDownList>
            </td>
            <td>
                Hồ sơ số
            </td>
            <td>
                <asp:TextBox ID="txtHoSoSo" runat="server" Width="200px" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <b>II.Thông tin văn bản</b>
            </td>
        </tr>
        <tr>
            <td>
                Ký hiệu văn bản
            </td>
            <td>
                <asp:TextBox ID="txtKyHieu" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
                Số lượng tờ
            </td>
            <td>
                <ew:NumericBox ID="txtSoluongto" runat="server" Width="200px">0</ew:NumericBox>
            </td>
        </tr>
        <tr>
            <td>
                Thời gian
            </td>
            <td>
                <asp:TextBox ID="txtThoiGian" runat="server"  Width="200px"></asp:TextBox>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Styles/calender.png" />
            </td>
            <td>
                Kí hiệu thông tin
            </td>
            <td>
                <asp:TextBox ID="txtKyhieuThongtin" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Ngôn ngữ
            </td>
            <td>
                <asp:DropDownList ID="ddlNgonNgu" runat="server" Height="25px" Width="200px">
                </asp:DropDownList>
            </td>
            <td>
                Tờ số
            </td>
            <td>
                <ew:NumericBox ID="txtToSo" runat="server" Width="200px">0</ew:NumericBox>
            </td>
        </tr>
        <tr>
            <td>
                Trích yếu
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtTrichYeu" runat="server" Rows="5" TextMode="MultiLine" Width="625px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Loại văn bản
            </td>
            <td>
                <asp:DropDownList ID="ddlLoaivanban" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
            <td>
                Độ mật&nbsp;
            </td>
            <td>
                <asp:DropDownList ID="ddlDomat" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Tác giả
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtTacgia" runat="server" Width="625px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Bút tích
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtButtich" runat="server" Width="625px" Rows="2" 
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Mức độ tin cậy
            </td>
            <td>
                <asp:DropDownList ID="ddlMucDoTinCay" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
            <td>
                Thời hạn bảo quản
            </td>
            <td>
                <asp:DropDownList ID="ddlThoiHan" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Ghi chú
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtGhiChu" runat="server" Rows="5" TextMode="MultiLine" Width="625px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Tài liệu
            </td>
            <td>
                <asp:Repeater runat="server" ID="rptDocs">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("Name") %>' CommandArgument='<%#Eval("ID") %>'
                            OnClick="downloadAttach"></asp:LinkButton>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
            </td>
            <td>
                Tình trạng vật lý
            </td>
            <td>
                <asp:DropDownList ID="ddlTinhTrang" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="3">
                <asp:Button ID="btAddApprover" runat="server" Text="Thêm mới" Width="120px" OnClick="btAddApprover_Click"
                    Visible="False" />
                <asp:Button ID="btCancel" runat="server" Text="Quay lại" Width="120px" OnClick="btCancel_Click"
                    Visible="False" />
            </td>
        </tr>
    </table>
</asp:Content>
