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

public partial class UserControls_OkulYorumYap : BaseUserControl
{
    protected void Page_Prerender(object sender, EventArgs e)
    {
        try
        {
            KontroluSakla();
            if (Query.GetInt("OkulID") <= 0)
            {
                pnlHata.Visible = true;
                return;
            }
            if (session.IsLoggedIn && session.KullaniciID >= 0)
            {
                pnlPuanYorum.Visible = true;
                lnkKullaniciYorumlar.NavigateUrl = "javascript:parent.document.location='" + OkulYorumlarimURLDondur(Query.GetInt("OkulID")) + "';";
                bool yorumVar = Okullar.KullaniciOkulaYorumYapmis(session.KullaniciID, Query.GetInt("OkulID"));

                if (yorumVar)
                {
                    string eskiYorum = Okullar.KullaniciOkulYorumunuDondur(session.KullaniciID, Query.GetInt("OkulID"));
                    if (Util.GecerliString(eskiYorum))
                    {
                        textYorum.Text = Util.DBToHTML(eskiYorum);
                    }
                    dugmeYorumGuncelle.Visible = true;
                }
                else
                {
                    dugmeYorumGonder.Visible = true;
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
        }
    }

    /// <summary>
    /// Yorumu kaydeder
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void YorumKaydet(object sender, EventArgs e)
    {
        if (!Okullar.OkulYorumKaydet(session.KullaniciID, Query.GetInt("OkulID"), textYorum.Text, session.KullaniciOnayPuani))
        {
            ltrDurum.Text = "Yorum kaydederken bir hata olustu. Lutfen tekrar deneyiniz.";
        }
        else
        {
            ltrDurum.Text = "Yorumunuz basariyla kaydedildi!";
            ltrScript.Text = "<script type='text/javascript'>setTimeout('self.parent.tb_remove()',1500);</script>";
        }
    }

    protected void YorumGuncelle(object sender, EventArgs e)
    {
        if (!Okullar.OkulYorumGuncelle(session.KullaniciID, Query.GetInt("OkulID"), textYorum.Text))
        {
            ltrDurum.Text = "Yorum guncellerken bir hata olustu, lutfen tekrar deneyin";
        }
        else
        {
            ltrDurum.Text = "Yorumunuz guncellendi!";
            ltrScript.Text = "<script type='text/javascript'>setTimeout('parent.$.fn.colorbox.close()',1500);</script>";
        }
    }

    void KontroluSakla()
    {
        dugmeYorumGonder.Visible = false;
        dugmeYorumGuncelle.Visible = false;
        pnlPuanYorum.Visible = false;
        pnlUyeOl.Visible = false;
        pnlHata.Visible = false;
    }
}
