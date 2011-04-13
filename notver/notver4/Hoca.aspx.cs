using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


using System.Text;

public partial class Hoca : BasePage
{
    protected void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        if (ex != null)
        {
            if (session != null)
            {
                Mesajlar.AdmineHataMesajiGonder(((System.Web.UI.Page)(sender)).Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
            }
            else
            {
                Mesajlar.AdmineHataMesajiGonder(((System.Web.UI.Page)(sender)).Request.Url.ToString(), ex.Message, -1, Enums.SistemHataSeviyesi.Orta);
            }
        }
    }


    protected void Page_Prerender(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                int queryHocaID = Query.GetInt("HocaID");
                if (queryHocaID >= 0)
                {
                    session.HocaYukle(queryHocaID);
                    //Hoca unvan + isim
                    if (!string.IsNullOrEmpty(session.HocaIsim))
                    {
                        if (!string.IsNullOrEmpty(session.HocaUnvan))
                        {
                            hocaIsim.Text = session.HocaUnvan + " " + session.HocaIsim + " / ";
                            Page.Title = "NotVerin - " + session.HocaUnvan + " " + session.HocaIsim;
                        }
                        else
                        {
                            hocaIsim.Text = session.HocaIsim + " / ";
                            Page.Title = "NotVerin - " + session.HocaIsim + " / ";
                        }
                    }
                    else
                    {
                        hocaIsim.Text = "Hoca bulunamadı";
                    }

                    //Hoca okullar
                    if (session.HocaOkulIsimleri == null || session.HocaOkulIsimleri.Length <= 0)
                    {
                        hocaOkullar.Text = "<span class=\"HocaOkullar\">(Okul bilgisi bulunamadı!)</span>";
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<span class=\"HocaOkullar\">");
                        for (int i = 0; i < session.HocaOkulIsimleri.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(session.HocaOkulIsimleri[i]))
                            {
                                sb.Append(session.HocaOkulIsimleri[i]);
                            }
                            if(session.HocaOkulBaslangicYillari[i] > 0)
                            {
                                sb.Append(" ( " + session.HocaOkulBaslangicYillari[i] + " - ");
                                if (session.HocaOkulBitisYillari[i] > 0)
                                {
                                    sb.Append(session.HocaOkulBitisYillari[i]);
                                }
                                else
                                {
                                    sb.Append("...");
                                }
                                sb.Append(" )");
                            }
                            sb.Append("<br/>");
                        }
                        sb.Append("</span>");
                        hocaOkullar.Text = sb.ToString();
                    }
                    int yorumID = Hocalar.KullaniciHocayaYorumYapmis(session.KullaniciID, Query.GetInt("HocaID"));

                    if (yorumID >= 0)
                    {
                        ltrYorumYazi.Text = "Yorum güncelle&nbsp;&nbsp;";
                    }
                    else
                    {
                        ltrYorumYazi.Text = "Yorum ekle&nbsp;&nbsp;";
                    }
                    lnkYorumum.NavigateUrl = Page.ResolveUrl("~/HocaYorumYap.aspx?HocaID=" + queryHocaID);
                }                
            }

        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
            GoToDefaultPage();
        }
    }

}
