﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HocaYorumYap.aspx.cs" Inherits="HocaYorumYap" %>
<%@ Register TagName="HocaYorumYap" TagPrefix="uc1" Src="~/UserControls/HocaYorumYap.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>NotVer - Hocalarla ogrencilerin rol degistirdigi yer..</title>
    <link href="App_Themes/Default/reset.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/Default/Default.css" rel="stylesheet" type="text/css" />

    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>

    <script src="Scripts/thickbox.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p style="text-align:right;padding:5px 5px 0 0;">
            <a href="javascript:self.parent.tb_remove();">kapat</a>
        </p>
        <hr />
        <uc1:HocaYorumYap ID="hocaYorumYap" runat="server"></uc1:HocaYorumYap>
    </div>
    </form>
</body>
</html>
