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

public partial class YorumSikayetEt : BasePage
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
            KontroluSakla();
            int queryYorumID = Query.GetInt("YorumID");
            int queryYorumTipi = Query.GetInt("YorumTipi");
            Enums.YorumTipi yorumTipi = (Enums.YorumTipi)queryYorumTipi;
            if (queryYorumID <= 0)
            {
                pnlHata.Visible = true;
                return;
            }
            if (session.IsLoggedIn && session.KullaniciID >= 0)
            {
                pnlSikayet.Visible = true;
            }
            else
            {
                pnlUyeOl.Visible = true;
            }
        }
        catch (Exception ex)
        {
            KontroluSakla();
            pnlHata.Visible = true;
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
        }
    }

    /// <summary>
    /// Sikayeti iletir
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void SikayetGonder(object sender, EventArgs e)
    {
        int queryYorumID = Query.GetInt("YorumID");
        int queryYorumTipi = Query.GetInt("YorumTipi");
        Enums.YorumTipi yorumTipi = (Enums.YorumTipi)queryYorumTipi;
        if (!Genel.YorumSikayetEt(queryYorumID, yorumTipi, textSikayetNeden.Text, session.KullaniciID))
        {
            ltrDurum.Text = "Şikayet iletirken bir hata oldu, lütfen tekrar deneyin";
        }
        else
        {
            ltrDurum.Text = "Şikayetini incelemeye aldık, ilgine teşekkürler";
            ltrScript.Text = "<script type='text/javascript'>setTimeout('parent.$.fn.colorbox.close()',1500);</script>";
        }
    }

    void KontroluSakla()
    {
        pnlSikayet.Visible = false;
        pnlUyeOl.Visible = false;
        pnlHata.Visible = false;
    }
}
