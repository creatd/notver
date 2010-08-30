using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Text;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : System.Web.UI.Page
{
    public Session session;
	public BasePage()
	{
        if (session == null)
        {
            session = new Session();
        }
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
    public void GoToDefaultPage()
    {
        Response.Redirect( "~\\Default.aspx" , true);
    }

    /// <summary>
    /// Bir hocanin ders vermis bulundugu okullari, okullari link yapmis sekilde tek bir string olarak dondurur
    /// </summary>
    /// <param name="HocaID"></param>
    /// <returns></returns>
    public string HocaOkullariniDondur(string HocaID)
    {
        try
        {
            if (string.IsNullOrEmpty(HocaID))
            {
                return null;
            }
            int hocaID = Convert.ToInt32(HocaID);
            DataTable dt = Hocalar.HocaOkullariniDondur(hocaID);
            if (dt != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("<a href=\"" + Page.ResolveUrl("~/Okul.aspx") + "?OkulID=" + dr["OKUL_ID"] + "\">" + dr["ISIM"] + "</a>");
                }
                string result = sb.ToString().Replace("</a><a", "</a><br /><a");    //Her okul ismi arasina <br /> koy
                return result;
            }
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public string HocaLinkiniDondur(string HocaIsmi, string HocaID)
    {
        return "<a href=\"" + Page.ResolveUrl("~/Hoca.aspx") + "?HocaID=" + HocaID + "\">" + HocaIsmi + "</a>";
    }

    protected string OkulLinkiniDondur(string okulIsim, string okulID)
    {
        return "<a href=\"" + Page.ResolveUrl("~/Okul.aspx") + "?OkulID=" + okulID + "\">" + okulIsim + "</a>";
    }

    protected string DersLinkiniDondur(string dersKod, string dersIsim, string dersID)
    {
        return "<a href='" + Page.ResolveUrl("~/Ders.aspx") + "?DersID=" + dersID + "' tooltip='" + dersIsim + "'\">" + dersKod + "</a>";
    }
}
