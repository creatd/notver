<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginBox.ascx.cs" Inherits="UserControls_LoginBox" %>

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

function Temizle(obj)
{
    obj.value='';
}
</script>


<div id="LoginBox">
    <asp:UpdatePanel runat="server" ID="pnlUP">
    <ContentTemplate>
        <div class="pnlLogin">
            <p>E-posta adresi</p>       
            <asp:TextBox runat="server" ID="txtEposta" CssClass="textbox"
                        ValidationGroup="vg" OnKeyDown="javascript:SetFocusLogin(event);" 
                        onclick="javascript:Temizle(this);"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtEposta"
                    CssClass="Hata" ErrorMessage="*" ToolTip="E-posta adresinizi girmelisiniz" ValidationGroup="vg" />
                <asp:RegularExpressionValidator runat="server" ID="emailFormat" ControlToValidate="txtEposta"
                        ErrorMessage="*" ToolTip="E-posta adresini yanlış girdin" CssClass="Hata"
                        ValidationExpression="^[a-zA-Z0-9.-_]+@[a-zA-Z0-9.:-]+$" ValidationGroup="vg">*</asp:RegularExpressionValidator>                    
            <p>Şifre</p>
                    <asp:TextBox runat="server" ID="txtSifre" TextMode="Password" CssClass="textbox"
                        ValidationGroup="vg" OnKeyDown="javascript:SetFocusLogin(event);" 
                        onclick="javascript:Temizle(this);"></asp:TextBox>
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
</div>
