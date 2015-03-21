<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="TimKiemCacVanBan.aspx.cs" Inherits="Function.TimKiemCacVanBan" %>

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
                    TÌM KIẾM VĂN BẢN
                </td>
            </tr>
            <tr>
                <td>
                    Cơ quan lưu trữ
                </td>
                <td>
                    <asp:DropDownList ID="ddlCoQuan" runat="server" Height="16px" Width="176px">
                    </asp:DropDownList>
                </td>
                <td>
                    Phông
                </td>
                <td>
                    <asp:DropDownList ID="ddlPhong" runat="server" Height="16px" Width="179px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Loại văn bản
                </td>
                <td>
                    <asp:DropDownList ID="ddlThoiHan" runat="server" Height="16px" Width="173px">
                    </asp:DropDownList>
                </td>
                <td>
                    Tình trạng vật lý
                </td>
                <td>
                    <asp:DropDownList ID="ddlTinhTrangVatLy" runat="server" Height="16px" Width="181px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Số kí hiệu
                </td>
                <td>
                    <asp:TextBox ID="txtKyHieuVanBan" runat="server" Width="267px"></asp:TextBox>
                </td>
                <td>
                    Tác giả
                </td>
                <td>
                    <asp:TextBox ID="txtTacGia" runat="server" Width="344px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Thời gian từ ngày
                </td>
                <td>
                    <asp:TextBox ID="txtThoiGianFrom" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Styles/calender.png" />
                </td>
                <td>
                    Đến ngày
                </td>
                <td>
                    <asp:TextBox ID="txtThoiGianTo" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Styles/calender.png" />
                </td>
            </tr>
            <tr>
                <td>
                    Từ khóa
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtKeyword" runat="server" Width="750px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="3">
                    <asp:Button ID="btTimKiem" runat="server" Text="Tìm Kiếm" Width="142px" OnClick="btTimKiem_Click" />
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
                        <RowStyle BackColor="White" Font-Size="12px" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="stt" HeaderText="STT">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="KyHieuVanBan" HeaderText="Số văn bản">
                                <ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ThoiGian" HeaderText="Thời gian">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="MaPhong" HeaderText="Mã phông">
                                <ItemStyle Width="70px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="MucLucSo" HeaderText="Mục lục số">
                                <ItemStyle Width="70px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="LoaiVanBan" HeaderText="Loại văn bản">
                                <ItemStyle Width="130px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="HoSoSo" HeaderText="Hồ sơ số">
                                <ItemStyle Width="70px"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Chi tiết" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"
                                Visible="true">
                                <ItemTemplate>
                                    <asp:ImageButton ID="bt_changing" ToolTip="Chi tiết" runat="server" ImageUrl="~/images/btn_Capnhat.jpg"
                                        Height="23px" Width="73px" CommandArgument='<%# Eval("ID") %>' BorderWidth="0"
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
