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
                lblDurum.Text = "tekrar dene";
                break;
            case -2:    //Kullanici engellenmis
                lblDurum.Text = "hesabın engellenmiştir";
                break;
            default:    //Bilinmeyen hata
                lblDurum.Text = "hata oluştu - tekrar dene";
                break;
        }
    }

    protected void SifremiUnuttum(object sender, EventArgs e)
    {
        lblDurum.Text = "";
        if (string.IsNullOrEmpty(txtEposta.Text))
        {            
            lblDurum.Text = "e-posta adresini girmelisin";
            return;
        }
        if (Uyelik.EpostaAdresiVarMi(txtEposta.Text))
        {
            if (Mesajlar.SifremiUnuttumEpostasiGonder(txtEposta.Text))
            {
                lblDurum.Text = "e-posta adresine şifre talimatları gönderildi";
            }
            else
            {
                lblDurum.Text = "bir hata oluştu, lütfen tekrar dene";
            }
        }
        else
        {
            lblDurum.Text = "bu e-posta adresi kayıtlı değil";
        }
    }
}
