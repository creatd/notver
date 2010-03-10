<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Okul.aspx.cs" Inherits="Okul" MasterPageFile="~/Masters/Okul.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
  <style type="text/css">
    .emptypng { background-image: url(empty.png); width: 32px; height: 32px; }
    .smileypng { background-image: url(smiley.png); width: 32px; height: 32px; }
    .donesmileypng { background-image: url(smiley-done.png); width: 32px; height: 32px; }
  </style>
</asp:Content>

<asp:Content ContentPlaceHolderID="content" runat="server">

okul
                                    <asp:ScriptManager ID="ScriptManager1" runat="server" />

                        <cc1:Editor ID="Editor1" runat="server" />
                            <asp:Rating ID="Rating1" runat="server" EmptyStarCssClass="emptypng" FilledStarCssClass="smileypng" 
        StarCssClass="smileypng" WaitingStarCssClass="donesmileypng">
    </asp:Rating>

                        
</asp:Content>