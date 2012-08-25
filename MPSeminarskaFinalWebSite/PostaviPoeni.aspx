<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.master" AutoEventWireup="true"
    CodeFile="PostaviPoeni.aspx.cs" Inherits="PostaviPoeni" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table width="100%" class="tabelaUpload" cellpadding="5">
        <tr align="left">
            <td>
                <asp:Label ID="lblIndeksFormat" Width="40%" runat="server" Text="Име на колоната со индекси на студентите:  "></asp:Label>
                <asp:TextBox ID="tbIndeksFormat" runat="server" ValidationGroup="grImport"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="Внесете име на колоната"
                    ControlToValidate="tbIndeksFormat" ForeColor="Red" ValidationGroup="grImport"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr align="left">
            <td>
                <asp:Label ID="lblSheetName" runat="server" Width="40%" Text="Име на страната која ја содржи табелата со поените:  "></asp:Label>
                <asp:TextBox ID="tbSheetName" runat="server" ValidationGroup="grImport"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator3" runat="server" ErrorMessage="Внесете име на страната"
                    ControlToValidate="tbSheetName" ForeColor="Red" ValidationGroup="grImport"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr align="left">
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" Width="35%" />
                <asp:Button ID="Import" runat="server" Text="Прикачување на поените на сите студенти"
                    OnClick="Import_Click" ValidationGroup="grImport" />
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Прикачете excel документ за да ги внесете поените од него"
                    ControlToValidate="FileUpload1" Display="Dynamic" ForeColor="Red" ValidationGroup="grImport"></asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="lblImport" runat="server" Text="Напомена!!! Имињата на колоните за содветните услови треба да се именувани исто како што ги имате сетирано"></asp:Label>
                <br />
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" Text="Услов:"></asp:Label>
                <asp:TextBox runat="server" ID="tbUslovProsek"></asp:TextBox>
                <asp:Label runat="server" ID="lblProsek"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="tbUslovProsek" Display="Dynamic" 
                    ErrorMessage="Внесете услов за да пресметате просек" ForeColor="#CC0000" 
                    ValidationGroup="grProsek"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton runat="server" ID="lbtnUslovProsek" 
                    Text="Пресметајте го просекот на Вашите студенти за соодветниот услов" 
                    onclick="lbtnUslovProsek_Click" ValidationGroup="grProsek" />
            </td>
        </tr>
    </table>
</asp:Content>
