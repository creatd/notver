<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hoca.aspx.cs" Inherits="Hoca" MasterPageFile="~/Masters/Hoca.master" 
 MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/UserControls/HocaPuanlari.ascx" TagName="HocaPuanlari" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/HocaResmi.ascx" TagName="HocaResmi" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/HocaYorumlari.ascx" TagName="HocaYorumlari" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/HocaYorumYap.ascx" TagName="HocaYorumYap" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/HocaAyniOkul.ascx" TagName="HocaAyniOkul" TagPrefix="uc1" %>


<asp:Content ContentPlaceHolderID="content" runat="server">

<table>
<tr>
<td>
    <uc1:HocaAyniOkul runat="server" ID="hocaAyniOkul"></uc1:HocaAyniOkul>
</td>
<td style="padding-top:0px;">
    <table>
        <tr>
            <td style="padding-top:0px;">
            <div style="float:left;">
                <uc1:HocaPuanlari runat="server" ID="HocaPuanlari1" />
            </div>
            <div style="vertical-align:top; text-align:right; width:150px; padding-left:20px; padding-right:5px;padding-top:0px;float:left;width:235px;height:100%;">
                <uc1:HocaResmi runat="server" ID="HocaResmi1" />
                <asp:Label ID="hocaIsim" runat="server" CssClass="HocaIsim"></asp:Label>
                <asp:Literal ID="hocaOkullar" runat="server"></asp:Literal>
            </div>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:HocaYorumlari runat="server" ID="HocaYorumlari1" />
            </td>
        </tr>
        <tr>
            <td>
                <uc1:HocaYorumYap runat="server" ID="HocaYorumYap1" />
            </td>
        </tr>
    </table>
</td>
</tr>
</table>






</asp:Content>
