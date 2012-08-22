<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.master" AutoEventWireup="true"
    CodeFile="NovaSkala.aspx.cs" Inherits="NovaSkala" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style8
        {
            width: 50%;
            text-align: center;
        }
        .valSummary ul
        {
            display: none;
            visibility: hidden;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Table ID="tblSkala" runat="server" BorderStyle="Solid" BorderWidth="2px" CellPadding="2"
        CellSpacing="3" GridLines="Both" Width="50%">
        <asp:TableHeaderRow runat="server">
            <asp:TableHeaderCell runat="server"> Оцена </asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server">  Долна граница </asp:TableHeaderCell>
            <asp:TableHeaderCell runat="server"> Горна граница </asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server">
                <asp:Label runat="server" ID="lblOcena5"></asp:Label>
            </asp:TableCell>
            <asp:TableCell runat="server">
                <asp:TextBox runat="server" ID="tbDolna5" Width="50%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbDolna5"
                    ErrorMessage="*" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbDolna5"
                    ErrorMessage="*" ForeColor="#CC0000" ValidationExpression="\d*"></asp:RegularExpressionValidator>
            </asp:TableCell>
            <asp:TableCell runat="server">
                <asp:TextBox runat="server" ID="tbGorna5" Width="50%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbGorna5"
                    ErrorMessage="*" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tbGorna5"
                    ErrorMessage="*" ForeColor="#CC0000" ValidationExpression="\d*"></asp:RegularExpressionValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server">
                <asp:Label runat="server" ID="lblOcena6"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell2" runat="server">
                <asp:TextBox runat="server" ID="tbDolna6" Width="50%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbDolna6"
                    ErrorMessage="*" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="tbDolna6"
                    ErrorMessage="*" ForeColor="#CC0000" ValidationExpression="\d*"></asp:RegularExpressionValidator>
            </asp:TableCell>
            <asp:TableCell ID="TableCell3" runat="server">
                <asp:TextBox runat="server" ID="tbGorna6" Width="50%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="tbGorna6"
                    ErrorMessage="*" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="tbGorna6"
                    ErrorMessage="*" ForeColor="#CC0000" ValidationExpression="\d*"></asp:RegularExpressionValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow1" runat="server">
            <asp:TableCell ID="TableCell1" runat="server">
                <asp:Label runat="server" ID="lblOcena7"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell4" runat="server">
                <asp:TextBox runat="server" ID="tbDolna7" Width="50%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbDolna7"
                    ErrorMessage="*" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="tbDolna7"
                    ErrorMessage="*" ForeColor="#CC0000" ValidationExpression="\d*"></asp:RegularExpressionValidator>
            </asp:TableCell>
            <asp:TableCell ID="TableCell5" runat="server">
                <asp:TextBox runat="server" ID="tbGorna7" Width="50%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="tbGorna7"
                    ErrorMessage="*" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="tbGorna7"
                    ErrorMessage="*" ForeColor="#CC0000" ValidationExpression="\d*"></asp:RegularExpressionValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow2" runat="server">
            <asp:TableCell ID="TableCell6" runat="server">
                <asp:Label runat="server" ID="lblOcena8"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell7" runat="server">
                <asp:TextBox runat="server" ID="tbDolna8" Width="50%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbDolna8"
                    ErrorMessage="*" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="tbDolna8"
                    ErrorMessage="*" ForeColor="#CC0000" ValidationExpression="\d*"></asp:RegularExpressionValidator>
            </asp:TableCell>
            <asp:TableCell ID="TableCell8" runat="server">
                <asp:TextBox runat="server" ID="tbGorna8" Width="50%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="tbGorna8"
                    ErrorMessage="*" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                    ControlToValidate="tbGorna8" ErrorMessage="*" ForeColor="#CC0000" ValidationExpression="\d*"></asp:RegularExpressionValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow3" runat="server">
            <asp:TableCell ID="TableCell9" runat="server">
                <asp:Label runat="server" ID="lblOcena9"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell10" runat="server">
                <asp:TextBox runat="server" ID="tbDolna9" Width="50%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbDolna9"
                    ErrorMessage="*" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="tbDolna9"
                    ErrorMessage="*" ForeColor="#CC0000" ValidationExpression="\d*"></asp:RegularExpressionValidator>
            </asp:TableCell>
            <asp:TableCell ID="TableCell11" runat="server">
                <asp:TextBox runat="server" ID="tbGorna9" Width="50%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="tbGorna9"
                    ErrorMessage="*" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                    ControlToValidate="tbGorna9" ErrorMessage="*" ForeColor="#CC0000" ValidationExpression="\d*"></asp:RegularExpressionValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow4" runat="server">
            <asp:TableCell ID="TableCell12" runat="server">
                <asp:Label runat="server" ID="lblOcena10"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell13" runat="server">
                <asp:TextBox runat="server" ID="tbDolna10" Width="50%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tbDolna10"
                    ErrorMessage="*" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="tbDolna10"
                    ErrorMessage="*" ForeColor="#CC0000" ValidationExpression="\d*"></asp:RegularExpressionValidator>
            </asp:TableCell>
            <asp:TableCell ID="TableCell14" runat="server">
                <asp:TextBox runat="server" ID="tbGorna10" Width="50%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="tbGorna10"
                    ErrorMessage="*" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                    ControlToValidate="tbGorna10" ForeColor="#CC0000" ValidationExpression="\d*"
                    ErrorMessage="*"></asp:RegularExpressionValidator>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server">
            <asp:TableCell runat="server" ColumnSpan=2>
                <asp:Label runat="server" ID="lblError" Text="* Задолжително поле со бројка" ForeColor="Red"></asp:Label>
                <br />
                <asp:Label runat="server" ID="lblSuccess" ></asp:Label>
            </asp:TableCell>

             <asp:TableCell runat="server" >
                <asp:Button runat="server" ID="btnSkala" Text="Зачувајте" OnClick="btnSkala_Click" Width="50%" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder5" runat="Server">
<asp:LinkButton runat="server" Text="Назад" ID="lbtnNazad" 
        onclick="lbtnNazad_Click">
</asp:LinkButton>

</asp:Content>
