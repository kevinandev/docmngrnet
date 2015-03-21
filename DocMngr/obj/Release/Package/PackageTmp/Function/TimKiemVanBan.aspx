<%@ Page Title="Tìm kiếm văn bản" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="TimKiemVanBan.aspx.cs" Inherits="Function.TimKiemVanBan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="../Scripts/calendar-en.min.js" type="text/javascript"></script>
    <link href="../Styles/calendar-blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtThoiGianFrom.ClientID %>").dynDateTime({
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
            $("#<%=txtThoiGianTo.ClientID %>").dynDateTime({
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
                    <b>TÌM KIẾM VĂN BẢN</b>
                </td>
            </tr>
            <tr>
                <td>
                    Cơ quan lưu trữ
                </td>
                <td>
                    <asp:DropDownList ID="ddlCoQuan" runat="server" Height="20px" Width="200px">
                    </asp:DropDownList>
                </td>
                <td>
                    Phông
                </td>
                <td>
                    <asp:DropDownList ID="ddlPhong" runat="server" Height="20px" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Loại văn bản
                </td>
                <td>
                    <asp:DropDownList ID="ddlLoaiVanBan" runat="server" Height="20px" Width="200px">
                    </asp:DropDownList>
                </td>
                <td>
                    Tình trạng vật lý
                </td>
                <td>
                    <asp:DropDownList ID="ddlTinhTrangVatLy" runat="server" Height="20px" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Số kí hiệu
                </td>
                <td>
                    <asp:TextBox ID="txtKyHieuVanBan" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td>
                    Tác giả
                </td>
                <td>
                    <asp:TextBox ID="txtTacGia" runat="server" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Thời gian từ ngày
                </td>
                <td>
                    <asp:TextBox ID="txtThoiGianFrom" runat="server" Width="200px"></asp:TextBox>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Styles/calender.png" />
                </td>
                <td>
                    Đến ngày
                </td>
                <td>
                    <asp:TextBox ID="txtThoiGianTo" runat="server" Width="200px"></asp:TextBox>
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Styles/calender.png" />
                </td>
            </tr>
            <tr>
                <td>
                    Từ khóa
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtKeyword" runat="server" Width="610px"></asp:TextBox>
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
                        BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px"
                        OnRowDataBound="onDgvDatabind">
                        <RowStyle BackColor="White" Font-Size="15px" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" Visible="false">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="stt" HeaderText="STT">
                                <ItemStyle Width="30px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="MaPhong" HeaderText="Phông số" Visible="false">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="HopSo" HeaderText="Hộp số" Visible="false">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="HoSoSo" HeaderText="Hồ sơ số" Visible="false">
                                <ItemStyle Width="70px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="KyHieuVanBan" HeaderText="Ký hiệu văn bản">
                                <ItemStyle Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ThoiGian" HeaderText="Ngày tháng">
                                <ItemStyle Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TrichYeu" HeaderText="Trích yếu nội dung">
                                <ItemStyle Width="250px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TenCoQuan" HeaderText="Cơ quan ban hành">
                                <ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ToSo" HeaderText="Tờ số">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="File toàn văn" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="200px" Visible="true">
                                <ItemTemplate>
                                    <asp:Repeater ID="rptAttachment" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("Name") %>' CommandArgument='<%#Eval("ID") %>'
                                                OnClick="downloadAttach"></asp:LinkButton>
                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                            </asp:TemplateField>
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
