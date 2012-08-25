<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.master" AutoEventWireup="true" CodeFile="PredmetStudent.aspx.cs" Inherits="PredmetStudent" %>

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
        
        .tabelaPoeni
        {
            margin: 10;
            width: 100%;
            padding : 2;
        }
        
        .tabelaPoeni th
        {
            margin: 30;
            padding :2px;
        }
        
        .tabelaPoeni input
        {
            width:50%;
            text-align:center;
        }
        

        
        
      
    </style>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style8">
        <tr>
            <td colspan="2" >
               <asp:Label ID="lblRealna" runat="server">
               </asp:Label>
               <br />
               <br />
                <asp:placeholder runat="server" ID="tblPoeniHolder"></asp:placeholder>
                <br />
                <asp:Label ID="lblProverka" runat="server">
               </asp:Label>
                <br />
                </td>
        </tr>
         <tr align="left">
            <td>
               <asp:Button ID="presmetaj" runat="server" Text="Пресметај вкупно поени и оценка" 
                    onclick="presmetaj_Click" Width="333px" />
            </td>
        </tr>
        <tr align="left">
            <td>
                <asp:Label ID="lblPoeni" runat="server"></asp:Label>
                <br />
            </td>
            
        </tr>
        <tr align="left">
            <td>
                Вашата оценка е <asp:Label ID="lblOcenka" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
        <td>
            <table  class="tabelaSkala">
    <tr>
        <td colspan=2>
            <asp:Label ID="lblSkala" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvSkala" runat="server" AutoGenerateColumns="False" 
                CellPadding="4" ForeColor="#333333" GridLines="None" 
                style="text-align: center" Height="100%">
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
        <td>
            <asp:GridView ID="gvUslov" runat="server" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                GridLines="Vertical" AutoGenerateColumns="False"
                    AllowPaging="True" onpageindexchanging="gvUslov_PageIndexChanging" 
                PageSize="5">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="uslov_ime" HeaderText="Услов" ReadOnly="True" />
                        <asp:BoundField DataField="Min_Procent" HeaderText="Минимален процент за положување (од 100)" />
                        <asp:BoundField DataField="Procent" HeaderText="Процент во оценката" />
                        <asp:BoundField DataField="Maks_poeni" HeaderText="Максимум поени" />
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
        </td>
    </tr>
</table>
        </td>
        </tr>
        
    </table>
</asp:Content>


