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

public partial class UserControls_UyeOl : BaseUserControl
{
    protected void Page_Prerender(object sender, EventArgs e)
    {
        try
        {
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
            lblDurum.Text = "E-posta adresi zaten kayitli. Sag ustten 'Giris yap'a basip 'Sifremi unuttum'a tiklayabilirsin.";
        }
        else if (result == 1)
        {
            bool universite_epostasi = false;
            //Universite epostasi mi 
            if(okulId >= 0)
            {
                string okul_alanadi = Okullar.OkulUrlDondur(okulId);
                if(!string.IsNullOrEmpty(okul_alanadi))
                {
                    if (okul_alanadi.Contains("www."))
                    {
                        okul_alanadi = okul_alanadi.Substring(okul_alanadi.IndexOf("www.") + 4).ToLowerInvariant();
                    }
                    else
                    {
                        okul_alanadi = okul_alanadi.Substring(okul_alanadi.IndexOf("http://") + 7).ToLowerInvariant();
                    }
                    string eposta_alanadi = eposta.Substring(eposta.IndexOf("@") + 1).ToLowerInvariant();
                    if (eposta_alanadi.Contains(okul_alanadi))
                    {
                        universite_epostasi = true;
                    }
                }
            }
            if (!Mesajlar.OnayEpostasiGonder(ad, eposta,universite_epostasi))
            {
                //Onay epostasi gonderemedik
                //TODO: Admin'e mesaj
            }
            Uyelik.GirisYap(eposta, sifre);
            RefreshPage();
        }
    }
}
