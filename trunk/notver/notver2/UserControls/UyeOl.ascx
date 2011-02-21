<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UyeOl.ascx.cs" Inherits="UserControls_UyeOl" %>

<script type="text/javascript">
function DurumTemizle() {
    document.getElementById("<%= lblDurum.ClientID %>").innerHTML  = "";
}

</script>

<div id="UyeOl" style="padding-bottom:10px;">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="pnlUyeOl">
                <p>Ad (*)</p>
                <asp:TextBox ID="txtAd" runat="server" onchange="DurumTemizle();" CssClass="textbox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAd"
                        ErrorMessage="Isim girilmelidir" ToolTip="Isim girilmelidir" ValidationGroup="vg1">*</asp:RequiredFieldValidator>                
                <p>Soyad (*)</p>
                <asp:TextBox ID="txtSoyad" runat="server" onchange="DurumTemizle();" CssClass="textbox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSoyad"
                        ErrorMessage="Soyad girilmelidir" ToolTip="Soyad girilmelidir" ValidationGroup="vg1">*</asp:RequiredFieldValidator>                                
                <p>E-posta (*)</p>
                    <asp:TextBox ID="txtEposta" runat="server" onchange="DurumTemizle();" CssClass="textbox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtEposta"
                        ErrorMessage="E-posta adresi girmeniz gereklidir" ToolTip="E-posta adresi girmeniz gereklidir"
                        ValidationGroup="vg1">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ID="emailFormat" ControlToValidate="txtEposta"
                        ErrorMessage="E-posta adresini yanlis girdiniz" ToolTip="E-posta adresini yanlis girdiniz"
                        ValidationExpression="^[a-zA-Z0-9.-]+@[a-zA-Z0-9.:-]+$" ValidationGroup="vg1">*</asp:RegularExpressionValidator>
                <p class="sessiz">(Okul e-postanla kayit olarak okulunu temsil edebilirsin)</p>
                <p>Kullanici adi</p>
                    <asp:TextBox ID="txtKullaniciAdi" runat="server" onchange="DurumTemizle();" CssClass="textbox"></asp:TextBox>
                <p class="sessiz">(Yorumlarin yayinlanirken ilk ismin yerine kullanici adinla yayinlansin istiyorsan)</p>
                <p>Sifre (*)</p>
                    <asp:TextBox ID="txtSifre" runat="server" TextMode="Password" onchange="DurumTemizle();" CssClass="textbox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtSifre"
                        ErrorMessage="Şifre girmeniz gereklidir" ToolTip="Şifre girmeniz gereklidir"
                        ValidationGroup="vg1">*</asp:RequiredFieldValidator>
                <p>Sifre tekrar (*)</p>
                    <asp:TextBox ID="txtSifre2" runat="server" TextMode="Password" onchange="DurumTemizle();" CssClass="textbox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="txtSifre2"
                        ErrorMessage="Şifre tekrar girilmelidir" ToolTip="Şifre tekrar girilmelidir"
                        ValidationGroup="vg1">*</asp:RequiredFieldValidator>
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