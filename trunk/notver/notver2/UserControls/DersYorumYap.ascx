<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DersYorumYap.ascx.cs" Inherits="UserControls_DersYorumYap" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<script type="text/javascript">
function HocaSecildi(obj)   {
    var dropDownList = obj;
    var hocaIsim = dropDownList.options[dropDownList.selectedIndex].text; 
    var hocaID = dropDownList.options[dropDownList.selectedIndex].value;
    if(hocaIsim.length > 1)    {
        document.getElementById('trPuanDersHoca').style.display='table-row';
    }   else    {
        document.getElementById('trPuanDersHoca').style.visibility='none';
    }
    if(hocaID == -2)    {   //Diger secildi
        document.getElementById('trBilinmeyenHoca').style.display='table-row';
    }   else    {
        document.getElementById('trBilinmeyenHoca').style.display='none';
    }
}
</script>
<script type="text/javascript">
function pageLoad() {
/* puanDersHoca tooltip'leri */
        if ($find("<%=puanDersHoca.ClientID %>" + "_RatingExtender").get_ReadOnly()) {
            var rate = $("#<%=puanDersHoca.ClientID %> > a").attr("title");
            if (rate != 0) {
                $("#<%=puanDersHoca.ClientID %> > a").attr("title", "Rated " + rate + " star(s)");
            }           
        }
        else {
            $("#<%=puanDersHoca.ClientID %>").find("span").each(function() {
                if (this.value == 1) {
                    $(this).attr("title", "Click to rate as 1 star");
                }
                if (this.value == 2) {
                    $(this).attr("title", "Click to rate as 2 stars");
                }
                if (this.value == 3) {
                    $(this).attr("title", "Click to rate as 3 stars");
                }
                if (this.value == 4) {
                    $(this).attr("title", "Click to rate as 4 stars");
                }
                if (this.value == 5) {
                    $(this).attr("title", "Click to rate as 5 stars");
                }
            });
        }    
/* puanDersZorluk tooltip'leri */    
        if ($find("<%=puanDersZorluk.ClientID %>" + "_RatingExtender").get_ReadOnly()) {
            var rate = $("#<%=puanDersZorluk.ClientID %> > a").attr("title");
            if (rate != 0) {
                $("#<%=puanDersZorluk.ClientID %> > a").attr("title", "Rated " + rate + " star(s)");
            }           
        }
        else {
            $("#<%=puanDersZorluk.ClientID %>").find("span").each(function() {
                if (this.value == 1) {
                    $(this).attr("title", "Click to rate as 1 star");
                }
                if (this.value == 2) {
                    $(this).attr("title", "Click to rate as 2 stars");
                }
                if (this.value == 3) {
                    $(this).attr("title", "Click to rate as 3 stars");
                }
                if (this.value == 4) {
                    $(this).attr("title", "Click to rate as 4 stars");
                }
                if (this.value == 5) {
                    $(this).attr("title", "Click to rate as 5 stars");
                }
            });
        }    
    }
</script>

<!-- Hoca puani goruntule/sakla -->
<script type="text/javascript">
$(document).ready(function() {
    HocaSecildi(document.getElementById('<%=drpDersHocalar.ClientID%>'));
});
</script>



<asp:Panel ID="pnlPuanYorum" runat="server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" />
    <asp:HyperLink Text="&nbsp;&nbsp;Yapmis oldugunuz yorumlari goruntulemek veya degistirmek icin tiklayin"
                runat="server" ID="lnkKullaniciYorumlar" ></asp:HyperLink>
    <br /><br />
    <table style="border: none;" width="640">
        <tr>
            <td style="text-align:right;width:350px;">
                Yorumunuz
            </td>
            <td class="DersYorumYapSutunSag">
                <asp:TextBox ID="textYorum" runat="server" MaxLength="2000" TextMode="MultiLine" CssClass="DersYorumYapTextbox" />
            </td>
        </tr>
    </table>
    <table style="border: none;" width="640">
        <tr>
            <td style="text-align:right;width:350px;">
                Ders zor muydu?
            </td>
            <td class="DersYorumYapSutunSag">
                <asp:Rating ID="puanDersZorluk" runat="server" EmptyStarCssClass="bosYildiz" FilledStarCssClass="doluYildiz"
                    StarCssClass="doluYildiz" WaitingStarCssClass="bekleYildiz"/>
            </td>
        </tr>
        <tr>
            <td style="text-align:right;width:350px;">
                Hangi hocadan aldiniz (opsiyonel)
            </td>
            <td class="DersYorumYapSutunSag">
                <asp:DropDownList runat="server" ID="drpDersHocalar" onchange="javascript:HocaSecildi(this);"></asp:DropDownList>
            </td>
        </tr>
        <tr id="trBilinmeyenHoca">
            <td style="text-align:right;width:350px;">
                Hocanin ismini girin
            </td>
            <td class="DersYorumYapSutunSag">
                <asp:TextBox runat="server" ID="txtBilinmeyenHocaIsmi"></asp:TextBox>
            </td>
        </tr>
        <tr id="trPuanDersHoca">
            <td style="text-align:right;width:350px;">
                Bu hocadan almak
            </td>
            <td class="DersYorumYapSutunSag">
                <asp:Rating ID="puanDersHoca" runat="server" EmptyStarCssClass="bosYildiz" FilledStarCssClass="doluYildiz"
                    StarCssClass="doluYildiz" WaitingStarCssClass="bekleYildiz" />
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2" style="color: Red;">
                <asp:Literal runat="server" ID="ltrDurum"></asp:Literal>
                <asp:Button ID="dugmeYorumGonder" Text="Gunah benden gitti" runat="server" OnClick="YorumKaydet"
                    CssClass="fltRight" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlUyeOl" runat="server">
    Puan vermek ve yorum yapabilmek icin giris yapmaniz gereklidir.
</asp:Panel>
<asp:Literal runat="server" ID="ltrScript"></asp:Literal>