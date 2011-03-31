<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AraHoca.ascx.cs" Inherits="UserControls_AraHoca" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<script type="text/javascript">
function SetFocusHoca(e)
{
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

    if(keycode == 13)
    {
        var cmd = <%= Page.GetPostBackEventReference(buttonHocaAra) %>;
    }
}

function Temizle_AraHoca(obj)   {
    if(obj.value.indexOf('girin') != -1) {
        obj.value='';
    }
}

function Yukle_AraHoca(obj) {
    if(obj.value == '')   {
        obj.value = 'Hoca ismini girin';
    }
    else {
        document.getElementById(' <%= buttonHocaAra.ClientID %>').focus();
    }
}

</script>

<div id="HocaAra" style="height:160px; width:265px; background-repeat:no-repeat; padding-left:25px;
    margin-right:25px;
    background-image:url('./App_Themes/Default/Images/postit.png'); float:left; display:block;">
    <p style="font-size:24px; color:#1c1c1c; font-weight:bold;
    padding-top:10px;">Hoca Ara</p>
    <asp:TextBox ID="hocaIsmi" runat="server" OnKeyDown="javascript:SetFocusHoca(event);" 
        onclick="javascript:Temizle_AraHoca(this);" onblur="javascript:Yukle_AraHoca(this);" BorderWidth="0"
        CssClass="araTextbox">Hoca ismini girin</asp:TextBox>
    <asp:LinkButton ID="buttonHocaAra" runat="server" Text="" OnClick="Ara" BorderWidth="0"
    CssClass="araTus"/>
    <asp:HyperLink runat="server" ID="lnkTumHocalar" NavigateUrl="~/TumHocalar.aspx"
        CssClass="araLink">
        <span style="width:10px;	height:30px;	float:left;	clear:left;	
            background-image:url('App_Themes/Default/Images/buton_sol.png');"></span>
            
        <span style="height:22px;	padding-top:8px;	padding-right:10px; font-size:14px;
	float:left;	background-image:url('App_Themes/Default/Images/buton_orta.png');">Tüm hocaları göster</span>
        
        <span style="width:30px;	height:30px;	float:left;	
            background-image:url('App_Themes/Default/Images/buton_sag_uzun.png');"></span>
    </asp:HyperLink>   
</div>