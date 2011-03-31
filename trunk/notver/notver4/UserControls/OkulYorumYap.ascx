<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OkulYorumYap.ascx.cs" Inherits="UserControls_OkulYorumYap" %>


<asp:Panel ID="pnlPuanYorum" runat="server" Width="510" Height="380" CssClass="OkulYorumYap">
    <p style="color:#626262; font-size:12px;">Yapmış olduğun tüm yorumları görüntülemek veya değiştirmek için
    <asp:HyperLink ID="lnkKullaniciYorumlar" runat="server" CssClass="lnkYorumlarim">buraya tıkla</asp:HyperLink></p>
    <br />
    <p>Yorumun</p>
    <p>
        <asp:TextBox runat="server" CssClass="multitextbox" TextMode="MultiLine" MaxLength="2000" 
        ID="textYorum" Width="500" Height="220"></asp:TextBox>
    </p>
    <p style="padding-top:20px;">
        <asp:ImageButton runat="server" ID="dugmeYorumGonder" ImageUrl="~/App_Themes/Default/Images/gonder.png" 
        OnClick="YorumKaydet"/>
        <asp:ImageButton runat="server" ID="dugmeYorumGuncelle" ImageUrl="~/App_Themes/Default/Images/gonder.png" 
        OnClick="YorumGuncelle"/>
    </p>
    <p class="durum">
        <asp:Literal runat="server" ID="ltrDurum"></asp:Literal>
    </p>
</asp:Panel>
<asp:Panel ID="pnlUyeOl" runat="server" CssClass="bilgi">
    <br/><br/>
    Yorum yapabilmek için giriş yapmalısın.
    <br/><br/>
    Üyeliğin yoksa ana sayfada sağ üstten hemen ücretsiz üye olabilirsin.
</asp:Panel>
<asp:Panel ID="pnlHata" runat="server" CssClass="durum">
Bir hata olustu :(
</asp:Panel>
<asp:Literal runat="server" ID="ltrScript"></asp:Literal>
