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

public partial class Giris : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.IsAuthenticated)
        {
            //Reload page
            ltrJS.Text = "<script type=\"text/javascript\"> top.document.location.href=top.document.location.href; </script>";
        }
    }


    protected void LoggedIn(object sender, EventArgs e)
    {
        /*  Bu calismiyor, o yuzden page_load'da yonlendirme yapiyoruz
        string url = Request.Url.ToString();
        url = url.Substring(0, url.IndexOf("Giris.aspx"));
        ltrJS.Text = "<script type=\"text/javascript\">" + "top.document.location.href='" + url + "'</script>";*/
        RefreshPage();
    }

    //TODO : calismiyo
    protected void Load(object sender, EventArgs a)
    {
        ((sender as Login).FindControl("UserName") as TextBox).Focus();
    }
}
