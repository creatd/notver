<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DersDosya.aspx.cs" Inherits="DersDosya" MasterPageFile="~/Masters/Giris.master" 
MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/UserControls/DersDosyalar.ascx" TagName="DersDosya" TagPrefix="uc1" %>
<%@ Register TagPrefix="uc1" TagName="Ayrac" Src="~/UserControls/Ayrac.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <script src="Scripts/jquery.qtip-1.0.0-rc3.min.js" type="text/javascript"></script>
    <!-- Colorbox -->
    <script type="text/javascript">
    $(document).ready(function(){
        $("a.colorbox").colorbox({iframe:true,width:'590px', height:'750px', close:''});
    });
    
    function resize(w,h)   {
        $("a.colorbox").colorbox.resize({width:w,height:h});
    }
    </script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="content" ID="mainContent">
<uc1:Ayrac runat="server" ID="ayrac" />
<uc1:DersDosya runat="server" ID="ucDersDosya"></uc1:DersDosya>
</asp:Content>