using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class UserControls_LoginBox : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsLoggedIn())   //Page.User.Identity.IsAuthenticated de kullanilabilirdi veya Request.IsAuthenticated
        {
        }
        else
        {
        }
    }

    protected void LogOut(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        LogOutUser();
        Response.Clear();
        RefreshPage();

    }

    protected void LoggedIn(object sender, EventArgs e)
    {
        //LogInUser();
    }

}
