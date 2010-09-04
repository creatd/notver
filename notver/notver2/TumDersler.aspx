<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TumDersler.aspx.cs" Inherits="TumDersler" MasterPageFile="~/Masters/Ders.master" %>

<%@ Register TagPrefix="uc1" TagName="OkulTumDersler" Src="~/UserControls/OkulTumDersler.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Ayrac" Src="~/UserControls/Ayrac.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="content" ID="content1">
<uc1:Ayrac runat="server" ID="ayrac" />
<a name='tepe'></a>
<asp:Literal runat="server" ID="ltrHarfDizini"></asp:Literal>
<br /><br />
<asp:Repeater runat="server" ID="repeaterOkullar">
    <HeaderTemplate>
        <table>
    </HeaderTemplate>
    <ItemTemplate>
        <tr><td>
        <%# OkulLinkBaslikDondur(DataBinder.Eval(Container.DataItem, "ISIM"), DataBinder.Eval(Container.DataItem, "OKUL_ID"))%>
        </td><td>
        <a href='#tepe'>Basa don</a>
        </td></tr>
        <tr><td colspan="2">
        <br />
        <uc1:OkulTumDersler runat="server" _OkulID='<%# DataBinder.Eval(Container.DataItem, "OKUL_ID")%>'></uc1:OkulTumDersler>
        <br /></td></tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
</asp:Content>
