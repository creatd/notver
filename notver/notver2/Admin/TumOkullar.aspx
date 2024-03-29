﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TumOkullar.aspx.cs" Inherits="Admin_TumOkullar" 
MasterPageFile="~/Masters/Admin.master" MaintainScrollPositionOnPostback="true"  ValidateRequest="false"%>

<asp:Content runat="server" ContentPlaceHolderID="content">
<h1>Okullar</h1>
    <asp:Label runat="server" ID="lblDurum1" CssClass="bilgi"></asp:Label>
    <br /><br />
    <asp:DataGrid ID="gridOkullar" runat="server" AllowPaging="true" AllowSorting="true"
            OnPageIndexChanged="grid_PageIndexChanged" PageSize="10"
            AutoGenerateColumns="false" BorderWidth="0" GridLines="Both" OnUpdateCommand="Update"
            OnEditCommand="Edit" OnCancelCommand="Cancel" OnItemCommand="ItemCommand">
        <Columns>
            <asp:BoundColumn DataField="OKUL_ID" HeaderText="Okul ID" ReadOnly="true"></asp:BoundColumn>
            <asp:BoundColumn DataField="IS_ACTIVE" HeaderText="Is Active"></asp:BoundColumn>
            <asp:BoundColumn DataField="ISIM" HeaderText="Isim (100)"></asp:BoundColumn>
            <asp:BoundColumn DataField="ADRES" HeaderText="Adres (50)"></asp:BoundColumn>
            <asp:BoundColumn DataField="KURULUS_TARIHI" HeaderText="Kurulus tarihi"></asp:BoundColumn>
            <asp:BoundColumn DataField="OGRENCI_SAYISI" HeaderText="Ogrenci sayisi"></asp:BoundColumn>
            <asp:BoundColumn DataField="AKADEMIK_SAYISI" HeaderText="Akademik sayisi"></asp:BoundColumn>
            <asp:BoundColumn DataField="WEB_ADRESI" HeaderText="Web adresi (256)"></asp:BoundColumn>
            <asp:EditCommandColumn ButtonType="LinkButton" EditText="..." CancelText="Iptal" 
            UpdateText="Guncelle"></asp:EditCommandColumn>
            <asp:ButtonColumn ButtonType="LinkButton" Text="X" CommandName="Sil1"></asp:ButtonColumn>
            <asp:ButtonColumn ButtonType="LinkButton" Text="Eminim" CommandName="Sil2" Visible="false"></asp:ButtonColumn>
        </Columns>
        <PagerStyle Visible="true" NextPageText="Ileri &gt;" PrevPageText="&lt; Geri" HorizontalAlign="Right" Mode="NumericPages" />
    </asp:DataGrid>
    <asp:Label runat="server" ID="lblDurum2" CssClass="bilgi"></asp:Label>
</asp:Content>
