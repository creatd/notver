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
        try
        {
            if (!Page.IsPostBack)
            {
                int queryHocaID = Query.GetInt("HocaID");
                if (queryHocaID > 0)
                {
                    session.HocaYukle(queryHocaID);
                    //Hoca unvan + isim
                    if (!string.IsNullOrEmpty(session.HocaIsim))
                    {
                        if (!string.IsNullOrEmpty(session.HocaUnvan))
                        {
                            hocaIsim.Text = session.HocaUnvan + " " + session.HocaIsim + " / ";
                            Page.Title = "NotVer.com - " + session.HocaUnvan + " " + session.HocaIsim;
                        }
                        else
                        {
                            hocaIsim.Text = session.HocaIsim;
                            Page.Title = "NotVer.com - " + session.HocaIsim + " / ";
                        }
                    }
                    else
                    {
                        hocaIsim.Text = "Hoca bulunamadi";
                    }

                    //Hoca okullar
                    if (session.HocaOkulIsimleri == null || session.HocaOkulIsimleri.Count() <= 0)
                    {
                        hocaOkullar.Text = "<span class=\"HocaOkullar\">(Okul bilgisi bulunamadi!)</span>";
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<span class=\"HocaOkullar\">");
                        for (int i = 0; i < session.HocaOkulIsimleri.Count(); i++)
                        {
                            if (!string.IsNullOrEmpty(session.HocaOkulIsimleri[i]) && session.HocaOkulBaslangicYillari[i] > 0)
                            {
                                sb.Append(session.HocaOkulIsimleri[i] + " ( " + session.HocaOkulBaslangicYillari[i] + " - ");
                                if (session.HocaOkulBitisYillari[i] > 0)
                                {
                                    sb.Append(session.HocaOkulBitisYillari[i]);
                                }
                                else
                                {
                                    sb.Append("...");
                                }
                                sb.Append(" )<br/>");
                            }
                        }
                        sb.Append("</span>");
                        hocaOkullar.Text = sb.ToString();
                    }
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
                    lnkYorumum.NavigateUrl = Page.ResolveUrl("HocaYorumGuncelle.aspx?HocaID=" + Query.GetInt("HocaID") + "&KeepThis=true&TB_iframe=true&modal=false&height=530&width=640");
                }
                else
                {
                    //Linke basinca yeni yorum gonderme acilsin
                    lnkYorumum.NavigateUrl = Page.ResolveUrl("HocaYorumYap.aspx?HocaID=" + Query.GetInt("HocaID") + "&KeepThis=true&TB_iframe=true&modal=false&height=530&width=640");
                }
            }
            else
            {
                pnlUyeOl.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
            GoToDefaultPage();
        }
    }

}
