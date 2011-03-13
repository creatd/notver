<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DersYorumlari.ascx.cs" Inherits="UserControls_DersYorumlari" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Panel ID="pnlYorumlar" runat="server" Visible="true" CssClass="dersYorumlar">
    <p style="text-align:right; font-weight:bold; font-size:11px; padding-bottom:5px;">
        Sayfa başı <asp:DropDownList runat="server" ID="dropSayfaBoyutu" OnSelectedIndexChanged="SayfaBoyutuDegisti" 
        AutoPostBack="True" CssClass="dropdownPager">
                        
                        
                        
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                        <asp:ListItem Text="40" Value="40"></asp:ListItem>
                        <asp:ListItem Text="Hepsi" Value="0"></asp:ListItem>
                    </asp:DropDownList> yorum
    </p>
    <asp:Repeater runat="server" ID="repeaterYorumlar" OnItemDataBound="repeaterYorumlar_ItemDataBound">
        <ItemTemplate>
            <div style="font-weight:bold; color:#505050; padding:10px; background-color:#f6f6f6;">
                <div style="float:left; width:830px;"><%# YorumBasligiOlustur( DataBinder.Eval(Container.DataItem, "KULLANICI_ADI") ,
                            DataBinder.Eval(Container.DataItem, "KULLANICI_ISIM") ,
                            DataBinder.Eval(Container.DataItem, "TARIH"), DataBinder.Eval(Container.DataItem, "HOCA_ISIM"), 
                            DataBinder.Eval(Container.DataItem, "KAYITSIZ_HOCA_ISIM"))%></div>
                <asp:HiddenField runat="server" ID="yorumID" Value=' <%# DataBinder.Eval(Container.DataItem , "DERSYORUM_ID") %>' />
                <asp:UpdatePanel runat="server" ID="pnlSevSevme" RenderMode="Inline">
                    <ContentTemplate>
                        <asp:Literal runat="server" ID="yorumPuan" Text=""></asp:Literal>
                        <asp:ImageButton runat="server" ID="yorumSev" ImageUrl="~/App_Themes/Default/Images/thumbsup.png" OnClick="yorumSev_click" ToolTip="Sevdim"></asp:ImageButton>
                        <asp:ImageButton runat="server" ID="yorumSevme" ImageUrl="~/App_Themes/Default/Images/thumbsdown.png" OnClick="yorumSevme_click" ToolTip="Sevmedim"></asp:ImageButton>
                        <asp:Label runat="server" ID="yorumPuanDurumu" CssClass="yorumPuanDurumu bilgi"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>                            
            </div>
            <p style="font-weight:bold; padding:10px 10px 0px 10px; color:#313131; font-size:13px;">
                <%# DataBinder.Eval(Container.DataItem, "YORUM")%>
            </p>
            <p style="text-align:right; padding-bottom:20px;">
                <a style="font-size:11px; font-weight:bold;" class="colorboxSikayet" 
        href="<%= Page.ResolveUrl("~/") %>YorumSikayetEt.aspx?YorumTipi=<%= ((int)Enums.YorumTipi.DersYorum).ToString() %>&YorumID=<%# DataBinder.Eval(Container.DataItem , "DERSYORUM_ID") %>">
                    şikayet et
                </a>
            </p>               
        </ItemTemplate>
    </asp:Repeater>
    <asp:Panel ID="pnlPager" runat="server">
        <div id="pager" style="text-align:center;">
            <asp:ImageButton ID="lnkOnceki" Text="Önceki" OnClick="OncekiYorumlaraGit" runat="server"
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
<asp:Panel ID="pnlYorumYok" runat="server" Visible="false" CssClass="dersYorumlar">
    <p style="font-weight:bold; padding:10px; color:#626262; font-size:13px; font-style:italic;">
        Daha önce yorum yapılmamış, ilk yorum yapan siz olun!
    </p>
</asp:Panel>