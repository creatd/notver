<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DersYorumGuncelle.aspx.cs" Inherits="DersYorumGuncelle" %>
<%@ Register TagName="DersYorumGuncelle" TagPrefix="uc1" Src="~/UserControls/DersYorumGuncelle.ascx" %>

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
        <p style="text-align:right;">
            <a href="javascript:self.parent.tb_remove();">kapat</a>
        </p>
        <hr />
        <uc1:DersYorumGuncelle ID="dersYorumGuncelle" runat="server"></uc1:DersYorumGuncelle>
    </div>
    </form>
</body>
</html>
