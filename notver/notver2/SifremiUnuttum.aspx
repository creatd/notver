<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SifremiUnuttum.aspx.cs" Inherits="SifremiUnuttum" 
MasterPageFile="~/Masters/Giris.master"%>

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
    Asagidaki kutulara yeni sifrenizi iki kere girerek sifrenizi degistirebilirsiniz.
    </span>
    <br /><br /><br />
    <p>Sifre</p>
        <asp:TextBox ID="txtSifre" runat="server" TextMode="Password" onchange="DurumTemizle();" CssClass="textbox"></asp:TextBox>
        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtSifre"
                        ErrorMessage="Şifre girmeniz gereklidir" ToolTip="Şifre girmeniz gereklidir"
                        >*</asp:RequiredFieldValidator>
    <p style="margin-top:20px;">Sifre tekrar</p>
        <asp:TextBox ID="txtSifre2" runat="server" TextMode="Password" onchange="DurumTemizle();" CssClass="textbox"></asp:TextBox>
        <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="txtSifre2"
                        ErrorMessage="Şifre tekrar girilmelidir" ToolTip="Şifre tekrar girilmelidir"
                        >*</asp:RequiredFieldValidator>
        <asp:CompareValidator runat="server" ID="Passwords" ControlToValidate="txtSifre2" ControlToCompare="txtSifre"
                        ErrorMessage="Girilen iki sifre ayni olmali" ToolTip="Girilen iki sifre ayni olmali"
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
    Kod yanlis. Sifrenizi sifirlamak icin gelen e-postadaki kod sadece ayni gun gecerlidir. 
    <br /><br />Tekrar sifre sifirlama
    talebinde bulunmak icin sag ustten "giris"e tiklayip "sifremi unuttum"a tiklayabilirsiniz.    
    </span>
</asp:Panel>
</div>
</asp:Content>