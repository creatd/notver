﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OkulYorumGuncelle.aspx.cs" Inherits="OkulYorumGuncelle" %>

<%@ Register TagName="OkulYorumGuncelle" TagPrefix="uc1" Src="~/UserControls/OkulYorumGuncelle.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>NotVer - Hocalarla ogrencilerin rol degistirdigi yer..</title>
    <link href="App_Themes/Default/reset.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/Default/Default.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/Default/Default2.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="background-color:#f6f6f6;">
        <uc1:OkulYorumGuncelle ID="okulYorumGuncelle" runat="server"></uc1:OkulYorumGuncelle>
    </div>
    </form>
</body>
</html>