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
    <span class="hata" style="font-size:18px; font-weight:bold;">E-posta onaylama basarisiz, tekrar onay e-postasi gondermek icin 
    <asp:Hyperlink runat="server" ID="lnkTekrarOnay"><span style="font-size:18px; font-weight:bold;">tiklayin</span></asp:Hyperlink>
    </span>
</asp:Panel>
</div>
</asp:Content>
