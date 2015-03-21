<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FunctionManage.aspx.cs" Inherits="DocMngr.FunctionManage" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <span id="Notification" class="failureNotification" runat="server">
        <asp:Literal ID="Message" runat="server"></asp:Literal>
    </span>
    <div id="divTop">
        <fieldset class="register">
            <legend>Thông tin chức năng</legend>
            <div style="width: 100%; height: 200px; overflow: auto; float: left">
                <p>
                    <asp:Label ID="FunctionNameLabel" runat="server" AssociatedControlID="TxtFunctionName">Tên chức năng:</asp:Label>
                    <asp:TextBox ID="TxtFunctionName" runat="server" CssClass="textEntry"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="FunctionNameRequired" runat="server" ControlToValidate="TxtFunctionName"
                        CssClass="failureNotification" ErrorMessage="Bắt buộc nhập tên chức năng" ToolTip="Bắt buộc nhập tên chức năng"
                        ValidationGroup="functionValidationGr">*</asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:Label ID="FunctionCodeLabel" runat="server" AssociatedControlID="TxtFunctionCode">Mã chức năng:</asp:Label>
                    <asp:TextBox ID="TxtFunctionCode" runat="server" CssClass="textEntry"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="FuctionCodeRequire" runat="server" ControlToValidate="TxtFunctionCode"
                        CssClass="failureNotification" ErrorMessage="Bắt buộc nhập mã chức năng." ToolTip="Bắt buộc nhập mã chức năng."
                        ValidationGroup="functionValidationGr">*</asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:Label ID="LblPath" runat="server" AssociatedControlID="TxtPath">Đường dẫn:</asp:Label>
                    <asp:TextBox ID="TxtPath" runat="server" CssClass="textEntry"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtPath"
                        CssClass="failureNotification" ErrorMessage="Bắt buộc nhập đường dẫn." ToolTip="Bắt buộc nhập đường dẫn."
                        ValidationGroup="functionValidationGr">*</asp:RequiredFieldValidator>
                </p>
            </div>
        </fieldset>
        <p class="submitButton">
            <asp:Button ID="BtnAdd" runat="server" Text="Thêm mới" />
            <asp:Button ID="BtnEdit" runat="server" Text="Sửa" />
            <asp:Button ID="BtnDelete" runat="server" Text="Xóa" />
            -----<asp:Button ID="btnSave" runat="server" Text="Lưu" ValidationGroup="functionValidationGr" />
            <asp:Button ID="btnCancel" runat="server" Text="Hủy" />
        </p>
    </div>
    <div id="divBottom">
        <asp:SqlDataSource ID="DscFunction" runat="server" ConnectionString="<%$ ConnectionStrings:appDB %>"
            SelectCommand="select row_number() over (order by function_id) as row_num,function_id, function_code, function_name, function_path, 
(case when active = 0 then 'FALSE' else 'TRUE' end) as active from functions"></asp:SqlDataSource>
        <asp:GridView ID="tblFunction" runat="server" AutoGenerateColumns="False" CellPadding="4"
            Width="100%" GridLines="Horizontal" Height="67px" PageSize="40" AllowSorting="True"
            BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px"
            OnSelectedIndexChanged="onSelectedRow" OnRowDataBound="onDataBound">
            <RowStyle BackColor="White" Font-Size="12px" ForeColor="#333333" />
            <Columns>
                <asp:TemplateField HeaderText="STT">
                    <ItemStyle Width="10%"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblRowNum" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mã chức năng">
                    <ItemStyle Width="20%"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblFunctionCode" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tên">
                    <ItemStyle Width="30%"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblFunctionName" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Đường dẫn">
                    <ItemStyle Width="30%"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblFunctionPath" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Hoạt động">
                    <ItemStyle Width="10%"></ItemStyle>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkActive" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#006BB9" />
            <PagerStyle BackColor="#006BB9" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#006BB9" Font-Bold="True" ForeColor="White" Font-Size="12px" />
            <HeaderStyle BackColor="#006BB9" Font-Bold="True" ForeColor="White" Font-Size="12px" />
            <EditRowStyle Font-Size="9px" />
        </asp:GridView>
        </td>
    </div>
</asp:Content>
