<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DersDosyaYukle.ascx.cs"
    Inherits="UserControls_DersDosyaYukle" %>
<script type="text/javascript">
function SetFocusDersDosya(e)
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
<table>
    <tr>
        <td>
            <asp:TextBox runat="server" ID="txtDersKodu" Text="Ders kodu" Width="100" onclick="javascript:return Temizle(this);"
            OnKeyDown="javascript:return SetFocusDersAra(event);"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDersKodu" Display="Dynamic"
                ErrorMessage="*" ID="reqDersIsmi" ValidationGroup="vgDersAra"></asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:LinkButton runat="server" ID="btnDersAra" OnClick="DersAra" ValidationGroup="vgDersAra"
                CausesValidation="true" Text="Ara"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Panel runat="server" ID="pnlDersAramaSonuclari" Visible="false">
                <asp:DataGrid runat="server" ID="gridDersAramaSonuclari" AutoGenerateColumns="false"
                    AllowPaging="false" AllowSorting="false" OnItemCommand="ItemCommand">
                    <Columns>
                        <asp:BoundColumn DataField="KOD" HeaderText="Ders Kodu"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DERS_ISIM" HeaderText="Isim"></asp:BoundColumn>
                        <asp:BoundColumn DataField="OKUL_ISIM" HeaderText="Universite"></asp:BoundColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" Text="Sec" CommandName="DersSec"
                                    CommandArgument='<%# DataBinder.Eval(Container,"DataItem.DERS_ID")%>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
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
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlDersAramaSonuclariBos" Visible="false">
                Ders bulunamadi
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td>
            Secilen ders :
        </td>
        <td>
            <asp:Label runat="server" ID="lblSecilenDers"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Dosya tipi :
        </td>
        <td>
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
            <asp:TextBox runat="server" TextMode="SingleLine" Width="250" ID="txtDosyaIsim"></asp:TextBox>
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
            Aciklama :
            <br />
            <span class="sessiz">(Opsiyonel)</span>
        </td>
        <td>
            <asp:TextBox runat="server" TextMode="MultiLine" Width="250" Height="100" ID="txtDosyaAciklama"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <asp:FileUpload runat="server" ID="fileUpload" />
        </td>
    </tr>
    <tr>
        <td colspan="2" align="right">
            <asp:Label runat="server" ID="lblYuklemeDurum" CssClass="hata"></asp:Label>
            <asp:LinkButton runat="server" ID="btnYukle" Text="Yukle" OnClick="DosyaYukle" ValidationGroup="vg1"
                CausesValidation="true"></asp:LinkButton>
        </td>
    </tr>
</table>
<asp:Literal runat="server" ID="ltrScript"></asp:Literal>