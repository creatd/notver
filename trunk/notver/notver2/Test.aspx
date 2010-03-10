
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
  void Page_Load()
  {
    if (Page.IsPostBack)
    {
      Label1.Text = "Your rating: " + r1.CurrentRating;
    }
  }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Control Toolkit</title>
  <style type="text/css">
    .emptypng { background-image: url(empty.png); width: 32px; height: 32px; }
    .smileypng { background-image: url(smiley.png); width: 32px; height: 32px; }
    .donesmileypng { background-image: url(smiley-done.png); width: 32px; height: 32px; }
  </style>
</head>
<body>
  <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="asm" runat="server" />
    <div>
      <asp:Label ID="Label1" runat="server" />
      <asp:Rating ID="r1" runat="server"
        CurrentRating="0" MaxRating="5"
        EmptyStarCssClass="emptypng" FilledStarCssClass="smileypng" 
        StarCssClass="smileypng" WaitingStarCssClass="donesmileypng" />
      <input type="submit" id="Submit1" runat="server" value="Rate!" />
      
    </div>
    <asp:Panel ID="Panel1" runat="server">
        <table>
        <tr>
        <td>sutun1
        </td>
        <td>
        sutun2
        </td>
        </tr>
        </table>
    </asp:Panel>
    <asp:Label ID="collapselabel" runat="server">label</asp:Label>
    <asp:CollapsiblePanelExtender ID="Panel1_CollapsiblePanelExtender" 
        runat="server" Enabled="True" TargetControlID="Panel1" AutoExpand="false" 
        AutoCollapse="false" CollapsedText="Benim de diyeceklerim var" ExpandedText="Vazgectim"
        ExpandDirection="Vertical" CollapsedSize="10" ExpandedSize="300" TextLabelID="collapselabel"
        CollapseControlID="collapselabel" ExpandControlID="collapselabel">
    </asp:CollapsiblePanelExtender>
  </form>
</body>
</html>
