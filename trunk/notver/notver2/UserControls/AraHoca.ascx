<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AraHoca.ascx.cs" Inherits="UserControls_AraHoca" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<script type="text/javascript">
function SetFocusHoca(e)
{
    var keycode;
    if(window.event)
    {
        keycode = window.event.keyCode;
    }
    else if(e.which)
    {
        keycode = e.which;
    }
    else if(e.keyCode)
    {
        keycode = e.keyCode;
    }

    if(keycode == 13)
    {
        document.getElementById('<%= buttonHocaAra.ClientID %>').focus();
    }
}

function Temizle(obj)
{
    obj.value='';
}
</script>

<table>
<tr>
    <td>
        <asp:TextBox ID="hocaIsmi" runat="server" OnKeyDown="javascript:return SetFocusHoca(event);" 
        onclick="javascript:return Temizle(this);">Hoca ismini girin</asp:TextBox>
    </td>
    <td>
        <asp:Button ID="buttonHocaAra" runat="server" Text="Hoca Ara" OnClick="Ara" />
    </td>
</tr>
<tr>
    <td colspan="2" style="text-align:right;">
        <asp:HyperLink runat="server" ID="lnkTumHocalar" NavigateUrl="~/TumHocalar.aspx">Tumu...</asp:HyperLink>
    </td>
</tr>
</table>