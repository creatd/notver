<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SifremiUnuttum.aspx.cs" Inherits="SifremiUnuttum" 
MasterPageFile="~/Masters/Giris.master" MaintainScrollPositionOnPostback="true"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
<script type="text/javascript">
function DurumTemizle() {
    document.getElementById("<%= lblDurum.ClientID %>").innerHTML  = "";
}

</script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="content">
<div style="text-align:center; margin-top:30px;">
<asp:Panel runat="server" ID="pnlBasari">
    <span style="font-size:14px; font-weight:bold;">
    Aşağıdaki kutulara yeni şifreni iki kere girerek şifreni değiştirebilirsin
    </span>
    <br /><br /><br />
    <p>Yeni şifre</p>
        <asp:TextBox ID="txtSifre" runat="server" TextMode="Password" onchange="DurumTemizle();" CssClass="textbox"></asp:TextBox>
        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtSifre"
                        ErrorMessage="Şifre girmelisin" ToolTip="Şifre girmelisin"
                        >*</asp:RequiredFieldValidator>
    <p style="margin-top:20px;">Yeni şifre (tekrar)</p>
        <asp:TextBox ID="txtSifre2" runat="server" TextMode="Password" onchange="DurumTemizle();" CssClass="textbox"></asp:TextBox>
        <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="txtSifre2"
                        ErrorMessage="Aynı şifreyi tekrar girmelisin" ToolTip="Aynı şifreyi tekrar girmelisin"
                        >*</asp:RequiredFieldValidator>
        <asp:CompareValidator runat="server" ID="Passwords" ControlToValidate="txtSifre2" ControlToCompare="txtSifre"
                        ErrorMessage="Girdiğin iki şifre aynı olmalı" ToolTip="Girdiğin iki şifre aynı olmalı"
                        >*</asp:CompareValidator>                        
    <br />
    <asp:ImageButton ID="btnSifreDegistir" OnClick="SifreDegistir" runat="server" CausesValidation="true"
                    CssClass="loginTus clear"
                ImageUrl="~/App_Themes/Default/Images/giris.png"/>
                
    <br /><br />
</asp:Panel>
<asp:Label runat="server" ID="lblDurum" CssClass="bilgi"></asp:Label>
<asp:Panel runat="server" ID="pnlHata">
    <span class="hata" style="font-size:14px; font-weight:bold;">
    Kod yanlış (Şifreni sıfırlamak için gelen e-postadaki link sadece aynı gün geçerlidir)
    <br /><br />Tekrar şifre sıfırlama
    talebinde bulunmak için sağ üstten "giriş"e tıklayıp "şifremi unuttum"a tıklayabilirsin
    </span>
</asp:Panel>
</div>
</asp:Content>