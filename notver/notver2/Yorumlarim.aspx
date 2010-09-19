<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Yorumlarim.aspx.cs" Inherits="Yorumlarim"
    MasterPageFile="~/Masters/Giris.master" %>

<asp:Content runat="server" ID="content" ContentPlaceHolderID="content">
    <table>
        <tr>
            <td colspan="3">
                <asp:Label runat="server" ID="lblYorumOzeti"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton runat="server" ID="btnDersYorumlarim" OnClick="DersYorumlariniGoster" Text="Ders yorumlarim"></asp:LinkButton>
            </td>
            <td>
                <asp:LinkButton runat="server" ID="btnOkulYorumlarim" OnClick="OkulYorumlariniGoster" Text="Okul yorumlarim"></asp:LinkButton>
            </td>
            <td>
                <asp:LinkButton runat="server" ID="btnHocaYorumlarim" OnClick="HocaYorumlariniGoster" Text="Hoca yorumlarim"></asp:LinkButton>
            </td>
        </tr>
    </table>
    <asp:Panel runat="server" ID="pnlDersYorumlarim">
        <asp:Repeater runat="server" ID="repeaterDersYorumlarim">
            <HeaderTemplate>
                <table>
                    <tr>
                        <td>Ders</td>
                        <td>Okul</td>
                        <td>Yorum</td>
                        <td>Gonderilme tarihi</td>
                        <td>Puani</td>
                        <td>Degistir</td>
                        <td>Sil</td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <asp:HiddenField runat="server" ID="yorumID" Value=' <%# DataBinder.Eval(Container.DataItem , "DERSYORUM_ID") %>' />
                    <asp:HiddenField runat="server" ID="yorumDurum" Value=' <%# DataBinder.Eval(Container.DataItem , "YORUM_DURUMU") %>' />
                    <td><%# DataBinder.Eval(Container.DataItem, "DERS_KODU") %></td>
                    <td><%# DataBinder.Eval(Container.DataItem, "OKUL_ISMI") %></td>
                    <td><%# DataBinder.Eval(Container.DataItem, "YORUM") %></td>
                    <td><%# DataBinder.Eval(Container.DataItem, "TARIH") %></td>
                    <td><%# DataBinder.Eval(Container.DataItem, "ALKIS_PUANI") %></td>
                    <td><a href="DersYorumGuncelle.aspx?DersID=<%# DataBinder.Eval(Container.DataItem , "DERS_ID")%>&KeepThis=true&TB_iframe=true&modal=true&height=400&width=600" class="thickbox">Guncelle</a></td>
                    <td><asp:LinkButton runat="server" ID="btnDersYorumSil" Text="Sil" OnClick="DersYorumSil" OnClientClick="return confirm('Iki kere dusundunuz mu?');"></asp:LinkButton></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                    <tr>
                        <td colspan="6" class="sessiz">
                            Gri renkteki yorumlar henuz onaylanmamis, kirmizi renktekiler ise sizin veya sistem tarafindan silinmis yorumlarinizdir.
                        </td>
                    </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>     
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlOkulYorumlarim">
        <asp:Repeater runat="server" ID="repeaterOkulYorumlarim">
            <HeaderTemplate>
                <table>
                    <tr>
                        <td>Okul</td>
                        <td>Yorum</td>
                        <td>Gonderilme tarihi</td>
                        <td>Puani</td>
                        <td>Degistir</td>
                        <td>Sil</td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <asp:HiddenField runat="server" ID="yorumID" Value=' <%# DataBinder.Eval(Container.DataItem , "OKULYORUM_ID") %>' />
                    <asp:HiddenField runat="server" ID="yorumDurum" Value=' <%# DataBinder.Eval(Container.DataItem , "YORUM_DURUMU") %>' />
                    <td><%# DataBinder.Eval(Container.DataItem, "OKUL_ISMI") %></td>
                    <td><%# DataBinder.Eval(Container.DataItem, "YORUM") %></td>
                    <td><%# DataBinder.Eval(Container.DataItem, "TARIH") %></td>
                    <td><%# DataBinder.Eval(Container.DataItem, "ALKIS_PUANI") %></td>
                    <td><a href="OkulYorumGuncelle.aspx?OkulID=<%# DataBinder.Eval(Container.DataItem , "OKUL_ID")%>&KeepThis=true&TB_iframe=true&modal=true&height=400&width=600" class="thickbox">Guncelle</a></td>
                    <td><asp:LinkButton runat="server" ID="btnOkulYorumSil" Text="Sil" OnClick="OkulYorumSil" OnClientClick="return confirm('Iki kere dusundunuz mu?');"></asp:LinkButton></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                    <tr>
                        <td colspan="6" class="sessiz">
                            Gri renkteki yorumlar henuz onaylanmamis, kirmizi renktekiler ise sizin veya sistem tarafindan silinmis yorumlarinizdir.
                        </td>
                    </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>    
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlHocaYorumlarim">
        <asp:Repeater runat="server" ID="repeaterHocaYorumlarim" OnItemDataBound="HocaYorum_ItemDataBound">
            <HeaderTemplate>
                <table>
                    <tr>
                        <td>Hoca</td>
                        <td>Yorum</td>
                        <td>Gonderilme tarihi</td>
                        <td>Puani</td>
                        <td>Degistir</td>
                        <td>Sil</td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <asp:HiddenField runat="server" ID="yorumID" Value=' <%# DataBinder.Eval(Container.DataItem , "HOCAYORUM_ID") %>' />
                    <asp:HiddenField runat="server" ID="yorumDurum" Value=' <%# DataBinder.Eval(Container.DataItem , "YORUM_DURUMU") %>' />
                    <td><%# DataBinder.Eval(Container.DataItem, "HOCA_ISMI") %></td>
                    <td><%# DataBinder.Eval(Container.DataItem, "YORUM") %></td>
                    <td><%# DataBinder.Eval(Container.DataItem, "TARIH") %></td>
                    <td><%# DataBinder.Eval(Container.DataItem, "ALKIS_PUANI") %></td>
                    <td><asp:Literal runat="server" ID="ltrHack" /><a href="HocaYorumGuncelle.aspx?HocaID=<%# DataBinder.Eval(Container.DataItem , "HOCA_ID")%>&KeepThis=true&TB_iframe=true&modal=true&height=400&width=600" class="thickbox">Guncelle</a><asp:Literal runat="server" ID="ltrHack2" /></td>
                    <td><asp:LinkButton runat="server" ID="btnHocaYorumSil" Text="Sil" OnClick="HocaYorumSil" OnClientClick="return confirm('Iki kere dusundunuz mu?');"></asp:LinkButton></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                    <tr>
                        <td colspan="6" class="sessiz">
                            Gri renkteki yorumlar henuz onaylanmamis, kirmizi renktekiler ise sizin veya sistem tarafindan silinmis yorumlarinizdir.
                        </td>
                    </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlYorumYok">
        Yorumunuz bulunmamaktadir
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlHata">
        Bir hata olustu, biz de anlamadik. Tekrar denerseniz belki duzelir.
    </asp:Panel>
    <asp:Literal runat="server" ID="ltrScript"></asp:Literal>
</asp:Content>
