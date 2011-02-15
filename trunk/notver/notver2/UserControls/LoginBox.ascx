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
    <a name="login"></a>
    <!-- Buraya erismek icin kullanilan bos link -->
    <asp:Panel runat="server" ID="pnlNoLogin" Visible="false" class="doldur">
        <div id="LoginBox_Once">
            <span style="padding-top: 31px; padding-left: 70px; float: left;">
                <asp:TextBox runat="server" ID="txtEposta" Width="70" Class="seffafTextBox"
                    ValidationGroup="vg" OnKeyDown="javascript:return SetFocusLogin(event);" onclick="javascript:return Temizle(this);">Buraya</asp:TextBox></span>
            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtEposta"
                CssClass="Hata" ErrorMessage="*" ToolTip="Kullanici adi girmelisiniz" ValidationGroup="vg" />
            <span style="padding-top: 10px; padding-left: 70px; float: left;">
                <asp:TextBox runat="server" ID="txtSifre" Width="70" Class="seffafTextBox" TextMode="Password"
                    ValidationGroup="vg" OnKeyDown="javascript:return SetFocusLogin(event);" onclick="javascript:return Temizle(this);"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtSifre"
                    ErrorMessage="*" ToolTip="Sifre girmelisiniz" ValidationGroup="vg" CssClass="Hata" /></span>
            <span class="sessiz fltLeft" style="padding-top: 5px;">
                <asp:CheckBox ID="RememberMe" runat="server" Text="beni hatirla" CssClass="sessiz styled" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="LoginButton" OnClick="GirisYap" runat="server" ValidationGroup="vg">Giris yap</asp:LinkButton>
            </span>
            <span>
                <asp:Label runat="server" ID="lblDurum"></asp:Label>
            </span>
        </div>
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
