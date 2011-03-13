<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ders.aspx.cs" Inherits="Ders" MasterPageFile="~/Masters/Giris.master" 
MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="uc1" TagName="DersYorumlari" Src="~/UserControls/DersYorumlari.ascx" %>
<%@ Register TagPrefix="uc1" TagName="DersYorumYap" Src="~/UserControls/DersYorumYap.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Ayrac" Src="~/UserControls/Ayrac.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <!-- Colorbox -->
    <script type="text/javascript">
    $(document).ready(function(){
        $("a.colorbox").colorbox({iframe:true,width:'590px', height:'620px', close:''});
        $("a.colorboxSikayet").colorbox({iframe:true,width:'590px', height:'480px', close:''});
    });
    </script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="content" ID="tumContent">
<uc1:Ayrac runat="server" ID="ayrac" />

<div id="dersBilgi" style="display:block; width:100%;">
    <p style="background-color:#f6f6f6; color:#191919; font-weight:bold; padding:30px; padding-bottom:40px;">
        <asp:Label runat="server" ID="lblDersIsim" CssClass="fltLeft"></asp:Label>
        <span style="color:#626262; padding-left:5px; float:left;">(<asp:Label runat="server" ID="lblDersOkulIsim"></asp:Label>)</span>
        <asp:HyperLink runat="server" ID="lnkDersDosyalar" CssClass="lnkIndir">Dosya arşivi &nbsp;&nbsp;
        <img src="App_Themes/Default/Images/indir.png" /></asp:HyperLink>
    </p>
    <p style="background-color:#FFFFFF; color:#000000; padding:25px; font-size:11px; font-weight:bold;">
        <asp:Label runat="server" ID="lblDersAciklama" CssClass="line150"></asp:Label>
    </p>
</div>

<div id="dersYorumlari" style="display:block; width:100%; margin-top:20px;">
    <p style="background-color:#f6f6f6; color:#191919; font-weight:bold; padding:30px; padding-bottom:40px;">
        Yorumlar
        <span style="color:#626262;"><asp:HyperLink runat="server" ID="lnkYorumum" CssClass="lnkYorumEkle colorbox">
        Yorum ekle&nbsp;&nbsp;
        <img src="App_Themes/Default/Images/ekle.png" /></asp:HyperLink></span>
    </p>
    <uc1:DersYorumlari runat="server" ID="dersYorumlari"></uc1:DersYorumlari>
</div>
    
</asp:Content>
