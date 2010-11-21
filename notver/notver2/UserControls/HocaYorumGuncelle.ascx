<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HocaYorumGuncelle.ascx.cs" Inherits="UserControls_HocaYorumGuncelle" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Panel ID="pnlPuanYorum" runat="server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" />
    <table style="border: none;" width="600">
        <tr>
            <td style="text-align:right;width:350px;">
                <asp:Label ID="Aciklama1" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Rating ID="Puan1" runat="server" EmptyStarCssClass="bosYildiz" FilledStarCssClass="doluYildiz"
                    StarCssClass="doluYildiz" WaitingStarCssClass="bekleYildiz" />
            </td>
        </tr>
        <tr>
            <td style="text-align:right;width:350px;">
                <asp:Label ID="Aciklama2" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Rating ID="Puan2" runat="server" EmptyStarCssClass="bosYildiz" FilledStarCssClass="doluYildiz"
                    StarCssClass="doluYildiz" WaitingStarCssClass="bekleYildiz" />
            </td>
        </tr>
        <tr>
            <td style="text-align:right;width:350px;">
                <asp:Label ID="Aciklama3" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Rating ID="Puan3" runat="server" EmptyStarCssClass="bosYildiz" FilledStarCssClass="doluYildiz"
                    StarCssClass="doluYildiz" WaitingStarCssClass="bekleYildiz" />
            </td>
        </tr>
        <tr>
            <td style="text-align:right;width:350px;">
                <asp:Label ID="Aciklama4" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Rating ID="Puan4" runat="server" EmptyStarCssClass="bosYildiz" FilledStarCssClass="doluYildiz"
                    StarCssClass="doluYildiz" WaitingStarCssClass="bekleYildiz" />
            </td>
        </tr>
        <tr>
            <td style="text-align:right;width:350px;">
                <asp:Label ID="Aciklama5" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Rating ID="Puan5" runat="server" EmptyStarCssClass="bosYildiz" FilledStarCssClass="doluYildiz"
                    StarCssClass="doluYildiz" WaitingStarCssClass="bekleYildiz" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Literal ID="ltrPuanDurum" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="text-align:right;width:350px;">
                Derslerinden aldiginiz not :
                <br />
                <span class="sessiz">(5 uzerinden)</span>
            </td>
            <td style="float:left;">
                <asp:DropDownList ID="dropGenelPuan" runat="server" CssClass="fltRight">
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                </asp:DropDownList>            
            </td>
        </tr>
    </table>
    <table style="border: none;"  width="600">
        <tr>
            <td class="HocaYorumYapSutunSol">
                Yorumunuz
            </td>
            <td class="HocaYorumYapSutunSag">
                <asp:TextBox ID="textYorum" runat="server" MaxLength="2000" TextMode="MultiLine" CssClass="HocaYorumYapTextbox" />
            </td>
        </tr>        
        <tr>
            <td class="HocaYorumYapSutunSol" colspan="2">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                    Hangi ders(ler)e yonelik :
                    <asp:DropDownList ID="dropHocaDersler" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="dropHocaDersler_Secildi">
                    </asp:DropDownList>
                    <asp:Label runat="server" ID="dersIsim"></asp:Label>
                    <asp:TextBox runat="server" ID="txtDersKodDiger" Width="350" ToolTip="Ders kodunu ya da ismini belirtin"></asp:TextBox>
                    <asp:Button runat="server" ID="dropDersEkle" OnClick="dropDersEkle_Click" Text="+"/>
                    <br />
                    <asp:Repeater ID="repeaterDersler" runat="server" OnItemCommand="repeaterDersSil">
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                        <li>
                            <%# Container.DataItem %> &nbsp;&nbsp;
                            <asp:Button runat="server" ID="dersSil" Text="x" />
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
        <tr>
            <td align="right" colspan="2" style="color: Red;">
                <asp:Literal runat="server" ID="ltrDurum"></asp:Literal>
                <asp:Button ID="dugmeYorumGuncelle" Text="Guncelle" runat="server" 
                    CssClass="fltRight" OnClick="PuanYorumGuncelle" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlUyeOl" runat="server">
    Puan vermek ve yorum yapabilmek icin giris yapmaniz gereklidir.
</asp:Panel>
<asp:Literal runat="server" ID="ltrScript"></asp:Literal>