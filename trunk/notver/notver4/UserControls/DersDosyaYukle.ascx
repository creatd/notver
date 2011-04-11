<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DersDosyaYukle.ascx.cs"
    Inherits="UserControls_DersDosyaYukle" %>
<script type="text/javascript">
    $(document).ready(function(){
        update_size(50,55);
    });
    
function update_size(w_bias,h_bias)  {
    var w = $(document).width() + w_bias;
    var h = $(document).height() + h_bias;
    parent.resize(w,h);
}
    
function SetFocusDersAra(e)
{
    var keycode;
    if(window.event)
    {
        keycode = window.event.keyCode;
    }
    else if(e.which)
    {
        keycode = e.which;
    }
    else if(e.keyCode)
    {
        keycode = e.keyCode;
    }

    if(keycode == 13)
    {
        document.getElementById('<%= btnDersAra.ClientID %>').focus();
    }
}

function Temizle(obj)
{
    obj.value='';
}
</script>
<asp:Panel ID="pnlDosyaYukle" runat="server" Width="510" Height="380" CssClass="DersDosyaYukle">
    <p style="padding-bottom:10px; font-size:11px;">
    Baska bir ders seç&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox runat="server" ID="txtDersKodu" Text="Ders kodu veya ismi" onclick="javascript:return Temizle(this);"
            OnKeyDown="javascript:return SetFocusDersAra(event);" CssClass="textbox"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDersKodu" Display="Dynamic"
                ErrorMessage="<img src='./App_Themes/Default/Images/hata.png' alt='hata'>" 
                ID="reqDersIsmi" ValidationGroup="vgDersAra" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:LinkButton runat="server" ID="btnDersAra" OnClick="DersAra" ValidationGroup="vgDersAra" 
        CausesValidation="true"><span style="font-size:11px;">Ara</span></asp:LinkButton>                
    </p>
    
    <asp:Panel runat="server" ID="pnlDersAramaSonuclari" Visible="false">
        <p style="text-align:right; font-weight:bold; font-size:11px; padding-bottom:5px;">
            Sayfa başı <asp:DropDownList runat="server" ID="dropSayfaBoyutu" OnSelectedIndexChanged="SayfaBoyutuDegisti" 
            AutoPostBack="True" CssClass="dropdownPager">                       
                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            <asp:ListItem Text="Hepsi" Value="0"></asp:ListItem>
                        </asp:DropDownList> ders
        </p>    
        <asp:DataGrid runat="server" ID="gridDersAramaSonuclari" AutoGenerateColumns="false"
            AllowPaging="false" AllowSorting="false" OnItemCommand="ItemCommand" GridLines="None">
            <Columns>
                <asp:BoundColumn DataField="KOD" HeaderText="Ders Kodu" HeaderStyle-ForeColor="#191919"></asp:BoundColumn>
                <asp:BoundColumn DataField="DERS_ISIM" HeaderText="Isim" HeaderStyle-ForeColor="#191919"></asp:BoundColumn>
                <asp:BoundColumn DataField="OKUL_ISIM" HeaderText="Universite" HeaderStyle-ForeColor="#191919"></asp:BoundColumn>
                <asp:TemplateColumn ItemStyle-ForeColor="#191919">
                    <ItemTemplate>
                        &nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server" Text="Seç" CommandName="DersSec"
                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.DERS_ID")%>'>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn Visible="false" DataField="OKUL_ID"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <asp:Panel ID="pnlPager" runat="server">
            <div id="pager" style="text-align:center;">
                <asp:ImageButton ID="lnkOnceki" Text="Önceki" OnClick="OncekiSayfayaGit" runat="server"
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
        </asp:Panel>
            

    </asp:Panel>
    <asp:Panel runat="server" ID="pnlDersAramaSonuclariBos" Visible="false" CssClass="hata">
        Ders bulunamadı
        <br /><br />
    </asp:Panel>

<table style="border: none; font-weight:bold; font-size:13px;" width="500">
    <tr style="border-top:solid 1pt #626262;">
        <td style="padding-bottom:30px; padding-top:30px;">
            Seçilen ders :
        </td>
        <td style="padding-bottom:30px; padding-top:30px;">
            <asp:Label runat="server" ID="lblSecilenDers"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Dosya tipi :
        </td>
        <td style="font-weight:normal;">
            <asp:RadioButtonList runat="server" ID="rbDosyaTipleri">
                <asp:ListItem Text="Sinav ve/veya Cozum" Value="0" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Ders notu" Value="1"></asp:ListItem>
                <asp:ListItem Text="Odev" Value="2"></asp:ListItem>
                <asp:ListItem Text="Proje" Value="3"></asp:ListItem>
                <asp:ListItem Text="Yararli kaynak" Value="4"></asp:ListItem>
                <asp:ListItem Text="Diger" Value="5"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td>
            Dosya isim:
            <br />
            <span class="sessiz">(Opsiyonel)</span>
        </td>
        <td>
            <asp:TextBox runat="server" TextMode="SingleLine" ID="txtDosyaIsim" CssClass="textbox"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Hoca :
            <br />
            <span class="sessiz">(Opsiyonel)</span>
        </td>
        <td>
            <asp:DropDownList runat="server" ID="drpDersHocalar"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            Açıklama :
            <br />
            <span class="sessiz">(Opsiyonel)</span>
        </td>
        <td>
            <asp:TextBox runat="server" TextMode="MultiLine" Width="250" Height="100" ID="txtDosyaAciklama" CssClass="multitextbox"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="left" style="padding-bottom:30px;">
            <asp:FileUpload runat="server" ID="fileUpload" />
        </td>
    </tr>
    <tr>
        <td colspan="2" align="right">
            <asp:ImageButton runat="server" ID="btnYukle" Text="Yükle" OnClick="DosyaYukle" ValidationGroup="vg1"
            ImageUrl="~/App_Themes/Default/Images/gonder.png" CausesValidation="true"></asp:ImageButton>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center" style="padding-top:20px;">
            <asp:Label runat="server" ID="lblYuklemeDurum" CssClass="hata"></asp:Label>
        </td>
    </tr>
</table>
</asp:Panel>
<asp:Panel ID="pnlUyeOl" runat="server" CssClass="bilgi">
    <br/><br/>
    Dosya yükleyebilmek için giriş yapmanız gereklidir.
    <br/><br/>
    Uyeliğiniz yoksa ana sayfada sağ üstten hemen ücretsiz üye olabilirsiniz.
</asp:Panel>
<asp:Panel ID="pnlHata" runat="server" CssClass="durum">
Bir hata oluştu :(
</asp:Panel>
<asp:Literal runat="server" ID="ltrScript"></asp:Literal>