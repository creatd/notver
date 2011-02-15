using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class _Default : BasePage
{
    protected void Page_Prerender(object sender, EventArgs e)
    {
        lblTimeout.Visible = false;
        if (!session.IsLoggedIn)
        {
            string timeout = Query.GetString("timeout");
            if (!string.IsNullOrEmpty(timeout) && timeout == "true")
            {
                lblTimeout.Visible = true;
            }
        }
    }
}
