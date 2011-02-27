<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OkulYorumGuncelle.ascx.cs" Inherits="UserControls_OkulYorumGuncelle" %>

<div id="OkulYorumGuncelle" style="background-color:#afafaf; color:#191919;">
    <p style="color:#626262;">Yapmis oldugunuz tum yorumlari goruntulemek veya degistirmek icin <asp:HyperLink runat="server" 
    NavigateUrl="~/Yorumlarim.aspx" CssClass="lnkYorumlarim">tiklayin</asp:HyperLink></p>
<asp:Panel ID="pnlYorum" runat="server">
    <p>
        Yorumunuz:
    </p>
    <p style="height:300px; width:100%;">
        <asp:TextBox runat="server" ID="textYorum" MaxLength="2000" TextMode="MultiLine">
        </asp:TextBox>
    </p>
    <p>
        <asp:ImageButton runat="server" ID="dugmeYorumGuncelle" ImageUrl="~/App_Themes/Default/Images/gonder.png" OnClick="YorumGuncelle"/>
    </p>
    <p style="font-weight:normal; color:#626262; font-size:12px;">
        <asp:Literal runat="server" ID="ltrDurum"></asp:Literal>
    </p>
</asp:Panel>
<asp:Panel ID="pnlUyeOl" runat="server">
    Yorum yapabilmek icin (sag ust koseden) giris yapmaniz veya uye olmaniz gereklidir.
</asp:Panel>
<asp:Literal runat="server" ID="ltrScript"></asp:Literal>
</div>