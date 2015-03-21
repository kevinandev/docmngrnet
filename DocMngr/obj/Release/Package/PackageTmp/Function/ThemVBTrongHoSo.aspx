<%@ Page Title="Thêm văn bản vào hồ sơ " Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="ThemVBTrongHoSo.aspx.cs" Inherits="Function.ThemVBTrongHoSo" %>

<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register Assembly="DTPicker" Namespace="DTPicker" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="../Scripts/calendar-en.min.js" type="text/javascript"></script>
    <link href="../Styles/calendar-blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtThoiGian.ClientID %>").dynDateTime({
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
                <asp:DropDownList ID="ddlCoQuan" runat="server" Height="28px" Width="600px" Enabled="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <span class="textrequire">*</span> Phông
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlPhong" runat="server" Height="37px" Width="600px" Enabled="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td>
                Mục lục số
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtMucLuc" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td>
                Hồ sơ số
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtHoSoSo" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
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
            <td colspan="3">
                <asp:TextBox ID="txtKyHieu" runat="server" Width="600px" Rows="2" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Số lượng tờ
            </td>
            <td colspan="3">
                <ew:NumericBox ID="txtSoluongto" runat="server" Width="200px">0</ew:NumericBox>
            </td>
        </tr>
        <tr>
            <td>
                Thời gian
            </td>
            <td>
                <asp:TextBox ID="txtThoiGian" runat="server" Width="200px"></asp:TextBox>
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
                <asp:TextBox ID="txtTrichYeu" runat="server" Rows="5" TextMode="MultiLine" Width="600px"></asp:TextBox>
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
                <asp:TextBox ID="txtTacgia" runat="server" Width="600px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Bút tích
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtButtich" runat="server" Width="600px" Rows="3" TextMode="MultiLine"></asp:TextBox>
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
                <asp:TextBox ID="txtGhiChu" runat="server" Rows="5" TextMode="MultiLine" Width="600px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Tài liệu
            </td>
            <td>
                <asp:FileUpload ID="FileUpload1" Width="250px" runat="server" />
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
            <td colspan="4">
                <asp:GridView ID="dgvAttachment" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    AllowPaging="false" Width="50%" GridLines="Horizontal" Height="67px" AllowSorting="True"
                    BackColor="White" BorderColor="#336666" BorderStyle="None" BorderWidth="0px"
                    DataKeyNames="ID" Style="text-align: left; margin-right: 46px;" OnRowDataBound="dgvAttachment_RowDataBound">
                    <RowStyle BackColor="White" Font-Size="15px" ForeColor="Black" />
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="false">
                            <ItemStyle Width="10px"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="STT" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px"
                            Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblSTT" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tên TL" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px"
                            Visible="true">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("Name") %>' CommandArgument='<%#Eval("ID") %>'
                                    OnClick="downloadAttach"></asp:LinkButton>--
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Xóa" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px"
                            Visible="true">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkDeleteAttachment" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" Font-Size="9px" />
                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" Font-Size="12px" />
                    <EditRowStyle Font-Size="9px" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="3">
                <asp:Button ID="btnComplete" runat="server" Text="Hoàn tất" Width="120px" OnClick="btAddApprover_Click"
                    CommandArgument="Complete" />
                <asp:Button ID="btAddApprover" runat="server" Text="Tiếp tục đính kèm" Width="180px"
                    OnClick="btAddApprover_Click" CommandArgument="Continue" />
                <asp:Button ID="btCancel" runat="server" Text="Quay lại" Width="120px" OnClick="btCancel_Click" />
                <asp:HiddenField ID="hidHoSo" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
