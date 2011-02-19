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
}
</script>



<div id="LoginBox">
    <asp:Panel runat="server" ID="pnlNoLogin" Visible="false" CssClass="pnlLogin">
        <p>E-posta adresi</p>       
        <asp:TextBox runat="server" ID="txtEposta" Width="70"
                    ValidationGroup="vg" OnKeyDown="javascript:return SetFocusLogin(event);" 
                    onclick="javascript:return Temizle(this);">Buraya</asp:TextBox>
            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtEposta"
                CssClass="Hata" ErrorMessage="*" ToolTip="Kullanici adi girmelisiniz" ValidationGroup="vg" />
        <p>Sifre</p>
                <asp:TextBox runat="server" ID="txtSifre" Width="70" TextMode="Password"
                    ValidationGroup="vg" OnKeyDown="javascript:return SetFocusLogin(event);" onclick="javascript:return Temizle(this);"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtSifre"
                    ErrorMessage="*" ToolTip="Sifre girmelisiniz" ValidationGroup="vg" CssClass="Hata" />
        <br /><asp:ImageButton ID="LoginButton" OnClick="GirisYap" runat="server" ValidationGroup="vg"
            ImageUrl="~/App_Themes/Default/Images/giris.png" CssClass="clear fltLeft"/>
        <br /><asp:CheckBox ID="RememberMe" runat="server" Text="beni hatirla"/>
        <asp:HyperLink runat="server" Text="Sifremi unuttum" CssClass="fltLeft"></asp:HyperLink>
        <asp:Label runat="server" ID="lblDurum"></asp:Label>
        <asp:Label runat="server" ID="lblTimeout">Timeout!</asp:Label>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlLogin" Visible="false" class="doldur">
        <div id="LoginBox_Sonra">
            <%= session.KullaniciAd.ToString() %>
            !
            <br />
            <br />
            <asp:LinkButton ID="LogoutButton" OnClick="CikisYap" Text="Cikis yap" Visible="true"
                runat="server" CssClass="fltRight" />
        </div>
    </asp:Panel>
</div>
