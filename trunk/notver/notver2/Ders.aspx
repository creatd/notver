﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ders.aspx.cs" Inherits="Ders" MasterPageFile="~/Masters/Giris.master" 
MaintainScrollPositionOnPostback="true" %>

<%@ Register runat="server" TagPrefix="uc1" TagName="DersYorumlari" Src="~/UserControls/DersYorumlari.ascx" %>
<%@ Register runat="server" TagPrefix="uc1" TagName="DersYorumYap" Src="~/UserControls/DersYorumYap.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="content" ID="tumContent">
<div>
    <table>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblDersIsim"></asp:Label>
                <br />
                <asp:Label runat="server" ID="lblDersOkulIsim"></asp:Label>
                <br />
                <br />
                <asp:Label runat="server" ID="lblDersAciklama"></asp:Label>
                <br />                
                <!-- Dersi veren hocalari link olarak buraya koy -->
            </td>
        </tr>
        <tr>
            <td>
                <uc1:DersYorumlari runat="server" ID="dersYorumlari"></uc1:DersYorumlari>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:DersYorumYap runat="server" ID="dersYorumYap"></uc1:DersYorumYap>
            </td>
        </tr>
    </table>
</div>       
</asp:Content>
