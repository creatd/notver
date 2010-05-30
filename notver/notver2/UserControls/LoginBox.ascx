<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginBox.ascx.cs" Inherits="UserControls_LoginBox" %>

<div id="LoginBox">
<asp:LoginView ID="LoginView1" runat="server">
    <AnonymousTemplate>
    <div id="LoginBox_Once">
        <a name="login"></a> <!-- Buraya erismek icin kullanilan bos link -->
        <span style="padding-top:35px; padding-left:70px; float:left;"><asp:TextBox runat="server" ID="txtUyeAdi" Width="70" Class="seffafTextBox"></asp:TextBox></span>
        <span style="padding-top:10px; padding-left:70px;float:left;"><asp:TextBox runat="server" ID="txtSifre" Width="70" Class="seffafTextBox" TextMode="Password"></asp:TextBox></span>
        <span class="sessiz fltLeft" style="padding-top:10px;padding-left:97px;"><asp:Hyperlink runat="server" NavigateUrl="~/Giris.aspx">Giris yap</asp:Hyperlink>
        
    </div>
    </AnonymousTemplate>
    <LoggedInTemplate>
    <div id="LoginBox_Sonra">
        Hello
        <asp:LoginName ID="LoginName1" runat="Server"></asp:LoginName>
        .
        <asp:Button ID="LogoutButton" OnClick="LogOut" Text="Log out" Visible="true" runat="server" />
    </div>
    </LoggedInTemplate>
</asp:LoginView>
</div>
