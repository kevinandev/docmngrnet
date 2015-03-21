<%@ Page Title="Quản lý thời hạn bảo quản" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QLThoiHanBaoQuan.aspx.cs" Inherits="Picklist.QLThoiHanBaoQuan" %>
<%@ Register assembly="eWorld.UI" namespace="eWorld.UI" tagprefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

  <div>
        <label style="font-size: larger; font-weight:bold; margin-bottom: 10px">QUẢN LÝ DANH MỤC LOẠI THỜI HẠN BẢO QUẢN </label>
        <div id="divInput">
        <table>
                <tr>
					<td><span class="textrequire">*</span>Tên</td>
					<td><asp:TextBox ID="tbxName"  runat="server"  Width="408px" />
          
                    </td>
                </tr>
				<tr>
					<td><span class="textrequire">*</span>Mô tả</td>
					<td><asp:TextBox ID="tbxDescription" runat="server" Width="752px" /></td>
                </tr>
                <tr>                
                <td colspan="2">
                <asp:Button ID="btAddApprover" runat="server" Text="Thêm mới" Width="100px" 
				onclick="btAddApprover_Click" />
				<asp:Button ID="btDelete" runat="server" Text="Xóa" Width="100px" 
                        onclick="btDelete_Click" />
			<asp:Button ID="btCancel" runat="server" Text="Hủy bỏ" Width="100px" 
				onclick="btCancel_Click" />
                </td>
                </tr>
                <tr>
                <td colspan="2">
                <asp:GridView ID="dgvApprover" runat="server" AutoGenerateColumns="False"
                PageSize="20" AllowSorting="True" AllowPaging="True" 
					onpageindexchanging="dgvApprover_PageIndexChanging">			
                <RowStyle BackColor="#F7F7DE" Font-Size="14px" />
                <Columns>
                 <asp:BoundField DataField="ID" HeaderText="" >
                        <ItemStyle Width="50" />
                    </asp:BoundField>
                <asp:BoundField DataField="stt" HeaderText="STT" >
                        <ItemStyle Width="50" />
                    </asp:BoundField>  
                    <asp:TemplateField HeaderText="Chọn" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="50">
						<ItemTemplate>
							<asp:CheckBox ID="cbChoose" runat="server" />                          
						</ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
					</asp:TemplateField>	
									
					<asp:TemplateField HeaderText="Sửa" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="50">
						<ItemTemplate>
							 <asp:ImageButton ID="bt_changing" ToolTip ="Sửa" runat="server" ImageUrl="~/Styles/edit.gif" Height="21px" Width="20px"  CommandArgument = '<%# Eval("ID") %>' BorderWidth="0" OnClick = "editApprover_Click" />
						</ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
					</asp:TemplateField>					
					
                    <asp:BoundField DataField="Name" HeaderText="Tên" >
                        <ItemStyle Width="180px" />
                    </asp:BoundField>     
                    <asp:BoundField DataField="Description" HeaderText="Mô tả" >
                        <ItemStyle Width="300px"></ItemStyle>
                    </asp:BoundField>
             					
                </Columns>
                <FooterStyle BackColor="#CCCC99" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" 
                    Font-Size="9px" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" 
                    Font-Size="14px" />
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
