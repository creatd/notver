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

using System.Collections.Generic;
using Amazon.S3.Model;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using Amazon.S3;

public partial class Admin_Default : BasePage
{
    protected void Page_Prerender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lblDurum.Text = "";
            try
            {
                IstatistikDoldur();
            }
            catch (Exception ex)
            {
                lblDurum.Text = "Hata oldu : <br/>" + ex.Message;
            }
        }
    }

    protected void IstatistikDoldur()
    {
        DataTable dtIstatistik = Genel.Admin_IstatistikDondur();
        if (dtIstatistik != null && dtIstatistik.Rows.Count == 1)
        {
            DataRow dr = dtIstatistik.Rows[0];
            lblUyeSayisi1.Text = dr["UYE_SAYISI_TOPLAM"].ToString();    //Engellenmisler de dahil
            lblUyeSayisi2.Text = dr["UYE_SAYISI"].ToString();
            lblToplamYorum1.Text = dr["TOPLAM_YORUM"].ToString();
            lblToplamYorum2.Text = dr["TOPLAM_YORUM_ONAYLI"].ToString();
            lblDersYorumSayisi1.Text = dr["DERS_YORUM"].ToString();
            lblDersYorumSayisi2.Text = dr["DERS_YORUM_ONAYLI"].ToString();
            lblHocaYorumSayisi1.Text = dr["HOCA_YORUM"].ToString();
            lblHocaYorumSayisi2.Text = dr["HOCA_YORUM_ONAYLI"].ToString();
            lblOkulYorumSayisi1.Text = dr["OKUL_YORUM"].ToString();
            lblOkulYorumSayisi2.Text = dr["OKUL_YORUM_ONAYLI"].ToString();
            lblOkunmamisMesajSayisi.Text = dr["MESAJ_SAYISI"].ToString();
            lblDosyaSayisi1.Text = dr["DOSYA_SAYISI"].ToString();
            lblDosyaSayisi2.Text = dr["DOSYA_SAYISI_ONAYLI"].ToString();
        }
    }
}
