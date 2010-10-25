<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Okul.aspx.cs" Inherits="Okul" MasterPageFile="~/Masters/Okul.master" %>

<%@ Register TagName="OkulYorumlari" TagPrefix="uc1" Src="~/UserControls/OkulYorumlari.ascx" %>
<%@ Register TagName="OkulYorumYap" TagPrefix="uc1" Src="~/UserControls/OkulYorumYap.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Ayrac" Src="~/UserControls/Ayrac.ascx" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="content" runat="server">
<uc1:Ayrac runat="server" ID="ayrac" />
<div>
    <table style="width:100%;">
        <tr>
            <td>
                <div id="OkulGenelBilgi" style="width:350px; background: url(App_Themes/Default/Images/defter/10.jpg) repeat;
                     padding:10px; vertical-align:top;">
                <asp:Label runat="server" ID="lblOkulIsim" CssClass="bold"></asp:Label>
                <br /><br />
                <u>Sehir :</u>&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblOkulAdres"></asp:Label>
                <br />
                <u>Kurulus tarihi :</u>&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblOkulKurulusTarihi"></asp:Label>
                <br />
                <u>Ogrenci sayisi :</u>&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblOgrenciSayisi"></asp:Label>
                <br />
                <u>Akademik personel sayisi :</u>&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblAkademikPersonelSayisi"></asp:Label>
                <br />
                <u>Web adresi :</u>&nbsp;&nbsp;&nbsp;<asp:HyperLink runat="server" ID="hpOkulWeb" Target="_blank"></asp:HyperLink>
                <br />
                </div>
            </td>
            <td style="padding-left:20px;">
                <div id="OkulResim" style="text-align:right;">
                    <img src="./App_Themes/Default/Images/Diger/bant.png" style="position:relative; top:25px; left:-100px; width:25px;" />
                    <br />
                    <asp:Image runat="server" ID="imgOkul" Height="250" />
                </div>
            </td>    
        </tr>
        <tr><td colspan="2"><br /></td></tr>
        <tr>
            <td colspan="2">
                <uc1:OkulYorumlari runat="server" ID="okulYorumlari"></uc1:OkulYorumlari>
            </td>
        </tr>
        <tr><td colspan="2"><br /></td></tr>
        <tr>
            <td colspan="2">
                <uc1:OkulYorumYap runat="server" ID="okulYorumYap"></uc1:OkulYorumYap>
            </td>
        </tr>
    </table>
</div>                        
</asp:Content>