<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TumMesajlar.aspx.cs" Inherits="Admin_TumMesajlar" 
MasterPageFile="~/Masters/Admin.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <!-- Colorbox -->
    <script type="text/javascript">
    $(document).ready(function(){
        $("a.colorbox").colorbox({iframe:true,width:'590px', height:'550px', close:''});
    });
    </script>
    
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="content">
<h1>Mesajlar</h1>
<br />
<asp:CheckBox Text="Tumunu goster" runat="server" ID="chkTumu" AutoPostBack="true" OnCheckedChanged="chk_changed"/>
<asp:Label runat="server" ID="lblAlici"></asp:Label>
<br />
    <asp:Label runat="server" ID="lblDurum1" CssClass="bilgi"></asp:Label>
    <br /><br />
    <asp:DataGrid ID="gridMesajlar" runat="server" AllowPaging="true" AllowSorting="true"
        OnPageIndexChanged="grid_PageIndexChanged" PageSize="10"
            AutoGenerateColumns="false" BorderWidth="0" GridLines="Both" OnItemCommand="ItemCommand">
        <Columns>
            <asp:BoundColumn DataField="MESAJ_ID" HeaderText="ID"></asp:BoundColumn>
            <asp:BoundColumn DataField="GONDEREN_ID" HeaderText="Gonderen ID"></asp:BoundColumn>
            <asp:BoundColumn DataField="BASLIK" HeaderText="Baslik"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="Mesaj ozet">
                <ItemTemplate>
                    <%# IcerikOzetDondur(DataBinder.Eval(Container.DataItem, "ICERIK").ToString()) %>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="GONDERME_ZAMANI" HeaderText="Gonderme zamani"></asp:BoundColumn>
            <asp:TemplateColumn>
                <ItemTemplate>
                    <a class="colorbox" href="./MesajOku.aspx?MesajID=<%# DataBinder.Eval(Container.DataItem, "MESAJ_ID").ToString() %>">Oku</a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:ButtonColumn ButtonType="LinkButton" Text="Okundu" CommandName="OkunduIsaretle"></asp:ButtonColumn>
            <asp:ButtonColumn ButtonType="LinkButton" Text="X" CommandName="Sil1"></asp:ButtonColumn>
            <asp:ButtonColumn ButtonType="LinkButton" Text="Eminim" CommandName="Sil2" Visible="false"></asp:ButtonColumn>
        </Columns>
        <PagerStyle Visible="true" NextPageText="Ileri &gt;" PrevPageText="&lt; Geri" HorizontalAlign="Right" Mode="NumericPages" />
    </asp:DataGrid>
<asp:Label runat="server" ID="lblDurum2" CssClass="bilgi"></asp:Label>
</asp:Content>


