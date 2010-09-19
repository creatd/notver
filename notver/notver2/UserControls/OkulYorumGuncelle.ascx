<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OkulYorumGuncelle.ascx.cs" Inherits="UserControls_OkulYorumGuncelle" %>

<asp:Label runat="server" ID="baslikPuanYorum" Width="600" Style="background-color: Gray;
    font-weight: bold;"></asp:Label>
<asp:Panel ID="pnlYorum" runat="server">
    <table style="border: solid 1pt;" border="1" width="600">
        <tr>
            <td class="OkulYorumYapSutunSol">
                Yorumunuz
            </td>
            <td class="OkulYorumYapSutunSag">
                <asp:TextBox ID="textYorum" runat="server" MaxLength="2000" TextMode="MultiLine" CssClass="OkulYorumYapTextbox" />
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2" style="color: Red;">
                <asp:Literal runat="server" ID="ltrDurum"></asp:Literal>
                <asp:Button ID="dugmeYorumGuncelle" Text="Guncelle" runat="server" OnClick="YorumGuncelle"
                    CssClass="fltRight" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlUyeOl" runat="server">
    Puan vermek ve/veya yorum yapabilmek icin (sag ust koseden) <a href="#login">giris yapmaniz</a> gereklidir.
    Uyeliginiz yoksa
    <asp:HyperLink ID="HyperLink1" runat="server" Text="uye olmak icin tiklayin!" NavigateUrl="~/Register.aspx"></asp:HyperLink>
</asp:Panel>
<asp:Literal runat="server" ID="ltrScript"></asp:Literal>