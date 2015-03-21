<%@ Page Title="Thêm mới/xem hồ sơ" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="ThemMoiHoSo.aspx.cs" Inherits="Function.ThemMoiHoSo" %>

<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register Assembly="DTPicker" Namespace="DTPicker" TagPrefix="cc1" %>
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
            <tr>
                <td colspan="4">
                    <label id="ldhead" runat="server">
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="textrequire">*</span> Cơ quan lưu trữ
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlCoQuan" runat="server" Height="28px" Width="625px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="textrequire">*</span> Phông
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlPhong" runat="server" Height="37px" Width="625px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr runat="server" visible="false">
                <td>
                    Mục lục số
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtMucLuc" runat="server" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Hộp số
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtHopSo" runat="server" Width="200px"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td>
                    Hồ sơ số:
                </td>
                <td>
                    <asp:TextBox ID="txtHoSoSo" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td>
                    Ngôn ngữ
                </td>
                <td>
                    <asp:DropDownList ID="ddlNgonNgu" runat="server" Height="20px" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Ký hiệu thông tin
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtKyHieu" runat="server" Width="625px" TextMode="MultiLine" Rows="2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="textrequire">*</span> Tiêu đề hồ sơ
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtTieude" runat="server" Rows="5" TextMode="MultiLine" Width="625px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Chú giải
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtGhiChu" runat="server" Rows="5" TextMode="MultiLine" Width="625px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Thời gian bắt đầu
                </td>
                <td>
                    <asp:TextBox ID="txtThoiGianBatDau" runat="server" Width="200px"></asp:TextBox>
                    <img src="../Styles/calender.png" />
                </td>
                <td>
                    Thời gian kết thúc
                </td>
                <td>
                    <asp:TextBox ID="txtThoiGiankT" runat="server" Width="200px"></asp:TextBox>
                    <img src="../Styles/calender.png" />
                </td>
            </tr>
            <tr>
                <td>
                    Bút tích
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtButtich" runat="server" Width="625px" TextMode="MultiLine" Rows="2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Số lượng tờ
                </td>
                <td>
                    <ew:NumericBox ID="txtSoluongto" runat="server" Width="200px">0</ew:NumericBox>
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
                    Chế độ SD
                </td>
                <td>
                    <asp:DropDownList ID="ddlChedoSD" runat="server" Width="200px">
                    </asp:DropDownList>
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
                    Tài liệu:
                    <asp:FileUpload ID="fulAttach" Width="250px" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="dgvAttachment" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        AllowPaging="false" Width="50%" GridLines="Horizontal" Height="67px" AllowSorting="True"
                        BackColor="White" BorderColor="#336666" BorderStyle="None" BorderWidth="0px"
                        DataKeyNames="ID" Style="text-align: left; margin-right: 46px;" 
                        onrowdatabound="dgvAttachment_RowDataBound">
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
                            <asp:TemplateField HeaderText="Xóa" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="50px" Visible="true">
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
                <td colspan="4">
                    <asp:CheckBox ID="chkUploadMore" runat="server" />Tiếp tục đính kèm---
                    <asp:HiddenField ID="hidIdPhong" runat="server" />
                    <asp:Button ID="btAddApprover" runat="server" Text="Thêm mới" Width="120px" OnClick="btAddApprover_Click" />
                    <asp:Button ID="btCancel" runat="server" Text="Quay lại" Width="120px" OnClick="btCancel_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
