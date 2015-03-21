<%@ Page Title="Thống kê tài liệu" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ThongKeTaiLieu.aspx.cs" Inherits="Function.ThongKeTaiLieu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table width="90%">
            <tr>
                <td colspan="4">
                    <b>THỐNG KÊ TÀI LIỆU</b>
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
                    <asp:DropDownList ID="ddlPhong" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Ký hiệu văn bản
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtKiHieuVanBan" runat="server" Width="600px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Tình trạng vật lý
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlTinhTrangVatLy" runat="server" Width="200px">
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
                    &nbsp;
                </td>
                <td colspan="3">
                    <asp:Button ID="btTimKiem" runat="server" Text="Tìm Kiếm" Width="120px" OnClick="btTimKiem_Click" />
                    <asp:Button ID="btDelete" runat="server" Text="Xóa" Width="120px" OnClick="btDelete_Click" />
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
                        BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" ShowHeader="true">
                        <RowStyle BackColor="White" Font-Size="15px" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="stt" HeaderText="STT">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="KyHieuVanBan" HeaderText="Ký hiệu văn bản">
                                <ItemStyle Width="170px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SoLuongTo" HeaderText="Số lượng tờ">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="KiHieuThongTin" HeaderText="Ký hiệu thông tin">
                                <ItemStyle Width="170px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="LoaiVanBanv" HeaderText="Loại văn bản">
                                <ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="MucDoTinCayv" HeaderText="Mức độ tin cậy">
                                <ItemStyle Width="120px"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Chọn" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30"
                                Visible="true">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbChoose" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sửa" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"
                                Visible="true">
                                <ItemTemplate>
                                    <asp:ImageButton ID="bt_changing" ToolTip="Chỉnh sửa" runat="server" ImageUrl="~/Styles/Edit.gif"
                                        Height="20px" Width="20px" CommandArgument='<%# Eval("ID") %>' BorderWidth="0"
                                        OnClick="changing" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Chi tiết" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"
                                Visible="true">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btView" ToolTip="Chỉnh sửa" runat="server" ImageUrl="~/Styles/icon_view.gif"
                                        Height="20px" Width="20px" CommandArgument='<%# Eval("ID") %>' BorderWidth="0"
                                        OnClick="viewing" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
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
