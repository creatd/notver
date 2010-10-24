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
using System.IO;

public partial class Okul : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if(Query.GetInt("OkulID") > 0)
            {
                DataTable dtOkul = Okullar.OkulProfilDondur(Query.GetInt("OkulID"));
                if (dtOkul != null && dtOkul.Rows.Count > 0)
                {
                    //Okul isim
                    if (Util.GecerliString(dtOkul.Rows[0]["ISIM"]))
                    {
                        lblOkulIsim.Text = dtOkul.Rows[0]["ISIM"].ToString();
                        session.OkulIsim = dtOkul.Rows[0]["ISIM"].ToString();
                    }
                    //Kurulus tarihi
                    if (dtOkul.Rows[0]["KURULUS_TARIHI"] != System.DBNull.Value)
                    {
                        DateTime dateTime;
                        DateTime.TryParse(dtOkul.Rows[0]["KURULUS_TARIHI"].ToString(), out dateTime);
                        lblOkulKurulusTarihi.Text = dateTime.ToString("dd/MM/yyyy");
                    }
                    //Okul adresi
                    if (Util.GecerliString(dtOkul.Rows[0]["ADRES"]))
                    {
                        lblOkulAdres.Text = dtOkul.Rows[0]["ADRES"].ToString();
                    }
                    //Ogrenci sayisi
                    if (Util.GecerliString(dtOkul.Rows[0]["OGRENCI_SAYISI"]))
                    {
                        lblOgrenciSayisi.Text = dtOkul.Rows[0]["OGRENCI_SAYISI"].ToString();
                    }
                    //Akademik personel sayisi
                    if (Util.GecerliString(dtOkul.Rows[0]["AKADEMIK_SAYISI"]))
                    {
                        lblAkademikPersonelSayisi.Text = dtOkul.Rows[0]["AKADEMIK_SAYISI"].ToString();
                    }
                    //Web adresi
                    if (Util.GecerliString(dtOkul.Rows[0]["WEB_ADRESI"]))
                    {
                        hpOkulWeb.Text = dtOkul.Rows[0]["WEB_ADRESI"].ToString();
                        hpOkulWeb.NavigateUrl = dtOkul.Rows[0]["WEB_ADRESI"].ToString();
                    }
                    //Okul resmi
                    string imageRelativePath = "~/Images/Okullar/p" + Query.GetInt("OkulID") + ".jpg";
                    string imageFilePath = Server.MapPath(imageRelativePath);
                    if (File.Exists(imageFilePath))
                    {
                        imgOkul.ImageUrl = imageRelativePath;
                    }
                    else
                    {
                        imgOkul.ImageUrl = "~/Images/Okullar/p_yok.jpg";
                    }
                }
            }
        }
    }
}
