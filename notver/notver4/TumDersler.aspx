<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TumDersler.aspx.cs" Inherits="TumDersler"
    MasterPageFile="~/Masters/Giris.master" MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="uc1" TagName="BolumTumDersler" Src="~/UserControls/BolumTumDersler.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Ayrac" Src="~/UserControls/Ayrac.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="content" ID="content1">
    <uc1:Ayrac runat="server" ID="ayrac" />
    <a name='tepe'></a>
    <div id="divArkaplan" style="background-color:#191919; clear:both; height:36px; margin-top:30px;
                width: 100%;" title="Bölümümün baş harfini seç">
                <!-- ltrHarfDizini'ne kod arkasinda stil veriyorum -->
        <asp:Literal runat="server" ID="ltrHarfDizini"></asp:Literal>
    </div>
    <br />
    <br />
    <p style="margin-left:50px; font-weight:normal; margin-bottom:15px; font-size:13px;"><b>Bölüm seç : </b>
    <asp:DropDownList runat="server" ID="drpOkulBolumler" OnSelectedIndexChanged="BolumSecildi" AutoPostBack="true"></asp:DropDownList>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ya da yukarıdan bölümün baş harfini seç</p>
    <asp:Repeater runat="server" ID="repeaterDersler">
        <HeaderTemplate>
            <table style="font-weight:bold; margin-left:13px;">
        </HeaderTemplate>
        <ItemTemplate>
        <tr id="item" style="margin-bottom:20px; background-color:#f6f6f6; width:960px;">
            <td id="okulBaslik" style="width:200px;display:block; padding-left:20px; padding-top:30px; padding-bottom:30px;">
                <%# BolumBaslikDondur(Eval("ISIM"))%>
            </td>
            <td id="okulDersler" style="width:640px; padding-left:20px; color:#626262; padding-top:30px; padding-bottom:30px;">
                <uc1:BolumTumDersler ID="BolumTumDersler1" runat="server" _BolumID='<%# Eval("BOLUM_ID")%>'>
                </uc1:BolumTumDersler>
            </td>
            <td id="linkBasaDon" style="width:30px;padding-left:20px; vertical-align:top; padding-top:10px;">
                <a href='#tepe'><img src='App_Themes/Default/Images/top.png' title="Basa don"/></a>
            </td>
        </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
                <tr id="item" style="margin-bottom:20px; background-color:#ffffff; width:960px;">
            <td id="okulBaslik" style="width:200px;display:block; padding-left:20px; padding-top:30px; padding-bottom:30px;">
                <%# BolumBaslikDondur(Eval("ISIM"))%>
            </td>

            <td id="okulDersler" style="width:640px; color:#626262; padding-left:20px; padding-top:30px; padding-bottom:30px;">
                <uc1:BolumTumDersler ID="BolumTumDersler2" runat="server" _BolumID='<%# Eval("BOLUM_ID")%>'>
                    </uc1:BolumTumDersler>
            </td>
            <td id="linkBasaDon" style="width:30px;padding-left:20px; vertical-align:top; padding-top:10px;">
                <a href='#tepe'><img src='App_Themes/Default/Images/top.png' title="Basa don"/></a>
            </td>
        </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
