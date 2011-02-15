<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Kayit.aspx.cs" Inherits="Kayit"
    MasterPageFile="~/Masters/Giris.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
<script type="text/javascript">
function DurumTemizle() {
    document.getElementById("<%= lblDurum.ClientID %>").innerHTML  = "";
}

</script>
</asp:Content>


<asp:Content ContentPlaceHolderID="content" runat="server">
    <asp:Panel runat="server" ID="pnlUyeOl" Visible="true" CssClass="UyeOl">
        <table border="0" style="width:250px;">
            <tr style="height:190px;">
                <td>
                    <br />
                </td>
            </tr>
            <tr style="height:66px; vertical-align:top;">
                <td style="padding-left:65px;">
                    <asp:TextBox ID="txtAd" runat="server" onchange="DurumTemizle();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAd"
                        ErrorMessage="Ad girilmelidir" ToolTip="Ad girilmelidir" ValidationGroup="vg1">*</asp:RequiredFieldValidator>                
                </td>
            </tr>
            <tr style="height:66px; vertical-align:top;">
                <td style="padding-left:65px;">
                    <asp:TextBox ID="txtSoyad" runat="server" onchange="DurumTemizle();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSoyad"
                        ErrorMessage="Soyad girilmelidir" ToolTip="Soyad girilmelidir" ValidationGroup="vg1">*</asp:RequiredFieldValidator>                
                </td>
            </tr>            
            <tr style="height:94px; vertical-align:top;">
                <td style="padding-left:65px;">
                    <asp:TextBox ID="txtEposta" runat="server" onchange="DurumTemizle();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtEposta"
                        ErrorMessage="E-posta adresi girmeniz gereklidir" ToolTip="E-posta adresi girmeniz gereklidir"
                        ValidationGroup="vg1">*</asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr style="height:65px; vertical-align:top;">
                <td style="padding-left:65px;">
                    <asp:TextBox ID="txtKullaniciAdi" runat="server" onchange="DurumTemizle();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtKullaniciAdi"
                        ErrorMessage="Kullanıcı adı girmeniz gereklidir" ToolTip="Kullanıcı adı girmeniz gereklidir"
                        ValidationGroup="vg1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="height:65px; vertical-align:top;">
                <td style="padding-left:65px;">
                    <asp:TextBox ID="txtSifre" runat="server" TextMode="Password" onchange="DurumTemizle();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtSifre"
                        ErrorMessage="Şifre girmeniz gereklidir" ToolTip="Şifre girmeniz gereklidir"
                        ValidationGroup="vg1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="height:48px; vertical-align:top;">
                <td style="padding-left:65px;">
                    <asp:TextBox ID="txtSifre2" runat="server" TextMode="Password" onchange="DurumTemizle();"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="txtSifre2"
                        ErrorMessage="Şifre tekrar girilmelidir" ToolTip="Şifre tekrar girilmelidir"
                        ValidationGroup="vg1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="height:120px; vertical-align:top;">
                 <td style="padding-left:220px;">
                    <asp:RadioButtonList runat="server" ID="rdCinsiyetler" onchange="DurumTemizle();">
                        <asp:ListItem Text="" Selected="True" Value="0"></asp:ListItem>
                        <asp:ListItem Text="" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr style="height:70px; vertical-align:top;">
                <td style="padding-left:65px;">
                    <asp:DropDownList ID="ddOkullar" runat="server" onchange="DurumTemizle();">
                    </asp:DropDownList>
                </td>
            </tr>           
            <tr style="height:70px; vertical-align:top;">
                <td style="padding-left:50px;">
                    <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="txtSifre"
                        ControlToValidate="txtSifre2" Display="Dynamic" ErrorMessage="Girdiğiniz iki şifre de aynı olmalıdır"
                        ValidationGroup="vg1"></asp:CompareValidator>
                </td>
            </tr>
           <tr style="height:70px; vertical-align:top;">
                <td style="color: Red;padding-left:50px;">
                    <asp:Label ID="lblDurum" runat="server" EnableViewState="False"></asp:Label>
                </td>
            </tr>
            <tr style="height:70px; vertical-align:top;">
                <td style="padding-left:170px;">
                    <asp:ImageButton runat="server" OnClick="KullaniciOlustur" CausesValidation="true" ValidationGroup="vg1"
                    ImageUrl="~/App_Themes/Default/Images/optik/testim_bitti.png" AlternateText="Testim bitti"/>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlUyeOl_Sonra" Visible="false">
        <table border="0">
            <tr>
                <td align="center" colspan="2">
                    Hesabınız oluşturulmuştur. Eposta adresinize onay mesajı gönderilmiştir. Üye ayrıcalıklarından
                    yararlanabilmek için bu onay mesajındaki linke tıklayarak üyeliğinizi onaylamanız
                    gereklidir.
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:Button ID="btnDevam" runat="server" CausesValidation="False"
                        Text="Ana sayfaya don" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
