<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OkulYorumYap.ascx.cs" Inherits="UserControls_OkulYorumYap" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Panel runat="server" ID="pnlYorumVar">
<asp:HyperLink Text="Yapmis oldugunuz yorumlari goruntulemek veya degistirmek icin tiklayin"
                runat="server" ID="lnkKullaniciYorumlar" ></asp:HyperLink>
</asp:Panel>

<asp:Image ID="baslikPuanYorum" runat="server" ImageUrl="~/App_Themes/Default/Images/postit/diyeceklerim_var.gif" CssClass="fltRight"/>
<asp:Panel ID="pnlPuanYorum" runat="server" Height="300" CssClass="OkulYorumYap">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" />
    <br />
    <br />
    <table border="0" style="height:358px;" border="1" width="600">
        <tr>
            <td style="padding-left:80px;">
                Yorumunuz
            </td>
        </tr>
        <tr>
            <td style="padding-left:90px;">
                <asp:TextBox ID="textYorum" runat="server" MaxLength="2000" TextMode="MultiLine" CssClass="OkulYorumYapTextbox" BackColor="Transparent"
                Width="490" Height="185"/>
            </td>
        </tr>
        <tr>
            <td align="right" style="color: Red; padding-right:20px;">
                <asp:Literal runat="server" ID="ltrDurum"></asp:Literal>
                <asp:Button ID="dugmeYorumGonder" Text="Gunah benden gitti" runat="server" OnClick="YorumKaydet"
                    CssClass="fltRight" BorderStyle="None" BackColor="Transparent"/>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:CollapsiblePanelExtender ID="pnlPuanYorum_CollapsiblePanelExtender" runat="server"
    Enabled="True" TargetControlID="pnlPuanYorum" AutoCollapse="false" AutoExpand="false"
    CollapseControlID="baslikPuanYorum" ExpandControlID="baslikPuanYorum" Collapsed="true"
    ExpandDirection="Vertical">
</asp:CollapsiblePanelExtender>
<asp:Panel ID="pnlUyeOl" runat="server">
    Puan vermek ve/veya yorum yapabilmek icin (sag ust koseden) <a href="#login">giris yapmaniz</a> gereklidir.
    Uyeliginiz yoksa
    <asp:HyperLink ID="HyperLink1" runat="server" Text="uye olmak icin tiklayin!" NavigateUrl="~/Register.aspx"></asp:HyperLink>
</asp:Panel>
