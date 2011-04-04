<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HocaYorumYap.ascx.cs"
    Inherits="UserControls_HocaYorumYap" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<ajax:ToolkitScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />

<script type="text/javascript">
    $(document).ready(function () {
        update_size(70, 110);
    });
    
    function update_size(w_bias,h_bias)  {
        var w = $(document).width() + w_bias;
        var h = $(document).height() + h_bias;
        parent.resize(w,h);
    }
</script>
<!-- Yildizlarin tooltip'leri -->
<script type="text/javascript">
    $(document).ready(function(){
        $('span#hocaYorumYap_Puan1_Star_1').attr('title' , 'Çok kötü');
        $('span#hocaYorumYap_Puan1_Star_2').attr('title' , 'Kötü');
        $('span#hocaYorumYap_Puan1_Star_3').attr('title' , 'İdare eder');
        $('span#hocaYorumYap_Puan1_Star_4').attr('title' , 'İyi');
        $('span#hocaYorumYap_Puan1_Star_5').attr('title' , 'Çok iyi');
        
        $('span#hocaYorumYap_Puan2_Star_1').attr('title' , 'Çok kötü');
        $('span#hocaYorumYap_Puan2_Star_2').attr('title' , 'Kötü');
        $('span#hocaYorumYap_Puan2_Star_3').attr('title' , 'İdare eder');
        $('span#hocaYorumYap_Puan2_Star_4').attr('title' , 'İyi');
        $('span#hocaYorumYap_Puan2_Star_5').attr('title' , 'Çok iyi');

        $('span#hocaYorumYap_Puan3_Star_1').attr('title' , 'Çok kıt');
        $('span#hocaYorumYap_Puan3_Star_2').attr('title' , 'Kıt');
        $('span#hocaYorumYap_Puan3_Star_3').attr('title' , 'İdare eder');
        $('span#hocaYorumYap_Puan3_Star_4').attr('title' , 'Bol');
        $('span#hocaYorumYap_Puan3_Star_5').attr('title' , 'Çok bol');
        
        $('span#hocaYorumYap_Puan4_Star_1').attr('title' , 'Çok yoğun');
        $('span#hocaYorumYap_Puan4_Star_2').attr('title' , 'Yoğun');
        $('span#hocaYorumYap_Puan4_Star_3').attr('title' , 'İdare eder');
        $('span#hocaYorumYap_Puan4_Star_4').attr('title' , 'Seyrek');
        $('span#hocaYorumYap_Puan4_Star_5').attr('title' , 'Çok seyrek');
        
        $('span#hocaYorumYap_Puan5_Star_1').attr('title' , 'Çok kötü');
        $('span#hocaYorumYap_Puan5_Star_2').attr('title' , 'Kötü');
        $('span#hocaYorumYap_Puan5_Star_3').attr('title' , 'İdare eder');
        $('span#hocaYorumYap_Puan5_Star_4').attr('title' , 'İyi');
        $('span#hocaYorumYap_Puan5_Star_5').attr('title' , 'Çok iyi');                        
    });
