using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using Amazon.S3.Model;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using Amazon.S3;

public partial class Admin_Default : BasePage
{
    protected void Page_Prerender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

        }
    }

    protected void Refresh()
    {
        Response.Redirect(Request.Url.PathAndQuery);
    }

}
