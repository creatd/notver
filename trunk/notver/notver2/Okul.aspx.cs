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

    protected void Page_Prerender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                int queryOkulID = Query.GetInt("OkulID");
                if (queryOkulID >= 0)
                {
                    session.OkulYukle(queryOkulID);
                    //Okul isim
                    if (!string.IsNullOrEmpty(session.OkulIsim))
                    {
                        lblOkulIsim.Text = session.OkulIsim;
                    }
                    else
                    {
                        lblOkulIsim.Text = "";
                    }
                    //Kurulus tarihi
                    if (session.OkulKurulusTarihi > 1800)
                    {
                        lblOkulKurulusTarihi.Text = session.OkulKurulusTarihi.ToString();
                    }
                    else
                    {
                        lblOkulKurulusTarihi.Text = "";
                    }
                    //Okul adresi
                    if (!string.IsNullOrEmpty(session.OkulAdres))
                    {
                        lblOkulAdres.Text = session.OkulAdres;
                    }
                    else
                    {
                        lblOkulAdres.Text = "";
                    }
                    //Ogrenci sayisi
                    if (session.OkulOgrenciSayisi > 0)
                    {
                        lblOgrenciSayisi.Text = session.OkulOgrenciSayisi.ToString();
                    }
                    else
                    {
                        lblOgrenciSayisi.Text = "";
                    }
                    //Akademik personel sayisi
                    if (session.OkulAkademikSayisi > 0)
                    {
                        lblAkademikPersonelSayisi.Text = session.OkulAkademikSayisi.ToString();
                    }
                    else
                    {
                        lblAkademikPersonelSayisi.Text = "";
                    }
                    //Web adresi
                    if (!string.IsNullOrEmpty(session.OkulWebAdresi))
                    {
                        hpOkulWeb.Text = session.OkulWebAdresi;
                        hpOkulWeb.NavigateUrl = session.OkulWebAdresi;
                    }
                    else
                    {
                        hpOkulWeb.Text = "";
                        hpOkulWeb.NavigateUrl = "";
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
                    bool yorumVar = Okullar.KullaniciOkulaYorumYapmis(session.KullaniciID, Query.GetInt("OkulID"));
                    if (yorumVar)
                    {
                        ltrYorumYazi.Text = "Yorum güncelle&nbsp;&nbsp;";
                    }
                    else
                    {
                        ltrYorumYazi.Text = "Yorum ekle&nbsp;&nbsp;";
                    }
                    lnkYorumum.NavigateUrl = Page.ResolveUrl("~/OkulYorumYap.aspx?OkulID=" + queryOkulID);
                }
            }
            catch (Exception ex)
            {
                Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
                GoToDefaultPage();
            }
        }
    }
}
