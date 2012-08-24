<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.master" AutoEventWireup="true"
    CodeFile="ProfessorSubject.aspx.cs" Inherits="ProfessorSubject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .tabelaSkala
        {
            width: 100%;
            margin: 10;
        }
        
      
        .tabelaUslov
        {
            width: 100%;
            margin: 10;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="style8">
        <tr align="center">
            <td style="text-align: center">
                <asp:Label ID="lblUslov" runat="server"></asp:Label>
                <br />
                <br />
                <asp:Label ID="lblUslovError" runat="server" Visible="False" ForeColor="Red"></asp:Label>
                <asp:GridView ID="gvUslov" runat="server" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False"
                    OnRowCancelingEdit="gvUslov_RowCancelingEdit" OnRowEditing="gvUslov_RowEditing"
                    OnRowUpdating="gvUslov_RowUpdating" OnRowDeleting="gvUslov_RowDeleting" 
                    AllowPaging="True" onpageindexchanging="gvUslov_PageIndexChanging" 
                    PageSize="5">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="uslov_ime" HeaderText="Услов" ReadOnly="True" />
                        <asp:BoundField DataField="Min_Procent" 
                            HeaderText="Минимален број поени за положување" />
                        <asp:BoundField DataField="Procent" HeaderText="Процент во оценката" />
                        <asp:BoundField DataField="Maks_poeni" HeaderText="Максимум Поени" />
                        <asp:CommandField CancelText="Откажи" EditText="Промени" NewText="Ново" SelectText="Селектирај"
                            ShowEditButton="True" UpdateText="Ажурирај" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" Text="Избриши"
                                    OnClientClick="javascript:return confirm('Дали сте сигурни дека сакате да ги избришете овој услов?');">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
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
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder5" runat="Server">
    <table class="tabelaUslov">
        <tr align="left">
            <td>
                <asp:Label ID="lblSkala" runat="server"></asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td>
                <asp:Label ID="lblSkalaEror" runat="server" Visible="False" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr align="left">
            <td>
                <asp:GridView ID="gvSkala" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="None" OnRowCancelingEdit="gvSkala_RowCancelingEdit"
                    OnRowEditing="gvSkala_RowEditing" OnRowUpdating="gvSkala_RowUpdating">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ocena_ocena" HeaderText="Оцена" ReadOnly="True" />
                        <asp:BoundField DataField="Min" HeaderText="Долна граница" />
                        <asp:BoundField DataField="Maks" HeaderText="Горна граница" />
                        <asp:CommandField CancelText="Откажи" DeleteText="Избриши" EditText="Промени" InsertText="Внеси"
                            NewText="Ново" SelectText="Селектирај" ShowEditButton="True" 
                            UpdateText="Ажурирај" />
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
        <tr align="left">
            <td>
                <asp:Button runat="server" Text="Внесете нова скала" ID="btnNovaSkala" 
                    onclick="btnNovaSkala_Click" />
            </td>
        </tr>

        <tr align="left">
            <td>
            <asp:Label ID="lblIndeksFormat" runat="server" Text="Име на колоната со индекси на студентите:  "></asp:Label>
                <asp:TextBox ID="tbIndeksFormat" runat="server" ValidationGroup="grImport"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="Внесете име на колоната" ControlToValidate="tbIndeksFormat" 
                    ForeColor="Red" ValidationGroup="grImport"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr align="left">
            <td>
            <asp:Label ID="lblSheetName" runat="server" Text="Име на Sheet кој ја содржи табелата со поеони:  "></asp:Label>
                <asp:TextBox ID="tbSheetName" runat="server" ValidationGroup="grImport"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator3" runat="server" 
                    ErrorMessage="Внесете име на Sheet" ControlToValidate="tbSheetName" 
                    ForeColor="Red" ValidationGroup="grImport"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr align="left">
            <td>
                 <asp:FileUpload ID="FileUpload1"  runat="server" />
                <asp:Button ID="Import" runat="server" Text="Префрлување на поените на сите студент" onclick="Import_Click" 
                    ValidationGroup="grImport" /> 
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                     ErrorMessage="Прикажете excel документ за да ги внесете поените од него" 
                     ControlToValidate="FileUpload1" Display="Dynamic" ForeColor="Red" 
                     ValidationGroup="grImport"></asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="lblImport" runat="server" Text="Напоменување, имињата на колоните за содветни услови треба да се исти како што ги имате сетирано на веб стрната"></asp:Label>
                <br />
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
