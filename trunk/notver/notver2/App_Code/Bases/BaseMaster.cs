using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


/// <summary>
/// Summary description for BaseMaster
/// </summary>
public class BaseMaster : System.Web.UI.MasterPage
{
    protected Session session;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (session == null)
        {
            session = new Session();
        }
    }

    protected void CikisYap(object sender, EventArgs e)
    {
        Uyelik.CikisYap();
        RefreshPage();
    }

    /// <summary>
    /// Refreshes current page
    /// </summary>
    public void RefreshPage()
    {
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }
}
