<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" 
MasterPageFile="~/Masters/Giris.master" MaintainScrollPositionOnPostback="true" %>

<asp:Content ContentPlaceHolderID="content" runat="server">
<p>Sayfamiza hos geldiniz!</p>
<p>Bu sayfada bircok yararli bilgi bulabilirsiniz</p>
<asp:Label runat="server" ID="lblTimeout">
Yarim saattir yoksun diye gittin sandık. Tekrar giriş yapmalısın.
</asp:Label>
</asp:Content>
