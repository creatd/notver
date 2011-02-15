<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TumHocalar.aspx.cs" Inherits="Admin_TumHocalar" 
MasterPageFile="~/Masters/Admin.master" MaintainScrollPositionOnPostback="true" %>

<asp:Content runat="server" ContentPlaceHolderID="content">
<h1>Hocalar</h1>
    <asp:DropDownList runat="server" ID="drpOkullar" OnSelectedIndexChanged="OkulSecildi"
    AutoPostBack="true">
    </asp:DropDownList>
    <br />
    <asp:Label runat="server" ID="lblDurum1"></asp:Label>
    <asp:DataGrid ID="gridHocalar" runat="server" AllowPaging="true" AllowSorting="true" PageSize="10"
            OnPageIndexChanged="grid_PageIndexChanged"
            AutoGenerateColumns="false" BorderWidth="0" GridLines="Both" OnUpdateCommand="Update"
            OnEditCommand="Edit" OnCancelCommand="Cancel" OnItemCommand="ItemCommand">
        <Columns>
            <asp:BoundColumn DataField="HOCA_ID" HeaderText="Hoca ID" ReadOnly="true"></asp:BoundColumn>
            <asp:BoundColumn DataField="IS_ACTIVE" HeaderText="Is Active"></asp:BoundColumn>
            <asp:BoundColumn DataField="ISIM" HeaderText="Isim (50)"></asp:BoundColumn>
            <asp:BoundColumn DataField="UNVAN" HeaderText="Unvan (50)"></asp:BoundColumn>
            <asp:BoundColumn DataField="YORUM_SAYISI" HeaderText="Yorum sayisi" ReadOnly="true"></asp:BoundColumn>
            <asp:EditCommandColumn ButtonType="LinkButton" EditText="..." CancelText="Iptal" 
            UpdateText="Guncelle"></asp:EditCommandColumn>
            <asp:ButtonColumn ButtonType="LinkButton" Text="X" CommandName="Sil1"></asp:ButtonColumn>
            <asp:ButtonColumn ButtonType="LinkButton" Text="Eminim" CommandName="Sil2" Visible="false"></asp:ButtonColumn>
        </Columns>
        <PagerStyle Visible="true" NextPageText="Ileri &gt;" PrevPageText="&lt; Geri" HorizontalAlign="Right" Mode="NumericPages"/>
    </asp:DataGrid>
    <asp:Label runat="server" ID="lblDurum2"></asp:Label>
</asp:Content>
