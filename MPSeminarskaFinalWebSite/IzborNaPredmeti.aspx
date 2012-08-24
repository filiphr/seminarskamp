<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.master" AutoEventWireup="true" CodeFile="IzborNaPredmeti.aspx.cs" Inherits="IzborNaPredmeti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style8
        {
            width: 99%;
        }
        .style9
        {
            text-align: left;
        }
    .style11
    {
        width: 650px;
    }
    .style12
    {
        text-align: left;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    </asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder5" Runat="Server">
    <table align="center" class="style8">
        <tr>
            <td class="style11">
                <asp:LoginView ID="LoginView1" runat="server" 
                    onviewchanged="LoginView1_ViewChanged">
        <AnonymousTemplate>
            <div class="style9">
                Мора да сте логирани за да пристапите на страната.<asp:HyperLink 
                    ID="HyperLink1" runat="server" NavigateUrl="~/Login.aspx">Логирај се!</asp:HyperLink>
                &nbsp;
            </div>
        </AnonymousTemplate>
        <RoleGroups>
            <asp:RoleGroup Roles="Студент">
                <ContentTemplate>
                    <asp:DataList ID="list" runat="server" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="3" GridLines="Both">
                        <ItemStyle ForeColor="#000066" />
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server"  />
                        &nbsp;»<asp:HyperLink ID="HyperLink2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Ime") %>' NavigateUrl='<%# "StudentSubject.aspx?predmet=" + DataBinder.Eval(Container.DataItem, "Ime") + "&predmet_kod="+ DataBinder.Eval(Container.DataItem, "Kod") %>'></asp:HyperLink> 
                        &nbsp;&nbsp;«
 </ItemTemplate>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
 <HeaderTemplate>
    Мои предмети
  </HeaderTemplate>
                        <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    </asp:DataList>
                </ContentTemplate>
            </asp:RoleGroup>
            <asp:RoleGroup Roles="Професор">
                <ContentTemplate>
                    <div class="style12">
                        <asp:DataList ID="list1" runat="server" BackColor="White" BorderColor="#CCCCCC" 
                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Both" 
                            Height="100%" Width="100%">
                            <EditItemStyle BorderStyle="Double" VerticalAlign="Middle" />
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            <HeaderTemplate>
                                <div class="style5">
                                    Одберете еден од предметите:</div>
                            </HeaderTemplate>
                            <ItemStyle ForeColor="#000066" BorderStyle="Double" />
                            <ItemTemplate>
                                <asp:Image ID="Image2" runat="server" 
                                    ImageUrl='<%#"~/Sliki/" + DataBinder.Eval(Container.DataItem, "Slika") %>' 
                                    ImageAlign="Middle" />
                                <asp:HyperLink ID="HyperLink3" runat="server" BorderStyle="None" 
                                    ForeColor="#3399FF" 
                                    NavigateUrl='<%# "ProfessorSubject.aspx?predmet=" + DataBinder.Eval(Container.DataItem, "Ime") + "&predmet_kod="+ DataBinder.Eval(Container.DataItem, "Kod") %>' 
                                    Text='<%# DataBinder.Eval(Container.DataItem, "Ime") %>'></asp:HyperLink>
                                <br />
                            </ItemTemplate>
                            <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        </asp:DataList>
                    </div>
                </ContentTemplate>
            </asp:RoleGroup>
        </RoleGroups>
    </asp:LoginView>
                
            </td>
        </tr>
    </table>
</asp:Content>

