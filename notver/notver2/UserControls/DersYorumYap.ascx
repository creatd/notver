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

<asp:ToolkitScriptManager runat="server" ScriptMode="Release"></asp:ToolkitScriptManager>

<asp:Panel ID="pnlPuanYorum" runat="server" Width="510" Height="395" CssClass="DersYorumYap">
    <p style="color:#626262; font-size:12px;">Yapmis oldugunuz tum yorumlari goruntulemek veya degistirmek icin 
    <asp:HyperLink ID="lnkKullaniciYorumlar" runat="server" CssClass="lnkYorumlarim">tiklayin</asp:HyperLink></p>
    <br />
    <p>Yorumunuz</p>
    <p style="margin-bottom:20px;">
        <asp:TextBox runat="server" CssClass="multitextbox" TextMode="MultiLine" MaxLength="2000" 
        ID="textYorum" Width="500" Height="220"></asp:TextBox>
    </p>

    <table style="border: none; font-weight:bold; font-size:13px;" width="500">
        <tr>
            <td style="width:220px; padding:10px 10px 10px 0px">
                Ders zor muydu:
                <br /><span class="bilgi">(1:kolay - 5:cok zor)</span> 
            </td>
            <td>
                <asp:Rating ID="puanDersZorluk" runat="server" EmptyStarCssClass="bosYildiz" FilledStarCssClass="doluYildiz"
                    StarCssClass="doluYildiz" WaitingStarCssClass="bekleYildiz"/>
            </td>
        </tr>
        <tr>
            <td style="width:220px; padding:10px 10px 10px 0px">
                Hangi hocadan aldiniz (opsiyonel):
            </td>
            <td>
                <asp:DropDownList runat="server" ID="drpDersHocalar" onchange="javascript:HocaSecildi(this);"></asp:DropDownList>
            </td>
        </tr>
        <tr id="trBilinmeyenHoca">
            <td style="width:220px; padding:10px 10px 10px 0px">
                Hocanin ismini girin:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtBilinmeyenHocaIsmi" CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
        <tr id="trPuanDersHoca">
            <td style="width:220px; padding:10px 10px 10px 0px">
                Bu hocadan almak:
            </td>
            <td class="DersYorumYapSutunSag">
                <asp:Rating ID="puanDersHoca" runat="server" EmptyStarCssClass="bosYildiz" FilledStarCssClass="doluYildiz"
                    StarCssClass="doluYildiz" WaitingStarCssClass="bekleYildiz" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style=" padding:10px 10px 10px 0px">
                <asp:ImageButton runat="server" ID="dugmeYorumGonder" ImageUrl="~/App_Themes/Default/Images/gonder.png" 
            OnClick="YorumKaydet"/>
            </td>
        </tr>
        <tr><td colspan="2" class="durum" style=" padding:10px 10px 10px 0px"><asp:Literal runat="server" ID="ltrDurum"></asp:Literal></td></tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlUyeOl" runat="server" CssClass="bilgi">
    <br/><br/>
    Yorum yapabilmek icin giris yapmaniz gereklidir.
    <br/><br/>
    Uyeliginiz yoksa ana sayfada sag ustten hemen ucretsiz uye olabilirsiniz.
</asp:Panel>
<asp:Panel ID="pnlHata" runat="server" CssClass="durum">
Bir hata olustu :(
</asp:Panel>
<asp:Literal runat="server" ID="ltrScript"></asp:Literal>