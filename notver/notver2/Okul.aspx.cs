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
            if(session.OkulID > 0)
            {
                DataTable dtOkul = Okullar.OkulProfilDondur(session.OkulID);
                if (dtOkul != null && dtOkul.Rows.Count > 0)
                {
                    //Okul isim
                    if (dtOkul.Rows[0]["ISIM"] != System.DBNull.Value)
                    {
                        lblOkulIsim.Text = dtOkul.Rows[0]["ISIM"].ToString();
                    }
                    //Kurulus tarihi
                    if (dtOkul.Rows[0]["KURULUS_TARIHI"] != System.DBNull.Value)
                    {
                        lblOkulKurulusTarihi.Text = dtOkul.Rows[0]["KURULUS_TARIHI"].ToString();
                    }
                    //Okul adresi
                    if (dtOkul.Rows[0]["ADRES"] != System.DBNull.Value)
                    {
                        lblOkulAdres.Text = dtOkul.Rows[0]["ADRES"].ToString();
                    }
                    //Ogrenci sayisi
                    if (dtOkul.Rows[0]["OGRENCI_SAYISI"] != System.DBNull.Value)
                    {
                        lblOgrenciSayisi.Text = dtOkul.Rows[0]["OGRENCI_SAYISI"].ToString();
                    }
                    //Akademik personel sayisi
                    if (dtOkul.Rows[0]["AKADEMIK_SAYISI"] != System.DBNull.Value)
                    {
                        lblAkademikPersonelSayisi.Text = dtOkul.Rows[0]["AKADEMIK_SAYISI"].ToString();
                    }
                    //Web adresi
                    if (dtOkul.Rows[0]["WEB_ADRESI"] != System.DBNull.Value)
                    {
                        hpOkulWeb.NavigateUrl = dtOkul.Rows[0]["WEB_ADRESI"].ToString();
                    }
                    //Okul resmi
                    string imageRelativePath = "~/Images/Okullar/p" + session.OkulID + ".jpg";
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
