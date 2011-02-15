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
        //TODO: Gecici olarak kaldirdim
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

        if (!Page.IsPostBack)
        {
            //Ustteki linkleri duzenle
            lnkPanel.Enabled = true;
            lnkIcerikEkle.Enabled = true;
            lnkTumDersler.Enabled = true;
            lnkTumDosyalar.Enabled = true;
            lnkTumHocalar.Enabled = true;
            lnkTumOkullar.Enabled = true;
            lnkTumOkulYorumlar.Enabled = true;
            lnkTumHocaYorumlar.Enabled = true;
            lnkTumDersYorumlar.Enabled = true;
            lnkTumUyeler.Enabled = true;
            string url = Request.Url.AbsolutePath;
            if(url.Contains("TumOkullar.aspx"))
            {
                lnkTumOkullar.Enabled = false;
            }
            else if (url.Contains("TumOkulYorumlar.aspx"))
            {
                lnkTumOkulYorumlar.Enabled = false;
            }
            else if (url.Contains("TumHocalar.aspx"))
            {
                lnkTumHocalar.Enabled = false;
            }
            else if (url.Contains("TumHocaYorumlar.aspx"))
            {
                lnkTumHocaYorumlar.Enabled = false;
            }
            else if (url.Contains("TumDersler.aspx"))
            {
                lnkTumDersler.Enabled = false;
            }
            else if (url.Contains("TumDersYorumlar.aspx"))
            {
                lnkTumDersYorumlar.Enabled = false;
            }
            else if (url.Contains("TumDosyalar.aspx"))
            {
                lnkTumDosyalar.Enabled = false;
            }
            else if (url.Contains("TumUyeler.aspx"))
            {
                lnkTumUyeler.Enabled = false;
            }
            else if (url.Contains("IcerikEkle.aspx"))
            {
                lnkIcerikEkle.Enabled = false;
            }
            else
            {
                lnkPanel.Enabled = false;
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
