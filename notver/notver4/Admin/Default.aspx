<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs"  ValidateRequest="false"
Inherits="Admin_Default" MasterPageFile="~/Masters/Admin.master" MaintainScrollPositionOnPostback="true" %>

<asp:Content runat="server" ContentPlaceHolderID="content">
<asp:ScriptManager runat="server"></asp:ScriptManager>
<h1>Admin paneli</h1>
<h1>Site istatistikleri</h1>
<p>Onaylanmış (Toplam)</p>
<p>
    Üye sayısı : &nbsp;&nbsp;<strong><asp:Label runat="server" ID="lblUyeSayisi1"></asp:Label></strong>
    &nbsp;&nbsp;(<asp:Label runat="server" ID="lblUyeSayisi2"></asp:Label>)
</p>
<p>
    Toplam yorum sayısı : &nbsp;&nbsp;<strong><asp:Label runat="server" ID="lblToplamYorum1"></asp:Label>
    &nbsp;&nbsp;(<asp:Label runat="server" ID="lblToplamYorum2"></asp:Label>)
    </strong>
</p>
<p>
    Okul yorum sayısı : &nbsp;&nbsp;<strong><asp:Label runat="server" ID="lblOkulYorumSayisi1"></asp:Label>
    &nbsp;&nbsp;(<asp:Label runat="server" ID="lblOkulYorumSayisi2"></asp:Label>)
    </strong>
</p>
<p>
    Hoca yorum sayısı : &nbsp;&nbsp;<strong><asp:Label runat="server" ID="lblHocaYorumSayisi1"></asp:Label>
    &nbsp;&nbsp;(<asp:Label runat="server" ID="lblHocaYorumSayisi2"></asp:Label>)
    </strong>
</p>
<p>
    Ders yorum sayısı : &nbsp;&nbsp;<strong><asp:Label runat="server" ID="lblDersYorumSayisi1"></asp:Label>
    &nbsp;&nbsp;(<asp:Label runat="server" ID="lblDersYorumSayisi2"></asp:Label>)
    </strong>
</p>
<p>
    Toplam dosya sayısı : &nbsp;&nbsp;<strong><asp:Label runat="server" ID="lblDosyaSayisi1"></asp:Label>
    &nbsp;&nbsp;(<asp:Label runat="server" ID="lblDosyaSayisi2"></asp:Label>)
    </strong>
</p>
<p>
    Okunmamış mesaj sayısı : &nbsp;&nbsp;<strong><asp:Label runat="server" ID="lblOkunmamisMesajSayisi"></asp:Label></strong>
</p>
<p>
    <asp:Label runat="server" ID="lblDurum" CssClass="bilgi"></asp:Label>
</p>
</asp:Content>
