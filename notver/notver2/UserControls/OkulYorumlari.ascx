<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OkulYorumlari.ascx.cs" Inherits="UserControls_OkulYorumlari" %>

<asp:Panel ID="pnlYorumlar" CssClass="okulYorumlar" runat="server" Visible="true">
    <p style="text-align:right; font-weight:bold; font-size:11px; padding-bottom:5px;">
        Sayfa basi <asp:DropDownList runat="server" ID="dropSayfaBoyutu" OnSelectedIndexChanged="SayfaBoyutuDegisti" 
        AutoPostBack="True" CssClass="dropdownPager">
                        <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                        <asp:ListItem Text="40" Value="40"></asp:ListItem>
                        <asp:ListItem Text="Hepsi" Value="0"></asp:ListItem>
                    </asp:DropDownList> yorum
    </p>
    <asp:Repeater runat="server" ID="repeaterYorumlar" OnItemDataBound="repeaterYorumlar_ItemDataBound">
        <ItemTemplate>
            <p style="font-weight:bold; color:#191919; padding:10px; background-color:#f6f6f6;">
                <%# YorumBasligiOlustur( DataBinder.Eval(Container.DataItem, "KULLANICI_ADI") ,
                                DataBinder.Eval(Container.DataItem, "TARIH"))%>
            </p>
            <p style="font-weight:bold; padding:10px; color:#626262; font-size:13px;">
                <%# DataBinder.Eval(Container.DataItem, "YORUM")%>
            </p>
            
            <table style="display:none; border: none; background:url('App_Themes/Default/Images/defter/Paper11.png') repeat White;" border="0" width="600">
            <tr>
                <td style="text-align:right;">
                    <asp:HiddenField runat="server" ID="yorumID" Value=' <%# DataBinder.Eval(Container.DataItem , "OKULYORUM_ID") %>' />
                    <asp:UpdatePanel runat="server" ID="pnlSevSevme">
                        <ContentTemplate>
                            <asp:Literal runat="server" ID="yorumPuan" Text=""></asp:Literal>
                            <asp:ImageButton runat="server" ID="yorumSev" OnClick="yorumSev_click" ImageUrl="~/App_Themes/Default/Images/diger/parmak_yukari.png"/>
                            <asp:ImageButton runat="server" ID="yorumSevme" OnClick="yorumSevme_click" ImageUrl="~/App_Themes/Default/Images/diger/parmak_asagi.png"/>
                            <asp:Literal runat="server" ID="yorumPuanDurumu" Text=""></asp:Literal>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            </table> 
        </ItemTemplate>
    </asp:Repeater>
    <asp:Panel ID="pnlPager" runat="server">
        <div id="pager" style="text-align:center;">
            <asp:ImageButton ID="lnkOnceki" Text="Onceki" OnClick="OncekiYorumlaraGit" runat="server"
            ImageUrl="~/App_Themes/Default/Images/prev.png"></asp:ImageButton>
            <asp:Repeater runat="server" ID="rptPager" OnItemCommand="rptPager_Command" OnItemDataBound="rptPager_DataBound">
                <ItemTemplate>
                        <asp:LinkButton runat="server" Text="<%# Container.DataItem %>" CommandName="SayfayaGit" 
                        CommandArgument="<%# Container.DataItem %>" ID="lnkSayfa" CssClass="pager"></asp:LinkButton></li>
                </ItemTemplate>
            </asp:Repeater>
            <asp:ImageButton ID="lnkSonraki" Text="Sonraki" OnClick="SonrakiYorumlaraGit" runat="server"
            ImageUrl="~/App_Themes/Default/Images/next.png">
            </asp:ImageButton>
        </div>
    </asp:Panel>
</asp:Panel>
<asp:Panel ID="pnlYorumYok" runat="server" Visible="false" CssClass="okulYorumlar">
    <p style="font-weight:bold; padding:10px; color:#626262; font-size:13px; font-style:italic;">
        Daha once yorum yapilmamis. Ilk yorum yapan siz olun
    </p>
</asp:Panel>

