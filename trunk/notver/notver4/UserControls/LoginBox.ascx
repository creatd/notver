<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginBox.ascx.cs" Inherits="UserControls_LoginBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<script type="text/javascript">
function SetFocusLogin(e)
{
    var keycode;
    if(window.event)
    {
        keycode = window.event.keyCode;
    }
    else if(e.which)
    {
        keycode = e.which;
    }
    else if(e.keyCode)
    {
        keycode = e.keyCode;
    }

    if(keycode == 13)
    {
        document.getElementById('<%= LoginButton.ClientID %>').focus();
    }
    document.getElementById('<%= lblDurum.ClientID %>').innerHTML='';
}
/* textbox : onclick ve onblur event'lerine koyabilirsin
function Temizle(obj)   {
    if(obj.value == 'E-posta adresi girin') {
        obj.value='';
    }
} */

</script>


<div id="LoginBox">
    <asp:UpdatePanel runat="server" ID="pnlUP">
    <ContentTemplate>
        <div class="pnlLogin">
            <p>E-posta adresi</p>       
            <asp:TextBox runat="server" ID="txtEposta" CssClass="textbox"
                        ValidationGroup="vg" OnKeyDown="javascript:SetFocusLogin(event);"></asp:TextBox>
            <span style="display:block; overflow:hidden; float:right; width:50px;">
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtEposta"
                    CssClass="Hata" ErrorMessage="<img src='./App_Themes/Default/Images/hata.png' alt='hata' style='width:15px; float:left;'>" ToolTip="E-posta adresini girmelisin" ValidationGroup="vg" />
                <asp:RegularExpressionValidator runat="server" ID="emailFormat" ControlToValidate="txtEposta"
                        ErrorMessage="<img src='./App_Themes/Default/Images/hata.png' alt='hata' style='width:15px; float:left;'>" ToolTip="E-posta adresini yanlış girdin" CssClass="Hata"
                        ValidationExpression="^[a-zA-ZçşöüğıÇŞÖÜĞİ0-9.-_]+@[a-zA-ZçşöüğıÇŞÖÜĞİ0-9.:-]+[.][a-zA-ZçşöüğıÇŞÖÜĞİ0-9.:-]+$" ValidationGroup="vg" />
                <asp:RequiredFieldValidator runat="server" ID="kullaniciAdiGir" CssClass="Hata" ControlToValidate="txtEposta"
                    ErrorMessage="<img src='./App_Themes/Default/Images/hata.png' alt='hata' style='width:15px; float:left;'>" ToolTip="E-posta adresini girmelisin" ValidationGroup="vg2"></asp:RequiredFieldValidator>
            </span>
            <p>Şifre</p>
                    <asp:TextBox runat="server" ID="txtSifre" TextMode="Password" CssClass="textbox"
                        ValidationGroup="vg" OnKeyDown="javascript:SetFocusLogin(event);"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtSifre"
                        ErrorMessage="<img src='./App_Themes/Default/Images/hata.png' alt='hata'>" ToolTip="Şifre girmelisin" ValidationGroup="vg" CssClass="Hata" />
                    
            <br /><asp:ImageButton ID="LoginButton" OnClick="GirisYap" runat="server" ValidationGroup="vg" 
            CssClass="loginTus clear fltLeft"
                ImageUrl="~/App_Themes/Default/Images/giris.png"/>
            <!-- <asp:CheckBox ID="RememberMe" runat="server" Text="beni hatirla" CssClass="beniHatirla"/> -->
            <br />
            <asp:LinkButton runat="server" Text="Şifremi unuttum!" CssClass="fltLeft clear sifremiUnuttum" 
            OnClick="SifremiUnuttum" ValidationGroup="vg2"></asp:LinkButton>
            
            <asp:Label runat="server" ID="lblDurum" CssClass="hata fltLeft girisDurum"></asp:Label>
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    <div id="Bekleme">
        <img src="./Scripts/images/loading.gif" />
    </div>
    <ajax:UpdatePanelAnimationExtender runat="server" TargetControlID="pnlUP">
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
