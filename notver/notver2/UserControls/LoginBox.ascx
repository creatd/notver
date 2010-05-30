<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginBox.ascx.cs" Inherits="UserControls_LoginBox" %>
<div id="LoginBox">
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            <div id="LoginBox_Once">
                <a name="login"></a>
                <!-- Buraya erismek icin kullanilan bos link -->
                <asp:Login ID="Login1" runat="server">
                    <LayoutTemplate>
                        <span style="padding-top: 31px; padding-left: 70px; float: left;">
                            <asp:TextBox runat="server" ID="UserName" Width="70" Class="seffafTextBox" ValidationGroup="vg"></asp:TextBox></span>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                            CssClass="Hata" ErrorMessage="*" ToolTip="Kullanici adi girmelisiniz" ValidationGroup="vg" />
                        <span style="padding-top: 10px; padding-left: 70px; float: left;">
                            <asp:TextBox runat="server" ID="Password" Width="70" Class="seffafTextBox" TextMode="Password"
                                ValidationGroup="vg"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                ErrorMessage="*" ToolTip="Sifre girmelisiniz" ValidationGroup="vg" CssClass="Hata" /></span>
                        <span class="sessiz fltLeft" style="padding-top:5px;">
                            <asp:CheckBox ID="RememberMe" runat="server" Text="beni hatirla" CssClass="sessiz styled" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="LoginButton" CommandName="Login" runat="server" ValidationGroup="vg">Giris yap</asp:LinkButton>
                        </span>
                    </LayoutTemplate>
                </asp:Login>
            </div>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <div id="LoginBox_Sonra">
                <asp:LoginName ID="LoginName1" runat="Server"></asp:LoginName>!
                <br /><br />
                <asp:LinkButton ID="LogoutButton" OnClick="LogOut" Text="Cikis yap" Visible="true" runat="server" CssClass="fltRight" />
            </div>
        </LoggedInTemplate>
    </asp:LoginView>
</div>
