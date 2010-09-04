<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AraDers.ascx.cs" Inherits="UserControls_AraDers" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<script type="text/javascript">
function SetFocusDers(e)
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
        document.getElementById('<%= buttonDersAra.ClientID %>').focus();
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
        <asp:TextBox ID="dersIsmi" runat="server" OnKeyDown="javascript:return SetFocusDers(event);" 
        onclick="javascript:return Temizle(this);">Ders ismini ya da kodunu girin</asp:TextBox>
    </td>
    <td>
        <asp:Button ID="buttonDersAra" runat="server" Text="Ders Ara" OnClick="Ara" />
    </td>
</tr>
<tr>
    <td colspan="2" style="text-align:right;">
        <asp:HyperLink runat="server" ID="lnkTumDersler" NavigateUrl="~/TumDersler.aspx">Tumu...</asp:HyperLink>
    </td>
</tr>
</table>
