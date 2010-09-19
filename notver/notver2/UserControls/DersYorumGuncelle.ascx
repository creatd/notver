﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DersYorumGuncelle.ascx.cs" Inherits="UserControls_DersYorumGuncelle" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<script type="text/javascript">
function HocaSecildi(obj)   {
    /* var ddlReport = document.getElementById("<%=drpDersHocalar.ClientID%>");
    var Text = ddlReport.options[ddlReport.selectedIndex].text;  */
    var dropDownList = obj;
    var hocaIsim = dropDownList.options[dropDownList.selectedIndex].text; 
    var hocaID = dropDownList.options[dropDownList.selectedIndex].value;
    if(hocaIsim.length > 1)    {
        document.getElementById('trPuanDersHoca').style.visibility='visible';
    }   else    {
        document.getElementById('trPuanDersHoca').style.visibility='hidden';
    }
    if(hocaID == -2)    {   //Diger secildi
        document.getElementById('trBilinmeyenHoca').style.visibility='visible';
    }   else    {
        document.getElementById('trBilinmeyenHoca').style.visibility='hidden';
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
    <br />
    <br />
    <table style="border: solid 1pt;" border="1" width="600">
        <tr>
            <td class="DersYorumYapSutunSol">
                Yorumunuz
            </td>
            <td class="DersYorumYapSutunSag">
                <asp:TextBox ID="textYorum" runat="server" MaxLength="2000" TextMode="MultiLine" CssClass="DersYorumYapTextbox" />
            </td>
        </tr>
        <tr>
            <td class="DersYorumYapSutunSol">
                Ders zor muydu?
            </td>
            <td class="DersYorumYapSutunSag">
                <asp:Rating ID="puanDersZorluk" runat="server" EmptyStarCssClass="bosYildiz" FilledStarCssClass="doluYildiz"
                    StarCssClass="doluYildiz" WaitingStarCssClass="bekleYildiz"/>
            </td>
        </tr>
        <tr>
            <td class="DersYorumYapSutunSol">
                Hangi hocadan aldiniz (opsiyonel)
            </td>
            <td class="DersYorumYapSutunSag">
                <asp:DropDownList runat="server" ID="drpDersHocalar" onchange="javascript:HocaSecildi(this);"></asp:DropDownList>
            </td>
        </tr>
        <tr id="trBilinmeyenHoca">
            <td class="DersYorumYapSutunSol">
                Hocanin ismini girin
            </td>
            <td class="DersYorumYapSutunSag">
                <asp:TextBox runat="server" ID="txtBilinmeyenHocaIsmi"></asp:TextBox>
            </td>
        </tr>
        <tr id="trPuanDersHoca">
            <td class="DersYorumYapSutunSol">
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
                <asp:Button ID="dugmeYorumGuncelle" Text="Guncelle" runat="server" OnClick="YorumGuncelle"
                    CssClass="fltRight" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlUyeOl" runat="server">
    Puan vermek ve/veya yorum yapabilmek icin (sag ust koseden) <a href="#login">giris yapmaniz</a> gereklidir.
    Uyeliginiz yoksa
    <asp:HyperLink ID="HyperLink1" runat="server" Text="uye olmak icin tiklayin!" NavigateUrl="~/Register.aspx"></asp:HyperLink>
</asp:Panel>
<asp:Literal runat="server" ID="ltrScript"></asp:Literal>