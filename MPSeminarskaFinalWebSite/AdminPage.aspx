<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.master" AutoEventWireup="true" CodeFile="AdminPage.aspx.cs" Inherits="AdminPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .style14
    {
        width: 100%;
    }
    .style16
    {
        text-align: left;
    }
    .tabelaPoeni
        {
            text-align:center;
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table class="style14">
    <tr>
        <td class="style16">
            <asp:ListBox ID="lstPredmeti" runat="server" AutoPostBack="True" Height="100%" 
                Width="62%" onselectedindexchanged="lstPredmeti_SelectedIndexChanged"></asp:ListBox>
        </td>
        <td class="style16">
            <asp:ListBox ID="lstStudenti" runat="server" AutoPostBack="True" Height="100%" 
                onselectedindexchanged="lstStudenti_SelectedIndexChanged" Width="62%"></asp:ListBox>
        </td>
    </tr>
    <tr>
        <td class="style16">
            <asp:Label ID="lblPoraka" runat="server" Width="62%"></asp:Label>
        </td>
        <td class="style16">
            <asp:Label ID="lblPoraka1" runat="server" Width="62%"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="style16">
            <asp:Button ID="btnPromeni" runat="server" Text="Промени" Width="15%" 
                onclick="btnPromeni_Click" ValidationGroup="gr1" />
            <asp:Button ID="btnIscisti" runat="server" Text="Исчисти" Width="15%" 
                onclick="btnIscisti_Click" />
            <asp:Button ID="btnVnesi" runat="server" Text="Внеси" Width="15%" 
                onclick="btnVnesi_Click" ValidationGroup="gr1" />
            <asp:Button ID="btnBrisi" runat="server" Text="Избриши" Width="15%" 
                onclick="btnBrisi_Click" ValidationGroup="gr1" />
            <br />
        </td>
        <td class="style16">
            <asp:Button ID="btnPromeniS" runat="server" Text="Промени" Width="15%" 
                onclick="btnPromeniS_Click" ValidationGroup="gr2" />
            <asp:Button ID="btnIscistiS" runat="server" Text="Исчисти" Width="15%" 
                onclick="btnIscistiS_Click" />
            <asp:Button ID="btnVnesiS" runat="server" Text="Внеси" Width="15%" 
                onclick="btnVnesiS_Click" ValidationGroup="gr2" />
            <asp:Button runat="server" Text="Избриши" Width="15%" ID="btnIzbrisiS" 
                onclick="btnIzbrisiS_Click" />
        </td>
    </tr>
    <tr>
        <td class="style16" rowspan="3">
            <asp:Label ID="Label1" runat="server" Text="Име на предмет" Width="22%"></asp:Label>
            <asp:TextBox ID="txtImePredmet" runat="server" Width="40%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtImePredmet" 
                ErrorMessage="Мора да внесете име на предметот" ForeColor="Red" 
                ValidationGroup="gr1"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Слика" Width="22%"></asp:Label>
            <asp:TextBox ID="txtSlika" runat="server" Width="40%"></asp:TextBox>
            <br />
            <br />
            <br />
            <br />
            <br />
        </td>
        <td class="style16">
            <asp:Label ID="Label3" runat="server" Text="Индекс" Width="22%"></asp:Label>
            <asp:TextBox ID="txtIndeksStudent" runat="server" Width="40%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="Внесете индекс на студентот" ForeColor="Red" 
                ControlToValidate="txtIndeksStudent" ValidationGroup="gr2"></asp:RequiredFieldValidator>
            <br />
        </td>
    </tr>
    <tr>
        <td class="style16">
            <asp:Label ID="Label4" runat="server" Text="Име" Width="22%"></asp:Label>
            <asp:TextBox ID="txtImeStudent" runat="server" Width="40%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="Внесете име на студентот" ForeColor="Red" 
                ControlToValidate="txtImeStudent" ValidationGroup="gr2"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="style16">
            <asp:Label ID="Label5" runat="server" Text="Презиме" Width="22%"></asp:Label>
            <asp:TextBox ID="txtPrezimeStudent" runat="server" Width="40%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ErrorMessage="Внесете презиме на студентот" 
                ControlToValidate="txtPrezimeStudent" ForeColor="Red" ValidationGroup="gr2"></asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolder5" Runat="Server">
    <table>
        <tr>
            <td >
                <asp:PlaceHolder ID="tblStudentPoeniHolder" runat="server"></asp:PlaceHolder>
            </td>
        </tr>

        <tr align="left">
            <td >
                <asp:Button runat="server" ID="btnPromeniPoeni" Text="Променете ги поените" 
                    style="margin: 1%" Visible="False"/>
            </td>

        </tr>
    </table>
    
    

</asp:Content>

