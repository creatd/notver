<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hoca.aspx.cs" Inherits="Hoca" MasterPageFile="~/Masters/Hoca.master" 
 MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/UserControls/HocaPuanlari.ascx" TagName="HocaPuanlari" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/HocaResmi.ascx" TagName="HocaResmi" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/HocaYorumlari.ascx" TagName="HocaYorumlari" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/HocaYorumYap.ascx" TagName="HocaYorumYap" TagPrefix="uc1" %>


<asp:Content ContentPlaceHolderID="content" runat="server">

<table>
<tr>
<td>
    Bu unideki diger hocalar
</td>
<td>
    <table>
        <tr>
            <td>
                <uc1:HocaPuanlari runat="server" ID="HocaPuanlari1" />
            </td>        
            <td>
                <uc1:HocaResmi runat="server" ID="HocaResmi1" />
                <asp:Label ID="hocaIsim" runat="server"></asp:Label>
                <br />
                <asp:Literal ID="hocaOkullar" runat="server"></asp:Literal>
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
