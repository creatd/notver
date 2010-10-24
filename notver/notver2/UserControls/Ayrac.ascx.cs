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

public partial class UserControls_Ayrac : BaseUserControl
{
    static string ayrac = "&nbsp;/&nbsp;&nbsp;";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string url = Page.Request.Url.ToString();
            pnlAyrac.Visible = true;
            lnkSeviye1.Visible = false;
            lnkSeviye2.Visible = false;
            lnkSeviye3.Visible = false;
            lnkSeviye4.Visible = false;
            if (url.Contains("TumDersler.aspx"))
            {
                lnkSeviye1.NavigateUrl = Page.ResolveUrl("~/TumDersler.aspx");
                lnkSeviye1.Text = "Tum dersler";
                lnkSeviye1.Visible = true;
                if (Query.GetInt("OkulID") > 0 && Util.GecerliString(session.OkulIsim))
                {
                    //lnkSeviye2.NavigateUrl = OkulURLDondur(Query.Get("OkulID"));
                    lnkSeviye2.Text = ayrac + session.OkulIsim + "'ndeki dersler";
                    lnkSeviye2.Enabled = false;
                    lnkSeviye2.Visible = true;
                }
            }
            else if (url.Contains("TumHocalar.aspx"))
            {
                lnkSeviye1.NavigateUrl = Page.ResolveUrl("~/TumHocalar.aspx");
                lnkSeviye1.Text = "Tum hocalar";
                lnkSeviye1.Visible = true;
                if (Query.GetInt("OkulID") > 0 && Util.GecerliString(session.OkulIsim))
                {
                    //lnkSeviye2.NavigateUrl = OkulURLDondur(Query.Get("OkulID"));
                    lnkSeviye2.Text = ayrac + session.OkulIsim + "'ndeki hocalar";
                    lnkSeviye2.Enabled = false;
                    lnkSeviye2.Visible = true;
                }
            }
            else if (url.Contains("Ders.aspx"))
            {
                lnkSeviye1.NavigateUrl = Page.ResolveUrl("~/TumDersler.aspx");
                lnkSeviye1.Text = "Tum dersler";
                lnkSeviye1.Visible = true;
                if (Util.GecerliString(session.DersOkulIsim) && session.DersOkulID > 0)
                {
                    lnkSeviye2.NavigateUrl = Page.ResolveUrl("~/TumDersler.aspx?OkulID=" + session.DersOkulID);
                    lnkSeviye2.Text = ayrac + session.DersOkulIsim + "'ndeki dersler";
                    lnkSeviye2.Visible = true;
                    lnkSeviye2.Enabled = true;
                    if(Query.GetInt("DersID") > 0 && Util.GecerliString(session.DersKod))
                    {
                        //lnkSeviye3.NavigateUrl = DersURLDondur(Query.Get("DersID"));
                        lnkSeviye3.Text = ayrac + session.DersKod;
                        lnkSeviye3.Enabled = false;
                        lnkSeviye3.Visible = true;
                    }
                }
            }
            else if (url.Contains("Hoca.aspx"))
            {
                lnkSeviye1.NavigateUrl = Page.ResolveUrl("~/TumHocalar.aspx");
                lnkSeviye1.Text = "Tum hocalar";
                lnkSeviye1.Visible = true;
                if (Query.GetInt("HocaID") > 0 && Util.GecerliString(session.HocaIsim))
                {
                    //lnkSeviye2.NavigateUrl = HocaURLDondur(Query.Get("HocaID"));
                    lnkSeviye2.Text = ayrac + session.HocaIsim;
                    lnkSeviye2.Enabled = false;
                    lnkSeviye2.Visible = true;
                }
            }
            else if (url.Contains("DersDosya.aspx"))
            {
                lnkSeviye1.NavigateUrl = Page.ResolveUrl("~/TumDersler.aspx");
                lnkSeviye1.Text = "Tum dersler";
                lnkSeviye1.Visible = true;
                if (Util.GecerliString(session.DersOkulIsim) && session.DersOkulID > 0)
                {
                    lnkSeviye2.NavigateUrl = Page.ResolveUrl("~/TumDersler.aspx?OkulID=" + session.DersOkulID);
                    lnkSeviye2.Text = ayrac + session.DersOkulIsim + "'ndeki dersler";
                    lnkSeviye2.Visible = true;
                    lnkSeviye2.Enabled = true;
                    if (Query.GetInt("DersID") > 0 && Util.GecerliString(session.DersKod))
                    {
                        lnkSeviye3.NavigateUrl = DersURLDondur(Query.GetInt("DersID"));
                        lnkSeviye3.Text = ayrac + session.DersKod;
                        lnkSeviye3.Enabled = true;
                        lnkSeviye3.Visible = true;

                        lnkSeviye4.Text = ayrac + session.DersKod + " dosyalari";
                        lnkSeviye4.Enabled = false;
                        lnkSeviye4.Visible = true;
                    }
                }
            }
            else
            {
                pnlAyrac.Visible = false;
            }


        }
    }
}
