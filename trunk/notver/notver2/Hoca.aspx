<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hoca.aspx.cs" Inherits="Hoca"
    MasterPageFile="~/Masters/Giris.master" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/UserControls/HocaPuanlari.ascx" TagName="HocaPuanlari" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/HocaYorumlari.ascx" TagName="HocaYorumlari" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/HocaAyniOkul.ascx" TagName="HocaAyniOkul" TagPrefix="uc1" %>
<%@ Register TagPrefix="uc1" TagName="Ayrac" Src="~/UserControls/Ayrac.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Scripts/StarRating.css" rel="stylesheet" type="text/css" />
    
    <!-- Colorbox -->
    <script type="text/javascript">
    $(document).ready(function(){
        $("a.colorbox").colorbox({iframe:true,width:'590px', height:'480px', close:''});
    });
    </script>
    
    <!-- Hoca puanlari icin gerekli -->
    <script type="text/javascript">
        function $A(v,o) { return((typeof(o)=='object'?o:document).getElementById(v)); }
        function $S(o) { return((typeof(o)=='object'?o:$A(o)).style); }
        function setRating(rating,n)
        {
            $S('puan'+n).visibility='visible';
            $S('puan'+n).width=rating+'px';
            $A('puanYazi'+n).innerHTML='% ' + Math.round(rating/100*100);
        }
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="content" runat="server">
    <uc1:Ayrac runat="server" ID="ayrac" />
    <div id="hocaUst" style="display:block; width:100%; margin-top:30px;">
        <table>
            <tr style="background-color:#f6f6f6;">
                <td style="width:679px; border-right:solid 1pt #afafaf;">
                    <p style="color:#191919; font-weight:bold; padding:30px; padding-bottom:40px;">
                        <asp:Label ID="hocaIsim" runat="server" CssClass="HocaIsim"></asp:Label>
                        <asp:Literal ID="hocaOkullar" runat="server"></asp:Literal>
                    </p>
                </td>
                <td style="width:280px;">
                   <p style="color:#191919; font-weight:bold; padding:30px; padding-bottom:40px;">
                        Cok Yorumlananlar
                    </p> 
                </td>
            </tr>
            <tr style="background-color:#ffffff;">
                <td style="width:679px; border-right:solid 1pt #afafaf; color:#191919;">
                    <uc1:HocaPuanlari runat="server" ID="HocaPuanlari1" />
                </td>
                <td style="width:280px;">
                    <uc1:HocaAyniOkul runat="server" ID="hocaAyniOkul" />
                </td>
            </tr>
        </table>
    </div>    
    
    <div id="hocaYorumlari" style="display:block; width:100%; margin-top:20px;">
        <p style="background-color:#f6f6f6; color:#191919; font-weight:bold; padding:30px; padding-bottom:40px;">
            Yorumlar
            <span style="color:#626262;"><asp:HyperLink runat="server" ID="lnkYorumum" CssClass="lnkYorumEkle colorbox">
            <asp:Literal runat="server" ID="ltrYorumYazi" /> 
            <img src="App_Themes/Default/Images/ekle.png" /></asp:HyperLink></span>
        </p>
        <uc1:HocaYorumlari runat="server" ID="HocaYorumlari1" />
    </div>    

</asp:Content>
