<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TumDosyalar.aspx.cs" Inherits="Admin_TumDosyalar" 
MasterPageFile="~/Masters/Admin.master" MaintainScrollPositionOnPostback="true" ValidateRequest="false" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        function SilinmeNedeniTemizle()
        {
            var o = document.getElementById('<%= txtSilinmeNedeni.ClientID %>');
            o.value = '';
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="content">
<h1>Dosyalar</h1>
    <asp:Label runat="server" ID="lblDurum1" CssClass="bilgi"></asp:Label>
    <br /><br />
    Dosya durumu:<asp:DropDownList runat="server" ID="drpDosyaDurum" OnSelectedIndexChanged="DurumSecildi" AutoPostBack=true></asp:DropDownList>
    Okul:<asp:DropDownList runat="server" ID="drpOkullar" OnSelectedIndexChanged="OkulSecildi" AutoPostBack="true"></asp:DropDownList>
    Ders:<asp:DropDownList runat="server" ID="drpDersler" OnSelectedIndexChanged="DersSecildi" AutoPostBack="true"></asp:DropDownList>
    <br />
    <br />
    <asp:DataGrid ID="gridDosyalar" runat="server" AllowPaging="true" AllowSorting="true"
        OnPageIndexChanged="grid_PageIndexChanged" PageSize="10" OnItemDataBound="grid_ItemDataBound"
            AutoGenerateColumns="false" BorderWidth="0" GridLines="Both" OnUpdateCommand="Update"
            OnEditCommand="Edit" OnCancelCommand="Cancel" OnItemCommand="ItemCommand">
        <Columns>
            <asp:BoundColumn DataField="DOSYA_ID" HeaderText="Dosya ID" ReadOnly="true"></asp:BoundColumn>
            <asp:BoundColumn DataField="DERS_ID" HeaderText="Ders ID"></asp:BoundColumn>
            <asp:BoundColumn DataField="DERS_KOD" HeaderText="Ders Kod" ReadOnly="true"></asp:BoundColumn>
            <asp:BoundColumn DataField="OKUL_ISIM" HeaderText="Okul" ReadOnly="true"></asp:BoundColumn>
            <asp:BoundColumn DataField="HOCA_ID" HeaderText="Hoca ID"></asp:BoundColumn>
            <asp:BoundColumn DataField="HOCA_ISIM" HeaderText="Hoca" ReadOnly="true"></asp:BoundColumn>
            <asp:BoundColumn DataField="DOSYA_KATEGORI_ID" HeaderText="Kategori ID [0,5]"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="Kategori">
                <ItemTemplate><%# (Enums.DosyaKategoriTipi)Convert.ToInt32(DataBinder.Eval(Container.DataItem, "DOSYA_KATEGORI_ID").ToString())%></ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="DOSYA_DURUMU" ItemStyle-CssClass="sakla" 
                HeaderStyle-CssClass="sakla" FooterStyle-CssClass="sakla"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="Durum">
                <ItemTemplate><%# (Enums.DosyaDurumu)Convert.ToInt32(DataBinder.Eval(Container.DataItem, "DOSYA_DURUMU").ToString()) %></ItemTemplate>
            </asp:TemplateColumn>            
            <asp:BoundColumn DataField="SILINME_NEDENI" HeaderText="Silinme nedeni (256)"></asp:BoundColumn>
            <asp:BoundColumn DataField="DOSYA_ISMI" HeaderText="Dosya ismi (256)"></asp:BoundColumn>
            <asp:BoundColumn DataField="DOSYA_ADRES" HeaderText="Dosya adres (256)"></asp:BoundColumn>
            <asp:BoundColumn DataField="ACIKLAMA" HeaderText="Aciklama (256)"></asp:BoundColumn>
            <asp:BoundColumn DataField="EKLENME_TARIHI" HeaderText="Eklenme tarihi"></asp:BoundColumn>
            <asp:BoundColumn DataField="EKLEYEN_KULLANICI_ID" HeaderText="Ekleyen kullanici"></asp:BoundColumn>
            <asp:BoundColumn DataField="INDIRILME_SAYISI" HeaderText="Indirilme sayisi"></asp:BoundColumn>
            <asp:BoundColumn DataField="BOYUT" HeaderText="Boyut" ReadOnly="true"></asp:BoundColumn>
            
            <asp:ButtonColumn ButtonType="LinkButton" Text="Onayla" CommandName="Onayla"></asp:ButtonColumn>
            <asp:ButtonColumn ButtonType="LinkButton" Text="Kaldir" CommandName="Kaldir"></asp:ButtonColumn>
            <asp:EditCommandColumn ButtonType="LinkButton" EditText="..." CancelText="Iptal" 
            UpdateText="Guncelle"></asp:EditCommandColumn>
            <asp:ButtonColumn ButtonType="LinkButton" Text="X" CommandName="Sil1"></asp:ButtonColumn>
            <asp:ButtonColumn ButtonType="LinkButton" Text="Eminim" CommandName="Sil2" Visible="false"></asp:ButtonColumn>
        </Columns>
        <PagerStyle Visible="true" NextPageText="Ileri &gt;" PrevPageText="&lt; Geri" HorizontalAlign="Right" Mode="NumericPages" />
    </asp:DataGrid>
    Yorum silinme nedeni (256): <asp:TextBox runat="server" ID="txtSilinmeNedeni" TextMode="MultiLine" MaxLength="256"
    Width="400"></asp:TextBox>
    <a onclick="SilinmeNedeniTemizle();">Temizle</a>
    <br />
    <asp:Label runat="server" ID="lblDurum2" CssClass="bilgi"></asp:Label>
</asp:Content>
