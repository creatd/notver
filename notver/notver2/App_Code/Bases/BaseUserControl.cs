using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Data.SqlClient;

/// <summary>
/// Summary description for BaseUserControl
/// </summary>
public class BaseUserControl : System.Web.UI.UserControl
{
   
    public static SqlConnection connection;

    public Session session;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (session == null)
        {
            session = new Session();
        }
    }

    /*public BaseUserControl()
    {
        if (session == null)
        {
            session = new Session();
        }
    }*/

    /// <summary>
    /// ID'si verilen okulun sayfasina gider
    /// </summary>
    /// <param name="okulID"></param>
    public void OkulaGit(string okulID)
    {
        Response.Redirect(Page.ResolveUrl("~/Okul.aspx") + "?OkulID=" + okulID , true);
    }

    /// <summary>
    /// Refreshes current page
    /// </summary>
    public void RefreshPage()
    {
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }

    /// <summary>
    /// Redirect to default.aspx
    /// </summary>
    public void GoToDefaultPage(string queryString)
    {
        if (!string.IsNullOrEmpty(queryString))
        {
            Response.Redirect("~\\Default.aspx?" + queryString, true);
        }
        else
        {
            Response.Redirect("~\\Default.aspx", true);
        }
    }

    public string DersURLDondur(object DersID)
    {
        if (Util.GecerliString(DersID))
        {
            return Page.ResolveUrl("~/Ders.aspx?DersID=" + DersID.ToString());
        }
        else
        {
            return "";
        }
    }

    public string DersYorumlarimURLDondur(object DersID)
    {
        if (Util.GecerliString(DersID))
        {
            return Page.ResolveUrl("~/Yorumlarim.aspx?DersID=" + DersID.ToString());
        }
        else
        {
            return "";
        }
    }

    public string OkulURLDondur(object OkulID)
    {
        if (Util.GecerliString(OkulID))
        {
            return Page.ResolveUrl("~/Okul.aspx?OkulID=" + OkulID.ToString());
        }
        else
        {
            return "";
        }
    }

    public string OkulYorumlarimURLDondur(object OkulID)
    {
        if (Util.GecerliString(OkulID))
        {
            return Page.ResolveUrl("~/Yorumlarim.aspx?OkulID=" + OkulID.ToString());
        }
        else
        {
            return "";
        }
    }

    public string HocaURLDondur(object HocaID)
    {
        if (Util.GecerliString(HocaID))
        {
            return Page.ResolveUrl("~/Hoca.aspx?HocaID=" + HocaID.ToString());
        }
        else
        {
            return "";
        }
    }

    public string HocaYorumlarimURLDondur(object HocaID)
    {
        if (Util.GecerliString(HocaID))
        {
            return Page.ResolveUrl("~/Yorumlarim.aspx?HocaID=" + HocaID.ToString());
        }
        else
        {
            return "";
        }
    }

    public string HocaLinkiniDondur(string HocaIsmi, string HocaID)
    {
        return "<a href=\"" + Page.ResolveUrl("~/Hoca.aspx") + "?HocaID=" + HocaID + "\">" + HocaIsmi + "</a>";
    }
}
