<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="QLLinhVuc.aspx.cs" Inherits="FunctionGroup.Picklist.QLLinhVuc" %>

<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <label style="font-size: larger; font-weight: bold; margin-bottom: 10px">
            QUẢN LÝ LĨNH VỰC TÀI LIỆU
        </label>
        <div id="divInput">
            <table>
                <tr>
                    <td>
                        <span class="textrequire">*</span>Mã
                    </td>
                    <td>
                        <asp:TextBox ID="txtCode" runat="server" Width="408px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="textrequire">*</span>Tên
                    </td>
                    <td>
                        <asp:TextBox ID="tbxName" runat="server" Width="408px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="textrequire">*</span>Mô tả
                    </td>
                    <td>
                        <asp:TextBox ID="tbxDescription" runat="server" Width="752px" TextMode="MultiLine"
                            Rows="2" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btAddApprover" runat="server" Text="Thêm mới" Width="100px" OnClick="btAddApprover_Click" />
                        <asp:Button ID="btDelete" runat="server" Text="Xóa" Width="100px" OnClick="btDelete_Click" />
                        <asp:Button ID="btCancel" runat="server" Text="Hủy bỏ" Width="100px" OnClick="btCancel_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="dgvApprover" runat="server" AutoGenerateColumns="False" PageSize="20"
                            AllowSorting="True" AllowPaging="True" DataKeyNames="ID" BorderWidth="1px" ShowHeader="true">
                            <RowStyle BackColor="#F7F7DE" Font-Size="14px" />
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="" Visible="false">
                                    <ItemStyle Width="50" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="STT" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSTT" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Chọn" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbChoose" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sửa" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="bt_changing" ToolTip="Sửa" runat="server" ImageUrl="~/Styles/edit.gif"
                                            Height="21px" Width="20px" CommandArgument='<%# Eval("ID") %>' BorderWidth="0"
                                            OnClick="editApprover_Click" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Code" HeaderText="Mã">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Name" HeaderText="Tên">
                                    <ItemStyle Width="180px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Description" HeaderText="Mô tả">
                                    <ItemStyle Width="300px"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" BorderWidth="1px" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" Font-Size="9px" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" Font-Size="14px" />
                            <EditRowStyle Font-Size="9px" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divButtons">
        </div>
    </div>
</asp:Content>
