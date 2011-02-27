<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hoca.aspx.cs" Inherits="Hoca"
    MasterPageFile="~/Masters/Giris.master" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/UserControls/HocaPuanlari.ascx" TagName="HocaPuanlari" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/HocaResmi.ascx" TagName="HocaResmi" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/HocaYorumlari.ascx" TagName="HocaYorumlari" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/HocaYorumYap.ascx" TagName="HocaYorumYap" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/HocaAyniOkul.ascx" TagName="HocaAyniOkul" TagPrefix="uc1" %>
<%@ Register TagPrefix="uc1" TagName="Ayrac" Src="~/UserControls/Ayrac.ascx" %>

<asp:Content ContentPlaceHolderID="content" runat="server">
    <uc1:Ayrac runat="server" ID="ayrac" />
    <div id="hocaUst" style="display:block; width:100%; margin-top:30px;">
        <table>
            <tr style="background-color:#f6f6f6;">
                <td style="width:679px; border-right:solid 1pt #afafaf;">
                    <p style="color:#191919; font-weight:bold; padding:30px; padding-bottom:40px;">
                        <asp:Label ID="hocaIsim" runat="server" CssClass="HocaIsim"></asp:Label>
                        <asp:Literal ID="hocaOkullar" runat="server"></asp:Literal>
                    </p>
                </td>
                <td style="width:280px;">
                   <p style="color:#191919; font-weight:bold; padding:30px; padding-bottom:40px;">
                        Cok Yorumlananlar
                    </p> 
                </td>
            </tr>
            <tr style="background-color:#ffffff;">
                <td style="width:679px; border-right:solid 1pt #afafaf; color:#191919;">
                    <uc1:HocaPuanlari runat="server" ID="HocaPuanlari1" />
                </td>
                <td style="width:280px;">
                    <uc1:HocaAyniOkul runat="server" ID="hocaAyniOkul"></uc1:HocaAyniOkul>
                </td>
            </tr>
        </table>
    </div>    
    
    <div id="hocaYorumlari" style="display:block; width:100%; margin-top:20px;">
        <p style="background-color:#f6f6f6; color:#191919; font-weight:bold; padding:30px; padding-bottom:40px;">
            Yorumlar
            <span style="color:#626262;"><asp:HyperLink runat="server" ID="lnkYorumum" CssClass="colorbox"
            NavigateUrl="~/HocaYorumYap.aspx">Yorum ekle &nbsp;&nbsp;  
            <img src="App_Themes/Default/Images/ekle.png" /></asp:HyperLink></span>
        </p>
        <uc1:HocaYorumlari runat="server" ID="HocaYorumlari1" />
    </div>    

    <asp:Panel ID="pnlYorumum" runat="server">

    </asp:Panel>
    <asp:Panel ID="pnlUyeOl" runat="server">
        Puan vermek ve yorum yapabilmek icin (sag ust koseden) <a href="#login">giris yapmaniz</a> gereklidir.
        Uyeliginiz yoksa
        <asp:HyperLink ID="HyperLink1" runat="server" Text="uye olmak icin tiklayin!" NavigateUrl="~/Register.aspx"></asp:HyperLink>
    </asp:Panel>    
</asp:Content>
