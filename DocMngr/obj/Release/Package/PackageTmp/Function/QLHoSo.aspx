<%@ Page Title="Quản lý hồ sơ lưu trữ" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="QLHoSo.aspx.cs" Inherits="Function.QLHoSo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 116px;
        }
        .style2
        {
            width: 311px;
        }
        .style3
        {
            width: 114px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <label style="font-size: larger; font-weight: bold; margin-bottom: 10px">
            DANH SÁCH HỒ SƠ LƯU TRỮ</label>
        <div id="divInput">
            <table>
                <tr>
                    <td class="style3">
                        Chọn Phông lưu trữ
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlPhongLuuTru" runat="server" Height="25px" Width="500px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="direction: ltr">
                        <asp:Button ID="btAdd" runat="server" Text="Thêm mới" OnClick="btAdd_Click" />
                        <asp:Button ID="btDelete" runat="server" Text="Xóa" OnClick="btDelete_Click" /><span
                            style="color: White">--------</span>
                        <asp:Button ID="btSearch" runat="server" Text="Hiển thị mục lục" Width="131px" OnClick="btSearch_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="direction: ltr; background-color: #336666; color: White; font-weight: bold">
                        <asp:Label runat="server" ID="lblRecordCount" Text="Tổng số hồ sơ: " />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:HiddenField runat="server" ID="hidPageIndex" />
                        <asp:GridView ID="dgvApprover" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            Width="100%" GridLines="Horizontal" Height="67px" PageSize="40" AllowSorting="True"
                            BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px"
                            Style="text-align: left; margin-right: 46px;" ShowHeader="true" AllowPaging="true"
                            OnPageIndexChanging="dgvApprover_PageIndexChanging" OnRowDataBound="dgvApprover_RowDataBound">
                            <RowStyle BackColor="White" Font-Size="15px" ForeColor="Black" />
                            <PagerTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 70%">
                                            <asp:Label ID="MessageLabel" ForeColor="Blue" Text="Select a page:" runat="server" />
                                            <asp:DropDownList ID="PageDropDownList" AutoPostBack="true" OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged"
                                                runat="server" />
                                        </td>
                                        <td style="width: 70%; text-align: right">
                                            <asp:Label ID="CurrentPageLabel" ForeColor="Blue" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </PagerTemplate>
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID">
                                    <ItemStyle Width="10px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="stt" HeaderText="STT">
                                    <ItemStyle Width="20px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="HopSo" HeaderText="Hộp số">
                                    <ItemStyle Width="80px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="HoSoSo" HeaderText="Hồ sơ số">
                                    <ItemStyle Width="50px"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Tiêu đề" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px"
                                    Visible="true">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("TieuDe") %>' OnClick="goToDetail"
                                            CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SoLuong" HeaderText="Số tờ">
                                    <ItemStyle Width="50px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ThoiGianBatDau" HeaderText="TG bắt đầu">
                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ThoiGianKetThuc" HeaderText="TG kết thúc">
                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Chọn" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30"
                                    Visible="true">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbChoose" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sửa" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50"
                                    Visible="true">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="bt_changing" ToolTip="Sửa" runat="server" ImageUrl="~/Styles/edit.gif"
                                            Height="21px" Width="20px" CommandArgument='<%# Eval("ID") %>' BorderWidth="0"
                                            OnClick="changing" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Thêm văn bản" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50"
                                    Visible="true">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="bt_addVanBan" ToolTip="Thêm văn bản vào hồ sơ" runat="server"
                                            ImageUrl="~/Styles/book_blue_add.png" Height="21px" Width="20px" CommandArgument='<%# Eval("ID") %>'
                                            BorderWidth="0" OnClick="bt_addVanBan_Add" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#333333" />
                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" Font-Size="13px" />
                            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" Font-Size="13px"
                                HorizontalAlign="Center" />
                            <EditRowStyle Font-Size="13px" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divButtons">
        </div>
    </div>
</asp:Content>
