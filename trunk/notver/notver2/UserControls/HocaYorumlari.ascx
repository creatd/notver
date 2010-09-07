<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HocaYorumlari.ascx.cs"
    Inherits="UserControls_HocaYorum" %>
<link href="Scripts/StarRating.css" rel="stylesheet" type="text/css" />
<asp:Panel ID="pnlYorumlar" runat="server" Visible="true">
    <asp:Repeater runat="server" ID="repeaterYorumlar" OnItemDataBound="repeaterYorumlar_ItemDataBound">
        <ItemTemplate>
            <table style="border: none;" border="0" width="600">
                <tr>
                    <td style="font-style: italic; color: rgb(150,150,150); font-size: 8pt;">
                        <%# YorumBasligiOlustur( DataBinder.Eval(Container.DataItem, "KULLANICI_ADI") ,
                                DataBinder.Eval(Container.DataItem, "TARIH"),
                                DataBinder.Eval(Container.DataItem, "KULLANICI_PUANARALIGI"))%>
                    </td>
                    <td>
                        <%# YorumBaslikGenelPuanResmiOlustur(DataBinder.Eval(Container.DataItem, "GENEL_PUAN"))%>
                    </td>
                    <td>
                        <asp:HiddenField runat="server" ID="yorumID" Value=' <%# DataBinder.Eval(Container.DataItem , "HOCAYORUM_ID") %>' />
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
    <asp:Label ID="lblYorumYok" runat="server">(Daha önce yorum yapilmamis). İlk yorum yapan </asp:Label><asp:HyperLink
        ID="linkYorumYap" Text="siz olun!" runat="server" NavigateUrl="~/Hoca.aspx#HocaYorumYap1"></asp:HyperLink>
</asp:Panel>
