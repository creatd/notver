<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UyeOl.ascx.cs" Inherits="UserControls_UyeOl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<script type="text/javascript">
function DurumTemizle() {
    document.getElementById("<%= lblDurum.ClientID %>").innerHTML  = "";
}

</script>

<div id="UyeOl">
    <asp:UpdatePanel runat="server" ID="pnlUpdate">
        <ContentTemplate>
            <div class="pnlUyeOl">
                <p style="margin-top: 0px; padding-top: 7px;">
                    Ad (*)</p>
                <asp:TextBox ID="txtAd" runat="server" onchange="DurumTemizle();" CssClass="textbox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAd"
                    ErrorMessage="<img src='./App_Themes/Default/Images/hata.png' alt='hata'>" ToolTip="İsim girmelisin" 
                    ValidationGroup="vg1" />
                <p>
                    Soyad (*)</p>
                <asp:TextBox ID="txtSoyad" runat="server" onchange="DurumTemizle();" CssClass="textbox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSoyad"
                    ErrorMessage="<img src='./App_Themes/Default/Images/hata.png' alt='hata'>" ToolTip="Soyad girmelisin" 
                    ValidationGroup="vg1" />
                <p>
                    E-posta (*)</p>
                <asp:TextBox ID="txtEposta" runat="server" onchange="DurumTemizle();" CssClass="textbox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtEposta"
                    ErrorMessage="<img src='./App_Themes/Default/Images/hata.png' alt='hata'>" 
                    ToolTip="E-posta adresini girmelisin"
                    ValidationGroup="vg1" />
                <asp:RegularExpressionValidator runat="server" ID="emailFormat" ControlToValidate="txtEposta"
                    ErrorMessage="<img src='./App_Themes/Default/Images/hata.png' alt='hata'>" ToolTip="E-posta adresini yanlış girdin"
                    ValidationExpression="^[a-zA-ZçşöüğıÇŞÖÜĞİ0-9.-_]+@[a-zA-ZçşöüğıÇŞÖÜĞİ0-9.:-]+[.][a-zA-ZçşöüğıÇŞÖÜĞİ0-9.:-]+$" ValidationGroup="vg1" />
                <p class="sessiz" style="color: #000000;">
                    (Okul e-postanla kayıt olarak okulunu temsil edebilirsin)</p>
                <p style="font-style: italic; font-size: 12px; padding-top: 5px;">
                    Kullanıcı adı (opsiyonel)</p>
                <asp:TextBox ID="txtKullaniciAdi" runat="server" onchange="DurumTemizle();" CssClass="textbox"></asp:TextBox>
                <p class="sessiz" style="color: #000000;">
                    (Yorumların yayınlanırken ilk ismin yerine kullanıcı adınla yayınlansın istiyorsan)</p>
                <p>
                    Cinsiyet</p>
                <asp:RadioButtonList runat="server" ID="rdCinsiyetler" onchange="DurumTemizle();"
                    CssClass="rdCinsiyetler">
                    <asp:ListItem Text="Bay" Selected="True" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Bayan" Value="1"></asp:ListItem>
                </asp:RadioButtonList>
                <p>
                    Okulun</p>
                <asp:DropDownList ID="ddOkullar" runat="server" onchange="DurumTemizle();" CssClass="drpOkullar"
                 OnSelectedIndexChanged="OkulSecildi" AutoPostBack="true" />
                <p>
                    Bölümün</p>
                <asp:DropDownList ID="drpBolumler" runat="server" onchange="DurumTemizle();" CssClass="drpOkullar" />
                <p style="padding-top: 5px;">
                    Şifre (*)</p>
                <asp:TextBox ID="txtSifre" runat="server" TextMode="Password" onchange="DurumTemizle();"
                    CssClass="textbox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtSifre"
                    ErrorMessage="<img src='./App_Themes/Default/Images/hata.png' alt='hata'>" 
                    ToolTip="Şifre girmelisin" ValidationGroup="vg1" />
                <p>
                    Şifre tekrar (*)</p>
                <asp:TextBox ID="txtSifre2" runat="server" TextMode="Password" onchange="DurumTemizle();"
                    CssClass="textbox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="txtSifre2"
                    ErrorMessage="<img src='./App_Themes/Default/Images/hata.png' alt='hata'>" ToolTip="Şifreni tekrar girmelisin"
                    ValidationGroup="vg1" />
                <asp:CompareValidator runat="server" ID="Passwords" ControlToValidate="txtSifre"
                    ControlToCompare="txtSifre2" ErrorMessage="<img src='./App_Themes/Default/Images/hata.png' alt='hata'>" ToolTip="Girilen iki şifre aynı olmalı"
                    ValidationGroup="vg1" />
                <p style="text-align:left;">
                <asp:ImageButton ID="btnUyeOl" OnClick="KullaniciOlustur" runat="server" ValidationGroup="vg1"
                    CssClass="loginTus clear" CausesValidation="true" ImageUrl="~/App_Themes/Default/Images/giris.png" />
                <asp:Label runat="server" ID="lblDurum" CssClass="hata girisDurum"></asp:Label>
                </p>
                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="Bekleme">
        <img src="./Scripts/images/loading.gif" />
    </div>
    <ajax:UpdatePanelAnimationExtender runat="server" TargetControlID="pnlUpdate">
    <Animations>
        <OnUpdating>
            <StyleAction animationtarget="Bekleme" Attribute="display" value="block" />
        </OnUpdating>
        <OnUpdated>
            <StyleAction animationtarget="Bekleme" Attribute="display" value="none" />
        </OnUpdated>
    </Animations>
    </ajax:UpdatePanelAnimationExtender>   
</div>

