<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AraOkul.ascx.cs" Inherits="UserControls_AraOkul" %>

<table>
<tr>
    <td>
    Okul sec :
    </td>
    <td>
        <asp:DropDownList ID="okulIsmi" runat="server" OnSelectedIndexChanged="OkulSecildi" AutoPostBack="true"></asp:DropDownList>
    </td>
</tr>
</table>
