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

public partial class UserControls_LoginBox : BaseUserControl
{
    protected void Page_Prerender(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
        }
    }

    protected void GirisYap(object sender, EventArgs e)
    {
        int sonuc = Uyelik.GirisYap(txtEposta.Text, txtSifre.Text);
        switch(sonuc)
        {
            case 0: //Sorun yok
                RefreshPage();
                lblDurum.Text = "";
                break;
            case -1:    //Eposta-sifre bulunamadi
                lblDurum.Text = "tekrar deneyin";
                break;
            case -2:    //Kullanici engellenmis
                lblDurum.Text = "hesabiniz engellenmistir";
                break;
            default:    //Bilinmeyen hata
                lblDurum.Text = "hata olustu - tekrar deneyin";
                break;
        }
    }

    protected void SifremiUnuttum(object sender, EventArgs e)
    {
        lblDurum.Text = "";
        if (string.IsNullOrEmpty(txtEposta.Text))
        {
            lblDurum.Text = "e-posta adresinizi girin";
            return;
        }
        if (Uyelik.EpostaAdresiVarMi(txtEposta.Text))
        {
            if (Mesajlar.SifremiUnuttumEpostasiGonder(txtEposta.Text))
            {
                lblDurum.Text = "e-posta adresinize sifre talimatlari gonderildi";
            }
            else
            {
                lblDurum.Text = "bir hata olustu, lutfen tekrar deneyin";
            }
        }
        else
        {
            lblDurum.Text = "bu e-posta adresi sistemimizde kayitli degil";
        }
    }
}
