<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HocaAyniOkul.ascx.cs" Inherits="UserControls_HocaAyniOkul" %>

<asp:Panel runat="server" ID="pnlKontrol">
<ol>
    <asp:Repeater runat="server" ID="repeater">
        <ItemTemplate>
            <li>
                <%= HocaLinkiDondur() %>
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ol>
</asp:Panel>