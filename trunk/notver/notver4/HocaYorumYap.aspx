<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HocaYorumYap.aspx.cs" Inherits="HocaYorumYap"
 MaintainScrollPositionOnPostback="true" %>
<%@ Register TagName="HocaYorumYap" TagPrefix="uc1" Src="~/UserControls/HocaYorumYap.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=ISO-8859-9" />
    <title>NotVerin - Hocalarla öğrenciler yer değiştiriyor</title>
    <link href="App_Themes/Default/reset.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/Default/Default.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/Default/Default2.css" rel="stylesheet" type="text/css" />
    
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="Scripts/colorbox.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>        
        <uc1:HocaYorumYap ID="hocaYorumYap" runat="server"></uc1:HocaYorumYap>
    </div>
    </form>
</body>
</html>
