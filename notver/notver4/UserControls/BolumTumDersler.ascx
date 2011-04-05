<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BolumTumDersler.ascx.cs" Inherits="UserControls_OkulTumDersler" %>

<asp:Repeater runat="server" ID="repeaterDersler">
    <HeaderTemplate>
        <table style="border-left:solid 1pt #c3c3c3;">
    </HeaderTemplate>
    <ItemTemplate>
        <tr><td style="padding-left:30px;">
    <a href='<%# DersURLDondur(DataBinder.Eval(Container.DataItem, "DERS_ID")) %>' >
    <%# DataBinder.Eval(Container.DataItem, "KOD")%> - <%#DataBinder.Eval(Container.DataItem, "DERS_ISIM")%></a>        
        </td></tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
<asp:Label runat="server" ID="lblDersYok">
    <span style="border-left:solid 1pt #c3c3c3; padding-left:30px; font-weight:normal; font-style:italic;">
        Henüz ders eklenmemiş
    </span>
</asp:Label>