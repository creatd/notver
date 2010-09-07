<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OkulTumHocalar.ascx.cs" Inherits="UserControls_OkulTumHocalar" %>

<asp:Repeater runat="server" ID="repeaterHocalar">
    <HeaderTemplate>
        <table>
    </HeaderTemplate>
    <ItemTemplate>
        <tr><td>
    <a href='<%# HocaURLDondur(DataBinder.Eval(Container.DataItem, "HOCA_ID")) %>' >
    <%# DataBinder.Eval(Container.DataItem, "HOCA_ISIM")%></a>        
        </td></tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
<asp:Label runat="server" ID="lblHocaYok">
    Henuz kayitli hoca yok :(        
</asp:Label>