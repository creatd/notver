<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" 
MasterPageFile="~/Masters/Giris.master" MaintainScrollPositionOnPostback="true" %>

<asp:Content ContentPlaceHolderID="content" runat="server">
<p style="font-size:16px; color:#626262; font-weight:bold; padding-top:50px; line-height:150%; text-align:center;">
NotVerin'e hoşgeldin!
<br /><br /><br /><br /> 
Şu an betadayız, bu nedenle sadece Boğaziçi Üniversitesi Bilgisayar Mühendisliğine dair
içerik mevcut. Testlerimiz bitince Boğaziçi ile başlayıp olabildiği kadar çok içerikle zenginleştireceğiz.
Bu süreçte bizimle paylaşmak istediklerin için aşağıda iletişim adreslerimizi bulabilirsin.
<br /><br /><br /><br />
Teşekkürler,<br />
NotVerin.com ekibi</p>
<asp:Label runat="server" ID="lblTimeout">
Yarim saattir yoksun diye gittin sandık. Tekrar giriş yapmalısın.
</asp:Label>
</asp:Content>
