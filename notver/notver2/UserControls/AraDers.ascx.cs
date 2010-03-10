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

public partial class UserControls_AraDers : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void DersIsmiGirildi(object sender, EventArgs e)
    {
        buttonAra.Focus();
    }
}
