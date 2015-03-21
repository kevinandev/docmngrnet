<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UserManagement.aspx.cs" Inherits="DocMngr.UserManagement" %>

<asp:Content ID="headContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divTop">
        <fieldset class="register">
            <legend>Thông tin user</legend>
            <div style="width: 50%; height: 200px; overflow: auto; float: left">
                <p>
                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Tên đăng nhập:</asp:Label>
                    <asp:TextBox ID="TxtUserName" runat="server" CssClass="textEntry"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                        CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                        ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                    <asp:TextBox ID="TxtEmail" runat="server" CssClass="textEntry"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                        CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required."
                        ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                    <asp:TextBox ID="TxtPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                        CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                        ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                    <asp:TextBox ID="TxtConfirmPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" CssClass="failureNotification"
                        Display="Dynamic" ErrorMessage="Confirm Password is required." ID="ConfirmPasswordRequired"
                        runat="server" ToolTip="Confirm Password is required." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="TxtPasswordCompare" runat="server" ControlToCompare="Password"
                        ControlToValidate="ConfirmPassword" CssClass="failureNotification" Display="Dynamic"
                        ErrorMessage="The Password and Confirmation Password must match." ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>
                </p>
            </div>
            <div style="width: 50%; height: 200px; overflow: auto; float: right">
                <p>
                    <asp:Label ID="LblFullName" runat="server" AssociatedControlID="TxtFullName">Họ và tên:</asp:Label>
                    <asp:TextBox ID="TxtFullName" runat="server" CssClass="textEntry"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ValidateFullName" runat="server" ControlToValidate="TxtFullName"
                        CssClass="failureNotification" ErrorMessage="Bắt buộc nhập họ và tên." ToolTip="Bắt buộc nhập họ và tên."
                        ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:Label ID="LblUnit" runat="server" AssociatedControlID="DrpUnit">Đơn vị:</asp:Label>
                    <asp:DropDownList ID="DrpUnit" runat="server" CssClass="textEntry">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="DrpUnitRequired" runat="server" ControlToValidate="DrpUnit"
                        CssClass="failureNotification" ErrorMessage="Chưa chọn đơn vị" ToolTip="Bắt buộc nhập đơn vị"
                        ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:Label ID="LblRole" runat="server" AssociatedControlID="DrpRole">Vai trò:</asp:Label>
                    <asp:DropDownList ID="DrpRole" runat="server" CssClass="textEntry">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="DrpRoleRequire" runat="server" ControlToValidate="DrpRole"
                        CssClass="failureNotification" ErrorMessage="Chưa chọn vai trò" ToolTip="Bắt buộc nhập vai trò"
                        ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:Label ID="LblActive" runat="server" AssociatedControlID="ChkActive">Kích hoạt:</asp:Label>
                    <asp:CheckBox ID="ChkActive" runat="server" />
                </p>
            </div>
        </fieldset>
        <p class="submitButton">
            <asp:Button ID="BtnResetPassword" runat="server" Text="Reset password" ValidationGroup="RegisterUserValidationGroup" />-----<asp:Button
                ID="BtnAdd" runat="server" Text="Thêm mới" ValidationGroup="RegisterUserValidationGroup" />
            <asp:Button ID="BtnEdit" runat="server" Text="Sửa" ValidationGroup="RegisterUserValidationGroup" />
            -----<asp:Button ID="btnSave" runat="server" Text="Lưu" ValidationGroup="RegisterUserValidationGroup" />
            <asp:Button ID="btnCancel" runat="server" Text="Hủy" ValidationGroup="RegisterUserValidationGroup" />
        </p>
    </div>
    <div id="divBottom">
        <asp:SqlDataSource ID="DscUser" runat="server" ConnectionString="<%$ ConnectionStrings:appDB %>"
            SelectCommand="select aU.UserName,aU.UserId,uE.full_name, mb.Email, uE.address, uE.fax, uE.mobile, uE.regency, uE.tel, u.name Unit_name
from aspnet_Users aU ,dbo.user_extend uE
left join unit u on uE.unit_id=uE.id
left join aspnet_Membership mb on uE.user_id = mb.UserId
where aU.UserId=uE.user_id"></asp:SqlDataSource>
        <asp:GridView ID="grdUser" runat="server" AutoGenerateColumns="false" AutoGenerateDeleteButton="false"
            AutoGenerateEditButton="false" AutoGenerateSelectButton="false" DataKeyNames="UserId"
            DataSourceID="DscUser" Width="100%">
            <Columns>
                <asp:BoundField DataField="UserName" HeaderText="User Name" InsertVisible="False"
                    ReadOnly="True" SortExpression="UserName" ItemStyle-Width="15%" />
                <asp:BoundField DataField="full_name" ItemStyle-Width="25%" HeaderText="Họ và tên"
                    SortExpression="full_name" />
                <asp:BoundField DataField="Unit_name" ItemStyle-Width="30%" HeaderText="Đơn vị" SortExpression="Unit_name" />
                <asp:BoundField DataField="tel" HeaderText="Điện thoại" SortExpression="tel" />
                <asp:BoundField DataField="UserID" Visible="false" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
