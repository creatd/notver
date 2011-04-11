<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EpostaOnayla.aspx.cs" Inherits="EpostaOnayla" 
MasterPageFile="~/Masters/Giris.master"  MaintainScrollPositionOnPostback="true"%>

<asp:Content runat="server" ContentPlaceHolderID="content">
<div style="text-align:center; margin-top:30px;">
<asp:Panel runat="server" ID="pnlOnayBasari">
    <span style="font-size:18px; font-weight:bold;">
    - E-posta adresin onaylandı -
    </span>
</asp:Panel>
<asp:Panel runat="server" ID="pnlOnayBasari_UniEposta">
    <span style="font-size:18px; font-weight:bold;">
    - Üniversite e-posta adresin onaylandı -
    </span>
</asp:Panel>
<asp:Panel runat="server" ID="pnlOnayHata">
    <span class="hata" style="font-size:18px; font-weight:bold;">- E-posta onaylama başarısız -</span>
    <br /> 
    <br />
    <br />
    <br />
    <br />
    <span style="font-size:14px;">Tekrar onay e-postası göndermek için</span>
    <br /><br />
    <p>e-posta adresini gir</p>
    <br />
        <asp:TextBox ID="txtEposta" runat="server" onchange="DurumTemizle();" CssClass="textbox"></asp:TextBox>
        <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtEposta"
            ToolTip="E-posta adresini girmelisin" ErrorMessage="<img src='./App_Themes/Default/Images/hata.png' alt='hata'>"
            ValidationGroup="vg1" />
    <br />
    <asp:ImageButton ID="btnSifreDegistir" OnClick="OnayEpostasiGonder" runat="server" CausesValidation="true"
                    CssClass="loginTus clear" ValidationGroup="vg1"
                ImageUrl="~/App_Themes/Default/Images/giris.png"/>
    <br /><br />
    <asp:Label runat="server" ID="lblDurum"></asp:Label>
</asp:Panel>
</div>
</asp:Content>
