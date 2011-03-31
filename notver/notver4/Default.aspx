<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" 
MasterPageFile="~/Masters/Giris.master" MaintainScrollPositionOnPostback="true" %>

<asp:Content ContentPlaceHolderID="content" runat="server">
<asp:Panel runat="server" ID="pnlTanitim">
<p style="font-size:16px; color:#626262; font-weight:bold; padding-top:50px; line-height:150%; text-align:center;">
NotVerin'e hoşgeldin!
<br /><br /><br /><br /> 
Şu an betadayız, bu nedenle sadece Boğaziçi Üniversitesi Bilgisayar Mühendisliğine dair
içerik mevcut. Testlerimiz bitince Boğaziçi ile başlayıp olabildiği kadar çok içerikle zenginleştireceğiz.
Bu süreçte bizimle paylaşmak istediklerin için aşağıda iletişim adreslerimizi bulabilirsin.
<br /><br /><br /><br />
Teşekkürler,<br />
NotVerin.com ekibi</p>
</asp:Panel>
<asp:Panel runat="server" ID="pnlHosgeldin">
<p style="font-size:16px; color:#626262; font-weight:bold; padding-top:50px; line-height:150%; text-align:center;">
    <span style="color:Black; font-size:18px;">NotVerin'e hoşgeldin!</span><br /><br />
    Girmis oldugun e-posta adresine gelen onay linkiyle uyeligini onaylatabilirsin.<br />
    Henuz yeni uye oldugun icin ilk yaptigin yorumlar onayimizdan gectikten sonra sistemde yayinlanmaya baslayacak.<br />
    Yorum yaptikca 1, dosya yukledikce ise 5 puan kazanacak; toplam 10 puan kazandiktan sonra da yaptigin yorumlar aninda sistemde yayinlanmaya
    baslayacak.<br /><br />
    Tekrar hosgeldin!
</p>
</asp:Panel>



</asp:Content>