</script>
<div id="pencere" style="width:100%; height:100%;">
<asp:Panel ID="pnlPuanYorum" runat="server" CssClass="HocaYorumYap">
    <p style="color:#626262; font-size:12px;">Yapmış olduğun tüm yorumları görüntülemek veya 
    değiştirmek için 
    <asp:HyperLink ID="lnkKullaniciYorumlar" runat="server" CssClass="lnkYorumlarim">buraya tıkla</asp:HyperLink></p>
    <br />
    <p>Yorumun</p>
    <p style="margin-bottom:20px;">
        <asp:TextBox runat="server" CssClass="multitextbox" TextMode="MultiLine" MaxLength="2000" 
        ID="textYorum" Width="500" Height="220"></asp:TextBox>
    </p>
    <table style="border: none;" width="450">
        <tr>
            <td style="width:220px; padding:10px 10px 10px 0px;">
                <asp:Label ID="Aciklama1" runat="server"></asp:Label>
            </td>
            <td>
                <ajax:Rating ID="Puan1" runat="server" EmptyStarCssClass="bosYildiz" FilledStarCssClass="doluYildiz"
                    StarCssClass="doluYildiz" WaitingStarCssClass="bekleYildiz" />
            </td>
        </tr>
        <tr>
            <td style="width:220px; padding:10px 10px 10px 0px;">
                <asp:Label ID="Aciklama2" runat="server"></asp:Label>
            </td>
            <td>
                <ajax:Rating ID="Puan2" runat="server" EmptyStarCssClass="bosYildiz" FilledStarCssClass="doluYildiz"
                    StarCssClass="doluYildiz" WaitingStarCssClass="bekleYildiz" />
            </td>
        </tr>
        <tr>
            <td style="width:220px; padding:10px 10px 10px 0px;">
                <asp:Label ID="Aciklama3" runat="server"></asp:Label>
            </td>
            <td>
                <ajax:Rating ID="Puan3" runat="server" EmptyStarCssClass="bosYildiz" FilledStarCssClass="doluYildiz"
                    StarCssClass="doluYildiz" WaitingStarCssClass="bekleYildiz" />
            </td>
        </tr>
        <tr>
            <td style="width:220px; padding:10px 10px 10px 0px;">
                <asp:Label ID="Aciklama4" runat="server"></asp:Label>
            </td>
            <td>
                <ajax:Rating ID="Puan4" runat="server" EmptyStarCssClass="bosYildiz" FilledStarCssClass="doluYildiz"
                    StarCssClass="doluYildiz" WaitingStarCssClass="bekleYildiz" />
            </td>
        </tr>
        <tr>
            <td style="width:220px; padding:10px 10px 10px 0px;">
                <asp:Label ID="Aciklama5" runat="server"></asp:Label>
            </td>
            <td>
                <ajax:Rating ID="Puan5" runat="server" EmptyStarCssClass="bosYildiz" FilledStarCssClass="doluYildiz"
                    StarCssClass="doluYildiz" WaitingStarCssClass="bekleYildiz" />
            </td>
        </tr>
        <tr>
            <td style="width:220px; padding:20px 10px 20px 0px;">
                Derslerinden aldığın not:
            </td>
            <td>
                <asp:DropDownList ID="dropGenelPuan" runat="server">
                </asp:DropDownList>            
            </td>
        </tr>       
        <tr>
            <td class="HocaYorumYapSutunSol" colspan="2" style="width:220px; padding:10px 10px 10px 0px;">
                <asp:UpdatePanel runat="server" ID="pnlUpdate">
                    <ContentTemplate>
                    Hangi ders(ler)e yönelik: 
                    <asp:DropDownList ID="dropHocaDersler" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="dropHocaDersler_Secildi">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Label runat="server" ID="dersIsim"></asp:Label>
                    <asp:TextBox runat="server" ID="txtDersKodDiger" CssClass="textbox" ToolTip="Ders kodunu ya da ismini belirtin"></asp:TextBox>
                    <asp:ImageButton runat="server" ID="dropDersEkle" OnClick="dropDersEkle_Click" 
                    OnClientClick="javascript:update_size(55,65);" ImageUrl="~/App_Themes/Default/Images/ekle.png"/>
                    <br />
                    <asp:Repeater ID="repeaterDersler" runat="server" OnItemCommand="repeaterDersSil">
                        <HeaderTemplate>
                            <ul style="color:#afafaf;">
                        </HeaderTemplate>
                        <ItemTemplate>
                        <li style="padding-top:5px; padding-bottom:5px;">
                            <%# Container.DataItem %> &nbsp;&nbsp;
                            <asp:ImageButton runat="server" ID="dersSil" ImageUrl="~/App_Themes/Default/Images/cikar.png" 
                            OnClientClick="javascript:update_size(50,25);" />
                        </li>
                        </ItemTemplate>                        
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div id="Bekleme">
                    <img src="./Scripts/images/loading.gif" />
                </div>
                <ajax:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" runat="server" TargetControlID="pnlUpdate">
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
        </tr>
    </table>
    
    <p style="padding-top:20px;">
        <asp:ImageButton runat="server" ID="dugmeYorumGonder" ImageUrl="~/App_Themes/Default/Images/gonder.png" 
        OnClick="PuanYorumKaydet"/>
        <asp:ImageButton runat="server" ID="dugmeYorumGuncelle" ImageUrl="~/App_Themes/Default/Images/gonder.png" 
        OnClick="PuanYorumGuncelle"/>
    </p>
    <p class="durum">
        <asp:Literal runat="server" ID="ltrDurum"></asp:Literal>
    </p>
    <asp:HiddenField runat="server" ID="hocaYorumID" />
</asp:Panel>
<asp:Panel ID="pnlUyeOl" runat="server" CssClass="bilgi">
    <br/><br/>
    Yorum yapabilmek için giriş yapmalısın.
    <br/><br/>
    Üyeliğin yoksa ana sayfada sağ üstten hemen ücretsiz üye olabilirsin.
</asp:Panel>
<asp:Panel ID="pnlHata" runat="server" CssClass="durum">
Bir hata oluştu :(
</asp:Panel>
<asp:Literal runat="server" ID="ltrScript"></asp:Literal>
</div>
