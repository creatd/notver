<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HocaYorumlari.ascx.cs"
    Inherits="UserControls_HocaYorum" %>
<link href="Scripts/StarRating.css" rel="stylesheet" type="text/css" />
<asp:Panel ID="pnlYorumlar" runat="server" Visible="true">
    <asp:Repeater runat="server" ID="repeaterYorumlar" OnItemDataBound="repeaterYorumlar_ItemDataBound">
        <ItemTemplate>
            <table style="border: solid 1pt;" border="1" width="600">
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
                    <td colspan="2">
                        Seviyorum cunku
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <%# DataBinder.Eval(Container.DataItem, "YORUM_OLUMLU") %>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Sevmiyorum cunku
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <%# DataBinder.Eval(Container.DataItem, "YORUM_OLUMSUZ")%>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Ozet olarak
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <%# DataBinder.Eval(Container.DataItem, "YORUM_OZET")%>
                    </td>
                </tr>
            </table>
            <br />
        </ItemTemplate>
    </asp:Repeater>
</asp:Panel>
<asp:Panel ID="pnlYorumYok" runat="server" Visible="false">
    <asp:Label ID="lblYorumYok" runat="server">(Daha önce yorum yapilmamis). İlk yorum yapan </asp:Label><asp:HyperLink
        ID="linkYorumYap" Text="siz olun!" runat="server" NavigateUrl="~/Hoca.aspx#HocaYorumYap1"></asp:HyperLink>
</asp:Panel>
