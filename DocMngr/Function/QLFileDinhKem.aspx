<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="QLFileDinhKem.aspx.cs" Inherits="FunctionGroup.Function.QLFileDinhKem" %>

<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register Assembly="DTPicker" Namespace="DTPicker" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        QUẢN LÝ FILE ĐÍNH KÈM</h1>
    <table>
        <tr>
            <td colspan="4">
                <label id="ldhead" runat="server">
                </label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <b>I.Thông tin hồ sơ</b>
            </td>
        </tr>
        <tr>
            <td>
                <span class="textrequire">*</span> Cơ quan lưu trữ
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlCoQuan" runat="server" Height="28px" Width="625px" Enabled="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <span class="textrequire">*</span> Phông
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlPhong" runat="server" Height="37px" Width="625px" Enabled="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Mục lục số
            </td>
            <td>
                <asp:DropDownList ID="ddlMucLuc" runat="server" Width="200px" Enabled="False">
                </asp:DropDownList>
            </td>
            <td>
                Hồ sơ số
            </td>
            <td>
                <asp:TextBox ID="txtHoSoSo" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr runat="server" id="row1VB">
            <td colspan="4">
                <b>II.Thông tin văn bản</b>
            </td>
        </tr>
        <tr runat="server" id="row2VB">
            <td>
                Ký hiệu văn bản
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtKyHieu" runat="server" Width="625px" TextMode="MultiLine" Rows="2"></asp:TextBox>
            </td>
        </tr>
        <tr runat="server" id="row3VB">
            <td>
                Số lượng tờ
            </td>
            <td colspan="3">
                <ew:NumericBox ID="txtSoluongto" runat="server" Width="200px">0</ew:NumericBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <b runat="server" id="vbTaiLieu">III. Tài liệu</b> <b runat="server" id="vbHoSo">II.
                    Tài liệu</b>
                <br />
                <asp:Repeater runat="server" ID="rptDocs">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("Name") %>' CommandArgument='<%#Eval("ID") %>'
                            OnClick="downloadAttach"></asp:LinkButton>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
                <br />
                <asp:FileUpload ID="fulAttach" Width="250px" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnComplete" runat="server" Text="Tải lên hoàn tất" OnClick="btnUpload_Click" CommandArgument="Complete" />---
                <asp:Button ID="btnUpload" runat="server" Text="Tải lên tiếp tục đính kèm" OnClick="btnUpload_Click" CommandArgument="Continue" />---
                <asp:Button ID="btnCancel" runat="server" Text="Hủy" OnClick="btnCancel_Click" />
                <asp:HiddenField ID="hidId" runat="server" />
                <asp:HiddenField ID="hidPrefix" runat="server" />
                <asp:HiddenField ID="hidRootId" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
