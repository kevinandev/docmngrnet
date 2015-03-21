<%@ Page Title="Quản lý phông lưu trữ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QLPhongLuuTru.aspx.cs" Inherits="Function.QLPhongLuuTru" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 142px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <label style="font-size: larger; font-weight:bold; margin-bottom: 10px">QUẢN LÝ PHÔNG LƯU TRỮ</label>
        <div id="divInput">
            <table>
                <tr>
					<td class="style1">Cơ quan lưu trữ:</td>
					<td> 
                        <asp:DropDownList ID="ddlCoquan" runat="server" 
                             Height="25px" Width="327px">
                        </asp:DropDownList>
                    </td>
                </tr>
				<tr>
					<td class="style1">Phông lưu trữ</td>
					<td><asp:TextBox ID="txtKeyword" runat="server" Width="713px" /></td>
                </tr>
                <tr>     
                <td class="style1">
                    <asp:ImageButton ID="btAdd" runat="server" ImageUrl="~/Styles/document_add.png" 
                        onclick="btAdd_Click" />
                    <asp:ImageButton ID="btDelete" runat="server" 
                        ImageUrl="~/Styles/document_delete.png" onclick="btDelete_Click" 
                        style="width: 16px" />
                    </td>          
                <td >
			<asp:Button ID="btSearch" runat="server" Text="Tìm Kiếm" Width="100px" onclick="btSearch_Click" 
				 />
                </td>
                </tr>
                <tr>
                <td colspan="2">
               <asp:GridView ID="dgvApprover" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" Width="100%" GridLines="Horizontal"  Height="67px" 
            PageSize="40"  
            AllowSorting="True" 
                      BackColor="White" 
            BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                        style="text-align: left; margin-right: 46px;" >
            <RowStyle BackColor="White" Font-Size="12px" ForeColor="#333333" />
            <Columns>   
            <asp:BoundField DataField="ID" HeaderText="ID" >                
<ItemStyle Width="30px"></ItemStyle>
 </asp:BoundField>  
             <asp:BoundField DataField="stt" HeaderText="STT" >                
<ItemStyle Width="30px"></ItemStyle>
                </asp:BoundField>                  
            <asp:BoundField DataField="MaPhong" HeaderText="Mã phông" >                
<ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>   
                 <asp:BoundField DataField="TenPhong" HeaderText="Tên phông" >                
<ItemStyle Width="200px"></ItemStyle>
                </asp:BoundField>  
                      <asp:BoundField DataField="TongSoTaiLieu" HeaderText="Tổng số tài liệu" >                
<ItemStyle Width="180px"></ItemStyle>
                </asp:BoundField>  
                
                  <asp:BoundField DataField="TaiLieuChinhLy" HeaderText="Tài liệu đã chỉnh lý" >                
<ItemStyle Width="180px"></ItemStyle>
                </asp:BoundField>  
                 <asp:BoundField DataField="TaiLieuChuaChinhLy" HeaderText="Tài liệu chưa chỉnh lý" >                
<ItemStyle Width="180px"></ItemStyle>
                </asp:BoundField>                             
                           
                <asp:TemplateField HeaderText="Chọn" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="30" Visible="true" >
                     
                    <ItemTemplate>
							<asp:CheckBox ID="cbChoose" runat="server" />
						</ItemTemplate>
                        </asp:TemplateField>     
                        
             <asp:TemplateField HeaderText="Sửa" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="50" Visible="true" >
                        <ItemTemplate>
                            <asp:ImageButton ID="bt_changing" ToolTip ="Sửa" runat="server" ImageUrl="~/Styles/edit.gif" Height="21px" Width="20px"  CommandArgument = '<%# Eval("ID") %>' BorderWidth="0" OnClick = "changing" />
                        </ItemTemplate>

<ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                        </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" 
                Font-Size="9px" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" 
                Font-Size="12px" />
            <EditRowStyle Font-Size="9px" />
        </asp:GridView>
                </td>
                </tr>							
		
            </table>
        </div>
        <div id="divButtons">
			
        </div>
        
        
    </div>
  
</asp:Content>
