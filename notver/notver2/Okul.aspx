<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Okul.aspx.cs" Inherits="Okul" MasterPageFile="~/Masters/Okul.master" %>

<%@ Register TagName="OkulYorumlari" TagPrefix="uc1" Src="~/UserControls/OkulYorumlari.ascx" %>
<%@ Register TagName="OkulYorumYap" TagPrefix="uc1" Src="~/UserControls/OkulYorumYap.ascx" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="content" runat="server">
<div>
    <table>
        <tr>
            <td style="width:350px;">
                <asp:Label runat="server" ID="lblOkulIsim"></asp:Label>
                <br />
                Sehir : <asp:Label runat="server" ID="lblOkulAdres"></asp:Label>
                <br />
                Kurulus Tarihi : <asp:Label runat="server" ID="lblOkulKurulusTarihi"></asp:Label>
                <br />
                Ogrenci sayisi : <asp:Label runat="server" ID="lblOgrenciSayisi"></asp:Label>
                <br />
                Akademik personel sayisi : <asp:Label runat="server" ID="lblAkademikPersonelSayisi"></asp:Label>
                <br />
                Web adresi : <asp:HyperLink runat="server" ID="hpOkulWeb"></asp:HyperLink>
                <br />
            </td>
            <td>
                <asp:Image runat="server" ID="imgOkul" Width="500" Height="250" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <uc1:OkulYorumlari runat="server" ID="okulYorumlari"></uc1:OkulYorumlari>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <uc1:OkulYorumYap runat="server" ID="okulYorumYap"></uc1:OkulYorumYap>
            </td>
        </tr>
    </table>
</div>                        
</asp:Content>