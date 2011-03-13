<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hata.aspx.cs" Inherits="Hata" MasterPageFile="~/Masters/Giris.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
<script type="text/javascript">
function AnaSayfayaGit(a)
{
    var a = document.getElementById('<%= lnkAnasayfa.ClientID %>');
    document.location = a;
}
</script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="content">
    <span style="padding-top:30px; display:block; text-align:center;" class="bilgi">
    - Bilinmeyen bir hata oluştu, ana sayfaya yönlendiriyorum sizi  -
    </span>
    <span style="display:none;"><asp:HyperLink runat="server" NavigateUrl="~/" ID="lnkAnasayfa"></asp:HyperLink></span>
    <script type="text/javascript">
        setTimeout('AnaSayfayaGit();',1500);
    </script>
</asp:Content>