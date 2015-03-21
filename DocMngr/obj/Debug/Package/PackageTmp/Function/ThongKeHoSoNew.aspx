<%@ Page Title="Thống kê hồ sơ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ThongKeHoSoNew.aspx.cs" Inherits="Function.ThongKeHoSoNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <div>
    <table width="90%">
    <tr><td colspan="4"> THỐNG KÊ HỒ SƠ</td></tr>
    <tr><td>Cơ quan lưu trữ</td><td>
        <asp:DropDownList ID="ddlCoQuan" runat="server" Width="200px">
        </asp:DropDownList> </td>
        <td>Phông</td><td>
            <asp:DropDownList ID="ddlPhong" runat="server" Width="200px">
            </asp:DropDownList>
        </td>    </tr>
        
      <tr><td>Thời hạn bảo quản </td><td>
        <asp:DropDownList ID="ddlThoiHan" runat="server" Width="200px">
        </asp:DropDownList> </td>
        <td> Tình trạng vật lý </td><td>
            <asp:DropDownList ID="ddlTinhTrangVatLy" runat="server" Width="200px">
            </asp:DropDownList>
        </td>    </tr>
        
        <tr><td>Mục lục  </td><td>
        <asp:DropDownList ID="ddlMucluc" runat="server" Width="200px">
        </asp:DropDownList> </td>
        <td>Chế độ sử dụng </td><td>
            <asp:DropDownList ID="ddlCheDoSuDung" runat="server" Width="200px">
            </asp:DropDownList>
        </td>    </tr>
        <tr>
        <td>Từ khóa</td>
        <td colspan ="3"> 
            <asp:TextBox ID="txtKeyword" runat="server" Width="650px"></asp:TextBox></td>
        </tr>
        <tr>
        <td>&nbsp;</td>
        <td colspan ="3"> 
            <asp:Button ID="btTimKiem" runat="server" Text="Tìm Kiếm" Width="142px" 
                onclick="btTimKiem_Click" />
                    </td>
        </tr>
        <tr>
        <td>&nbsp;</td>
        <td colspan ="3"> 
            &nbsp;</td>
        </tr>
        <tr>
      
        <td colspan ="4"> 
            <asp:GridView ID="dgrResult" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" Width="100%" GridLines="Horizontal"  
            Height="67px" 
            PageSize="40"  
            AllowSorting="True" 
                      BackColor="White" 
            BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" >
            <RowStyle BackColor="White" Font-Size="12px" ForeColor="#333333" />
             <Columns>      
              <asp:BoundField DataField="ID" HeaderText="ID" >                
<ItemStyle Width="50px"></ItemStyle>
                </asp:BoundField>               
            <asp:BoundField DataField="stt" HeaderText="STT" >                
<ItemStyle Width="50px"></ItemStyle>
                </asp:BoundField>   
                 <asp:BoundField DataField="MucLucSov" HeaderText="Mục lục số" >                
<ItemStyle Width="70px"></ItemStyle>
                </asp:BoundField>  
              
                                 <asp:BoundField DataField="HopSov" HeaderText="Hộp số" >                
<ItemStyle Width="50px"></ItemStyle> </asp:BoundField>   
                <asp:BoundField DataField="HoSoSo" HeaderText="Hồ sơ số" >                
<ItemStyle Width="70px"></ItemStyle>
                </asp:BoundField>  
                
 <asp:BoundField DataField="TieuDe" HeaderText="Tiêu Đề" >                
<ItemStyle Width="300px"></ItemStyle>
                </asp:BoundField>  
                        <asp:BoundField DataField="ThoiHanBaoQuanv" HeaderText="Thời hạn bảo quản" >                
<ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>            
                           
             <asp:TemplateField HeaderText="Chi tiết" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="80px" Visible="true" >
                        <ItemTemplate>
                            <asp:ImageButton ID="bt_changing" ToolTip ="Chi tiết" runat="server" ImageUrl="~/Styles/icon_view.gif" Height="21px" Width="20px"  CommandArgument = '<%# Eval("ID") %>' BorderWidth="0" OnClick = "changing" />
                        </ItemTemplate>

<ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                        </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" 
                Font-Size="9px" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" 
                Font-Size="12px" />
            <EditRowStyle Font-Size="9px" />
        </asp:GridView></td>
        </tr>
    </table>
    </div>
</asp:Content>
