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
    protected void Page_PreRender(object sender, EventArgs e)
    {
        KontroluSakla();
        pnlPuanYorum.Visible = true;
        if (Query.GetInt("DersYorumID") <= 0)
        {
            return;
        }
        if (session.IsLoggedIn && session.KullaniciID > 0)
        {
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
            DataTable dtEskiYorum = Dersler.KullaniciDersYorumunuDondur(session.KullaniciID, Query.GetInt("DersID"));
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
        dugmeYorumGuncelle.Visible = false;
    }
}
