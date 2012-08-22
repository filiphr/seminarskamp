<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.master" AutoEventWireup="true" CodeFile="StudentSubject.aspx.cs" Inherits="StudentSubject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style8
        {
            width: 100%;
            margin: 10;
            text-align:center;
        }
        
        .tabelaSkala
        {
            width: 100%;
            margin: 10;
            text-align:left;
        }
        
        .tabelaProverka
        {
            margin: 10;
            width: 100%;
            padding : 2;
        }
        .headerRow
        {
            margin: 30;
            padding :2px;
        }
        
        .dataText
        {
            width:80%;
            text-align:center;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style8">
        <tr>
            <td colspan="2" >
               <asp:Label ID="lblRealna" runat="server">
               </asp:Label>
               <br />
               <br />
                <asp:GridView ID="gvRealna" runat="server" BackColor="White" 
                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                    GridLines="Vertical" AutoGenerateColumns="False">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="I_Kolokvium_Usno" HeaderText="I Колоквиум Усно" />
                        <asp:BoundField DataField="II_Kolokvium_Usno" HeaderText="II Колоквиум Усно" />
                        <asp:BoundField DataField="Ispit_Usno" HeaderText="Испит Усно" />
                        <asp:BoundField DataField="I_Kolokvium_Pismeno" 
                            HeaderText="I Колоквиум Писмено" />
                        <asp:BoundField DataField="II_Kolokvium_Pismeno" 
                            HeaderText="II Колоквиум Писмено" />
                        <asp:BoundField DataField="Ispit_Pismeno" HeaderText="Испит Писмено" />
                        <asp:BoundField DataField="Laboratoriski" HeaderText="Лабораториски" />
                        <asp:BoundField DataField="Prisustvo" HeaderText="Присуство" />
                        <asp:BoundField DataField="Domasna" HeaderText="Домашна" />
                        <asp:BoundField DataField="Seminarska" HeaderText="Семинарска" />
                        <asp:BoundField DataField="Testovi" HeaderText="Тестови" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />
                </asp:GridView>     
                <br />
                </td>
        </tr>
        <tr>
            <td colspan="2">
            <asp:Label ID="lblProverka" runat="server">
               </asp:Label>
            <br />
            <br />
            <asp:placeholder runat="server" ID="tblProverkaHolder"></asp:placeholder>
                <br />
               <br />
            </td>
        </tr>

         <tr align="left">
            <td>
               <asp:Button ID="presmetaj" runat="server" Text="Пресметај вкпно поени и оценка" 
                    onclick="presmetaj_Click" />
            </td>
        </tr>
        <tr align="left">
            <td>
                Имате вкупно <asp:Label ID="lblPoeni" runat="server"></asp:Label> поени.
            </td>
            
        </tr>
        <tr align="left">
            <td>
                Вашата оценка е <asp:Label ID="lblOcenka" runat="server"></asp:Label>
            </td>
        </tr>
        
    </table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder5" Runat="Server">
    <table  class="tabelaSkala">
    <tr>
        <td>
            <asp:Label ID="lblSkala" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvSkala" runat="server" AutoGenerateColumns="False" 
                CellPadding="4" ForeColor="#333333" GridLines="None" 
                style="text-align: center" Width="429px">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="ocena_ocena" HeaderText="Оцена" />
                    <asp:BoundField DataField="Min" HeaderText="Долна граница" />
                    <asp:BoundField DataField="Maks" HeaderText="Горна граница" />
                </Columns>
                <EditRowStyle BackColor="#7C6F57" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#E3EAEB" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                <SortedAscendingHeaderStyle BackColor="#246B61" />
                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                <SortedDescendingHeaderStyle BackColor="#15524A" />
            </asp:GridView>
            
        </td>
    </tr>
</table>
        

    </asp:Content>

