<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TumDersler.aspx.cs" Inherits="TumDersler"
    MasterPageFile="~/Masters/Ders.master" %>

<%@ Register TagPrefix="uc1" TagName="OkulTumDersler" Src="~/UserControls/OkulTumDersler.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Ayrac" Src="~/UserControls/Ayrac.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="content" ID="content1">
    <uc1:Ayrac runat="server" ID="ayrac" />
    <a name='tepe'></a>
    <br />
    <div id="divArkaplan" style="background: url('./App_Themes/Default/Images/defter/11.jpg') repeat scroll 0 0 Transparent;
                width: 100%;">
                <!-- ltrHarfDizini'ne kod arkasinda stil veriyorum -->
        <asp:Literal runat="server" ID="ltrHarfDizini"></asp:Literal>
    </div>
    <br />
    <br />
    <asp:Repeater runat="server" ID="repeaterOkullar">
        <HeaderTemplate>
            <table style="background: url('./App_Themes/Default/Images/defter/11.jpg') repeat scroll 0 0 Transparent;
                width: 100%; border: none 0px Transparent;">
        </HeaderTemplate>
        <ItemTemplate>
            <tr style="padding-bottom:10px;">
                <td style="padding-bottom:10px; text-decoration:underline;">
                    <h2><%# OkulLinkBaslikDondur(DataBinder.Eval(Container.DataItem, "ISIM"), DataBinder.Eval(Container.DataItem, "OKUL_ID"))%></h2>
                </td>
                <td style="text-align:right;">
                    <a href='#tepe'>Basa don</a>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding-left:20px;">    
                    <uc1:OkulTumDersler runat="server" _OkulID='<%# DataBinder.Eval(Container.DataItem, "OKUL_ID")%>'>
                    </uc1:OkulTumDersler>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
