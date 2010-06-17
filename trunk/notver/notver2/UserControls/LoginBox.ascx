﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginBox.ascx.cs" Inherits="UserControls_LoginBox" %>
<div id="LoginBox">
    <a name="login"></a>
    <!-- Buraya erismek icin kullanilan bos link -->
    <asp:Panel runat="server" ID="pnlNoLogin" Visible="false" class="doldur">
        <div id="LoginBox_Once">
            <span style="padding-top: 31px; padding-left: 70px; float: left;">
                <asp:TextBox runat="server" ID="txtKullaniciAdi" Width="70" Class="seffafTextBox"
                    ValidationGroup="vg"></asp:TextBox></span>
            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtKullaniciAdi"
                CssClass="Hata" ErrorMessage="*" ToolTip="Kullanici adi girmelisiniz" ValidationGroup="vg" />
            <span style="padding-top: 10px; padding-left: 70px; float: left;">
                <asp:TextBox runat="server" ID="txtSifre" Width="70" Class="seffafTextBox" TextMode="Password"
                    ValidationGroup="vg"></asp:TextBox>
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
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlLogin" Visible="false" class="doldur">
        <div id="LoginBox_Sonra">
            <asp:LoginName ID="LoginName1" runat="Server"></asp:LoginName>
            !
            <br />
            <br />
            <asp:LinkButton ID="LogoutButton" OnClick="CikisYap" Text="Cikis yap" Visible="true"
                runat="server" CssClass="fltRight" />
        </div>
    </asp:Panel>
</div>