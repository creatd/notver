<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register"
    MasterPageFile="~/Masters/Giris.master" %>

<asp:Content ContentPlaceHolderID="content" runat="server">
    <asp:Panel runat="server" ID="pnlUyeOl" Visible="true">
        <table border="0">
            <tr>
                <td align="center" colspan="2">
                    Aramıza katılmak için aşağıdaki 6 kutuyu doldurmanız yeterli!
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="UserNameLabel" runat="server">Kullanıcı 
                                adı:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtKullaniciAdi" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtKullaniciAdi"
                        ErrorMessage="Kullanıcı adı girmeniz gereklidir" ToolTip="Kullanıcı adı girmeniz gereklidir"
                        ValidationGroup="vg1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="PasswordLabel" runat="server">Şifre:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSifre" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtSifre"
                        ErrorMessage="Şifre girmeniz gereklidir" ToolTip="Şifre girmeniz gereklidir"
                        ValidationGroup="vg1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="ConfirmPasswordLabel" runat="server">Şifre (tekrar):</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSifre2" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="txtSifre2"
                        ErrorMessage="Şifre tekrar girilmelidir" ToolTip="Şifre tekrar girilmelidir"
                        ValidationGroup="vg1">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" ID="CompareValidatorForPassword" ControlToValidate="txtSifre2"
                        ControlToCompare="txtSifre" ErrorMessage="Iki girilen sifre ayni olmalidir" Display="Dynamic"
                        ValidationGroup="vg1"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="IsimLabel" runat="server">Isminiz (Ad Soyad):</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtIsim" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="IsimRequired" runat="server" ControlToValidate="txtIsim"
                        ErrorMessage="Isim girilmelidir" ToolTip="Isim girilmelidir" ValidationGroup="vg1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCinsiyet" runat="server">Cinsiyetiniz :</asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList runat="server" ID="rdCinsiyetler">
                        <asp:ListItem Text="Bay" Selected="True" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Bayan" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="OkulLabel" runat="server">Okulunuz (opsiyonel):</asp:Label>
                    <br />
                    <asp:Label ID="OkulNot" runat="server">(Okulunuza ait eposta adresinizle kayit olarak okulunuzu onaylatabilirsiniz)</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddOkullar" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="EmailLabel" runat="server">E-posta adresi:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEposta" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtEposta"
                        ErrorMessage="E-posta adresi girmeniz gereklidir" ToolTip="E-posta adresi girmeniz gereklidir"
                        ValidationGroup="vg1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="txtSifre"
                        ControlToValidate="txtSifre2" Display="Dynamic" ErrorMessage="Girdiğiniz iki şifre de aynı olmalıdır"
                        ValidationGroup="vg1"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="color: Red;">
                    <asp:Literal ID="lblDurum" runat="server" EnableViewState="False"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:LinkButton runat="server" Text="Uye Ol" OnClick="KullaniciOlustur" CausesValidation="true"
                    ValidationGroup="vg1"></asp:LinkButton>
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
