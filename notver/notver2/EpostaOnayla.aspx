<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EpostaOnayla.aspx.cs" Inherits="EpostaOnayla" 
MasterPageFile="~/Masters/Giris.master" %>

<asp:Content runat="server" ContentPlaceHolderID="content">
<div style="text-align:center; margin-top:30px;">
<asp:Panel runat="server" ID="pnlOnayBasari">
    <span style="font-size:18px; font-weight:bold;">
    E-posta adresin onaylandi.
    </span>
</asp:Panel>
<asp:Panel runat="server" ID="pnlOnayBasari_UniEposta">
    <span style="font-size:18px; font-weight:bold;">
    Universite e-posta adresin onaylandi.
    </span>
</asp:Panel>
<asp:Panel runat="server" ID="pnlOnayHata">
    <span class="hata" style="font-size:18px; font-weight:bold;">E-posta onaylama basarisiz.</span>
    <br /> 
    <br />
    <br />
    <br />
    <br />
    <span style="font-size:14px;">Tekrar onay e-postasi gondermek icin</span>
    <br /><br />
    <p>e-posta adresinizi girin</p>
    <br />
        <asp:TextBox ID="txtEposta" runat="server" onchange="DurumTemizle();" CssClass="textbox"></asp:TextBox>
        <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtEposta"
            ErrorMessage="E-posta adresi girmeniz gereklidir" ToolTip="E-posta adresi girmeniz gereklidir"
            ValidationGroup="vg1">*</asp:RequiredFieldValidator>
    <br />
    <asp:ImageButton ID="btnSifreDegistir" OnClick="OnayEpostasiGonder" runat="server" CausesValidation="true"
                    CssClass="loginTus clear"
                ImageUrl="~/App_Themes/Default/Images/giris.png"/>
    <br /><br />
    <asp:Label runat="server" ID="lblDurum"></asp:Label>
</asp:Panel>
</div>
</asp:Content>
