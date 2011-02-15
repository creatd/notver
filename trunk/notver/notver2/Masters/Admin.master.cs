using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class Masters_Admin : System.Web.UI.MasterPage
{
    Session session;
    protected void Page_Prerender(object sender, EventArgs e)
    {
        if (session == null)
        {
            session = new Session();
        }
        if (!session.IsLoggedIn)
        {
            GoToLoginPage_WithRedirect();
        }
        else
        {
            if (session.KullaniciUyelikRol != Enums.UyelikRol.Admin && session.KullaniciUyelikRol != Enums.UyelikRol.Moderator)
            {
                GoToLoginPage();
            }
        }
    }

    public void GoToLoginPage_WithRedirect()
    {
        Response.Redirect("~/Giris.aspx?yonlendir=" + Request.Url.AbsolutePath + Request.Url.Query.Replace("?","!").Replace("&",","), true);
    }

    public void GoToLoginPage()
    {
        Response.Redirect("~/Giris.aspx", true);
    }
}
