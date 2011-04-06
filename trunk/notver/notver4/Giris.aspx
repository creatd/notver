<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Giris.aspx.cs" Inherits="Giris" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Notverin - Giris yap ya da uye ol</title>
    <link href="../App_Themes/Default/reset.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Default/Default.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Default/thickbox.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
    function DurumTemizle() {
        document.getElementById("<%= lblDurum1.ClientID %>").innerHTML  = "";
    }
    </script>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <table border="0" style="width:250px;">
                <tr><td>E-posta adresi</td></tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtEposta" runat="server" onchange="DurumTemizle();"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtEposta"
                            ErrorMessage="E-posta adresi girmeniz gereklidir" ToolTip="E-posta adresi girmeniz gereklidir"
                            ValidationGroup="vg1">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr><td><h1>Giris yapin</h1></td></tr>
                <tr><td>Sifre</td></tr>
                <tr>                    
                    <td><asp:TextBox ID="txtSifreGiris" runat="server" TextMode="Password"></asp:TextBox></td>
                    <td><asp:LinkButton runat="server" ID="btnGirisYap" OnClick="GirisYap">Giris Yap</asp:LinkButton></td>
                </tr>        
                <tr>
                    <td style="color: Red;padding-left:50px;">
                        <asp:Label ID="lblDurum1" runat="server" EnableViewState="False"></asp:Label>
                    </td>
                </tr>                
            </table>
    </div>
    </form>
</body>
</html>
