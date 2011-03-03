<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Okul.aspx.cs" Inherits="Okul" 
MasterPageFile="~/Masters/Giris.master" MaintainScrollPositionOnPostback="true" %>

<%@ Register TagName="OkulYorumlari" TagPrefix="uc1" Src="~/UserControls/OkulYorumlari.ascx" %>
<%@ Register TagName="OkulYorumYap" TagPrefix="uc1" Src="~/UserControls/OkulYorumYap.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Ayrac" Src="~/UserControls/Ayrac.ascx" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    
    <!-- Colorbox -->
    <script type="text/javascript">
    $(document).ready(function(){
        $("a.colorbox").colorbox({iframe:true,width:'590px', height:'480px', close:''});
    });
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="content" runat="server">
<uc1:Ayrac runat="server" ID="ayrac" />

<div id="okulBilgi" style="display:block; width:100%; margin-top:30px;">
    <p style="background-color:#f6f6f6; color:#191919; font-weight:bold; padding:30px; padding-bottom:40px;">
        <asp:Label runat="server" ID="lblOkulIsim" CssClass="fltLeft"></asp:Label>
    </p>
    <table style="background-color:#FFFFFF; color:#000000; padding:25px; font-size:11px; font-weight:bold;
    width:100%; height:250px;"> 
        <tr>    
            <td style="width:300px; font-size:14px; line-height:200%; font-weight:normal; padding-left:20px;">
                <i>Sehir :</i>&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblOkulAdres"></asp:Label>
                <br />
                <i>Kurulus tarihi :</i>&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblOkulKurulusTarihi"></asp:Label>
                <br />
                <i>Ogrenci sayisi :</i>&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblOgrenciSayisi"></asp:Label>
                <br />
                <i>Akademik personel sayisi :</i>&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblAkademikPersonelSayisi"></asp:Label>
                <br />
                <i>Web adresi :</i>&nbsp;&nbsp;&nbsp;<asp:HyperLink runat="server" ID="hpOkulWeb" Target="_blank"></asp:HyperLink>
                <br />
            </td>
            <td style="text-align:right;"><asp:Image runat="server" ID="imgOkul" Height="250" /></td>
        </tr>
    </table>
</div>

<div id="okulYorumlari" style="display:block; width:100%; margin-top:20px;">
    <p style="background-color:#f6f6f6; color:#191919; font-weight:bold; padding:30px; padding-bottom:40px;">
        Yorumlar
        <span style="color:#626262;"><asp:HyperLink runat="server" ID="lnkYorumum" CssClass="lnkYorumEkle colorbox"> 
        <asp:Literal runat="server" ID="ltrYorumYazi" />
        <img src="App_Themes/Default/Images/ekle.png" /></asp:HyperLink></span>
    </p>
    <uc1:OkulYorumlari runat="server" ID="okulYorumlari1"></uc1:OkulYorumlari>
</div>
</asp:Content>