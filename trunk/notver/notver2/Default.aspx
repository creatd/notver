<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" 
MasterPageFile="~/Masters/Giris.master" %>

<asp:Content ContentPlaceHolderID="content" runat="server">
<p>Sayfamiza hos geldiniz!</p>
<p>Bu sayfada bircok yararli bilgi bulabilirsiniz</p>
<asp:Label runat="server" ID="lblTimeout">
Yarim saattir yoksunuz diye gittiniz sandik. Tekrar giris yapmaniz gereklidir.
</asp:Label>
</asp:Content>
