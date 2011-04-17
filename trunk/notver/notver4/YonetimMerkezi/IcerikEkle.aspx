<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IcerikEkle.aspx.cs"  ValidateRequest="false"
Inherits="Admin_IcerikEkle" MasterPageFile="~/Masters/Admin.master" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content runat="server" ContentPlaceHolderID="content">
<ajax:ToolkitScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
<h1>İçerik Ekle</h1>
<h2>Okul Ekle</h2>
<asp:Panel runat="server" ID="pnlOkulEkle">
    <table>
        <tr>
            <td>Is_Active</td>
            <td><asp:DropDownList runat="server" ID="drpOkulEkleActive">
                <asp:ListItem Text="Evet" Value="1" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Hayir" Value="0"></asp:ListItem>
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td>İsim (100)</td>
            <td><asp:TextBox runat="server" ID="txtOkulIsim" Width="400"></asp:TextBox><span style="color:Red; display:inline;">*</span></td>
        </tr>
        <tr>
            <td>Adres (50)</td>
            <td><asp:TextBox runat="server" ID="txtOkulAdres" Width="400"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Kuruluş Tarihi</td>
            <td><asp:TextBox runat="server" ID="txtOkulKurulusTarihi" Width="60"></asp:TextBox></td>                
        </tr>
        <tr>
            <td>Öğrenci Sayısı</td>
            <td><asp:TextBox runat="server" ID="txtOkulOgrenciSayisi" Width="60"></asp:TextBox></td>                
        </tr>
        <tr>
            <td>Akademik Sayısı</td>
            <td><asp:TextBox runat="server" ID="txtOkulAkademikSayisi" Width="60"></asp:TextBox></td>                
        </tr>
        <tr>
            <td>Web adresi (256)</td>
            <td><asp:TextBox runat="server" ID="txtOkulWebAdresi" Width="400"></asp:TextBox></td>                
        </tr>
        <tr>
            <td><asp:Button runat="server" Text="Okul ekle" OnClick="OkulEkle" /></td>
            <td><asp:Label runat="server" ID="lblDurumOkulEkle" CssClass="bilgi"></asp:Label></td>
        </tr>        
    </table>
</asp:Panel>
<h2>Okul Bolum Ekle</h2>
<asp:Panel runat="server" ID="pnlOkulBolumEkle">
    <table>
        <tr>
            <td>Okul</td>
            <td><asp:DropDownList runat="server" ID="drpOkullar"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>Isim (256)</td>
            <td><asp:TextBox runat="server" ID="txtBolumIsim" Width="200"></asp:TextBox><span style="color:Red; display:inline;">*</span></td>
        </tr>
        <tr>
            <td>Is Active</td>
            <td><asp:DropDownList runat="server" ID="drpBolumEkleActive">
                <asp:ListItem Text="Evet" Value="1" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Hayir" Value="0"></asp:ListItem>
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td><asp:Button ID="btnBolumEkle" runat="server" Text="Bolum ekle" OnClick="BolumEkle" /></td>
            <td><asp:Label runat="server" ID="lblDurumBolumEkle" CssClass="bilgi"></asp:Label></td>
        </tr> 
    </table>
</asp:Panel>
<h2>Ders Ekle</h2>
<asp:Panel runat="server" ID="pnlDersEkle">
    <table>
        <tr>
            <td>Okul</td>
            <td><asp:DropDownList runat="server" ID="drpDersOkullar" OnSelectedIndexChanged="DersEkle_OkulSecildi"
            AutoPostBack="true"></asp:DropDownList><span style="color:Red; display:inline;">*</span></td>
        </tr>
        <tr>
            <td>Bolum</td>
            <td><asp:DropDownList runat="server" ID="drpDersEkle_Bolumler"></asp:DropDownList><span style="color:Red; display:inline;">*</span></td>
        </tr>
        <tr>
            <td>Is_Active</td>
            <td><asp:DropDownList runat="server" ID="drpDersIsActive">
                <asp:ListItem Text="Evet" Value="1" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Hayir" Value="0"></asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>Kod (50)</td>
            <td><asp:TextBox runat="server" ID="txtDersKod" Width="400"></asp:TextBox><span style="color:Red; display:inline;">*</span></td>
        </tr>
        <tr>
            <td>İsim (150)</td>
            <td><asp:TextBox runat="server" ID="txtDersIsim" Width="400"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Açıklama (2000)</td>
            <td><asp:TextBox runat="server" ID="txtDersAciklama" Width="600" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Button runat="server" ID="btnDersEkle" Text="Ders Ekle" OnClick="DersEkle" /></td>
            <td><asp:Label runat="server" ID="lblDurumDersEkle" CssClass="bilgi"/></td>
        </tr>
    </table>
