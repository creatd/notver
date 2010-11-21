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

public partial class UserControls_DersYorumGuncelle : BaseUserControl
{
    protected void Page_Prerender(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                KontroluSakla();
            }
            int queryDersYorumID = Query.GetInt("DersYorumID");
            if (queryDersYorumID <= 0)
            {
                return;
            }
            if (session.IsLoggedIn && session.KullaniciID > 0)
            {
                pnlPuanYorum.Visible = true;
                //s: drpDersHocalar'i duzenle
                drpDersHocalar.Items.Clear();

                //Dersi veren hocalari doldur
                DataTable dtDersiVerenHocalar = Dersler.DersiVerenHocalariKullaniciyaGoreDondur(Query.GetInt("DersID"), session.KullaniciID);
                if (!Dersler.KullaniciDerseGenelYorumYapmis(session.KullaniciID, Query.GetInt("DersID")))
                {
                    drpDersHocalar.Items.Add(new ListItem("-", "-1"));
                }
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
                drpDersHocalar.Items.Add(new ListItem("Diger", "-2"));
                //e: drpDersHocalar'i duzenle

                //Kullanicinin daha once yaptigi yorumu yukle
                DataTable dtEskiYorum = Dersler.DersYorumunuDondur(queryDersYorumID);
                if (dtEskiYorum != null && dtEskiYorum.Rows.Count > 0)
                {
                    if (Util.GecerliString(dtEskiYorum.Rows[0]["YORUM"]))
                    {
                        textYorum.Text = Util.DBToHTML(dtEskiYorum.Rows[0]["YORUM"].ToString());
                    }

                    //HocaID'yi sec
                    if (Util.GecerliStringSayi(dtEskiYorum.Rows[0]["HOCA_ID"]) && Util.GecerliString(dtEskiYorum.Rows[0]["HOCA_ISIM"]))
                    {
                        drpDersHocalar.Items.Add(new ListItem(dtEskiYorum.Rows[0]["HOCA_ISIM"].ToString(), dtEskiYorum.Rows[0]["HOCA_ID"].ToString()));
                        drpDersHocalar.SelectedValue = dtEskiYorum.Rows[0]["HOCA_ID"].ToString();
                    }
                    else if (Util.GecerliString(dtEskiYorum.Rows[0]["KAYITSIZ_HOCA_ISIM"]))
                    {
                        drpDersHocalar.SelectedValue = "-2";
                        txtBilinmeyenHocaIsmi.Text = dtEskiYorum.Rows[0]["KAYITSIZ_HOCA_ISIM"].ToString();
                    }

                    if(Util.GecerliStringSayi(dtEskiYorum.Rows[0]["ZORLUK_PUANI"]))
                    {
                        puanDersZorluk.CurrentRating = Convert.ToInt32(dtEskiYorum.Rows[0]["ZORLUK_PUANI"]);
                    }
                    if (Util.GecerliStringSayi(dtEskiYorum.Rows[0]["TAVSIYE_PUANI"]))
                    {
                        puanDersHoca.CurrentRating = Convert.ToInt32(dtEskiYorum.Rows[0]["TAVSIYE_PUANI"]);
                    }
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
    protected void YorumGuncelle(object sender, EventArgs e)
    {
        if (!Dersler.DersYorumGuncelle(Query.GetInt("DersYorumID") , textYorum.Text,puanDersZorluk.CurrentRating, Convert.ToInt32(drpDersHocalar.SelectedValue), puanDersHoca.CurrentRating, txtBilinmeyenHocaIsmi.Text))
        {
            ltrDurum.Text = "Yorumunuzu guncellerken bir hata olustu. Lutfen tekrar deneyin";
        }
        else
        {
            ltrDurum.Text = "Yorumunuz basariyla guncellendi!";
            ltrScript.Text = "<script type='text/javascript'>setTimeout('self.parent.tb_remove()',1500);</script>";
        }
    }

    void KontroluSakla()
    {
        pnlPuanYorum.Visible = false;
        pnlUyeOl.Visible = false;
    }
}
