<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KullaniciGeribildirimGonder.aspx.cs" Inherits="KullaniciGeribildirimGonder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="pnlGeriBildirim" runat="server" Width="510" Height="380" CssClass="OkulYorumYap">
            <p>
                Site ile ilgili yorumunuzu bize gonderin!</p>
            <p>
                <asp:TextBox runat="server" CssClass="multitextbox" TextMode="MultiLine" MaxLength="2000"
                    ID="textGeriBildirim" Width="500" Height="220"></asp:TextBox>
            </p>
             <p style="padding-top: 20px;">
                <asp:ImageButton runat="server" ID="dugmeGeriBildirimGonder" ImageUrl="~/App_Themes/Default/Images/gonder.png"
                     onClick="GeriBildirimGonder"/>
            </p>
            <p class="durum">
                <asp:Literal runat="server" ID="ltrDurum"></asp:Literal>
            </p>
        </asp:Panel>
        <asp:Panel ID="pnlUyeOl" runat="server" CssClass="bilgi">
            <br />
            <br />
            Þikayette bulunabilmek için giriþ yapman gerekli
            <br />
            <br />
            Üyeliðin yoksa ana sayfada sað üstten hemen ücretsiz üye olabilirsin
        </asp:Panel>
        <asp:Panel ID="pnlHata" runat="server" CssClass="durum">
            Bir hata oldu :(
        </asp:Panel>

    </div>
    </form>
</body>
</html>
