<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Giris.aspx.cs" Inherits="Giris" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Notverin - Giris yap ya da uye ol</title>
    <link href="../App_Themes/Default/reset.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Default/Default.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Default/thickbox.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
    function DurumTemizle() {
        document.getElementById("<%= lblDurum1.ClientID %>").innerHTML  = "";
        document.getElementById("<%= lblDurum2.ClientID %>").innerHTML  = "";
    }
    </script>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <table border="0" style="width:250px;">
                <tr><td>E-posta adresi</td></tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtEposta" runat="server" onchange="DurumTemizle();"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtEposta"
                            ErrorMessage="E-posta adresi girmeniz gereklidir" ToolTip="E-posta adresi girmeniz gereklidir"
                            ValidationGroup="vg1">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr><td><h1>Giris yapin</h1></td></tr>
                <tr><td>Sifre</td></tr>
                <tr>                    
                    <td><asp:TextBox ID="txtSifreGiris" runat="server"></asp:TextBox></td>
                    <td><asp:LinkButton runat="server" ID="btnGirisYap" OnClick="GirisYap">Giris Yap</asp:LinkButton></td>
                </tr>        
                <tr>
                    <td style="color: Red;padding-left:50px;">
                        <asp:Label ID="lblDurum1" runat="server" EnableViewState="False"></asp:Label>
                    </td>
                </tr>
                <tr><td><h1>Ya da hemen ucretsiz uye olun</h1></td></tr>          
                <tr><td>Ad</td></tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtAd" runat="server" onchange="DurumTemizle();"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAd"
                            ErrorMessage="Ad girilmelidir" ToolTip="Ad girilmelidir" ValidationGroup="vg1">*</asp:RequiredFieldValidator>                
                    </td>
                </tr>
                <tr><td>Soyad</td></tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtSoyad" runat="server" onchange="DurumTemizle();"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSoyad"
                            ErrorMessage="Soyad girilmelidir" ToolTip="Soyad girilmelidir" ValidationGroup="vg1">*</asp:RequiredFieldValidator>                
                    </td>
                </tr>                
                <tr><td>Kullanici Adi</td></tr>
                <tr><td class="mute">Yorumlarinizi isim yerine kullanici adinizla yayinlayabilirsiniz</td></tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtKullaniciAdi" runat="server" onchange="DurumTemizle();"></asp:TextBox>
                    </td>
                </tr>
                <tr><td>Sifre</td></tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtSifre" runat="server" TextMode="Password" onchange="DurumTemizle();"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtSifre"
                            ErrorMessage="Şifre girmeniz gereklidir" ToolTip="Şifre girmeniz gereklidir"
                            ValidationGroup="vg1">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr><td>Sifre (tekrar)</td></tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtSifre2" runat="server" TextMode="Password" onchange="DurumTemizle();"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="txtSifre2"
                            ErrorMessage="Şifre tekrar girilmelidir" ToolTip="Şifre tekrar girilmelidir"
                            ValidationGroup="vg1">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr><td>Cinsiyet</td></tr>
                <tr>
                     <td>
                        <asp:RadioButtonList runat="server" ID="rdCinsiyetler" onchange="DurumTemizle();">
                            <asp:ListItem Text="" Selected="True" Value="0"></asp:ListItem>
                            <asp:ListItem Text="" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr><td>Okulunuz</td></tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddOkullar" runat="server" onchange="DurumTemizle();">
                        </asp:DropDownList>
                    </td>
                </tr>           
                <tr>
                    <td>
                        <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="txtSifre"
                            ControlToValidate="txtSifre2" Display="Dynamic" ErrorMessage="Girdiğiniz iki şifre de aynı olmalıdır"
                            ValidationGroup="vg1"></asp:CompareValidator>
                    </td>
                </tr>
               <tr>
                    <td style="color: Red;padding-left:50px;">
                        <asp:Label ID="lblDurum2" runat="server" EnableViewState="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="ImageButton1" runat="server" OnClick="KullaniciOlustur" CausesValidation="true" ValidationGroup="vg1"
                        ImageUrl="~/App_Themes/Default/Images/optik/testim_bitti.png" AlternateText="Testim bitti"/>
                    </td>
                </tr>
            </table>
    </div>
    </form>
</body>
</html>