</asp:Panel>
<h2>Hoca Ekle</h2>
<asp:Panel runat="server" ID="pnlHocaEkle">
    <table> 
        <tr>
            <td>Is_Active</td>
            <td><asp:DropDownList runat="server" ID="drpHocaEkleActive">
                <asp:ListItem Text="Evet" Value="1" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Hayir" Value="0"></asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>İsim (50)</td>
            <td><asp:TextBox runat="server" ID="txtHocaIsim" Width="400"></asp:TextBox><span style="color:Red; display:inline;">*</span></td>
        </tr>
        <tr>
            <td>Ünvan (50)</td>
            <td><asp:TextBox runat="server" ID="txtHocaUnvan" Width="400"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Yorum sayısı</td>
            <td><asp:TextBox runat="server" ID="txtHocaYorumSayisi" Width="60">0</asp:TextBox></td>
        </tr>
        <tr><td colspan="2">Hoca-Okul</td></tr>
    </table>
    <asp:UpdatePanel runat="server" ID="update1">
    <ContentTemplate>
        <table>
            <tr>
                <td><asp:DropDownList runat="server" ID="drpHocaOkullar" runat="server" OnSelectedIndexChanged="HocaEkle_OkulSecildi"
                AutoPostBack="true"></asp:DropDownList>&nbsp;&nbsp;
                <asp:DropDownList runat="server" ID="drpHocaEkle_Bolumler"></asp:DropDownList>
                <asp:TextBox runat="server" ID="txtHocaOkulBaslangicYili" Width="60" ToolTip="Bilmiyosan bos birak"></asp:TextBox>
                &nbsp;-&nbsp;
                <asp:TextBox runat="server" ID="txtHocaOkulBitisYili" Width="60" 
                ToolTip="Bilmiyosan boş bırak, hala devam ediyosa 0 yaz"></asp:TextBox>
                </td>
                <td><asp:Button runat="server" ID="btnHocaOkulEkle" OnClick="HocaOkulEkle" Text="+" /></td>
            </tr>
            <asp:Repeater runat="server" ID="repeaterHocaOkullar" OnItemCommand="RepeaterHocaOkullar_OkulSil">
                <HeaderTemplate></HeaderTemplate>
                <ItemTemplate>
                    <tr><td><%# Container.DataItem%></td>
                    <td><asp:Button runat="server" ID="okulSil" Text="x" /></td></tr>
                </ItemTemplate>
                <FooterTemplate></FooterTemplate>
            </asp:Repeater>    
            <tr><td colspan="2"><asp:Label runat="server" ID="lblHocaOkulEkleDurum" CssClass="bilgi"></asp:Label></td></tr>        
        </table>      
    </ContentTemplate>  
    </asp:UpdatePanel>  
    <div id="Bekleme" class="Bekleme">
        <img src="../Scripts/images/loading.gif" />
    </div>
    <ajax:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" runat="server" TargetControlID="update1">
    <Animations>
        <OnUpdating>
            <StyleAction animationtarget="Bekleme" Attribute="display" value="block" />
        </OnUpdating>
        <OnUpdated>
            <StyleAction animationtarget="Bekleme" Attribute="display" value="none" />
        </OnUpdated>
    </Animations>
    </ajax:UpdatePanelAnimationExtender>                      
    <asp:UpdatePanel runat="server" ID="update2">
    <ContentTemplate>
        <table>
            <tr>
                <td>Hoca-Ders</td>
                <td><asp:Button runat="server" ID="hocaDerslerGuncelle" OnClick="HocaDerslerGuncelle" 
                Text="Dersleri guncelle" ToolTip="Secili okullarda verilen tum dersleri dropdown'a yukler"/></td>
            </tr>
            <tr><td colspan="2">Okulların hepsini eklemeden buraya geçme</td></tr>
            <tr>
                <td><asp:DropDownList runat="server" ID="drpHocaDersler" runat="server"></asp:DropDownList></td>
                <td><asp:Button runat="server" ID="btnHocaDersEkle" OnClick="HocaDersEkle" Text="+" /></td>
            </tr>
            <asp:Repeater runat="server" ID="repeaterHocaDersler" OnItemCommand="RepeaterHocaDersler_DersSil">
                <HeaderTemplate></HeaderTemplate>
                <ItemTemplate>
                    <tr><td><%# Container.DataItem%></td>
                    <td><asp:Button runat="server" ID="dersSil" Text="x" /></td></tr>
                </ItemTemplate>
                <FooterTemplate></FooterTemplate>
            </asp:Repeater>  
            <tr><td colspan="2"><asp:Label runat="server" ID="lblHocaDersEkleDurum" CssClass="bilgi"></asp:Label></td></tr>        
        </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    <div id="Bekleme2" class="Bekleme">
        <img src="../Scripts/images/loading.gif" />
    </div>
    <ajax:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" runat="server" TargetControlID="update2">
    <Animations>
        <OnUpdating>
            <StyleAction animationtarget="Bekleme2" Attribute="display" value="block" />
        </OnUpdating>
        <OnUpdated>
            <StyleAction animationtarget="Bekleme2" Attribute="display" value="none" />
        </OnUpdated>
    </Animations>
    </ajax:UpdatePanelAnimationExtender>     
    <table>
        <tr>
            <td><asp:Button runat="server" ID="btnHocaEkle" Text="Hoca ekle" OnClick="HocaEkle" /></td>
            <td><asp:Label runat="server" ID="lblDurumHocaEkle"></asp:Label></td>
        </tr>    
    </table>
