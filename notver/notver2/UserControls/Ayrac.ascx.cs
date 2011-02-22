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
    static string ayrac = "<span style='color:#05d4b4'>&nbsp;>&nbsp;&nbsp;</span>";
    static string sonSeviye_baslangic = "<span style='color:#FEE41D;'>";
    static string sonSeviye_bitis = "</span>";
    protected void Page_Prerender(object sender, EventArgs e)
    {
        try
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
                    /*lnkSeviye1.NavigateUrl = Page.ResolveUrl("~/TumDersler.aspx");
                    lnkSeviye1.Text = "Tum dersler";
                    lnkSeviye1.Visible = true;
                    if (Query.GetInt("OkulID") > 0 && Util.GecerliString(session.OkulIsim))
                    {
                        //lnkSeviye2.NavigateUrl = OkulURLDondur(Query.Get("OkulID"));
                        lnkSeviye2.Text = ayrac + session.OkulIsim + "'ndeki dersler";
                        lnkSeviye2.Enabled = false;
                        lnkSeviye2.Visible = true;
                    }*/
                    pnlAyrac.Visible = false;
                }
                else if (url.Contains("TumHocalar.aspx"))
                {
                    /*lnkSeviye1.NavigateUrl = Page.ResolveUrl("~/TumHocalar.aspx");
                    lnkSeviye1.Text = "Tum hocalar";
                    lnkSeviye1.Visible = true;
                    if (Query.GetInt("OkulID") > 0 && Util.GecerliString(session.OkulIsim))
                    {
                        //lnkSeviye2.NavigateUrl = OkulURLDondur(Query.Get("OkulID"));
                        lnkSeviye2.Text = ayrac + session.OkulIsim + "'ndeki hocalar";
                        lnkSeviye2.Enabled = false;
                        lnkSeviye2.Visible = true;
                    }*/
                    pnlAyrac.Visible = false;
                }
                else if (url.Contains("TumOkullar.aspx"))
                {
                    pnlAyrac.Visible = false;
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
                        if (Query.GetInt("DersID") > 0 && Util.GecerliString(session.DersKod))
                        {
                            //lnkSeviye3.NavigateUrl = DersURLDondur(Query.Get("DersID"));
                            lnkSeviye3.Text = ayrac + sonSeviye_baslangic + session.DersKod + sonSeviye_bitis;
                            lnkSeviye3.Enabled = false;
                            lnkSeviye3.Visible = true;
                        }
                        else
                        {
                            lnkSeviye2.Text = sonSeviye_baslangic + lnkSeviye2.Text + sonSeviye_bitis;
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
                        lnkSeviye2.Text = ayrac + sonSeviye_baslangic + session.HocaIsim + sonSeviye_bitis;
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
                        if (Query.GetInt("DersID") > 0)
                        {
                            lnkSeviye3.NavigateUrl = DersURLDondur(Query.GetInt("DersID"));
                            DataTable dtDers = Dersler.DersProfilDondur(Query.GetInt("DersID"));
                            if (dtDers != null && Util.GecerliString(session.DersKod))
                            {
                                lnkSeviye3.Text = ayrac + session.DersKod;
                                lnkSeviye3.Enabled = true;
                                lnkSeviye3.Visible = true;

                                lnkSeviye4.Text = ayrac + sonSeviye_baslangic + session.DersKod + " dosyalari" + sonSeviye_bitis;
                                lnkSeviye4.Enabled = false;
                                lnkSeviye4.Visible = true;
                            }
                        }
                        else
                        {
                            lnkSeviye2.Text = sonSeviye_baslangic + lnkSeviye2.Text + sonSeviye_bitis;
                        }
                    }
                }
                else
                {
                    pnlAyrac.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
        }
    }
}
