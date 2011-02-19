<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TumDersYorumlar.aspx.cs" 
Inherits="Admin_TumDersYorumlar"  ValidateRequest="false"
MasterPageFile="~/Masters/Admin.master" MaintainScrollPositionOnPostback="true" %>

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
<h1>Ders Yorumlari</h1>
<p>Yorumun ait oldugu kullanici degistirilmesi kullanici onay puanlarini etkiledigi icin buradan yapilamaz</p>
<p>Her derse her kullanici sadece bir kere yorum yapabilir</p>
<br />
    <asp:Label runat="server" ID="lblDurum1"></asp:Label>
    <br />
    Yorum durumu:<asp:DropDownList runat="server" ID="drpYorumDurumu" OnSelectedIndexChanged="DurumSecildi" AutoPostBack=true></asp:DropDownList>
    Okul:<asp:DropDownList runat="server" ID="drpOkullar" OnSelectedIndexChanged="OkulSecildi" AutoPostBack=true></asp:DropDownList>
    Ders:<asp:DropDownList runat="server" ID="drpDersler" OnSelectedIndexChanged="DersSecildi" AutoPostBack="true"></asp:DropDownList>
    <br />
    <br />
    
    <asp:DataGrid ID="gridDersYorumlar" runat="server" AllowPaging="true" AllowSorting="true"
        OnPageIndexChanged="grid_PageIndexChanged" OnItemDataBound="grid_ItemDataBound" PageSize="10"
            AutoGenerateColumns="false" BorderWidth="0" GridLines="Both" OnUpdateCommand="Update"
            OnEditCommand="Edit" OnCancelCommand="Cancel" OnItemCommand="ItemCommand">
        <Columns>
            <asp:BoundColumn DataField="DERSYORUM_ID" HeaderText="DersYorum ID" ReadOnly="true"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="Yorum durumu">
                <ItemTemplate>
                    <%# (Enums.YorumDurumu)Convert.ToInt32(DataBinder.Eval(Container.DataItem, "YORUM_DURUMU").ToString()) %>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="YORUM_DURUMU"  ItemStyle-CssClass="sakla" 
                HeaderStyle-CssClass="sakla" FooterStyle-CssClass="sakla"></asp:BoundColumn>
            <asp:BoundColumn DataField="SILINME_NEDENI" HeaderText="Silinme nedeni (256)"></asp:BoundColumn>
            <asp:BoundColumn DataField="DERS_ID" HeaderText="Ders ID"></asp:BoundColumn>
            <asp:BoundColumn DataField="DERS_KOD" HeaderText="Ders Kodu" ReadOnly="true"></asp:BoundColumn>
            <asp:BoundColumn DataField="OKUL_ISIM" HeaderText="Okul Isim" ReadOnly="true"></asp:BoundColumn>
            <asp:BoundColumn DataField="KULLANICI_ID" HeaderText="Kullanici ID" ReadOnly="true"></asp:BoundColumn>
            <asp:BoundColumn DataField="YORUM" HeaderText="Yorum (2000)"></asp:BoundColumn>
            <asp:BoundColumn DataField="TARIH" HeaderText="Gonderilme tarihi"></asp:BoundColumn>
            <asp:BoundColumn DataField="ALKIS_PUANI" HeaderText="Alkis puani"></asp:BoundColumn>
            <asp:BoundColumn DataField="ZORLUK_PUANI" HeaderText="Zorluk puani [1,5]"></asp:BoundColumn>
            
            <asp:ButtonColumn ButtonType="LinkButton" Text="Onayla" CommandName="Onayla"></asp:ButtonColumn>
            <asp:ButtonColumn ButtonType="LinkButton" Text="Kaldir" CommandName="Kaldir"></asp:ButtonColumn>
            <asp:EditCommandColumn ButtonType="LinkButton" EditText="..." CancelText="Iptal" 
            UpdateText="Guncelle"></asp:EditCommandColumn>
            <asp:ButtonColumn ButtonType="LinkButton" Text="X" CommandName="Sil1"></asp:ButtonColumn>
            <asp:ButtonColumn ButtonType="LinkButton" Text="Eminim" CommandName="Sil2" Visible="false"></asp:ButtonColumn>
        </Columns>
        <PagerStyle Visible="true" NextPageText="Ileri &gt;" PrevPageText="&lt; Geri" HorizontalAlign="Right" Mode="NumericPages" />
    </asp:DataGrid>
    Yorum silinme nedeni (256): <asp:TextBox runat="server" ID="txtSilinmeNedeni" TextMode="MultiLine" MaxLength="256"></asp:TextBox>
    <a onclick="SilinmeNedeniTemizle();">Temizle</a>
    <br />
    <asp:Label runat="server" ID="lblDurum2"></asp:Label>
</asp:Content>
