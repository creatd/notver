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
            lnkTumMesajlar.Enabled = true;

            lnkPanel.CssClass = "";
            lnkIcerikEkle.CssClass = "";
            lnkTumDersler.CssClass = "";
            lnkTumDosyalar.CssClass = "";
            lnkTumHocalar.CssClass = "";
            lnkTumOkullar.CssClass = "";
            lnkTumOkulYorumlar.CssClass = "";
            lnkTumHocaYorumlar.CssClass = "";
            lnkTumDersYorumlar.CssClass = "";
            lnkTumUyeler.CssClass = "";
            lnkTumMesajlar.CssClass = "";

            string url = Request.Url.AbsolutePath;
            if(url.Contains("TumOkullar.aspx"))
            {
                lnkTumOkullar.Enabled = false;
                lnkTumOkullar.CssClass = "secili";
            }
            else if (url.Contains("TumOkulYorumlar.aspx"))
            {
                lnkTumOkulYorumlar.Enabled = false;
                lnkTumOkulYorumlar.CssClass = "secili";
            }
            else if (url.Contains("TumHocalar.aspx"))
            {
                lnkTumHocalar.Enabled = false;
                lnkTumHocalar.CssClass = "secili";
            }
            else if (url.Contains("TumHocaYorumlar.aspx"))
            {
                lnkTumHocaYorumlar.Enabled = false;
                lnkTumHocaYorumlar.CssClass = "secili";
            }
            else if (url.Contains("TumDersler.aspx"))
            {
                lnkTumDersler.Enabled = false;
                lnkTumDersler.CssClass = "secili";
            }
            else if (url.Contains("TumDersYorumlar.aspx"))
            {
                lnkTumDersYorumlar.Enabled = false;
                lnkTumDersYorumlar.CssClass = "secili";
            }
            else if (url.Contains("TumDosyalar.aspx"))
            {
                lnkTumDosyalar.Enabled = false;
                lnkTumDosyalar.CssClass = "secili";
            }
            else if (url.Contains("TumUyeler.aspx"))
            {
                lnkTumUyeler.Enabled = false;
                lnkTumUyeler.CssClass = "secili";
            }
            else if (url.Contains("TumMesajlar.aspx"))
            {
                lnkTumMesajlar.Enabled = false;
                lnkTumMesajlar.CssClass = "secili";
            }
            else if (url.Contains("IcerikEkle.aspx"))
            {
                lnkIcerikEkle.Enabled = false;
                lnkIcerikEkle.CssClass = "secili";
            }
            else
            {
                lnkPanel.Enabled = false;
                lnkPanel.CssClass = "secili";
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
