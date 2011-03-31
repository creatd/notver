<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YorumSikayetEt.aspx.cs" Inherits="YorumSikayetEt"
 MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=ISO-8859-9" />
    <title>NotVerin - Hocalarla öğrenciler yer değiştiriyor</title>
    <link href="App_Themes/Default/reset.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/Default/Default.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/Default/Default2.css" rel="stylesheet" type="text/css" />

    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>

    <script src="Scripts/colorbox.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="pnlSikayet" runat="server" Width="510" Height="380" CssClass="OkulYorumYap">
            <p>
                Lütfen şikayet nedenini belirt</p>
            <p>
                <asp:TextBox runat="server" CssClass="multitextbox" TextMode="MultiLine" MaxLength="2000"
                    ID="textSikayetNeden" Width="500" Height="220"></asp:TextBox>
            </p>
            <p style="padding-top: 20px;">
                <asp:ImageButton runat="server" ID="dugmeSikayetGonder" ImageUrl="~/App_Themes/Default/Images/gonder.png"
                    OnClick="SikayetGonder" />
            </p>
            <p class="durum">
                <asp:Literal runat="server" ID="ltrDurum"></asp:Literal>
            </p>
        </asp:Panel>
        <asp:Panel ID="pnlUyeOl" runat="server" CssClass="bilgi">
            <br />
            <br />
            Şikayette bulunabilmek için giriş yapman gerekli
            <br />
            <br />
            Üyeliğin yoksa ana sayfada sağ üstten hemen ücretsiz uye olabilirsin
        </asp:Panel>
        <asp:Panel ID="pnlHata" runat="server" CssClass="durum">
            Bir hata oldu :(
        </asp:Panel>
        <asp:Literal runat="server" ID="ltrScript"></asp:Literal>
    </div>
    </form>
</body>
</html>
