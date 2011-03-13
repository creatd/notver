﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DersYorumGuncelle.ascx.cs" Inherits="UserControls_DersYorumGuncelle" %>
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
                    $(this).attr("title", "Çok kötü fikir");
                }
                if (this.value == 2) {
                    $(this).attr("title", "Kötü fikir");
                }
                if (this.value == 3) {
                    $(this).attr("title", "Orta karar");
                }
                if (this.value == 4) {
                    $(this).attr("title", "İyi fikir");
                }
                if (this.value == 5) {
                    $(this).attr("title", "Çok iyi fikir");
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
                    $(this).attr("title", "Çok kolay");
                }
                if (this.value == 2) {
                    $(this).attr("title", "Kolay");
                }
                if (this.value == 3) {
                    $(this).attr("title", "Normal");
                }
                if (this.value == 4) {
                    $(this).attr("title", "Zor");
                }
                if (this.value == 5) {
                    $(this).attr("title", "Çok zor");
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
    <table>
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
                Hangi hocadan aldınız (opsiyonel)
            </td>
            <td class="DersYorumYapSutunSag">
                <asp:DropDownList runat="server" ID="drpDersHocalar" onchange="javascript:HocaSecildi(this);"></asp:DropDownList>
            </td>
        </tr>
        <tr id="trBilinmeyenHoca">
            <td style="text-align:right;width:350px;">
                Hocanın ismini girin
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
                <asp:Button ID="dugmeYorumGuncelle" Text="Guncelle" runat="server" OnClick="YorumGuncelle"
                    CssClass="fltRight" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlUyeOl" runat="server">
    Puan vermek ve yorum yapabilmek için giriş yapmanız gereklidir.
</asp:Panel>
<asp:Literal runat="server" ID="ltrScript"></asp:Literal>