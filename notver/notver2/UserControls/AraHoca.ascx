<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AraHoca.ascx.cs" Inherits="UserControls_AraHoca" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<script type="text/javascript">
function SetFocus(e)
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
        document.getElementById('<%= buttonAra.ClientID %>').focus();
    }
}
</script>

<table>
<tr>
    <td>
        <asp:TextBox ID="hocaIsmi" runat="server" OnKeyDown="javascript:return SetFocus(event);"></asp:TextBox>
    </td>
    <td>
        <asp:Button ID="buttonAra" runat="server" Text="Hoca Ara" OnClick="buttonAra_Click" />
    </td>
</tr>
</table>