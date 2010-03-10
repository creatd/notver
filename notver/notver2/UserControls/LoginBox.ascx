<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginBox.ascx.cs" Inherits="UserControls_LoginBox" %>




<asp:LoginView ID="LoginView1" runat="server">
    <AnonymousTemplate>
        <asp:Hyperlink runat="server" NavigateUrl="~/Giris.aspx?height=150&width=300&modal=true&TB_iframe=true" CssClass="thickbox">Giris yapin</asp:Hyperlink> ya da
        <asp:HyperLink ID="UyeOlunLink" NavigateUrl="~/Register.aspx" runat="server" Text="Uye Olun"></asp:HyperLink>
    </AnonymousTemplate>
    <LoggedInTemplate>
        Hello
        <asp:LoginName ID="LoginName1" runat="Server"></asp:LoginName>
        .
        <asp:Button ID="LogoutButton" OnClick="LogOut" Text="Log out" Visible="true" runat="server" />
    </LoggedInTemplate>
</asp:LoginView>
