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

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="center" class="style8">
        <tr>
            <td class="style11">
                <asp:LoginView ID="LoginView1" runat="server">
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
                    <asp:DataList ID="lstStudent" runat="server" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="4" GridLines="Vertical" BackColor="White" 
                        BorderColor="#DEDFDE" ForeColor="Black" Width="100%">
                        <ItemStyle BackColor="#F7F7DE" />
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#"~/Sliki/" + DataBinder.Eval(Container.DataItem, "Slika") %>' 
                                    ImageAlign="Middle"  />
                        &nbsp;<asp:HyperLink ID="HyperLink2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Ime") %>' NavigateUrl='<%# "PredmetStudent.aspx?predmet=" + DataBinder.Eval(Container.DataItem, "Ime") + "&predmet_kod="+ DataBinder.Eval(Container.DataItem, "Kod") %>' ForeColor="#6A6972"></asp:HyperLink> 
                        &nbsp;&nbsp
 </ItemTemplate>
                        <AlternatingItemStyle BackColor="White" />
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
 <HeaderTemplate>
    Мои предмети
  </HeaderTemplate>
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    </asp:DataList>
                </ContentTemplate>
            </asp:RoleGroup>
            <asp:RoleGroup Roles="Професор">
                <ContentTemplate>
                    <div class="style12">
                        <asp:DataList ID="lstProfesor" runat="server" BackColor="White" BorderColor="#DEDFDE" 
                            BorderStyle="None" BorderWidth="1px" CellPadding="4" GridLines="Vertical" 
                            Height="100%" Width="100%" ForeColor="Black">
                            <AlternatingItemStyle BackColor="White" />
                            <EditItemStyle BorderStyle="Double" VerticalAlign="Middle" />
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <HeaderTemplate>
                                <div class="style5">
                                    Одберете еден од предметите:</div>
                            </HeaderTemplate>
                            <ItemStyle BorderStyle="Double" BackColor="#F7F7DE" />
                            <ItemTemplate>
                                <asp:Image ID="Image2" runat="server" 
                                    ImageUrl='<%#"~/Sliki/" + DataBinder.Eval(Container.DataItem, "Slika") %>' 
                                    ImageAlign="Middle" />
                                <asp:HyperLink ID="HyperLink3" runat="server" BorderStyle="NotSet" 
                                    ForeColor="#6A6972" 
                                    NavigateUrl='<%# "PredmetProfesor.aspx?predmet=" + DataBinder.Eval(Container.DataItem, "Ime") + "&predmet_kod="+ DataBinder.Eval(Container.DataItem, "Kod") %>' 
                                    Text='<%# DataBinder.Eval(Container.DataItem, "Ime") %>'></asp:HyperLink>
                                <br />
                            </ItemTemplate>
                            <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
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

