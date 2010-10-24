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
    protected void Page_PreRender(object sender, EventArgs e)
    {
        KontroluSakla();
        pnlPuanYorum.Visible = true;
        if (Query.GetInt("OkulID") <= 0)
        {
            return;
        }
        if (session.IsLoggedIn && session.KullaniciID > 0)
        {
            bool yorumVar = false;

            if (!Okullar.KullaniciOkulaYorumYapmis(session.KullaniciID, Query.GetInt("OkulID")))
            {
                dugmeYorumGonder.Visible = true;
            }
            else
            {
                yorumVar = true;
                //Kullanicinin daha once yaptigi yorumu yukle
                string eskiYorum = Okullar.KullaniciOkulYorumunuDondur(session.KullaniciID, Query.GetInt("OkulID"));
                textYorum.Text = eskiYorum;
            }

            if (yorumVar)
            {
                baslikPuanYorum.Visible = false;
                lnkKullaniciYorumlar.Visible = true;
                lnkKullaniciYorumlar.NavigateUrl = OkulYorumlarimURLDondur(Query.GetInt("OkulID"));
            }
            else
            {
                baslikPuanYorum.Visible = true;
                dugmeYorumGonder.Visible = true;
                lnkKullaniciYorumlar.Visible = false;
            }            
        }
        else
        {
            pnlUyeOl.Visible = true;
        }
    }

    /// <summary>
    /// Yorumu kaydeder
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void YorumKaydet(object sender, EventArgs e)
    {
        if (!Okullar.OkulYorumKaydet(session.KullaniciID, Query.GetInt("OkulID"), textYorum.Text))
        {
            ltrDurum.Text = "Yorum kaydederken bir hata olustu. Lutfen tekrar deneyiniz.";
        }
        else
        {
            ltrDurum.Text = "Yorumunuz basariyla kaydedildi!";
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
        }
    }

    void KontroluSakla()
    {
        pnlPuanYorum.Visible = false;
        pnlUyeOl.Visible = false;
        baslikPuanYorum.Visible = false;
        dugmeYorumGonder.Visible = false;
    }

}
