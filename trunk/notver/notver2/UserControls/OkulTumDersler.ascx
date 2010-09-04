<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OkulTumDersler.ascx.cs" Inherits="UserControls_OkulTumDersler" %>

<asp:Repeater runat="server" ID="repeaterDersler">
    <HeaderTemplate>
        <table>
    </HeaderTemplate>
    <ItemTemplate>
        <tr><td>
    <a href='<%# DersURLDondur(DataBinder.Eval(Container.DataItem, "DERS_ID")) %>' >
    <%# DataBinder.Eval(Container.DataItem, "KOD")%> - <%#DataBinder.Eval(Container.DataItem, "DERS_ISIM")%></a>        
        </td></tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
<asp:Label runat="server" ID="lblDersYok">
    Henuz kayitli ders yok :(        
</asp:Label>