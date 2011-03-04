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
    protected void Page_Prerender(object sender, EventArgs e)
    {
        try
        {
            KontroluSakla();
            int queryDersID = Query.GetInt("DersID");
            if (queryDersID <= 0)
            {
                pnlHata.Visible = true;
                return;
            }
            if (session.IsLoggedIn && session.KullaniciID > 0)
            {
                pnlPuanYorum.Visible = true;
                bool yeniYorum = false;
                if (Query.GetInt("DersYorumID") >= 0)
                {
                    //Yorum guncelleme
                    //Kullanicinin daha once yaptigi yorumu yukle
                    DataTable dtEskiYorum = Dersler.DersYorumunuDondur(Query.GetInt("DersYorumID"));
                    if (dtEskiYorum != null && dtEskiYorum.Rows.Count > 0)
                    {
                        DataRow drEskiYorum = dtEskiYorum.Rows[0];
                        if (Util.GecerliString(drEskiYorum["YORUM"]))
                        {
                            textYorum.Text = drEskiYorum["YORUM"].ToString();
                        }
                        //HocaID'yi sec
                        drpDersHocalar.Enabled = false;
                        if (Util.GecerliString(drEskiYorum["HOCA_ISIM"]) && Util.GecerliSayi(drEskiYorum["HOCA_ID"]))
                        {
                            drpDersHocalar.Items.Add(new ListItem(drEskiYorum["HOCA_ISIM"].ToString(), drEskiYorum["HOCA_ID"].ToString()));
                        }
                        else if(Util.GecerliString(dtEskiYorum.Rows[0]["KAYITSIZ_HOCA_ISIM"]))
                        {
                            drpDersHocalar.Items.Add(new ListItem(drEskiYorum["KAYITSIZ_HOCA_ISIM"].ToString(), "-2"));
                        }
                        else
                        {
                        }
                    }
                }
                else
                {
                    //Yeni yorum
                    //s: drpDersHocalar'i duzenle
                    drpDersHocalar.Items.Clear();

                    //Dersi veren hocalari doldur
                    DataTable dtDersiVerenHocalar = Dersler.DersiVerenHocalariKullaniciyaGoreDondur(queryDersID, session.KullaniciID);
                    if (!Dersler.KullaniciDerseGenelYorumYapmis(session.KullaniciID, queryDersID))
                    {
                        drpDersHocalar.Items.Add(new ListItem("-", "-1"));
                    }
                    if (dtDersiVerenHocalar != null)
                    {
                        foreach (DataRow dr in dtDersiVerenHocalar.Rows)
                        {
                            drpDersHocalar.Items.Add(new ListItem(dr["HOCA_ISIM"].ToString(), dr["HOCA_ID"].ToString()));
                        }
                    }
                    else
                    {
                        pnlHata.Visible = true;
                        return;
                    }
                    drpDersHocalar.Items.Add(new ListItem("Diger", "-2"));
                }

                
                //e: drpDersHocalar'i duzenle

                /*if (!Dersler.KullaniciDerseYorumYapmis(session.KullaniciID, Query.Get("DersID")))
                {
                    dugmeYorumGonder.Visible = true;
                }
                else
                {
                    yorumVar = true;
                    //Kullanicinin daha once yaptigi yorumu yukle
                    DataTable dtEskiYorum = Dersler.KullaniciDersYorumunuDondur(session.KullaniciID, Query.Get("DersID"));
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
                
                }*/
                lnkKullaniciYorumlar.NavigateUrl = "javascript:parent.document.location='" + DersYorumlarimURLDondur(queryDersID) + "';";
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
        if (!Dersler.DersYorumKaydet(session.KullaniciID, Query.GetInt("DersID"), textYorum.Text, puanDersZorluk.CurrentRating , Convert.ToInt32(drpDersHocalar.SelectedValue), puanDersHoca.CurrentRating, txtBilinmeyenHocaIsmi.Text,session.KullaniciOnayPuani))
        {
            ltrDurum.Text = "Yorum kaydederken bir hata olustu. Lutfen tekrar deneyiniz.";
        }
        else
        {
            ltrDurum.Text = "Yorumunuz basariyla kaydedildi!";
            ltrScript.Text = "<script type='text/javascript'>setTimeout('parent.$.fn.colorbox.close()',1500);</script>";
        }
    }

    void KontroluSakla()
    {
        pnlPuanYorum.Visible = false;
        pnlUyeOl.Visible = false;
        pnlHata.Visible = false;
    }
}
