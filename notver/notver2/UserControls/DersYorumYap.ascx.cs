﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


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
                    dugmeYorumGuncelle.Visible = true;
                    //Kullanicinin daha once yaptigi yorumu yukle
                    DataTable dtEskiYorum = Dersler.DersYorumunuDondur(Query.GetInt("DersYorumID"));
                    if (dtEskiYorum != null && dtEskiYorum.Rows.Count > 0)
                    {
                        DataRow drEskiYorum = dtEskiYorum.Rows[0];
                        if (Util.GecerliString(drEskiYorum["YORUM"]))
                        {
                            textYorum.Text = Util.DBToHTML(drEskiYorum["YORUM"].ToString());
                        }
                        //HocaID'yi sec
                        drpDersHocalar.Enabled = false;
                        if (Util.GecerliString(drEskiYorum["HOCA_ISIM"]) && Util.GecerliSayi(drEskiYorum["HOCA_ID"]))
                        {
                            drpDersHocalar.Items.Add(new ListItem(drEskiYorum["HOCA_ISIM"].ToString(), drEskiYorum["HOCA_ID"].ToString()));
                        }
                        else if(Util.GecerliString(dtEskiYorum.Rows[0]["KAYITSIZ_HOCA_ISIM"]))
                        {
                            drpDersHocalar.Items.Add(new ListItem("Diger", "-2"));
                            txtBilinmeyenHocaIsmi.Text = drEskiYorum["KAYITSIZ_HOCA_ISIM"].ToString();
                        }
                        else
                        {
                        }
                        
                        if (Util.GecerliSayi(drEskiYorum["ZORLUK_PUANI"]))
                        {
                            puanDersZorluk.CurrentRating = Convert.ToInt32(drEskiYorum["ZORLUK_PUANI"]);
                        }

                        if (Util.GecerliSayi(drEskiYorum["TAVSIYE_PUANI"]))
                        {
                            puanDersHoca.CurrentRating = Convert.ToInt32(drEskiYorum["TAVSIYE_PUANI"]);
                        }
                    }
                }
                else
                {
                    //Yeni yorum
                    dugmeYorumGonder.Visible = true;
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
        if (string.IsNullOrEmpty(textYorum.Text))
        {
            ltrDurum.Text = "Yorum girmeyi unuttun";
            return;
        }

        ltrDurum.Text = "";
        if (puanDersZorluk.CurrentRating < 1 || puanDersZorluk.CurrentRating > 5)
        {
            ltrDurum.Text = "Ders zor muydu sorusuna cevap vermedin";
            return;
        }
        int HocaID = -1;
        if (drpDersHocalar != null && drpDersHocalar.Items.Count > 0)
        {
            if (Util.GecerliSayi(drpDersHocalar.SelectedValue))
            {
                HocaID = Convert.ToInt32(drpDersHocalar.SelectedValue);
                if (HocaID >= 0)
                {
                    if (puanDersHoca.CurrentRating < 1 || puanDersHoca.CurrentRating > 5)
                    {
                        ltrDurum.Text = "Bu hocadan almak sorusuna cevap vermedin";
                        return;
                    }
                }
            }
        }
        //Diger sectiyse, hoca ismi bos olamaz
        if (Util.GecerliSayi(drpDersHocalar.SelectedValue) && Convert.ToInt32(drpDersHocalar.SelectedValue) == -2)
        {
            if (string.IsNullOrEmpty(txtBilinmeyenHocaIsmi.Text))
            {
                ltrDurum.Text = "Hocanın ismini girmedin";
                return;
            }
        }
        if (!Dersler.DersYorumKaydet(session.KullaniciID, Query.GetInt("DersID"), textYorum.Text,
            puanDersZorluk.CurrentRating, HocaID, puanDersHoca.CurrentRating, 
            txtBilinmeyenHocaIsmi.Text,session.KullaniciOnayPuani))
        {
            ltrDurum.Text = "Yorum kaydederken bir hata oldu, lütfen tekrar deneyin.";
        }
        else
        {
            ltrDurum.Text = "Yorumun başarıyla kaydedildi!";
            ltrScript.Text = "<script type='text/javascript'>setTimeout('parent.$.fn.colorbox.close()',1500);</script>";
        }
    }

    /// <summary>
    /// Yorumu gunceller
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void YorumGuncelle(object sender, EventArgs e)
    {
        ltrDurum.Text = "";
        if (puanDersZorluk.CurrentRating < 1 || puanDersZorluk.CurrentRating > 5)
        {
            ltrDurum.Text = "Ders zor muydu sorusuna cevap vermedin";
            return;
        }
        if (string.IsNullOrEmpty(textYorum.Text))
        {
            ltrDurum.Text = "Yorum girmeyi unuttun";
            return;
        }
        int HocaID = -1;
        if (drpDersHocalar != null && drpDersHocalar.Items.Count > 0)
        {
            if (Util.GecerliSayi(drpDersHocalar.SelectedValue))
            {
                HocaID = Convert.ToInt32(drpDersHocalar.SelectedValue);
                if (HocaID >= 0)
                {
                    if (puanDersHoca.CurrentRating < 1 || puanDersHoca.CurrentRating > 5)
                    {
                        ltrDurum.Text = "Bu hocadan almak sorusuna cevap vermedin";
                        return;
                    }
                }
            }
        }
        if (!Dersler.DersYorumGuncelle(Query.GetInt("DersYorumID"), textYorum.Text, puanDersZorluk.CurrentRating, 
            HocaID, puanDersHoca.CurrentRating, txtBilinmeyenHocaIsmi.Text, 
            session.KullaniciOnayPuani))
        {
            ltrDurum.Text = "Yorum güncellerken bir hata oldu, lütfen tekrar deneyin.";
        }
        else
        {
            ltrDurum.Text = "Yorumun başarıyla güncellendi!";
            ltrScript.Text = "<script type='text/javascript'>setTimeout('parent.$.fn.colorbox.close()',1500);</script>";
        }
    }


    void KontroluSakla()
    {
        pnlPuanYorum.Visible = false;
        pnlUyeOl.Visible = false;
        pnlHata.Visible = false;
        dugmeYorumGonder.Visible = false;
        dugmeYorumGuncelle.Visible = false;
    }
}
