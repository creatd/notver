<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DersDosyalar.ascx.cs"
    Inherits="UserControls_DersDosyalar" %>

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

<table style="width:960px;">
<tr>
    <td id="divKlasorler">
        <p>
            <asp:LinkButton runat="server" Text="Sinav ve Cozumler" ID="butKlasor0" CommandArgument="0"
                OnCommand="KlasorSec"  /></p>
        <span>&nbsp;</span>
        <p>
            <asp:LinkButton runat="server" Text="Ders Notlari" ID="butKlasor1" CommandArgument="1"
                OnCommand="KlasorSec" /></p>
        <span>&nbsp;</span>
        <p>
            <asp:LinkButton runat="server" Text="Odevler" ID="butKlasor2" CommandArgument="2"
                OnCommand="KlasorSec" /></p>
        <span>&nbsp;</span>
        <p>
            <asp:LinkButton runat="server" Text="Projeler" ID="butKlasor3" CommandArgument="3"
                OnCommand="KlasorSec" /></p>
        <span>&nbsp;</span>
        <p>
            <asp:LinkButton runat="server" Text="Yararli Kaynaklar" ID="butKlasor4" CommandArgument="4"
                OnCommand="KlasorSec" /></p>
        <span>&nbsp;</span>
        <p>
            <asp:LinkButton runat="server" Text="Diger" ID="butKlasor5" CommandArgument="5" OnCommand="KlasorSec" /></p>
        <span>&nbsp;</span>
        <p>
            <br />
            <asp:LinkButton runat="server" Text="Hepsi" ID="butKlasor6" CommandArgument="6" OnCommand="KlasorSec" /></p>        
        <br /><br /><br />

    </td>
    <td class="pnlDosyalar">
        <asp:Panel runat="server" ID="pnlDosyalar">
            <p style="text-align:right; font-weight:bold; font-size:11px; padding-bottom:5px;">
                Sayfa basi&nbsp;<asp:DropDownList runat="server" ID="dropSayfaBoyutu" OnSelectedIndexChanged="SayfaBoyutuDegisti"
                    AutoPostBack="True">
                    <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                    <asp:ListItem Text="40" Value="40"></asp:ListItem>
                    <asp:ListItem Text="Hepsi" Value="0"></asp:ListItem>
                </asp:DropDownList>&nbsp;dosya    
            </p>
            <asp:DataGrid runat="server" ID="gridDosyalar" AutoGenerateColumns="false" OnItemDataBound="gridDosyalar_ItemDataBound"
                AllowSorting="true" AllowPaging="false" OnItemCommand="gridDosyalar_ItemCommand" CssClass="gridDosyalar" BorderStyle="None"
                BorderWidth="0" GridLines="None">
                <Columns>
                    <asp:BoundColumn DataField="DOSYA_ISMI" HeaderText="Dosya Ismi" ItemStyle-Width="300px" HeaderStyle-ForeColor="Black"
                    HeaderStyle-Height="50px" HeaderStyle-Font-Size="12px"></asp:BoundColumn>
                    <asp:BoundColumn DataField="HOCA_ISIM" HeaderText="Hoca Ismi" ItemStyle-Width="150px"  HeaderStyle-ForeColor="Black"
                    HeaderStyle-Height="50px" HeaderStyle-Font-Size="12px"></asp:BoundColumn>
                    <asp:BoundColumn DataField="EKLENME_TARIHI" HeaderText="Eklenme Tarihi" ItemStyle-Width="150px" HeaderStyle-ForeColor="Black"
                    HeaderStyle-Height="50px" HeaderStyle-Font-Size="12px"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DOSYA_ADRES" Visible="false"></asp:BoundColumn>
                    <asp:ButtonColumn ButtonType="LinkButton" HeaderText="Indir" CommandName="DosyaIndir" HeaderStyle-ForeColor="Black"
                    HeaderStyle-Height="50px" HeaderStyle-Font-Size="12px" Text="&lt;img src=&quot;./App_Themes/Default/Images/indir.png&quot; /&gt;">
                    </asp:ButtonColumn>
                    <asp:TemplateColumn>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid> 
        </asp:Panel> 
        <asp:Panel ID="pnlDosyaYok" runat="server" Visible="false">
            <p style="font-weight:bold; padding:10px; color:#626262; font-size:13px; font-style:italic;">
                Daha once sectiginiz kategoride dosya eklenmemis. Sol taraftan baska bir kategori secin.
            </p>
        </asp:Panel>         
    </td>
</tr>
<tr>
    <td style="width:160px;color:#F25755; padding-bottom:30px; background-color:#F6F6F6; border-right:1pt solid #C3C3C3;">
        <br /><br />
        <a style="padding-left:10px; display:block;"
        href="DersDosyaYukle.aspx?DersID=<%=Query.GetInt("DersID")%>">Dosya yuklemek icin tiklayin</a>
    </td>
    <td style="background-color:White; padding-left:30px; padding-right:30px;">
        <div id="pager" style="text-align:center; height:100%;">
            <asp:ImageButton ID="lnkOnceki" Text="Onceki" OnClick="OncekiSayfayaGit" runat="server"
            ImageUrl="~/App_Themes/Default/Images/prev.png"></asp:ImageButton>
            <asp:Repeater runat="server" ID="rptPager" OnItemCommand="rptPager_Command" OnItemDataBound="rptPager_DataBound">
                <ItemTemplate>
                        <asp:LinkButton runat="server" Text="<%# Container.DataItem %>" CommandName="SayfayaGit" 
                        CommandArgument="<%# Container.DataItem %>" ID="lnkSayfa" CssClass="pager"></asp:LinkButton></li>
                </ItemTemplate>
            </asp:Repeater>
            <asp:ImageButton ID="lnkSonraki" Text="Sonraki" OnClick="SonrakiSayfayaGit" runat="server"
            ImageUrl="~/App_Themes/Default/Images/next.png">
            </asp:ImageButton>
        </div>    
    </td>
</tr>
</table>



<asp:Literal runat="server" ID="ltrScript" EnableViewState="false"></asp:Literal>
