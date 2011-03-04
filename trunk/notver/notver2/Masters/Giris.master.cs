using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Masters_Giris : BaseMaster
{
    protected void Page_Prerender(object sender, EventArgs e)
    {
        if (session.IsLoggedIn)
        {
            pnl_login.Visible = true;
            pnl_noLogin.Visible = false;
            pnlTimeout.Visible = false;
        }
        else
        {
            pnl_login.Visible = false;
            pnl_noLogin.Visible = true;
            pnlTimeout.Visible = false;
            if (Context.Session != null && Context.Session.IsNewSession)
            {
                string cookie = Request.Headers["Cookie"];
                if (!string.IsNullOrEmpty(cookie) && cookie.IndexOf("ASP.NET_SessionId") >= 0)
                {
                    pnlTimeout.Visible = true;
                    pnl_noLogin.Visible = false;
                }
            }
        }
        if (Request.Url.AbsolutePath.Contains("Yorumlarim.aspx"))
        {
            lnkHesabim.Enabled = false;
        }
        else
        {
            lnkHesabim.Enabled = true;
        }

    }
}
