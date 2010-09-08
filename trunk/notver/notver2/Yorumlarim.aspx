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
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlOkulYorumlarim">
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlHocaYorumlarim">
        <asp:Repeater runat="server" ID="repeaterHocaYorumlarim">
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
                    <td><asp:LinkButton runat="server" ID="btnGuncelle" Text="Guncelle" OnClick="HocaYorumGuncelle"></asp:LinkButton></td>
                    <td><asp:LinkButton runat="server" ID="btnSil" Text="Sil" OnClick="HocaYorumSil" OnClientClick="return confirm('Iki kere dusundunuz mu?');"></asp:LinkButton></td>
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
