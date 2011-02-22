<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DersDosyalar.ascx.cs"
    Inherits="UserControls_DersDosyalar" %>
<%@ Register TagName="DersDosyaYukle" TagPrefix="uc1" Src="~/UserControls/DersDosyaYukle.ascx" %>

<script type="text/javascript">
// Create the tooltips only on document load
$(document).ready(function() 
{
   // Notice the use of the each() method to acquire access to each elements attributes
   $('#divDosyalar a[tooltip]').each(function()
   {
      $(this).qtip({
         content: $(this).attr('tooltip'), // Use the tooltip attribute of the element for the content
         style: 'light' // Give it a crea mstyle to make it stand out
      });
   });
});
</script>

<!-- facebox 
<script type="text/javascript">
$(document).ready(function($) {
  $('a[rel*=facebox]').facebox()
}); 
</script> -->

<div id="divKlasorler" style="float: left; width: 200px; height: 100%;">
    <p>
        <asp:LinkButton runat="server" Text="Sinav ve Cozumler" ID="butKlasor0" CommandArgument="0"
            OnCommand="KlasorSec" /></p>
    <p>
        <asp:LinkButton runat="server" Text="Ders Notlari" ID="butKlasor1" CommandArgument="1"
            OnCommand="KlasorSec" /></p>
    <p>
        <asp:LinkButton runat="server" Text="Odevler" ID="butKlasor2" CommandArgument="2"
            OnCommand="KlasorSec" /></p>
    <p>
        <asp:LinkButton runat="server" Text="Projeler" ID="butKlasor3" CommandArgument="3"
            OnCommand="KlasorSec" /></p>
    <p>
        <asp:LinkButton runat="server" Text="Yararli Kaynaklar" ID="butKlasor4" CommandArgument="4"
            OnCommand="KlasorSec" /></p>
    <p>
        <asp:LinkButton runat="server" Text="Diger" ID="butKlasor5" CommandArgument="5" OnCommand="KlasorSec" /></p>
    <p>
        <br />
        <asp:LinkButton runat="server" Text="Hepsi" ID="butKlasor6" CommandArgument="6" OnCommand="KlasorSec" /></p>        
    <p>
        <br />
        <br />
        <br />
        <a href="DersDosyaYukle.aspx?DersID=<%=Query.GetInt("DersID")%>&KeepThis=true&TB_iframe=true&modal=false&height=700&width=600" class="thickbox">Dosya yuklemek icin tiklayin</a>
    </p>
</div>
<asp:Panel runat="server" ID="pnlDosyalar">
    <div id="divDosyalar" style="float: left; width: 670px;">
        <asp:DataGrid runat="server" ID="gridDosyalar" AutoGenerateColumns="false" OnItemDataBound="gridDosyalar_ItemDataBound"
            AllowSorting="true" AllowPaging="false" OnItemCommand="gridDosyalar_ItemCommand">
            <Columns>
                <asp:BoundColumn DataField="DOSYA_ISMI" HeaderText="Dosya Ismi"></asp:BoundColumn>
                <asp:BoundColumn DataField="HOCA_ISIM" HeaderText="Hoca Ismi"></asp:BoundColumn>
                <asp:BoundColumn DataField="EKLENME_TARIHI" HeaderText="Eklenme Tarihi"></asp:BoundColumn>
                <asp:BoundColumn DataField="DOSYA_ADRES" Visible="false"></asp:BoundColumn>
                <asp:ButtonColumn ButtonType="PushButton" HeaderText="Indir" CommandName="DosyaIndir"></asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        <asp:Panel ID="pnlPager" runat="server">
            <table class="pager">
                <tr>
                    <td>
                        <asp:LinkButton ID="lnkOnceki" Text="Onceki" OnClick="OncekiSayfayaGit" runat="server"></asp:LinkButton>
                    </td>
                    <td>
                        <asp:Repeater runat="server" ID="rptPager" OnItemCommand="rptPager_Command" OnItemDataBound="rptPager_DataBound">
                            <HeaderTemplate>
                                <ol>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li>
                                    <asp:LinkButton runat="server" Text="<%# Container.DataItem %>" CommandName="SayfayaGit"
                                        CommandArgument="<%# Container.DataItem %>" ID="lnkSayfa"></asp:LinkButton></li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ol></FooterTemplate>
                        </asp:Repeater>
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkSonraki" Text="Sonraki" OnClick="SonrakiSayfayaGit" runat="server"></asp:LinkButton>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="dropSayfaBoyutu" OnSelectedIndexChanged="SayfaBoyutuDegisti"
                            AutoPostBack="True">
                            <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                            <asp:ListItem Text="40" Value="40"></asp:ListItem>
                            <asp:ListItem Text="Hepsi" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Panel>
<asp:Panel ID="pnlDosyaYok" runat="server" Visible="false">
    <asp:Label ID="lblYorumYok" runat="server">(Daha önce dosya eklenmemis) Sol taraftaki menuden dosya ekleyerek ilk dosya ekleyen </asp:Label><asp:HyperLink
        ID="linkYorumYap" Text="siz olun!" runat="server" NavigateUrl="#DosyaEkle"></asp:HyperLink>
</asp:Panel>
<div id="dosyaYukle" style="display: none;">
    <uc1:DersDosyaYukle runat="server"></uc1:DersDosyaYukle>
</div>
<asp:Literal runat="server" ID="ltrScript" EnableViewState="false"></asp:Literal>
