<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test2.aspx.cs" Inherits="Test2" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
  <style type="text/css">
    .emptypng { background-image: url(empty.png); width: 32px; height: 32px; }
    .smileypng { background-image: url(smiley.png); width: 32px; height: 32px; }
    .donesmileypng { background-image: url(smiley-done.png); width: 32px; height: 32px; }
  </style>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server"  />
    <asp:Rating ID="Rating1" runat="server" EmptyStarCssClass="emptypng" FilledStarCssClass="smileypng" 
        StarCssClass="smileypng" WaitingStarCssClass="donesmileypng" />
    </form>
</body>
</html>
