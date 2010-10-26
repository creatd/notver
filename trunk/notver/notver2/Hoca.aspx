<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hoca.aspx.cs" Inherits="Hoca"
    MasterPageFile="~/Masters/Hoca.master" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/UserControls/HocaPuanlari.ascx" TagName="HocaPuanlari" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/HocaResmi.ascx" TagName="HocaResmi" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/HocaYorumlari.ascx" TagName="HocaYorumlari" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/HocaYorumYap.ascx" TagName="HocaYorumYap" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/HocaAyniOkul.ascx" TagName="HocaAyniOkul" TagPrefix="uc1" %>
<%@ Register TagPrefix="uc1" TagName="Ayrac" Src="~/UserControls/Ayrac.ascx" %>
<asp:Content ContentPlaceHolderID="content" runat="server">
    <uc1:Ayrac runat="server" ID="ayrac" />
    <br />
    <table style="background:url('App_Themes/Default/Images/defter/15.png') scroll -20px 0% White; width:710px; margin:auto;">
        <tr>
            <td style="padding-top: 0px;">
                <div style="float: left;">
                    <uc1:HocaPuanlari runat="server" ID="HocaPuanlari1" />
                </div>
                <div style="vertical-align: top; text-align: right; padding-left: 20px;
                    padding-right: 5px; padding-top: 0px; float: left; width: 215px; height: 100%;">
                    <uc1:HocaResmi runat="server" ID="HocaResmi1" />
                    <asp:Label ID="hocaIsim" runat="server" CssClass="HocaIsim"></asp:Label>
                    <asp:Literal ID="hocaOkullar" runat="server"></asp:Literal>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <div id="Defter">
        <table style="width:100%;">
            <tr>
                <td style="vertical-align: top; padding-top: 350px;">
                    <uc1:HocaAyniOkul runat="server" ID="hocaAyniOkul"></uc1:HocaAyniOkul>
                </td>
                <td style="padding-top: 0px; vertical-align: top;">
                    <table style="width:100%;">
                        <tr>
                            <td>
                                <uc1:HocaYorumlari runat="server" ID="HocaYorumlari1" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <asp:Panel ID="pnlYorumum" runat="server">
        <table style="width:100%; text-align:right;">
            <tr>
                <td>
                    <asp:HyperLink runat="server" ID="lnkYorumum" Text="Benim de diyeceklerim var! " CssClass="thickbox"><img src="App_Themes/Default/Images/diger/el_kaldir.png" /></asp:HyperLink>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlUyeOl" runat="server">
        Puan vermek ve yorum yapabilmek icin (sag ust koseden) <a href="#login">giris yapmaniz</a> gereklidir.
        Uyeliginiz yoksa
        <asp:HyperLink ID="HyperLink1" runat="server" Text="uye olmak icin tiklayin!" NavigateUrl="~/Register.aspx"></asp:HyperLink>
    </asp:Panel>    
</asp:Content>
