<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TumUyeler.aspx.cs" Inherits="Admin_TumUyeler" 
MasterPageFile="~/Masters/Admin.master" MaintainScrollPositionOnPostback="true" ValidateRequest="false"%>

<asp:Content runat="server" ContentPlaceHolderID="content">
<h1>Uyeler</h1>
<br />
<p>Kullanici adi ve epostanin essiz oldugundan emin ol!</p>
<br />
Okul :&nbsp;&nbsp;<asp:DropDownList runat="server" ID="drpOkullar" OnSelectedIndexChanged="OkulSecildi" AutoPostBack="true"></asp:DropDownList>
<br />
<br />
    <asp:Label runat="server" ID="lblDurum1" CssClass="bilgi"></asp:Label>
    <br /><br />
    <asp:DataGrid ID="gridUyeler" runat="server" AllowPaging="true" AllowSorting="true"
        OnPageIndexChanged="grid_PageIndexChanged" PageSize="10"
            AutoGenerateColumns="false" BorderWidth="0" GridLines="Both" OnUpdateCommand="Update"
            OnEditCommand="Edit" OnCancelCommand="Cancel" OnItemCommand="ItemCommand">
        <Columns>
            <asp:BoundColumn DataField="UYE_ID" HeaderText="Uye ID" ReadOnly="true"></asp:BoundColumn>
            <asp:BoundColumn DataField="EPOSTA" HeaderText="Eposta (256)"></asp:BoundColumn>
            <asp:BoundColumn DataField="IS_BLOCKED" HeaderText="Bloke"></asp:BoundColumn>
            <asp:BoundColumn DataField="BLOK_NEDENI" HeaderText="Blok nedeni (256)"></asp:BoundColumn>
            <asp:BoundColumn DataField="KULLANICI_ADI" HeaderText="Kullanici adi (256)"></asp:BoundColumn>
            <asp:BoundColumn DataField="AD" HeaderText="Ad (50)"></asp:BoundColumn>
            <asp:BoundColumn DataField="SOYAD" HeaderText="Soyad (50)"></asp:BoundColumn>
            <asp:BoundColumn DataField="OKUL_ID"></asp:BoundColumn>
            <asp:BoundColumn DataField="OKUL_ISMI" HeaderText="Okul" ReadOnly="true"></asp:BoundColumn>
            <asp:BoundColumn DataField="UYELIK_DURUMU" HeaderStyle-CssClass="sakla" ItemStyle-CssClass="sakla" FooterStyle-CssClass="sakla"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="Uyelik Durumu">
                <ItemTemplate><%# (Enums.UyelikDurumu)Convert.ToInt32(DataBinder.Eval(Container.DataItem, "UYELIK_DURUMU").ToString())%></ItemTemplate>
            </asp:TemplateColumn>            
            <asp:BoundColumn DataField="ROL_ID" HeaderText="RolID"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="Rol">
                <ItemTemplate><%# (Enums.UyelikRol)Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ROL_ID").ToString())%></ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="CINSIYET" HeaderText="Kiz mi"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="Cinsiyet">
                <ItemTemplate><%# (Enums.Cinsiyet)Convert.ToInt32(Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "CINSIYET").ToString()))%></ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="ONAY_PUANI" HeaderText="Onay puani"></asp:BoundColumn>
            
            <asp:EditCommandColumn ButtonType="LinkButton" EditText="..." CancelText="Iptal" 
            UpdateText="Guncelle"></asp:EditCommandColumn>
            <asp:ButtonColumn ButtonType="LinkButton" Text="X" CommandName="Sil1"></asp:ButtonColumn>
            <asp:ButtonColumn ButtonType="LinkButton" Text="Eminim" CommandName="Sil2" Visible="false"></asp:ButtonColumn>
        </Columns>
        <PagerStyle Visible="true" NextPageText="Ileri &gt;" PrevPageText="&lt; Geri" HorizontalAlign="Right" Mode="NumericPages" />
    </asp:DataGrid>
    <asp:Label runat="server" ID="lblDurum2" CssClass="bilgi"></asp:Label>
    <br /><br />
    <asp:Label runat="server" ID="lblAciklama"></asp:Label>
</asp:Content>
