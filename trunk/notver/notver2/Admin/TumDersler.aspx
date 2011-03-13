<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TumDersler.aspx.cs" Inherits="Admin_TumDersler"
    MasterPageFile="~/Masters/Admin.master" MaintainScrollPositionOnPostback="true" ValidateRequest="false" %>

<asp:Content runat="server" ContentPlaceHolderID="content">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <h1>
        Dersler</h1>
    <asp:DropDownList runat="server" ID="drpOkullar" OnSelectedIndexChanged="OkulSecildi"
    AutoPostBack="true">
    </asp:DropDownList>
    <br />
    <asp:Label runat="server" ID="lblDurum1" CssClass="bilgi"></asp:Label>
    <br />
    <br />
    <asp:DataGrid ID="gridDersler" runat="server" AllowPaging="true" AllowSorting="true"
    OnPageIndexChanged="grid_PageIndexChanged" PageSize="10"
        AutoGenerateColumns="false" BorderWidth="0" GridLines="Both" OnUpdateCommand="Update"
        OnEditCommand="Edit" OnCancelCommand="Cancel" OnItemCommand="ItemCommand">
        <Columns>
            <asp:BoundColumn DataField="DERS_ID" HeaderText="Ders ID" ReadOnly="true"></asp:BoundColumn>
            <asp:BoundColumn DataField="IS_ACTIVE" HeaderText="Is Active"></asp:BoundColumn>
            <asp:BoundColumn DataField="OKUL_ID" HeaderText="Okul ID"></asp:BoundColumn>
            <asp:BoundColumn DataField="OKUL_ISIM" HeaderText="Okul Isim" ReadOnly="true"></asp:BoundColumn>
            <asp:BoundColumn DataField="KOD" HeaderText="Kod (50)"></asp:BoundColumn>
            <asp:BoundColumn DataField="ISIM" HeaderText="Isim (150)"></asp:BoundColumn>
            <asp:BoundColumn DataField="ACIKLAMA" HeaderText="Aciklama (2000)"></asp:BoundColumn>
            <asp:EditCommandColumn ButtonType="LinkButton" EditText="..." CancelText="Iptal"
                UpdateText="Guncelle"></asp:EditCommandColumn>
            <asp:ButtonColumn ButtonType="LinkButton" Text="X" CommandName="Sil1"></asp:ButtonColumn>
            <asp:ButtonColumn ButtonType="LinkButton" Text="Eminim" CommandName="Sil2" Visible="false">
            </asp:ButtonColumn>
        </Columns>
        <PagerStyle Visible="true" NextPageText="Ileri &gt;" PrevPageText="&lt; Geri" HorizontalAlign="Right" Mode="NumericPages" />
    </asp:DataGrid>
    <asp:Label runat="server" ID="lblDurum2" CssClass="bilgi"></asp:Label>
    
    <asp:Panel runat="server" ID="pnlKayitsizDersler">
        <h1>Kayitsiz dersler</h1>
        <table>
            <tr>
                <td>
                    <asp:DropDownList runat="server" ID="drpKayitsizDersler"></asp:DropDownList>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr><td>
                                    <asp:DropDownList runat="server" ID="drpOkullar2" OnSelectedIndexChanged="OkulSecildi2"
                                    AutoPostBack="true"></asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList runat="server" ID="drpOkulDersler"></asp:DropDownList>
                                </td></tr>
                                <tr><td align="center">
                                    ya da
                                </td></tr>
                                <tr><td>
                                    Ders ID&nbsp;:&nbsp;<asp:TextBox runat="server" ID="txtDersID"></asp:TextBox>
                                </td></tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Iliskilendir" OnClick="KayitsizDersIliskilendir" />
                </td>
            </tr>
            <tr>
                <td colspan="3" align="right">
                    <asp:Label runat="server" ID="lblDurum3" CssClass="bilgi"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>    
</asp:Content>
