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

using System.Text;

public partial class Hoca : BasePage
{
    protected void Page_Prerender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                int queryHocaID = Query.GetInt("HocaID");
                if (queryHocaID > 0)
                {
                    session.HocaYukle(queryHocaID);
                    //Hoca unvan + isim
                    if (Util.GecerliString(session.HocaIsim))
                    {
                        if (Util.GecerliString(session.HocaUnvan))
                        {
                            hocaIsim.Text = session.HocaUnvan + " " + session.HocaIsim;
                            Page.Title = "NotVer.com - " + session.HocaUnvan + " " + session.HocaIsim;
                        }
                        else
                        {
                            hocaIsim.Text = session.HocaIsim;
                            Page.Title = "NotVer.com - " + session.HocaIsim;
                        }                        
                    }
                    else
                    {
                        hocaIsim.Text = "Hoca bulunamadi";
                    }

                    //Hoca okullar
                    if(session.HocaOkulIsimleri == null || session.HocaOkulIsimleri.Count() <= 0)
                    {
                        hocaOkullar.Text = "<span class=\"HocaOkullar\">(Okul bilgisi bulunamadi!)</span>";
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<span class=\"HocaOkullar\">");
                        for(int i=0; i<session.HocaOkulIsimleri.Count() ; i++)
                        {
                            if(Util.GecerliString(session.HocaOkulIsimleri[i]) && session.HocaOkulBaslangicYillari[i] > 0)
                            {
                                sb.Append(session.HocaOkulIsimleri[i] + "<br />(" + session.HocaOkulBaslangicYillari[i] + " - ");
                                if(session.HocaOkulBitisYillari[i] > 0)
                                {
                                    sb.Append(session.HocaOkulBitisYillari[i]);
                                }
                                else
                                {
                                    sb.Append("...");
                                }
                                sb.Append(") <br /><br />");
                            }
                        }
                        sb.Append("</span>");
                        hocaOkullar.Text = sb.ToString();
                    }                    
                }
            }
                /*
                //s: HocaProfil
                DataTable dtProfil = Hocalar.HocaProfilDondur(Query.GetInt("HocaID"));
                if(dtProfil == null || dtProfil.Rows.Count ==0)    //Hoca bulunamadi ya da hata olustu
                {
                    hocaIsim.Text = "Hoca bulunamadi";                    
                    return;
                }
                if (Util.GecerliString(dtProfil.Rows[0]["HOCA_ISIM"]))
                {
                    session.HocaIsim = dtProfil.Rows[0]["HOCA_ISIM"].ToString();
                    hocaIsim.Text = session.HocaIsim;
                    if (Util.GecerliString(dtProfil.Rows[0]["HOCA_UNVAN"]))
                    {
                        string hocaUnvan = dtProfil.Rows[0]["HOCA_UNVAN"].ToString();
                        if (!string.IsNullOrEmpty(hocaUnvan))
                        {
                            hocaIsim.Text = hocaUnvan + " " + session.HocaIsim;
                        }
                    }    
                }
                
                //Hocanin kayitli oldugu bir okul yok!
                if (!Util.GecerliString(dtProfil.Rows[0]["OKUL_ISIM"]))
                {
                    hocaOkullar.Text = "<span class=\"HocaOkullar\"(Okul bilgisi bulunamadi!)</span>";
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<span class=\"HocaOkullar\">");
                    foreach (DataRow dr in dtProfil.Rows)
                    {
                        sb.Append(dr["OKUL_ISIM"] + "<br />(" + dr["START_YEAR"] + " - ");
                        if (dr["END_YEAR"] != System.DBNull.Value)
                        {
                            sb.Append(dr["END_YEAR"]);
                        }
                        else
                        {
                            sb.Append("...");
                        }
                        sb.Append(") <br /><br />");
                    }
                    sb.Append("</span>");
                    hocaOkullar.Text = sb.ToString();
                }
                //e: HocaProfil

                Page.Title = "NotVer.com - " + session.HocaIsim;    //TODO: calisiyo mu gec mi kaliyo
                 * */
            catch
            {
                GoToDefaultPage();
            }
        }
        pnlUyeOl.Visible = false;
        pnlYorumum.Visible = false;
        if (session.IsLoggedIn)
        {
            pnlYorumum.Visible = true;
            bool yorumVar = false;

            if (Hocalar.KullaniciHocayaYorumYapmis(session.KullaniciID, Query.GetInt("HocaID")))
            {
                yorumVar = true;
            }

            if (yorumVar)
            {
                //Linke basinca guncelleme acilsin
                lnkYorumum.NavigateUrl = Page.ResolveUrl("HocaYorumGuncelle.aspx?HocaID=" + Query.GetInt("HocaID") + "&KeepThis=true&TB_iframe=true&modal=true&height=530&width=640");
            }
            else
            {
                //Linke basinca yeni yorum gonderme acilsin
                lnkYorumum.NavigateUrl = Page.ResolveUrl("HocaYorumYap.aspx?HocaID=" + Query.GetInt("HocaID") + "&KeepThis=true&TB_iframe=true&modal=true&height=530&width=640");
            }
        }
        else
        {
            pnlUyeOl.Visible = true;
        }
    }
}
