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
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtEposta"
                    CssClass="Hata" ErrorMessage="*" ToolTip="E-posta adresinizi girmelisiniz" ValidationGroup="vg" />
                <asp:RegularExpressionValidator runat="server" ID="emailFormat" ControlToValidate="txtEposta"
                        ErrorMessage="*" ToolTip="E-posta adresini yanlış girdin" CssClass="Hata"
                        ValidationExpression="^[a-zA-Z0-9.-_]+@[a-zA-Z0-9.:-]+$" ValidationGroup="vg">*</asp:RegularExpressionValidator>                    
            <p>Şifre</p>
                    <asp:TextBox runat="server" ID="txtSifre" TextMode="Password" CssClass="textbox"
                        ValidationGroup="vg" OnKeyDown="javascript:SetFocusLogin(event);"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtSifre"
                        ErrorMessage="*" ToolTip="Şifre girmelisin" ValidationGroup="vg" CssClass="Hata" />
                    
            <br /><asp:ImageButton ID="LoginButton" OnClick="GirisYap" runat="server" ValidationGroup="vg" 
            CssClass="loginTus clear fltLeft"
                ImageUrl="~/App_Themes/Default/Images/giris.png"/>
            <!-- <asp:CheckBox ID="RememberMe" runat="server" Text="beni hatirla" CssClass="beniHatirla"/> -->
            <br />
            <asp:LinkButton runat="server" Text="Şifremi unuttum!" CssClass="fltLeft clear sifremiUnuttum" 
            OnClick="SifremiUnuttum"></asp:LinkButton>
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
