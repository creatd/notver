<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ders.aspx.cs" Inherits="Ders" MasterPageFile="~/Masters/Giris.master" 
MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="uc1" TagName="DersYorumlari" Src="~/UserControls/DersYorumlari.ascx" %>
<%@ Register TagPrefix="uc1" TagName="DersYorumYap" Src="~/UserControls/DersYorumYap.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Ayrac" Src="~/UserControls/Ayrac.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="content" ID="tumContent">
<uc1:Ayrac runat="server" ID="ayrac" />
<div>
    <table>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblDersIsim"></asp:Label>
                <br />
                <asp:Label runat="server" ID="lblDersOkulIsim"></asp:Label>
            </td>
            <td>
                <asp:HyperLink runat="server" ID="lnkDersDosyalar">Dersle ilgili bircok dosya icin tiklayin</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label runat="server" ID="lblDersAciklama"></asp:Label>
                <br />                
                <!-- Dersi veren hocalari link olarak buraya koy -->
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <uc1:DersYorumlari runat="server" ID="dersYorumlari"></uc1:DersYorumlari>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <uc1:DersYorumYap runat="server" ID="dersYorumYap"></uc1:DersYorumYap>
            </td>
        </tr>
    </table>
</div>       
</asp:Content>
