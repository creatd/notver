<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HocaYorumlari.ascx.cs"
    Inherits="UserControls_HocaYorum" %>
    
<asp:Panel ID="pnlYorumlar" runat="server" Visible="true" CssClass="hocaYorumlar">
    <p style="text-align:right; font-weight:bold; font-size:11px; padding-bottom:5px;">
        Sayfa basi <asp:DropDownList runat="server" ID="dropSayfaBoyutu" OnSelectedIndexChanged="SayfaBoyutuDegisti" 
        AutoPostBack="True" CssClass="dropdownPager" EnableViewState="true">
                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
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
                <table style="font-weight:bold; color:#505050; padding:10px; background-color:#f6f6f6; width:100%;">
                    <tr>    
                        <td style="width:460px; padding:10px;"><%# YorumBasligiOlustur( DataBinder.Eval(Container.DataItem, "KULLANICI_ADI") ,
                                 DataBinder.Eval(Container.DataItem, "KULLANICI_ISIM"),
                                DataBinder.Eval(Container.DataItem, "TARIH"),
                                DataBinder.Eval(Container.DataItem, "KULLANICI_PUANARALIGI"),
                                DataBinder.Eval(Container.DataItem, "DERS_KODU"))%></td>
                        <td style="width:100px; padding:10px; padding-right:50px;"><%# YorumBaslikGenelPuanResmiOlustur(DataBinder.Eval(Container.DataItem, "GENEL_PUAN"))%>     </td>
                        <td style="padding:10px; width:270px; text-align:right;">
                            <asp:UpdatePanel runat="server" ID="pnlSevSevme">
                                <ContentTemplate>
                                    <asp:Literal runat="server" ID="yorumPuan" Text=""></asp:Literal>
                                    <asp:ImageButton runat="server" ID="yorumSev" Text="Sevdim" OnClick="yorumSev_click" ImageUrl="~/App_Themes/Default/Images/thumbsup.png" ToolTip="Sev"></asp:ImageButton>
                                    <asp:ImageButton runat="server" ID="yorumSevme" Text="Sevmedim" OnClick="yorumSevme_click" ImageUrl="~/App_Themes/Default/Images/thumbsdown.png" ToolTip="Sevme"></asp:ImageButton>
                                    <br />
                                    <asp:Label runat="server" ID="yorumPuanDurumu" CssClass="yorumPuanDurumu bilgi"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>                        
                        </td>
                    </tr>
                </table>
            
            <p style="font-weight:bold; padding:10px 10px 0px 10px; color:#313131; font-size:13px;">
                <%# DataBinder.Eval(Container.DataItem, "YORUM")%>
            </p>
            <p style="text-align:right; padding-bottom:20px;">
                <a style="font-size:11px; font-weight:bold;" class="colorboxSikayet" 
        href="<%= Page.ResolveUrl("~/") %>YorumSikayetEt.aspx?YorumTipi=<%= ((int)Enums.YorumTipi.HocaYorum).ToString() %>&YorumID=<%# DataBinder.Eval(Container.DataItem , "HOCAYORUM_ID") %>">
                    sikayet et
                </a>
            </p>   
            <asp:HiddenField runat="server" ID="yorumID" Value=' <%# DataBinder.Eval(Container.DataItem , "HOCAYORUM_ID") %>' />
            <br />
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
<asp:Panel ID="pnlYorumYok" runat="server" Visible="false" CssClass="dersYorumlar">
    <p style="font-weight:bold; padding:10px; color:#626262; font-size:13px; font-style:italic;">
        Daha once yorum yapilmamis. Ilk yorum yapan siz olun
    </p>
</asp:Panel>
