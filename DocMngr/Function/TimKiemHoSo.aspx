<%@ Page Title="Tìm kiếm hồ sơ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="TimKiemHoso.aspx.cs" Inherits="Function.TimKiemHoso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="../Scripts/calendar-en.min.js" type="text/javascript"></script>
    <link href="../Styles/calendar-blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtThoiGianStartFrom.ClientID %>").dynDateTime({
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

        $(document).ready(function () {
            $("#<%=txtThoiGianStartTo.ClientID %>").dynDateTime({
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
        $(document).ready(function () {
            $("#<%=txtThoiGianEndFrom.ClientID %>").dynDateTime({
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

        $(document).ready(function () {
            $("#<%=txtThoiGianEndTo.ClientID %>").dynDateTime({
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <table width="90%">
            <tr>
                <td colspan="4">
                    <b>TÌM KIẾM HỒ SƠ</b>
                </td>
            </tr>
            <tr>
                <td>
                    Cơ quan lưu trữ
                </td>
                <td>
                    <asp:DropDownList ID="ddlCoQuan" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
                <td>
                    Phông
                </td>
                <td>
                    <asp:DropDownList ID="ddlPhong" Width="200px" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Thời hạn bảo quản
                </td>
                <td>
                    <asp:DropDownList ID="ddlThoiHan" Width="200px" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    Tình trạng vật lý
                </td>
                <td>
                    <asp:DropDownList ID="ddlTinhTrangVatLy" Width="200px" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Mục lục
                </td>
                <td>
                    <asp:DropDownList ID="ddlMucluc" Width="200px" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    Chế độ sử dụng
                </td>
                <td>
                    <asp:DropDownList ID="ddlCheDoSuDung" Width="200px" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Thời gian bắt đầu từ ngày
                </td>
                <td>
                    <asp:TextBox ID="txtThoiGianStartFrom" runat="server" Width="200px"></asp:TextBox>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Styles/calender.png" />
                </td>
                <td>
                    Đến ngày
                </td>
                <td>
                    <asp:TextBox ID="txtThoiGianStartTo" runat="server" Width="200px"></asp:TextBox>
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Styles/calender.png" />
                </td>
            </tr>
            <tr>
                <td>
                    Thời gian kết thúc từ ngày
                </td>
                <td>
                    <asp:TextBox ID="txtThoiGianEndFrom" runat="server" Width="200px"></asp:TextBox>
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Styles/calender.png" />
                </td>
                <td>
                    Đến ngày
                </td>
                <td>
                    <asp:TextBox ID="txtThoiGianEndTo" runat="server" Width="200px"></asp:TextBox>
                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Styles/calender.png" />
                </td>
            </tr>
            <tr>
                <td>
                    Từ khóa
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtKeyword" runat="server" Width="650px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="3">
                    <asp:Button ID="btTimKiem" runat="server" Text="Tìm Kiếm" Width="120px" OnClick="btTimKiem_Click" />
                    <asp:Button ID="btexport" runat="server" Text="Xuất Excel" Width="120px" OnClick="btexport_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="dgrResult" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Width="100%" GridLines="Horizontal" Height="67px" PageSize="40" AllowSorting="True"
                        BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px">
                        <RowStyle BackColor="White" Font-Size="15px" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" Visible="false">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="stt" HeaderText="STT">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="MaPhong" HeaderText="Phông số">
                                <ItemStyle Width="70px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="HopSo" HeaderText="Hộp số">
                                <ItemStyle Width="70px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="HoSoSo" HeaderText="Hồ sơ số">
                                <ItemStyle Width="70px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TieuDe" HeaderText="Tiêu Đề">
                                <ItemStyle Width="300px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ThoiGianBatDau" HeaderText="Từ ngày">
                                <ItemStyle Width="80px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ThoiGianKetThuc" HeaderText="Đến ngày">
                                <ItemStyle Width="80px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SoLuong" HeaderText="Số tờ">
                                <ItemStyle Width="30px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ThoiHanBaoQuanv" HeaderText="Thời hạn bảo quản">
                                <ItemStyle Width="80px"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Chi tiết" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"
                                Visible="true">
                                <ItemTemplate>
                                    <asp:ImageButton ID="bt_changing" ToolTip="Chi tiết" runat="server" ImageUrl="../Styles/icon_view.gif"
                                        Height="20px" Width="20px" CommandArgument='<%# Eval("ID") %>' BorderWidth="0"
                                        OnClick="changing" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" Font-Size="9px" />
                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" Font-Size="12px" />
                        <EditRowStyle Font-Size="9px" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
