<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.master" AutoEventWireup="true"
    CodeFile="AdminStrana.aspx.cs" Inherits="AdminStrana" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            text-align: center;
            margin: 10;
            width:inherit;
            padding: 2;
        }
        
        .tabelaPoeni th
        {
            margin: 30;
            padding: 2px;
        }

        .tabelaPoeni input
        {
            width: 50%;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="style14" cellpadding="10">
        <tr>
            <td class="style16" style="width:50%;">
                <asp:ListBox ID="lstPredmeti" runat="server" AutoPostBack="True" Height="100%" Width="100%"
                    OnSelectedIndexChanged="lstPredmeti_SelectedIndexChanged" Rows="6"></asp:ListBox>
            </td>
            <td class="style16" align="right" style="width:50%;">
                <asp:ListBox ID="lstStudenti" runat="server" AutoPostBack="True" Height="100%" OnSelectedIndexChanged="lstStudenti_SelectedIndexChanged"
                    Width="100%"></asp:ListBox>
            </td>
        </tr>
        <tr>
            <td class="style16">
                <asp:Label ID="lblPredmetPoraka" runat="server" Width="50%"></asp:Label>
            </td>
            <td class="style16">
                <asp:Label ID="lblPoraka1" runat="server" Width="50%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style16">
                <asp:Button ID="btnPromeni" runat="server" Text="Промени" Width="22%" OnClick="btnPromeni_Click"
                    ValidationGroup="gr1" />
                <asp:Button ID="btnIscisti" runat="server" Text="Исчисти" Width="22%" OnClick="btnIscisti_Click" />
                <asp:Button ID="btnVnesi" runat="server" Text="Внеси" Width="22%" OnClick="btnVnesi_Click"
                    ValidationGroup="gr1" />
                <asp:Button ID="btnBrisi" runat="server" Text="Избриши" Width="22%" OnClick="btnBrisi_Click"
                    ValidationGroup="gr1" />
                <br />
            </td>
            <td class="style16">
                <asp:Button ID="btnPromeniS" runat="server" Text="Промени" Width="23%" OnClick="btnPromeniS_Click"
                    ValidationGroup="gr2" />
                <asp:Button ID="btnIscistiS" runat="server" Text="Исчисти" Width="23%" OnClick="btnIscistiS_Click" />
                <asp:Button ID="btnVnesiS" runat="server" Text="Внеси" Width="23%" OnClick="btnVnesiS_Click"
                    ValidationGroup="gr2" />
                <asp:Button runat="server" Text="Избриши" Width="23%" ID="btnIzbrisiS" OnClick="btnIzbrisiS_Click" />
            </td>
        </tr>
        <tr>
            <td class="style16" rowspan="3">
                <asp:Label ID="Label1" runat="server" Text="Име на предмет:" Width="45%"></asp:Label>
                <asp:TextBox ID="txtImePredmet" runat="server" Width="45%"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtImePredmet"
                    ErrorMessage="Мора да внесете име на предметот" ForeColor="Red" ValidationGroup="gr1"></asp:RequiredFieldValidator>
                <br />
                <br />
                <asp:Label ID="Label2" runat="server" Text="Слика" Width="45%"></asp:Label>
                <asp:TextBox ID="txtSlika" runat="server" Width="45%"></asp:TextBox>

            </td>
            <td class="style16">
                <asp:Label ID="Label3" runat="server" Text="Индекс" Width="45%"></asp:Label>
                <asp:TextBox ID="txtIndeksStudent" runat="server" Width="49%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Внесете индекс на студентот"
                    ForeColor="Red" ControlToValidate="txtIndeksStudent" ValidationGroup="gr2"></asp:RequiredFieldValidator>
                <br />
            </td>
        </tr>
        <tr>
            <td class="style16">
                <asp:Label ID="Label4" runat="server" Text="Име" Width="45%"></asp:Label>
                <asp:TextBox ID="txtImeStudent" runat="server" Width="49%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Внесете име на студентот"
                    ForeColor="Red" ControlToValidate="txtImeStudent" ValidationGroup="gr2"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style16">
                <asp:Label ID="Label5" runat="server" Text="Презиме" Width="45%"></asp:Label>
                <asp:TextBox ID="txtPrezimeStudent" runat="server" Width="49%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Внесете презиме на студентот"
                    ControlToValidate="txtPrezimeStudent" ForeColor="Red" ValidationGroup="gr2"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table>
                    <tr>
                        <td>
                            <asp:PlaceHolder ID="tblStudentPoeniHolder" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                    <tr align="left">
                        <td >
                            <asp:Button runat="server" ID="btnPromeniPoeni" Text="Променете ги поените" Style="margin: 1%"
                                Visible="False" onclick="btnPromeniPoeni_Click" />
                        </td>
                    </tr>
                    <tr align="left">
                        <td >
                            <asp:Label runat="server" ID="lblPromeniPoeniPoraka"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
