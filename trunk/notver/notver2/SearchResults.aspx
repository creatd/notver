<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchResults.aspx.cs" Inherits="SearchResults"
    MasterPageFile="~/Masters/Giris.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="headContent">
    <script type="text/javascript">
    // Create the tooltips only on document load
    $(document).ready(function() 
    {
       // Notice the use of the each() method to acquire access to each elements attributes
       $('#divDersler a[tooltip]').each(function()
       {
          $(this).qtip({
             content: $(this).attr('tooltip'), // Use the tooltip attribute of the element for the content
             style: 'light' // Give it a crea mstyle to make it stand out
          });
       });
    });
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="content" runat="server">
    <asp:Panel ID="pnlHocalar" runat="server" Visible="false">
        <h1>
            Hoca arama sonuclari</h1>
        <asp:DataGrid ID="dataGridHoca" runat="server" AllowPaging="true" AllowSorting="true"
            AutoGenerateColumns="false">
            <ItemStyle CssClass="AramaSonuclariHocalarRow" />
            <AlternatingItemStyle CssClass="AramaSonuclariHocalarRow2" />
            <HeaderStyle CssClass="AramaSonuclariHocalarHeader" />
            <Columns>
                <asp:TemplateColumn>
                    <HeaderTemplate>
                        Hoca Ismi</HeaderTemplate>
                    <ItemTemplate>
                        <%# HocaLinkiniDondur(DataBinder.Eval(Container.DataItem, "ISIM").ToString(), DataBinder.Eval(Container.DataItem, "HOCA_ID").ToString())%></ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <HeaderTemplate>
                        Okul</HeaderTemplate>
                    <ItemTemplate>
                        <%# HocaOkullariniDondur( DataBinder.Eval(Container.DataItem, "HOCA_ID").ToString() )%></ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle Visible="false" NextPageText="Ileri &gt;" PrevPageText="&lt; Geri" HorizontalAlign="Right" />
        </asp:DataGrid>
    </asp:Panel>
    <asp:Panel ID="pnlDersler" runat="server" Visible="false" CssClass="pnlDersler">
    <div id="divDersler">
        <h1>
            Ders arama sonuclari</h1>
            <br />
        <asp:DataGrid ID="dataGridDersler" runat="server" AllowPaging="true" AllowSorting="true"
            AutoGenerateColumns="false" CssClass="AramaSonuclari" BorderWidth="0" GridLines="None">
            <ItemStyle CssClass="AramaSonuclariDerslerRow" />
            <AlternatingItemStyle CssClass="AramaSonuclariDerslerRow2" />
            <HeaderStyle CssClass="AramaSonuclariDerslerHeader" />
            <Columns>
                <asp:TemplateColumn>
                    <HeaderTemplate>
                        Ders Kodu</HeaderTemplate>
                    <ItemTemplate>
                        <strong><%# DersLinkiniDondur(DataBinder.Eval(Container.DataItem, "KOD").ToString(), DataBinder.Eval(Container.DataItem, "DERS_ISIM").ToString(), DataBinder.Eval(Container.DataItem, "DERS_ID").ToString())%></strong></ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <HeaderTemplate></HeaderTemplate>
                    <ItemTemplate>
                        <%# DersDosyalarLinkiniDondur(DataBinder.Eval(Container.DataItem, "DERS_ID").ToString())%>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <HeaderTemplate>
                        Okul</HeaderTemplate>
                    <ItemTemplate>
                        <%# OkulLinkiniDondur( DataBinder.Eval(Container.DataItem, "OKUL_ISIM").ToString() , DataBinder.Eval(Container.DataItem, "OKUL_ID").ToString() )%></ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle Visible="false" NextPageText="Ileri &gt;" PrevPageText="&lt; Geri" HorizontalAlign="Right" />
        </asp:DataGrid> 
    </div>                   
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlSonucYok" Visible="false" CssClass="pnlSonucYok">
        <p>
            <asp:Label runat="server" ID="lblSonucYok"></asp:Label>
        </p>
    </asp:Panel>
</asp:Content>
