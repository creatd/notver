<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OkulTumHocalar.ascx.cs" Inherits="UserControls_OkulTumHocalar" %>

<asp:Repeater runat="server" ID="repeaterHocalar">
    <HeaderTemplate>
        <table style="border-left:solid 1pt #c3c3c3;">
    </HeaderTemplate>
    <ItemTemplate>
        <tr><td style="padding-left:30px;">
    <a href='<%# HocaURLDondur(DataBinder.Eval(Container.DataItem, "HOCA_ID")) %>' >
    <%# DataBinder.Eval(Container.DataItem, "HOCA_ISIM")%></a>        
        </td></tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
<asp:Label runat="server" ID="lblHocaYok">
    <span style="border-left:solid 1pt #c3c3c3; padding-left:30px; font-weight:normal; font-style:italic;">
        Henuz hoca yok
    </span>
</asp:Label>