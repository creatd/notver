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

public partial class Kayit : BasePage
{
    protected void Page_Prerender(object sender, EventArgs e)
    {
        try
        {
            if (session.IsLoggedIn)
            {
                GoToDefaultPage();
            }
            if (!Page.IsPostBack)
            {
                DropDownList Okullar = ddOkullar as DropDownList;
                Okullar.Items.Add(new ListItem("-", "-1"));
                foreach (DataRow dr in session.dtOkullar.Rows)
                {
                    Okullar.Items.Add(new ListItem(dr["ISIM"].ToString(), dr["OKUL_ID"].ToString()));
                }
            }
        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
            GoToDefaultPage();
        }
    }


    protected void KullaniciOlustur(object sender, EventArgs e)
    {
        string kullaniciAdi = txtKullaniciAdi.Text.Trim();
        string sifre = txtSifre.Text.Trim();
        string ad = txtAd.Text.Trim();
        string soyad = txtSoyad.Text.Trim();
        int okulId = Convert.ToInt32(ddOkullar.SelectedValue);
        string eposta = txtEposta.Text.Trim();
        Enums.Cinsiyet cinsiyet = (Enums.Cinsiyet)Convert.ToInt32(rdCinsiyetler.SelectedValue);
        int result = Uyelik.KullaniciOlustur(kullaniciAdi, ad, soyad, okulId, eposta, Enums.UyelikDurumu.EpostaOnayBekliyor, Enums.UyelikRol.Kullanici, sifre, cinsiyet);
        lblDurum.Text = "";
        if (result == -1)
        {
            lblDurum.Text = "Kullanici adi alinmis, lutfen baska bir kullanici adi secin.";
        }
        else if (result == 0)
        {
            lblDurum.Text = "Bilinmeyen hata, lutfen tekrar deneyin.";
        }
        else if (result == -2)
        {
            lblDurum.Text = "E-posta adresi zaten kayitli. Sifrenizi unuttuysaniz sag ustten 'Sifremi unuttum'a tiklayiniz.";
        }
        else if (result == 1)
        {
            Uyelik.GirisYap(eposta, sifre);
            GoToDefaultPage();
        }
    }
}