</asp:Panel>
<h2>Ders Dosya Ekle</h2>
<asp:Panel ID="pnlDersDosyaEkle" runat="server">
<asp:UpdatePanel runat="server" ID="update3">
    <ContentTemplate>
        <table>
            <tr>
                <td>Okul</td>
                <td><asp:DropDownList runat="server" ID="drpDosyaOkullar" 
                OnSelectedIndexChanged="drpDosyaOkullar_OkulSecildi" AutoPostBack="true"></asp:DropDownList><span style="color:Red; display:inline;">*</span></td>
            </tr>
            <tr>
                <td>Ders</td>
                <td><asp:DropDownList runat="server" ID="drpDosyaDersler"></asp:DropDownList><span style="color:Red; display:inline;">*</span></td>
            </tr>
            <tr>
                <td>Dosya durum</td>
                <td><asp:DropDownList runat="server" ID="drpDosyaDurum"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>Dosya tipi</td>
                <td><asp:DropDownList runat="server" ID="drpDosyaTipler"></asp:DropDownList><span style="color:Red; display:inline;">*</span></td>
            </tr>
            <tr>
                <td>Dosya isim (256)</td>
                <td><asp:TextBox runat="server" ID="txtDosyaIsim"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Hoca</td>
                <td><asp:DropDownList runat="server" ID="drpDosyaHocalar"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>Açıklama (256)</td>
                <td><asp:TextBox runat="server" ID="txtDosyaAciklama"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2"><asp:Label runat="server" ID="lblDurumDosyaYukle" CssClass="bilgi"></asp:Label></td>
            </tr>
        </table>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnDosyaYukle" />
    </Triggers>
</asp:UpdatePanel>
<div id="Bekleme3" class="Bekleme">
    <img src="./Scripts/images/loading.gif" />
</div>
<ajax:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender3" runat="server" TargetControlID="update3">
<Animations>
    <OnUpdating>
        <StyleAction animationtarget="Bekleme3" Attribute="display" value="block" />
    </OnUpdating>
    <OnUpdated>
        <StyleAction animationtarget="Bekleme3" Attribute="display" value="none" />
    </OnUpdated>
</Animations>
</ajax:UpdatePanelAnimationExtender> 
    <table>
        <tr>
            <td><asp:FileUpload runat="server" ID="fileUpload" EnableViewState="true" /></td>
            <td><asp:Button runat="server" ID="btnDosyaYukle" OnClick="DersDosyaYukle" Text="Yukle" /></td>
        </tr>
    </table>        
</asp:Panel>
</asp:Content>
