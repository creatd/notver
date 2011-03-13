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

public partial class EpostaOnayla : BasePage
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
            pnlOnayBasari.Visible = false;
            pnlOnayBasari_UniEposta.Visible = false;
            pnlOnayHata.Visible = true;
            string kullanici_eposta = Query.GetString("KullaniciEposta");
            string onay_kodu = Query.GetString("OnayKodu");
            if (!string.IsNullOrEmpty(kullanici_eposta) && !string.IsNullOrEmpty(onay_kodu))
            {
                bool universite_epostasi = false;
                if (onay_kodu[0] == '1')
                {
                    universite_epostasi = true;
                }
                onay_kodu = onay_kodu.Substring(1);
                string dogru_onay_kodu = Uyelik.OnayIcinHashOlustur(kullanici_eposta);
                if (!string.IsNullOrEmpty(dogru_onay_kodu) && onay_kodu == dogru_onay_kodu)
                {
                    if (!Uyelik.KullaniciEpostaOnayla(kullanici_eposta,universite_epostasi))
                    {
                        //TODO: admin'e mesaj
                        //Kullanicinin epostasini onaylamamiz gerekirken veritabanina yazamadik
                    }
                    pnlOnayHata.Visible = false;
                    if (universite_epostasi)
                    {
                        pnlOnayBasari_UniEposta.Visible = true;
                    }
                    else
                    {
                        pnlOnayBasari.Visible = true;
                    }
                }
            }
        }
    }

    protected void OnayEpostasiGonder(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtEposta.Text))
        {
            lblDurum.Text = "E-posta adresini girmelisin";
            return;
        }
        string eposta = txtEposta.Text.ToLowerInvariant().Trim();
        if (Uyelik.EpostaAdresiVarMi(eposta))
        {
            DataTable dtKullanici = Uyelik.KullaniciProfilDondur(eposta);
            if (dtKullanici != null && dtKullanici.Rows.Count == 1)
            {
                DataRow dr = dtKullanici.Rows[0];
                string kullanici_ismi ="";
                if(Util.GecerliString(dr["AD"]))
                {
                    kullanici_ismi = dr["AD"].ToString();
                }
                string okul_alanadi = "";
                if (Util.GecerliString(dr["OKUL_URL"]))
                {
                    okul_alanadi = dr["OKUL_URL"].ToString();
                }

                bool universite_epostasi = false;
                if (!string.IsNullOrEmpty(okul_alanadi))
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

                if (Mesajlar.OnayEpostasiGonder(kullanici_ismi, eposta, universite_epostasi))
                {
                    lblDurum.Text = "E-posta adresine onay e-postası gönderildi";
                }
                else
                {
                    lblDurum.Text = "Bir hata oldu, lütfen tekrar deneyin";
                }
            }
            else
            {
                lblDurum.Text = "Bir hata oldu, lütfen tekrar deneyin";
            }

        }
        else
        {
            lblDurum.Text = "Bu e-posta adresi sistemimizde kayıtlı değil";
        }
    }
}
