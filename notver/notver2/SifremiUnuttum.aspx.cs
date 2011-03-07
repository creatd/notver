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

public partial class SifremiUnuttum : BasePage
{
    protected void Page_Prerender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (session.IsLoggedIn)
            {
                GoToDefaultPage();
            }
            pnlBasari.Visible = false;
            pnlHata.Visible = true;
            string kullanici_eposta = Query.GetString("KullaniciEposta");
            string onay_kodu = Query.GetString("Kod");
            if (!string.IsNullOrEmpty(kullanici_eposta) && !string.IsNullOrEmpty(onay_kodu))
            {
                string dogru_onay_kodu = Uyelik.SifremiUnuttumIcinHashOlustur(kullanici_eposta);
                if (!string.IsNullOrEmpty(dogru_onay_kodu) && onay_kodu == dogru_onay_kodu)
                {
                    pnlBasari.Visible = true;
                    pnlHata.Visible = false;
                }
            }
        }
    }

    protected void SifreDegistir(object sender, EventArgs e)
    {
        string kullanici_eposta = Query.GetString("KullaniciEposta");
        if (Uyelik.KullaniciSifreDegistir(kullanici_eposta, txtSifre.Text))
        {
            pnlBasari.Visible = false;
            lblDurum.Text = "Sifreniz degistirildi. Yeni sifrenizle giris yapabilirsiniz.";
        }
        else
        {
            //TODO: Admine msj
            lblDurum.Text = "Bir hata olustu. Lutfen tekrar deneyin.";
        }
    }

}
