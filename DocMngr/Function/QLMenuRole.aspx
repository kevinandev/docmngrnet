<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="QLMenuRole.aspx.cs" Inherits="Function.QLMenuRole" %>

<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <label style="font-size: larger; font-weight: bold; margin-bottom: 10px">
            QUẢN LÝ PHÂN QUYỀN MENU
        </label>
        <div id="divInput">
            <table>
                <col width="15%" />
                <col width="35%" />
                <col width="15%" />
                <col width="35%" />
                <tr>
                    <td>
                        <span class="textrequire">*</span>Menu
                    </td>
                    <td colspan="3">
                        <asp:DropDownList runat="server" ID="ddlMenu" Width="300px">
                        </asp:DropDownList>
                        <br />
                        <asp:Button ID="btnMenuSubmit" runat="server" Text="Tìm kiếm" OnClick="btnMenuSubmit_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="textrequire">*</span>Vai trò truy cập
                    </td>
                    <td colspan="3">
                        <asp:DropDownList runat="server" ID="ddlRole" Width="300px">
                        </asp:DropDownList>
                        <br />
                        <asp:Button ID="btnRoleAdd" runat="server" Text="Thêm role vào menu" OnClick="btnRoleAdd_Click" />
                        <asp:Button ID="btnRoleRemove" runat="server" Text="Xóa role khỏi menu" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        Danh sách role:
                        <br />
                        <asp:GridView ID="grdRoleOfMenu" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                            AllowPaging="false" DataKeyNames="RoleId" BorderWidth="1px" OnRowDataBound="dgvOnDataBind"
                            ShowHeader="true">
                            <RowStyle BackColor="#F7F7DE" Font-Size="15px" />
                            <Columns>
                                <asp:TemplateField HeaderText="STT" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSTT" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="RoleName" HeaderText="Phân quyền">
                                    <ItemStyle Width="400px" />
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
