<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AraOkul.ascx.cs" Inherits="UserControls_AraOkul" %>



<div id="OkulSec" style="height:160px; width:265px; background-repeat:no-repeat; padding-left:25px;
    background-image:url('./App_Themes/Default/Images/postit.png'); float:left; display:block;">
    <p style="font-size:24px; color:#1c1c1c; font-weight:bold;
    padding-top:10px;">Okul Seç</p>
    <asp:DropDownList ID="okulIsmi" runat="server" CssClass="select"
    OnSelectedIndexChanged="OkulSecildi" AutoPostBack="true"></asp:DropDownList>
</div>