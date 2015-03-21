<%@ Page Title="Quản lý hồ sơ lưu trữ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QLHoSo.aspx.cs" Inherits="Function.QLHoSo" %>
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
        <label style="font-size: larger; font-weight:bold; margin-bottom: 10px">DANH SÁCH HỒ SƠ LƯU TRỮ</label>
        <div id="divInput">
            <table>
                <tr>
					<td class="style1">Cơ quan lưu trữ:</td>
					<td> 
                        <asp:DropDownList ID="ddlCoQuan" runat="server" 
                             Height="25px" Width="300px">
                        </asp:DropDownList>
                    </td>
                     <td class="style3">Chọn Phông lưu trữ</td>
					<td class="style2"><asp:DropDownList ID="ddlPhongLuuTru" runat="server" 
                             Height="25px" Width="300px">
                        </asp:DropDownList></td>
                   
                </tr>
				<tr>
					<td class="style1">Mục lục số</td>
					<td><asp:TextBox ID="txtMucLuc" runat="server" Width="300px" /></td>
                    <td class="style3">Hộp số</td>
					<td class="style2"><asp:TextBox ID="txtHopSo" runat="server" Width="300px" /></td>
                </tr>
                <tr> <td class="style1">Thời hạn bảo quản:</td>
					<td colspan="3"> 
                        <asp:DropDownList ID="ddlThoiHan" runat="server" 
                             Height="25px" Width="300px">
                        </asp:DropDownList>
                    </td>
                </tr>
                	<tr>
					<td class="style1">Từ khóa</td>
					<td colspan="3"><asp:TextBox ID="txtKeyword" runat="server" Width="700px" /></td>
                   
                </tr>
                <tr> 
                <td class="style1">  
                    <asp:ImageButton ID="btAdd" runat="server" ImageUrl="~/Styles/document_add.png" 
                        onclick="btAdd_Click" style="height: 16px" />
                    <asp:ImageButton ID="btDelete" runat="server" 
                        ImageUrl="~/Styles/document_delete.png" OnClick="btDelete_Click" /></td>    
                <td colspan="3">
                   <asp:Button ID="btSearch" runat="server" Text="Tìm Kiếm" Width="100px" onclick="btSearch_Click"  />
                    </td> 
                </tr>
                <tr>
                
                <td colspan="4">
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
<ItemStyle Width="10px"></ItemStyle>
 </asp:BoundField>  
             <asp:BoundField DataField="stt" HeaderText="STT" >                
<ItemStyle Width="20px"></ItemStyle>
                </asp:BoundField>                  
         
                 <asp:BoundField DataField="TenPhong" HeaderText="Tên phông" >                
<ItemStyle Width="200px"></ItemStyle>
                </asp:BoundField>  
                      <asp:BoundField DataField="MucLucName" HeaderText="Mục lục" >                
<ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField> 
                  <asp:BoundField DataField="HoSoSo" HeaderText="Hồ sơ số" >                
<ItemStyle Width="50px"></ItemStyle>
                </asp:BoundField>  
                 <asp:BoundField DataField="TieuDe" HeaderText="Tiêu đề" >                
<ItemStyle Width="180px"></ItemStyle>
                </asp:BoundField>      
                 <asp:BoundField DataField="KyHieu" HeaderText="Ký hiệu" >                
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

                          <asp:TemplateField HeaderText="Thêm văn bản" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="50" Visible="true" >
                        <ItemTemplate>
                            <asp:ImageButton ID="bt_addVanBan" ToolTip ="Thêm văn bản vào hồ sơ" runat="server" ImageUrl="~/Styles/book_blue_add.png" Height="21px" Width="20px"  CommandArgument = '<%# Eval("ID") %>' BorderWidth="0" OnClick = "bt_addVanBan_Add" />
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
