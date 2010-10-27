<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HocaPuanlari.ascx.cs" Inherits="UserControls_HocaPuanlari" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<!--<script src="Scripts/StarRating.js" type="text/javascript"></script> -->

<link href="Scripts/StarRating.css" rel="stylesheet" type="text/css" />


<script type="text/javascript">
function $(v,o) { return((typeof(o)=='object'?o:document).getElementById(v)); }
function $S(o) { return((typeof(o)=='object'?o:$(o)).style); }
function setRating(rating,n)
{
    $S('puan'+n).visibility='visible';
    $S('puan'+n).width=rating+'px';
    $('puanYazi'+n).innerHTML=Math.round(rating/84*100)+'%';
}
</script>


<asp:Panel ID="panelPuanlar" runat="server">
<table border="0" cellpadding="10" width="450" style="border:none;" id="HocaPuanlari">
<tr>
    <td align="center" colspan="2">
        <h1>NotVerin.com karnesi</h1>
    </td>
</tr>
<tr>
    <td colspan="2">
        <img src="App_Themes/Default/Images/diger/cizgi.png" ID="cizgi"/>
    </td>
</tr>
<tr>
    <td class="hocaPuanSolSutun">
        <asp:Label ID="Aciklama1" runat="server"></asp:Label>
    </td>
    <td class="hocaPuanSagSutun">
        <table>
            <tr>
                <td>
                    <ul class="star" id="star1">
                        <li id="puan1" style="BACKGROUND: url('App_Themes/Default/Images/stars.gif') left 25px; FONT-SIZE: 1px; visibility:hidden;" >
                        </li>
                    </ul>
                </td>
                <td>
                    <div id="puanYazi1" class="user">-</div>
                </td>
            </tr>
        </table>        
    </td>
</tr>
<tr>    
    <td class="hocaPuanSolSutun">
        <asp:Label ID="Aciklama2" runat="server"></asp:Label>
    </td>
    <td class="hocaPuanSagSutun">
        <table>
            <tr>
                <td>
                    <ul class="star" id="star2">
                        <li id="puan2" style="BACKGROUND: url('App_Themes/Default/Images/stars.gif') left 25px; FONT-SIZE: 1px; visibility:hidden;" >
                        </li>
                    </ul>
                </td>
                <td>
                    <div id="puanYazi2" class="user">-</div>
                </td>
            </tr>
        </table>        
    </td>
</tr>
<tr>
    <td class="hocaPuanSolSutun">
        <asp:Label ID="Aciklama3" runat="server"></asp:Label>
    </td>
    <td class="hocaPuanSagSutun">
        <table>
            <tr>
                <td >
                    <ul class="star" id="star3">
                        <li id="puan3" style="BACKGROUND: url('App_Themes/Default/Images/stars.gif') left 25px; FONT-SIZE: 1px; visibility:hidden;" >
                        </li>
                    </ul>
                </td>
                <td>
                    <div id="puanYazi3" class="user">-</div>
                </td>
            </tr>
        </table>        
    </td>
</tr>
<tr>    
    <td class="hocaPuanSolSutun">
        <asp:Label ID="Aciklama4" runat="server"></asp:Label>
    </td>
    <td class="hocaPuanSagSutun">
        <table>
            <tr>
                <td  >
                    <ul class="star" id="star4">
                        <li id="puan4" style="BACKGROUND: url('App_Themes/Default/Images/stars.gif') left 25px; FONT-SIZE: 1px; visibility:hidden;" >
                        </li>
                    </ul>
                </td>
                <td>
                    <div id="puanYazi4" class="user">-</div>
                </td>
            </tr>
        </table>        
    </td>
</tr>
<tr>

    <td class="hocaPuanSolSutun">
        <asp:Label ID="Aciklama5" runat="server"></asp:Label>
    </td>
    <td class="hocaPuanSagSutun">
        <table>
            <tr>
                <td >
                    <ul class="star" id="star5">
                        <li id="puan5" style="BACKGROUND: url('App_Themes/Default/Images/stars.gif') left 25px; FONT-SIZE: 1px; visibility:hidden;" >
                        </li>
                    </ul>
                </td>
                <td>
                    <div id="puanYazi5" class="user">-</div>
                </td>
            </tr>
        </table>        
    </td>
</tr>
</table>
</asp:Panel>

<asp:Panel ID="pnlNotYok" runat="server" Visible="false">
    <br />
&nbsp;&nbsp;<asp:Label ID="lblNotYok" runat="server">Daha önce not verilmemiş. İlk not veren </asp:Label><asp:HyperLink ID="linkNotVer" Text="siz olun!" runat="server" NavigateUrl="~/HocaNotVer.aspx"></asp:HyperLink> 
</asp:Panel>

<asp:Literal ID="script" runat="server"></asp:Literal>
