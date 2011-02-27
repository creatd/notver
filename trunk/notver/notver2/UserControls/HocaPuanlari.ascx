<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HocaPuanlari.ascx.cs" Inherits="UserControls_HocaPuanlari" %>




<asp:Panel ID="panelPuanlar" runat="server">
<table border="0" cellpadding="10" width="450" style="border:none;" id="HocaPuanlari">
<tr>
    <td class="hocaPuanSolSutun">
        <asp:Label ID="Aciklama1" runat="server"></asp:Label>
    </td>
    <td class="hocaPuanSagSutun">
        <table>
            <tr>
                <td>
                    <ul class="star" id="star1">
                        <li id="puan1" style="BACKGROUND: url('App_Themes/Default/Images/yildizlar.png') left 22px; FONT-SIZE: 1px; visibility:hidden;" >
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
                        <li id="puan2" style="BACKGROUND: url('App_Themes/Default/Images/yildizlar.png') left 22px; FONT-SIZE: 1px; visibility:hidden;" >
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
                        <li id="puan3" style="BACKGROUND: url('App_Themes/Default/Images/yildizlar.png') left 22px; FONT-SIZE: 1px; visibility:hidden;" >
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
                        <li id="puan4" style="BACKGROUND: url('App_Themes/Default/Images/yildizlar.png') left 22px; FONT-SIZE: 1px; visibility:hidden;" >
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
                        <li id="puan5" style="BACKGROUND: url('App_Themes/Default/Images/yildizlar.png') left 22px; FONT-SIZE: 1px; visibility:hidden;" >
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
    <p style="font-weight:bold; padding:10px; color:#626262; font-size:13px; font-style:italic;">
        Daha once puan verilmemis. Ilk puan veren siz olun
    </p>
</asp:Panel>

<asp:Literal ID="script" runat="server"></asp:Literal>
