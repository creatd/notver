<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchResults.aspx.cs" Inherits="SearchResults" MasterPageFile="~/Masters/Giris.master" %>

<asp:Content ContentPlaceHolderID="content" runat="server">

<asp:Panel ID="PanelHocalar" runat="server" Visible="false">
<h1>Hoca arama sonuclari</h1>
<asp:DataGrid ID="dataGridHoca" runat="server" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false">
<ItemStyle CssClass="AramaSonuclariHocalarRow" />
<AlternatingItemStyle CssClass="AramaSonuclariHocalarRow2" />
<HeaderStyle CssClass="AramaSonuclariHocalarHeader" />
<Columns>

    <asp:TemplateColumn>
        <HeaderTemplate>Hoca Ismi</HeaderTemplate>
        <ItemTemplate><%# HocaLinkiniDondur(DataBinder.Eval(Container.DataItem, "ISIM").ToString(), DataBinder.Eval(Container.DataItem, "HOCA_ID").ToString())%></ItemTemplate>
    </asp:TemplateColumn>
    
    <asp:TemplateColumn>
        <HeaderTemplate>Okul</HeaderTemplate>
        <ItemTemplate><%# HocaOkullariniDondur( DataBinder.Eval(Container.DataItem, "HOCA_ID").ToString() )%></ItemTemplate>
    </asp:TemplateColumn>
    
</Columns>
<PagerStyle Visible="false" NextPageText="Ileri &gt;" PrevPageText="&lt; Geri" HorizontalAlign="Right" />
</asp:DataGrid>
</asp:Panel>

<asp:Panel ID="PanelOkullar" runat="server" Visible="false">
<h1>Okul arama sonuclari</h1>
</asp:Panel>

<asp:Panel ID="PanelDersler" runat="server" Visible="false">
<h1>Ders arama sonuclari</h1>
</asp:Panel>

</asp:Content>
