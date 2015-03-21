<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="QLMenu.aspx.cs" Inherits="Function.QLMenu" %>

<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <label style="font-size: larger; font-weight: bold; margin-bottom: 10px">
            QUẢN LÝ CHỨC NĂNG
        </label>
        <div id="divInput">
            <table>
                <col width="15%" />
                <col width="35%" />
                <col width="15%" />
                <col width="35%" />
                <tr>
                    <td>
                        <span class="textrequire">*</span>Mã
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtCode" runat="server" Width="408px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="textrequire">*</span>Tên
                    </td>
                    <td>
                        <asp:TextBox ID="tbxName" runat="server" Width="250px" />
                    </td>
                    <td>
                        <span class="textrequire">*</span>Loại menu
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlType" Width="200px">
                            <asp:ListItem Value="1">Menu con</asp:ListItem>
                            <asp:ListItem Value="0">Menu gốc</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr runat="server" id="rowMasterId">
                    <td>
                        <span class="textrequire">*</span>Menu cha
                    </td>
                    <td colspan="3">
                        <asp:DropDownList runat="server" ID="ddlMaster" Width="408px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="textrequire">*</span>Đường dẫn
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="tbxUrl" runat="server" Width="408px" TextMode="SingleLine" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="textrequire">*</span>Sắp xếp
                    </td>
                    <td colspan="3">
                        <ew:NumericBox ID="txtOrder" runat="server" Width="408px">0</ew:NumericBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="btAddApprover" runat="server" Text="Thêm mới" Width="100px" OnClick="btAddApprover_Click" />
                        <asp:Button ID="btDelete" runat="server" Text="Xóa" Width="100px" OnClick="btDelete_Click" />
                        <asp:Button ID="btCancel" runat="server" Text="Hủy bỏ" Width="100px" OnClick="btCancel_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="dgvApprover" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                            AllowPaging="false" DataKeyNames="id" BorderWidth="1px" OnRowDataBound="dgvOnDataBind" ShowHeader="true">
                            <RowStyle BackColor="#F7F7DE" Font-Size="15px" />
                            <Columns>
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
                                            Height="21px" Width="20px" CommandArgument='<%# Eval("id") %>' BorderWidth="0"
                                            OnClick="editApprover_Click" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="code" HeaderText="Mã">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="text" HeaderText="Tên">
                                    <ItemStyle Width="180px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Menu cha" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaster" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="250px"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" BorderWidth="1px" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" Font-Size="13px" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" Font-Size="14px" />
                            <EditRowStyle Font-Size="13px" />
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
