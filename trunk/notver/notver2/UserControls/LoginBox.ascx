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
}

function Temizle(obj)
{
    obj.value='';
    document.getElementById('<%= lblDurum.ClientID %>').innerHTML='';
}
</script>


<div id="LoginBox">
    <asp:UpdatePanel runat="server">
    <ContentTemplate>
        <div class="pnlLogin">
            <p>E-posta adresi</p>       
            <asp:TextBox runat="server" ID="txtEposta" CssClass="textbox"
                        ValidationGroup="vg" OnKeyDown="javascript:return SetFocusLogin(event);" 
                        onclick="javascript:return Temizle(this);"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtEposta"
                    CssClass="Hata" ErrorMessage="*" ToolTip="Kullanici adi girmelisiniz" ValidationGroup="vg" />
            <p>Sifre</p>
                    <asp:TextBox runat="server" ID="txtSifre" TextMode="Password" CssClass="textbox"
                        ValidationGroup="vg" OnKeyDown="javascript:return SetFocusLogin(event);" 
                        onclick="javascript:return Temizle(this);"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtSifre"
                        ErrorMessage="*" ToolTip="Sifre girmelisiniz" ValidationGroup="vg" CssClass="Hata" />
            <br /><asp:ImageButton ID="LoginButton" OnClick="GirisYap" runat="server" ValidationGroup="vg" 
            CssClass="loginTus clear fltLeft"
                ImageUrl="~/App_Themes/Default/Images/giris.png"/>
            <asp:CheckBox ID="RememberMe" runat="server" Text="beni hatirla" CssClass="beniHatirla"/>
            <br />
            <asp:HyperLink runat="server" Text="Sifremi unuttum!" CssClass="fltLeft clear sifremiUnuttum"></asp:HyperLink>
            <asp:Label runat="server" ID="lblDurum" CssClass="hata fltLeft girisDurum"></asp:Label>
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</div>
