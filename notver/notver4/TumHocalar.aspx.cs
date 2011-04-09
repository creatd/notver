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
using System.Collections.Generic;

public partial class TumHocalar : BasePage
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

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                /*DataTable dtOkullar = null;
                //Query string'de OkulID varsa o okul icin dondurecegiz
                if (Query.GetInt("OkulID") > 0)
                {
                    int okulID = Query.GetInt("OkulID");
                    dtOkullar = Okullar.OkulProfilDondur(okulID);
                    if (dtOkullar != null && dtOkullar.Rows.Count > 0)
                    {
                        dtOkullar.Columns.Add(new DataColumn("OKUL_ID", System.Type.GetType("System.Int32")));
                        dtOkullar.Rows[0]["OKUL_ID"] = okulID;
                    }

                }
                else  //Query string'de OkulID yok, tum okullar icin dondurecegiz
                {
                    dtOkullar = Okullar.OkullariDondur();
                }*/

                DataTable dtBolumler = null;
                //TODO: v3'te duzelt
                // Okuldaki tum bolumleri dondur
                dtBolumler = Okullar.BolumleriDondur(-99);

                if (dtBolumler != null)
                {
                    repeaterBolumler.DataSource = dtBolumler;
                    repeaterBolumler.DataBind();
                    repeaterBolumler.Visible = true;
                    HarfDiziniOlustur(dtBolumler);

                    drpOkulBolumler.Items.Clear();
                    drpOkulBolumler.Items.Add(new ListItem("Tümü", "-1"));
                    foreach (DataRow dr in dtBolumler.Rows)
                    {
                        drpOkulBolumler.Items.Add(new ListItem(dr["ISIM"].ToString(), dr["BOLUM_ID"].ToString()));
                    }
                }
                else
                {
                    repeaterBolumler.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
            GoToDefaultPage();
        }
    }

    protected void BolumSecildi(object sender, EventArgs e)
    {
        if (Util.GecerliSayi(drpOkulBolumler.SelectedValue))
        {
            int secili_bolumID = Convert.ToInt32(drpOkulBolumler.SelectedValue);

            DataTable dtBolumler = null;
            if (secili_bolumID < 0) // Tum bolumler
            {
                //TODO: v3'te duzelt
                // Okuldaki tum bolumleri dondur
                dtBolumler = Okullar.BolumleriDondur(-99);
            }
            else    // Sadece bu bolumdeki dersleri dondur
            {
                //TODO: v3'te duzelt
                // Okuldaki tum bolumleri dondur
                dtBolumler = Okullar.BolumDondur(secili_bolumID);
            }

            if (dtBolumler != null)
            {
                repeaterBolumler.DataSource = dtBolumler;
                repeaterBolumler.DataBind();
                repeaterBolumler.Visible = true;
                HarfDiziniOlustur(dtBolumler);
            }
            else
            {
                repeaterBolumler.Visible = false;
            }
        }
    }

    protected void HarfDiziniOlustur(DataTable dtOkullar)
    {
        Hashtable harfSayimi = new Hashtable();
        foreach (DataRow dr in dtOkullar.Rows)
        {
            harfSayimi[dr["ISIM"].ToString()[0]] = true;
        }

        LinkedList<char> alfabe = Alfabe(true);
        StringBuilder sb = new StringBuilder();
        sb.Append("<ol class='dizin' style='font-weight:normal; padding:10px; text-align:center;'>");
        foreach (char ch in alfabe)
        {
            if (harfSayimi.ContainsKey(ch))
            {
                sb.Append("<li><b><a href='#" + ch + "'>" + ch + "</a></b></li>");
            }
            else
            {
                sb.Append("<li class='sessiz'>" + ch + "</li>");
            }
        }
        sb.Append("</ol>");
        ltrHarfDizini.Text = sb.ToString();
    }

    protected string BolumBaslikDondur(object BolumIsim)
    {
        if (Util.GecerliString(BolumIsim) && BolumIsim.ToString().Length > 0)
        {
            char bas_harf = BolumIsim.ToString()[0];
            return "<a name='" + bas_harf + "' />" +
                BolumIsim.ToString();
        }
        else
        {
            return "";
        }
    }

    //Kullanilmiyor
    protected string OkulLinkBaslikDondur(object OkulIsim, object OkulID)
    {
        if (Util.GecerliString(OkulIsim) && Util.GecerliString(OkulID))
        {
            return "<a href='" + OkulURLDondur(OkulID) + "' name='" + OkulIsim.ToString()[0] + "'>"
                + OkulIsim.ToString() + "</a>";
        }
        else
        {
            return "";
        }
    }
}
