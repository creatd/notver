<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DersYorumlari.ascx.cs" Inherits="UserControls_DersYorumlari" %>

<asp:Panel ID="pnlYorumlar" runat="server" Visible="true" CssClass="dersYorumlar">
    <asp:Repeater runat="server" ID="repeaterYorumlar" OnItemDataBound="repeaterYorumlar_ItemDataBound">
        <ItemTemplate>
            <table style="border: none;" border="0" width="600">
                <tr>
                    <td style="font-style: italic; color: rgb(150,150,150); font-size: 8pt;">
                        <%# YorumBasligiOlustur( DataBinder.Eval(Container.DataItem, "KULLANICI_ADI") ,
                            DataBinder.Eval(Container.DataItem, "TARIH"), DataBinder.Eval(Container.DataItem, "HOCA_ISIM"), 
                            DataBinder.Eval(Container.DataItem, "KAYITSIZ_HOCA_ISIM"))%>
                    </td>
                    <td>
                        <asp:HiddenField runat="server" ID="yorumID" Value=' <%# DataBinder.Eval(Container.DataItem , "DERSYORUM_ID") %>' />
                        <asp:UpdatePanel runat="server" ID="pnlSevSevme">
                            <ContentTemplate>
                                <asp:Literal runat="server" ID="yorumPuan" Text=""></asp:Literal>
                                <asp:LinkButton runat="server" ID="yorumSev" Text="Sevdim" OnClick="yorumSev_click"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="yorumSevme" Text="Sevmedim" OnClick="yorumSevme_click"></asp:LinkButton>
                                <asp:Literal runat="server" ID="yorumPuanDurumu" Text=""></asp:Literal>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <%# DataBinder.Eval(Container.DataItem, "YORUM")%>
                    </td>
                </tr>
            </table>
            <br />
        </ItemTemplate>
    </asp:Repeater>
    <asp:Panel ID="pnlPager" runat="server">
        <table class="pager">
            <tr>
                <td>
                    <asp:LinkButton ID="lnkOnceki" Text="Onceki" OnClick="OncekiYorumlaraGit" runat="server"></asp:LinkButton>
                </td>
                <td>
                    <asp:Repeater runat="server" ID="rptPager" OnItemCommand="rptPager_Command" OnItemDataBound="rptPager_DataBound">
                        <HeaderTemplate>
                            <ol>
                        </HeaderTemplate>
                        <ItemTemplate>
                                <li><asp:LinkButton runat="server" Text="<%# Container.DataItem %>" CommandName="SayfayaGit" CommandArgument="<%# Container.DataItem %>" ID="lnkSayfa"></asp:LinkButton></li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ol></FooterTemplate>
                    </asp:Repeater>
                </td>
                <td>
                    <asp:LinkButton ID="lnkSonraki" Text="Sonraki" OnClick="SonrakiYorumlaraGit" runat="server"></asp:LinkButton>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="dropSayfaBoyutu" OnSelectedIndexChanged="SayfaBoyutuDegisti" AutoPostBack="True">
                        <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                        <asp:ListItem Text="40" Value="40"></asp:ListItem>
                        <asp:ListItem Text="Hepsi" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Panel>
<asp:Panel ID="pnlYorumYok" runat="server" Visible="false">
    <br />
    <asp:Label ID="lblYorumYok" runat="server">(Daha önce yorum yapilmamis). İlk yorum yapan </asp:Label><asp:HyperLink
        ID="linkYorumYap" Text="siz olun!" runat="server" NavigateUrl="~/Ders.aspx#DersYorumYap1"></asp:HyperLink>
</asp:Panel>