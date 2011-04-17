<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MesajOku.aspx.cs" Inherits="Admin_MesajOku" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mesaj oku</title>
    
    <link href="../App_Themes/Default/reset.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Default/Admin.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/colorbox.css" rel="stylesheet" type="text/css" />

    <link href="../App_Themes/Default/Default2.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel runat="server" ID="pnlMesaj">
    <div style="padding:10px;">
        <table>
            <tr style="background-color:Gray;" align="center">
                <td style="padding:10px;">
                    <asp:Label runat="server" ID="lblBaslik"></asp:Label>
                </td>
            </tr>
            <tr align="right">
                <td style="font-weight:normal;">
                    gönderen ID : <strong><asp:Label runat="server" ID="lblGonderen"></asp:Label></strong>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    tarih : <strong><asp:Label runat="server" ID="lblGonderilmeTarihi"></asp:Label></strong>
                </td>
            </tr>
            <tr style="border:solid 1pt Gray;">
                <td style="background-color:White; padding:20px;">
                    <asp:Label runat="server" ID="lblMesaj"></asp:Label>
                </td>
            </tr>
            <tr align="right">
                <td>
                    <asp:Button runat="server" ID="btnSil" Text="Sil" OnClick="MesajSil" />
                </td>
            </tr>
            <tr align="right">
                <td>
                    <asp:Label runat="server" ID="lblDurum" CssClass="bilgi"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlGirisYap">
        Giriş yapmalısın
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlHata" CssClass="hata">
        Hata oldu :(
    </asp:Panel>
    </form>
</body>
</html>
