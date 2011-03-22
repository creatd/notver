<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AraDers.ascx.cs" Inherits="UserControls_AraDers" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<script type="text/javascript">
function SetFocusDers(e) {
    var keycode;
    if(window.event)
    {
        keycode = window.event.keyCode;
    }
    else if(e.which)
    {
        keycode = e.which;
    }
    else if(e.keyCode)
    {
        keycode = e.keyCode;
    }

    if (keycode == 13) {   //Enter'a basti      
        var cmd = <%= Page.GetPostBackEventReference(buttonDersAra) %>;
    }
}

function Temizle_AraDers(obj) {
    if(obj.value.indexOf('girin') != -1) {
        obj.value='';
    }
}

function Yukle_AraDers(obj) {
    if(obj.value == '')   {
        obj.value = 'İsmini ya da kodunu girin';
    }
    else {
        document.getElementById(' <%= buttonDersAra.ClientID %>').focus();
    }
}

</script>

<div id="DersAra" style="height:160px; width:265px; background-repeat:no-repeat; padding-left:25px; 
    margin-right:25px; margin-left:15px; 
    background-image:url('./App_Themes/Default/Images/postit.png'); float:left; display:block;">
    <p style="font-size:24px; color:#1c1c1c; font-weight:bold;
    padding-top:10px;">Ders Ara</p>
    <asp:TextBox ID="dersIsmi" runat="server" OnKeyDown="javascript:SetFocusDers(event);" 
        onclick="javascript:Temizle_AraDers(this);" onblur="javascript:Yukle_AraDers(this);" BorderWidth="0"
        CssClass="araTextbox">İsmini ya da kodunu girin</asp:TextBox>
    <asp:LinkButton ID="buttonDersAra" runat="server" Text="" OnClick="Ara" BorderWidth="0"
    CssClass="araTus"/>
    <asp:HyperLink runat="server" ID="lnkTumDersler" NavigateUrl="~/TumDersler.aspx"
        CssClass="araLink">
        <span style="width:10px;
	height:30px;	float:left;	clear:left;	background-image:url('App_Themes/Default/Images/buton_sol.png');"></span>
        <span style="height:22px;	padding-top:8px;	padding-right:10px; font-size:14px;
	float:left;	background-image:url('App_Themes/Default/Images/buton_orta.png');">Tüm dersleri göster</span>
        <span style="width:30px;
	height:30px;	float:left;	background-image:url('App_Themes/Default/Images/buton_sag_uzun.png');"></span>
    </asp:HyperLink>   
</div>
