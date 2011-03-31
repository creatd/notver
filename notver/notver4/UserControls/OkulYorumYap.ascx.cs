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


public partial class UserControls_OkulYorumYap : BaseUserControl
{
    protected void Page_Prerender(object sender, EventArgs e)
    {
        try
        {
            KontroluSakla();
            int queryOkulID = Query.GetInt("OkulID");
            if (queryOkulID <= 0)
            {
                pnlHata.Visible = true;
                return;
            }
            if (session.IsLoggedIn && session.KullaniciID >= 0)
            {
                pnlPuanYorum.Visible = true;
                lnkKullaniciYorumlar.NavigateUrl = "javascript:parent.document.location='" + OkulYorumlarimURLDondur(queryOkulID) + "';";
                bool yorumVar = Okullar.KullaniciOkulaYorumYapmis(session.KullaniciID, queryOkulID);

                if (yorumVar)
                {
                    string eskiYorum = Okullar.KullaniciOkulYorumunuDondur(session.KullaniciID, queryOkulID);
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
            KontroluSakla();
            pnlHata.Visible = true;
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
        if (string.IsNullOrEmpty(textYorum.Text))
        {
            ltrDurum.Text = "Yorum girmeyi unuttun";
            return;
        }
        if (!Okullar.OkulYorumKaydet(session.KullaniciID, Query.GetInt("OkulID"), textYorum.Text, session.KullaniciOnayPuani))
        {
            ltrDurum.Text = "Yorum kaydederken bir hata oluştu, lütfen tekrar deneyin.";
        }
        else
        {
            ltrDurum.Text = "Yorumun başarıyla kaydedildi!";
            ltrScript.Text = "<script type='text/javascript'>setTimeout('parent.$.fn.colorbox.close()',1500);</script>";
        }
    }

    protected void YorumGuncelle(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(textYorum.Text))
        {
            ltrDurum.Text = "Yorum girmeyi unuttun";
            return;
        }
        if (!Okullar.OkulYorumGuncelle(session.KullaniciID, Query.GetInt("OkulID"), textYorum.Text, session.KullaniciOnayPuani))
        {
            ltrDurum.Text = "Yorum güncellerken bir hata oluştu, lütfen tekrar deneyin";
        }
        else
        {
            ltrDurum.Text = "Yorumun guncellendi!";
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
