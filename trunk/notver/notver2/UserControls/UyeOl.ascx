<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UyeOl.ascx.cs" Inherits="UserControls_UyeOl" %>

<script type="text/javascript">
function DurumTemizle() {
    document.getElementById("<%= lblDurum.ClientID %>").innerHTML  = "";
}

</script>

<div id="UyeOl">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="pnlUyeOl">
                <p style="margin-top:0px; padding-top:7px;">Ad (*)</p>
                <asp:TextBox ID="txtAd" runat="server" onchange="DurumTemizle();" CssClass="textbox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAd"
                        ErrorMessage="İsim girmelisin" ToolTip="Isim girilmelidir" ValidationGroup="vg1">*</asp:RequiredFieldValidator>                
                <p>Soyad (*)</p>
                <asp:TextBox ID="txtSoyad" runat="server" onchange="DurumTemizle();" CssClass="textbox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSoyad"
                        ErrorMessage="Soyad girmelisin" ToolTip="Soyad girilmelidir" ValidationGroup="vg1">*</asp:RequiredFieldValidator>                                
                <p>E-posta (*)</p>
                    <asp:TextBox ID="txtEposta" runat="server" onchange="DurumTemizle();" CssClass="textbox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtEposta"
                        ErrorMessage="E-posta adresi girmelisin" ToolTip="E-posta adresi girmelisin"
                        ValidationGroup="vg1">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ID="emailFormat" ControlToValidate="txtEposta"
                        ErrorMessage="E-posta adresini yanlış girdin" ToolTip="E-posta adresini yanlış girdin"
                        ValidationExpression="^[a-zA-Z0-9.-_]+@[a-zA-Z0-9.:-]+$" ValidationGroup="vg1">*</asp:RegularExpressionValidator>
                <p class="sessiz" style="color:#000000;">(Okul e-postanla kayıt olarak okulunu temsil edebilirsin)</p>
                <p style="font-style:italic; font-size:12px; padding-top:5px;">Kullanıcı adı (opsiyonel)</p>
                    <asp:TextBox ID="txtKullaniciAdi" runat="server" onchange="DurumTemizle();" CssClass="textbox"></asp:TextBox>
                <p class="sessiz" style="color:#000000;">(Yorumların yayınlanırken ilk ismin yerine kullanıcı adınla yayınlansın istiyorsan)</p>
                <p style="padding-top:5px;">Şifre (*)</p>
                    <asp:TextBox ID="txtSifre" runat="server" TextMode="Password" onchange="DurumTemizle();" CssClass="textbox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtSifre"
                        ErrorMessage="Şifre girmelisin" ToolTip="Şifre girmelisin"
                        ValidationGroup="vg1">*</asp:RequiredFieldValidator>
                <p>Şifre tekrar (*)</p>
                    <asp:TextBox ID="txtSifre2" runat="server" TextMode="Password" onchange="DurumTemizle();" CssClass="textbox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="txtSifre2"
                        ErrorMessage="Şifreni tekrar girmelisin" ToolTip="Şifreni tekrar girmelisin"
                        ValidationGroup="vg1">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" ID="Passwords" ControlToValidate="txtSifre" ControlToCompare="txtSifre2"
                        ErrorMessage="Girilen iki şifre aynı olmalı" ToolTip="Girilen iki şifre aynı olmalı"
                        ValidationGroup="vg1">*</asp:CompareValidator>
                <p>Cinsiyet</p>
                    <asp:RadioButtonList runat="server" ID="rdCinsiyetler" onchange="DurumTemizle();" CssClass="rdCinsiyetler">
                        <asp:ListItem Text="Bay" Selected="True" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Bayan" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                <p>Okulunuz</p>
                    <asp:DropDownList ID="ddOkullar" runat="server" onchange="DurumTemizle();" CssClass="drpOkullar">
                    </asp:DropDownList>
                <br /><asp:ImageButton ID="btnUyeOl" OnClick="KullaniciOlustur" runat="server" ValidationGroup="vg1" 
                    CssClass="loginTus clear fltLeft" CausesValidation="true"
                ImageUrl="~/App_Themes/Default/Images/giris.png"/>
                <asp:Label runat="server" ID="lblDurum" CssClass="hata fltLeft girisDurum"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>