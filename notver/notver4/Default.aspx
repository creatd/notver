<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" 
MasterPageFile="~/Masters/Giris.master" MaintainScrollPositionOnPostback="true" %>

<asp:Content ContentPlaceHolderID="content" runat="server">
<asp:Panel runat="server" ID="pnlTanitim">
<div style="margin-top:20px; padding-left:20px; text-align:center;">
<img src="App_Themes/Default/Images/default.png" />
</div>
<table>
    <tr style="font-size:15px; color:#626262; font-weight:bold; padding-top:50px; line-height:150%; text-align:center;">
        <td style="width:300px; padding-left:15px; line-height:120%;">"Aldığın dersi ara yorumları oku,<br />
        önemli noktaları öğren!<br />
        Dersin arşivini bul, sınavları kolaylaştır.<br />
        Üye ol, ders notunu paylaş, herkese kolaylaştır!<br />
        Yorum yap, dersi tanıt."</td>
        <td style="width:260px; padding-left:25px; line-height:120%;">"Aradığın hocayla ilgili yapılmış<br />
        yorumları oku, üye ol sen de yorum yap.<br />
        Ders alacağın hocayı yakından tanı,<br />
        yeni alacaklara sen tanıt!"</td>
        <td style="width:300px; padding-left:20px; line-height:120%;">"Okulun hakkında neler söylenmiş, incele!<br />
        Üye ol yorum yap, okulunu tanıt!</td>
    </tr>
</table>
</asp:Panel>
<asp:Panel runat="server" ID="pnlHosgeldin">
<p style="font-size:16px; color:#626262; font-weight:bold; padding-top:50px; line-height:150%; text-align:center;">
    <span style="color:Black; font-size:18px;">NotVerin'e hoşgeldin!</span><br /><br />
    Girmiş olduğun e-posta adresine gelen onay linkiyle üyeliğini onaylatabilirsin.<br />
    Henüz yeni üye olduğun için ilk yaptığın yorumlar onayıdan geçtikten sonra sistemde yayınlanmaya başlayacak.<br />
    Yorum yaptıkça <strong>1</strong>, dosya yukledikçe <strong>5</strong> puan kazanacak; toplam 10 puan kazandıktan sonra da yaptığın yorumlar anında sistemde yayınlanmaya
    başlayacak.<br /><br />
    Tekrar hoşgeldin!
</p>
</asp:Panel>



</asp:Content>
