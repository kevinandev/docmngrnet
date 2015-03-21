<%@ Page Title="Danh sách văn bản trong hồ sơ" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="QLVanBanHoSo.aspx.cs" Inherits="FunctionGroup.Function.QLVanBanHoSo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 80px;
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
            DANH SÁCH VĂN BẢN</label>
        <div id="divInput">
            <table>
                <tr>
                    <td class="style1">
                        Phông
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlPhong" runat="server" Height="37px" Width="625px" Enabled="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Hộp
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtHop" runat="server" Width="300px" 
                            AutoCompleteType="Disabled" AutoPostBack="true"></asp:TextBox>
                        <!--<asp:Button ID="btnLoad" Text="Tải danh sách hồ sơ" runat="server" OnClick="btnLoad_Click" />-->
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Hồ sơ số
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlHoSo" runat="server" Height="37px" Width="625px" AutoPostBack="true"
                            Enabled="true" OnSelectedIndexChanged="ddlHoSo_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="btAdd" runat="server" Text="Thêm văn bản" OnClick="btAdd_Click" />
                        --
                        <asp:Button ID="btDelete" runat="server" Text="Xóa" OnClick="btDelete_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:HiddenField runat="server" ID="hidPageIndex" />
                        <asp:GridView ID="dgvApprover" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            AllowPaging="true" Width="100%" GridLines="Horizontal" Height="67px" PageSize="40"
                            AllowSorting="True" BackColor="White" BorderColor="#336666" BorderStyle="Double"
                            BorderWidth="3px" OnRowDataBound="onDgvDatabind" DataKeyNames="ID" Style="text-align: left;
                            margin-right: 46px;">
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
                                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false">
                                    <ItemStyle Width="10px"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="STT" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px"
                                    Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSTT" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="KyHieuVanBan" HeaderText="Ký hiệu văn bản">
                                    <ItemStyle Width="180px"  HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ThoiGian" HeaderText="Ngày tháng văn bản" >
                                    <ItemStyle Width="80px" HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Cơ quan ban hành" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="100px" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCoQuan" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TrichYeu" HeaderText="Trích yếu nội dung">
                                    <ItemStyle Width="220px"  HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ToSo" HeaderText="Tờ số">
                                    <ItemStyle Width="30px"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="File toàn văn" ItemStyle-HorizontalAlign="Left"
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
        <div id="divButtons">
            <asp:HiddenField ID="hidHoSoID" runat="server" />
        </div>
    </div>
</asp:Content>
