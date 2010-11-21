<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ders.aspx.cs" Inherits="Ders" MasterPageFile="~/Masters/Giris.master" 
MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="uc1" TagName="DersYorumlari" Src="~/UserControls/DersYorumlari.ascx" %>
<%@ Register TagPrefix="uc1" TagName="DersYorumYap" Src="~/UserControls/DersYorumYap.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Ayrac" Src="~/UserControls/Ayrac.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
<script type="text/javascript">
    function change_parent_url(url) {
        document.location=url;
    }		
</script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="content" ID="tumContent">
<uc1:Ayrac runat="server" ID="ayrac" />
<div id="divArkaplan" style="background:url('App_Themes/default/Images/defter/IMG_0004.jpg') no-repeat 0 0 White; margin-bottom:20px; padding-bottom:10px;">
    <table>
        <tr>
            <td style="padding-left:110px;padding-top:10px;"">
                <h1><asp:Label runat="server" ID="lblDersIsim"></asp:Label></h1>
                <h2>(<asp:Label runat="server" ID="lblDersOkulIsim"></asp:Label>)</h2>
            </td>
            <td style="text-align:center;padding-top:10px;"">
                <asp:HyperLink runat="server" ID="lnkDersDosyalar"><img src="App_Themes/Default/Images/diger/disket.gif" /> <br/>Dersle ilgili bircok dosya icin tiklayin </asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2"style="padding-left:110px; padding-top:10px;">
                <asp:Label runat="server" ID="lblDersAciklama"></asp:Label>
                <br />                
                <!-- Dersi veren hocalari link olarak buraya koy -->
            </td>
        </tr>
    </table>
</div>
<div>
    <table>
        <tr>
            <td colspan="2">
                <uc1:DersYorumlari runat="server" ID="dersYorumlari"></uc1:DersYorumlari>
            </td>
        </tr>
    </table>
</div>
<br />
<asp:Panel ID="pnlYorumum" runat="server">
    <table style="width:100%; text-align:right;">
        <tr>
            <td>
                <asp:HyperLink runat="server" ID="lnkYorumum" Text="Benim de diyeceklerim var! " CssClass="thickbox"><img src="App_Themes/Default/Images/diger/el_kaldir.png" /></asp:HyperLink>
            </td>
        </tr>
    </table>
</asp:Panel>     
</asp:Content>
