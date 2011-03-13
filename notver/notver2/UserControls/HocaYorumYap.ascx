<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HocaYorumYap.ascx.cs"
    Inherits="UserControls_HocaYorumYap" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:ToolkitScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />

<asp:Panel ID="pnlPuanYorum" runat="server" CssClass="HocaYorumYap">
    <p style="color:#626262; font-size:12px;">Yapmış olduğun tüm yorumları görüntülemek veya 
    değiştirmek için 
    <asp:HyperLink ID="lnkKullaniciYorumlar" runat="server" CssClass="lnkYorumlarim">tiklayin</asp:HyperLink></p>
    <br />
    <p>Yorumunuz</p>
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
                <asp:Rating ID="Puan1" runat="server" EmptyStarCssClass="bosYildiz" FilledStarCssClass="doluYildiz"
                    StarCssClass="doluYildiz" WaitingStarCssClass="bekleYildiz" />
            </td>
        </tr>
        <tr>
            <td style="width:220px; padding:10px 10px 10px 0px;">
                <asp:Label ID="Aciklama2" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Rating ID="Puan2" runat="server" EmptyStarCssClass="bosYildiz" FilledStarCssClass="doluYildiz"
                    StarCssClass="doluYildiz" WaitingStarCssClass="bekleYildiz" />
            </td>
        </tr>
        <tr>
            <td style="width:220px; padding:10px 10px 10px 0px;">
                <asp:Label ID="Aciklama3" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Rating ID="Puan3" runat="server" EmptyStarCssClass="bosYildiz" FilledStarCssClass="doluYildiz"
                    StarCssClass="doluYildiz" WaitingStarCssClass="bekleYildiz" />
            </td>
        </tr>
        <tr>
            <td style="width:220px; padding:10px 10px 10px 0px;">
                <asp:Label ID="Aciklama4" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Rating ID="Puan4" runat="server" EmptyStarCssClass="bosYildiz" FilledStarCssClass="doluYildiz"
                    StarCssClass="doluYildiz" WaitingStarCssClass="bekleYildiz" />
            </td>
        </tr>
        <tr>
            <td style="width:220px; padding:10px 10px 10px 0px;">
                <asp:Label ID="Aciklama5" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Rating ID="Puan5" runat="server" EmptyStarCssClass="bosYildiz" FilledStarCssClass="doluYildiz"
                    StarCssClass="doluYildiz" WaitingStarCssClass="bekleYildiz" />
            </td>
        </tr>
        <tr>
            <td style="width:220px; padding:20px 10px 20px 0px;">
                Derslerinden aldığınız not:
            </td>
            <td>
                <asp:DropDownList ID="dropGenelPuan" runat="server">
                </asp:DropDownList>            
            </td>
        </tr>       
        <tr>
            <td class="HocaYorumYapSutunSol" colspan="2" style="width:220px; padding:10px 10px 10px 0px;">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                    Hangi ders(ler)e yönelik : 
                    <asp:DropDownList ID="dropHocaDersler" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="dropHocaDersler_Secildi">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Label runat="server" ID="dersIsim"></asp:Label>
                    <asp:TextBox runat="server" ID="txtDersKodDiger" CssClass="textbox" ToolTip="Ders kodunu ya da ismini belirtin"></asp:TextBox>
                    <asp:ImageButton runat="server" ID="dropDersEkle" OnClick="dropDersEkle_Click" ImageUrl="~/App_Themes/Default/Images/ekle.png"/>
                    <br />
                    <asp:Repeater ID="repeaterDersler" runat="server" OnItemCommand="repeaterDersSil">
                        <HeaderTemplate>
                            <ul style="color:#afafaf;">
                        </HeaderTemplate>
                        <ItemTemplate>
                        <li style="padding-top:5px; padding-bottom:5px;">
                            <%# Container.DataItem %> &nbsp;&nbsp;
                            <asp:ImageButton runat="server" ID="dersSil" ImageUrl="~/App_Themes/Default/Images/cikar.png" />
                        </li>
                        </ItemTemplate>                        
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                    </ContentTemplate>
                </asp:UpdatePanel>
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
