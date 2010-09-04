<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DersDosya.aspx.cs" Inherits="DersDosya" MasterPageFile="~/Masters/DersDosya.master" 
MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/UserControls/DersDosyalar.ascx" TagName="DersDosya" TagPrefix="uc1" %>
<%@ Register TagPrefix="uc1" TagName="Ayrac" Src="~/UserControls/Ayrac.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="content" ID="mainContent">
<uc1:Ayrac runat="server" ID="ayrac" />
<uc1:DersDosya runat="server" ID="ucDersDosya"></uc1:DersDosya>
</asp:Content>