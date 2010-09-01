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

public partial class UserControls_DersYorumYap : BaseUserControl
{
    protected void Page_PreRender(object sender, EventArgs e)
    {
        KontroluSakla();
        pnlPuanYorum.Visible = true;
        if (session.DersID <= 0)
        {
            return;
        }
        if (session.IsLoggedIn && session.KullaniciID > 0)
        {
            baslikPuanYorum.Visible = true;

            //Dersi veren hocalari doldur
            DataTable dtDersiVerenHocalar = Dersler.DersiVerenHocalariDondur(session.DersID);
            drpDersHocalar.Items.Add(new ListItem("-", "-1"));
            if (dtDersiVerenHocalar != null && dtDersiVerenHocalar.Rows.Count > 0)
            {
                foreach (DataRow dr in dtDersiVerenHocalar.Rows)
                {
                    drpDersHocalar.Items.Add(new ListItem(dr["HOCA_ISIM"].ToString(), dr["HOCA_ID"].ToString()));
                }
            }
            else
            {
                //TODO: Admin'e haber ver
            }

            bool yorumVar = false;

            if (!Dersler.KullaniciDerseYorumYapmis(session.KullaniciID, session.DersID))
            {
                dugmeYorumGonder.Visible = true;
            }
            else
            {
                yorumVar = true;
                //Kullanicinin daha once yaptigi yorumu yukle
                DataTable dtEskiYorum = Dersler.KullaniciDersYorumunuDondur(session.KullaniciID, session.DersID);
                if (dtEskiYorum != null && dtEskiYorum.Rows.Count > 0)
                {
                    if(Util.GecerliString(dtEskiYorum.Rows[0]["YORUM"]))
                    {
                        textYorum.Text = dtEskiYorum.Rows[0]["YORUM"].ToString();
                    }
                    //HocaID'yi sec
                    if(Util.GecerliString(dtEskiYorum.Rows[0]["HOCA_ID"]))
                    {
                        drpDersHocalar.SelectedValue = dtEskiYorum.Rows[0]["HOCA_ID"].ToString();
                    }
                }
                
            }

            if (yorumVar)
            {
                baslikPuanYorum.Text = "Yorumumu degistirecegim";
                dugmeYorumGuncelle.Visible = true;
            }
            else
            {
                baslikPuanYorum.Text = "Benim de diyeceklerim var";
                dugmeYorumGonder.Visible = true;
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
        if (!Dersler.DersYorumKaydet(session.KullaniciID, session.DersID, textYorum.Text, Convert.ToInt32(drpDersHocalar.SelectedValue)))
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
        if (!Dersler.DersYorumGuncelle(session.KullaniciID, session.DersID, textYorum.Text, Convert.ToInt32(drpDersHocalar.SelectedValue)))
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
        dugmeYorumGuncelle.Visible = false;
    }
}
