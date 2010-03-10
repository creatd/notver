<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Giris.aspx.cs" Inherits="Giris" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Giris Yapin</title>
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript">
    function kutuKapat() {
            self.parent.tb_remove();
        }
    /* TODO : calismiyo */
    $(document).ready(function() {
        $('#Login1_UserName').focus();
    });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
    <div>
       <asp:Login ID="Login1" runat="server" OnLoggedIn="LoggedIn" OnLoad="Load">
            <LayoutTemplate>
                <table border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0">
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User 
                                Name:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                            ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="ctl00$ctl03">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                            ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="ctl00$ctl03">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="color: Red;">
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HyperLink ID="SifremiUnuttum" runat="server" Text="Sifremi unuttum" NavigateUrl="~/Register.aspx"></asp:HyperLink>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="ctl00$ctl03" />
                                    </td>
                                </tr>
                                <tr>
                                <td colspan="2" style="text-align:right;">
                                    <a href="javascript:kutuKapat();">Pencereyi kapat</a>                                
                                </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
        </asp:Login>
        
    </div>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>    
    </form>
    <asp:Literal runat="server" ID="ltrJS"></asp:Literal>

</body>
</html>
