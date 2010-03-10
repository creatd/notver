<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AraDers.ascx.cs" Inherits="UserControls_AraDers" %>

<script type="text/javascript">
function SetFocus(e)
{
    var keycode;
    if(window.event)
    {
        keycode = window.event.keyCode;
    }
    else if(e)
    {
        keycode = e.which;
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
        <asp:TextBox ID="dersIsmi" runat="server" OnTextChanged="DersIsmiGirildi" AutoPostBack="true" OnKeyDown="javascript:return SetFocus(this);"></asp:TextBox>
    </td>
    <td>
        <asp:Button ID="buttonAra" runat="server" Text="Ders Ara" />
    </td>
</tr>
</table>
