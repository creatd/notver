﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TumHocalar.aspx.cs" Inherits="Admin_TumHocalar" 
MasterPageFile="~/Masters/Admin.master" MaintainScrollPositionOnPostback="true" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>


<asp:Content runat="server" ContentPlaceHolderID="content">
<ajax:ToolkitScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
<h1>Hocalar</h1>
    <p>Okul : <asp:DropDownList runat="server" ID="drpOkullar" OnSelectedIndexChanged="OkulSecildi"
    AutoPostBack="true">
    </asp:DropDownList>
    </p>
    <p>Bolum : <asp:DropDownList runat="server" ID="drpOkulBolumler" OnSelectedIndexChanged="BolumSecildi"
    AutoPostBack="true"></asp:DropDownList>
    </p>
    <br />
    <asp:Label runat="server" ID="lblDurum1" CssClass="bilgi"></asp:Label>
    <br /><br />
    <asp:DataGrid ID="gridHocalar" runat="server" AllowPaging="true" AllowSorting="true"
            OnPageIndexChanged="grid_PageIndexChanged" PageSize="10"
            AutoGenerateColumns="false" BorderWidth="0" GridLines="Both" OnUpdateCommand="Update"
            OnEditCommand="Edit" OnCancelCommand="Cancel" OnItemCommand="ItemCommand">
        <Columns>
            <asp:BoundColumn DataField="HOCA_ID" HeaderText="Hoca ID" ReadOnly="true"></asp:BoundColumn>
            <asp:BoundColumn DataField="IS_ACTIVE" HeaderText="Is Active"></asp:BoundColumn>
            <asp:BoundColumn DataField="ISIM" HeaderText="Isim (50)"></asp:BoundColumn>
            <asp:BoundColumn DataField="UNVAN" HeaderText="Unvan (50)"></asp:BoundColumn>
            <asp:BoundColumn DataField="YORUM_SAYISI" HeaderText="Yorum sayisi" ReadOnly="true"></asp:BoundColumn>
            <asp:ButtonColumn ButtonType="LinkButton" Text="Detay" CommandName="Detay"></asp:ButtonColumn>
            <asp:EditCommandColumn ButtonType="LinkButton" EditText="..." CancelText="Iptal" 
            UpdateText="Guncelle"></asp:EditCommandColumn>
            <asp:ButtonColumn ButtonType="LinkButton" Text="X" CommandName="Sil1"></asp:ButtonColumn>
            <asp:ButtonColumn ButtonType="LinkButton" Text="Eminim" CommandName="Sil2" Visible="false"></asp:ButtonColumn>
        </Columns>
        <PagerStyle Visible="true" NextPageText="Ileri &gt;" PrevPageText="&lt; Geri" HorizontalAlign="Right" Mode="NumericPages"/>
    </asp:DataGrid>
    
    <asp:Panel ID="pnlHocaDersler" runat="server" Visible="false">
        <h2>Hoca - Dersler</h2>
        <asp:UpdatePanel runat="server" ID="pnlUpdate2">
        <ContentTemplate>        
        <asp:DataGrid ID="gridHocaDersler" runat="server" AllowPaging="true" AllowSorting="true"
            OnPageIndexChanged="grid2_PageIndexChanged" PageSize="10"
            AutoGenerateColumns="false" BorderWidth="0" GridLines="Both" OnItemCommand="ItemCommand2">
            <Columns>
                <asp:BoundColumn DataField="DERS_ID" HeaderText="Ders ID" ReadOnly="true"></asp:BoundColumn>
                <asp:BoundColumn DataField="KOD" HeaderText="Ders Kodu" ReadOnly="true"></asp:BoundColumn>
                <asp:BoundColumn DataField="OKUL_ISIM" HeaderText="Okul" ReadOnly="true"></asp:BoundColumn>
                
                <asp:ButtonColumn ButtonType="LinkButton" Text="X" CommandName="Sil1"></asp:ButtonColumn>
                <asp:ButtonColumn ButtonType="LinkButton" Text="Eminim" CommandName="Sil2" Visible="false"></asp:ButtonColumn>
            </Columns>
            <SelectedItemStyle BackColor="Gray" />
            <PagerStyle Visible="true" NextPageText="Ileri &gt;" PrevPageText="&lt; Geri" HorizontalAlign="Right" Mode="NumericPages"/>
        </asp:DataGrid>  
            <table>
                <tr>
                    <td>Yeni</td>
                    <td><asp:DropDownList runat="server" ID="drpOkullar2" OnSelectedIndexChanged="OkulSecildi2"
                    AutoPostBack="true"></asp:DropDownList></td>
                    <td><asp:DropDownList runat="server" ID="drpOkulBolumler2" OnSelectedIndexChanged="BolumSecildi2"
                    AutoPostBack="true"></asp:DropDownList></td>
                    <td><asp:DropDownList runat="server" ID="drpDersler2"></asp:DropDownList></td>
                    <td><asp:LinkButton runat="server" ID="btnHocaDersEkle" OnClick="HocaDersEkle" 
                    Text="Ekle"></asp:LinkButton></td>
                </tr>                
                <tr>
                    <td colspan="4">
                        <asp:Label runat="server" ID="lblDurum3" CssClass="bilgi"></asp:Label>
                    </td>
                </tr>
            </table>      
        </ContentTemplate>
        </asp:UpdatePanel>
        <div id="Bekleme2" class="Bekleme">
            <img src="../Scripts/images/loading.gif" />
        </div>
        <ajax:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" runat="server" 
        TargetControlID="pnlUpdate2">
        <Animations>
            <OnUpdating>
                <StyleAction animationtarget="Bekleme2" Attribute="display" value="block" />
            </OnUpdating>
            <OnUpdated>
                <StyleAction animationtarget="Bekleme2" Attribute="display" value="none" />
            </OnUpdated>
        </Animations>
        </ajax:UpdatePanelAnimationExtender>           
    </asp:Panel>
    
    <asp:Panel ID="pnlHocaOkullar" runat="server" Visible="false">
        <h2>Hoca - Okullar</h2>
        <asp:DataGrid ID="gridHocaOkullar" runat="server" AllowPaging="true" AllowSorting="true"
            OnPageIndexChanged="grid3_PageIndexChanged" PageSize="10"
            AutoGenerateColumns="false" BorderWidth="0" GridLines="Both" OnItemCommand="ItemCommand3">
            <Columns>
                <asp:BoundColumn DataField="OKUL_ID" HeaderText="Okul ID" ReadOnly="true"></asp:BoundColumn>
                <asp:BoundColumn DataField="ISIM" HeaderText="Okul" ReadOnly="true"></asp:BoundColumn>
                <asp:BoundColumn DataField="BOLUM_ISIM" HeaderText="Bolum" ReadOnly="true"></asp:BoundColumn>
                <asp:BoundColumn DataField="START_YEAR" HeaderText="Baslangic Yili"></asp:BoundColumn>
                <asp:BoundColumn DataField="END_YEAR" HeaderText="Bitis Yili"></asp:BoundColumn>
                <asp:ButtonColumn ButtonType="LinkButton" Text="X" CommandName="Sil1"></asp:ButtonColumn>
                <asp:ButtonColumn ButtonType="LinkButton" Text="Eminim" CommandName="Sil2" Visible="false"></asp:ButtonColumn>
            </Columns>
            <SelectedItemStyle BackColor="Gray" />
            <PagerStyle Visible="true" NextPageText="Ileri &gt;" PrevPageText="&lt; Geri" HorizontalAlign="Right" Mode="NumericPages"/>
        </asp:DataGrid>    
        <table>
            <tr>
                <td>Yeni</td>
                <td><asp:DropDownList runat="server" ID="drpOkullar3" OnSelectedIndexChanged="OkulSecildi3" 
                AutoPostBack="true"></asp:DropDownList></td>
                <td><asp:DropDownList runat="server" ID="drpOkulBolumler3"></asp:DropDownList></td>
                <td>Baslangic yili : <asp:TextBox runat="server" ID="txtOkulBaslangicYili" Width="60"
                ToolTip="Bilmiyosan bos birak"></asp:TextBox></td>
                <td>Bitis yili : <asp:TextBox runat="server" ID="txtOkulBitisYili" Width="60"
                ToolTip="Bilmiyosan bos birak, hala devam ediyosa 0 yaz"></asp:TextBox></td>
                <td><asp:LinkButton runat="server" ID="btnHocaOkulEkle" OnClick="HocaOkulEkle" Text="Ekle"></asp:LinkButton></td>
            </tr>
        </table>
    </asp:Panel>   
    
    <asp:Panel runat="server" ID="pnlKayitsizHocalar">
        <h1>Kayitsiz hocalar</h1>
        <table>
            <tr>
                <td>
                    <asp:DropDownList runat="server" ID="drpKayitsizHocalar"></asp:DropDownList>
                </td>
                <td>
                    <asp:UpdatePanel runat="server" ID="pnlUpdate">
                        <ContentTemplate>
                            <table>
                                <tr><td>
                                    <asp:DropDownList runat="server" ID="drpOkullar4" OnSelectedIndexChanged="OkulSecildi4"
                                    AutoPostBack="true"></asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList runat="server" ID="drpOkulBolumler4"
                                    OnSelectedIndexChanged="BolumSecildi4" AutoPostBack="true"></asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList runat="server" ID="drpOkulHocalar4"></asp:DropDownList>
                                </td></tr>
                                <tr><td align="center">
                                    ya da
                                </td></tr>
                                <tr><td>
                                    Hoca ID&nbsp;:&nbsp;<asp:TextBox runat="server" ID="txtHocaID"></asp:TextBox>
                                </td></tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div id="Bekleme" class="Bekleme">
                        <img src="../Scripts/images/loading.gif" />
                    </div>
                    <ajax:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" runat="server" 
                    TargetControlID="pnlUpdate">
                    <Animations>
                        <OnUpdating>
                            <StyleAction animationtarget="Bekleme" Attribute="display" value="block" />
                        </OnUpdating>
                        <OnUpdated>
                            <StyleAction animationtarget="Bekleme" Attribute="display" value="none" />
                        </OnUpdated>
                    </Animations>
                    </ajax:UpdatePanelAnimationExtender>                        
                </td>
                <td>
                    <asp:Button runat="server" Text="Iliskilendir" OnClick="KayitsizHocaIliskilendir" />
                </td>
            </tr>
            <tr>
                <td colspan="3" align="right">
                    <asp:Label runat="server" ID="lblDurum4" CssClass="bilgi"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    
    <asp:Label runat="server" ID="lblDurum2" CssClass="bilgi"></asp:Label> 
</asp:Content>
