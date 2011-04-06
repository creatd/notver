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

public partial class Giris : BasePage
{
    protected void Page_Prerender(object sender, EventArgs e)
    {
        try
        {
            if (session.IsLoggedIn)
            {
                Yonlendir();
            }
        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
            GoToDefaultPage();
        }
    }

    protected void GirisYap(object sender, EventArgs e)
    {
        if (Uyelik.GirisYap(txtEposta.Text, txtSifreGiris.Text) == 0)
        {
            //session.LoggedIn = true;
            //session.KullaniciAdi = txtUsername.Text.Trim();
            lblDurum1.Text = "";
            Yonlendir();
        }
        else
        {
            lblDurum1.Text = "Giris yapilamadi. Lutfen kullanici adi/sifrenizi kontrol edin.";
        }
    }

    protected void Yonlendir()
    {
        string redirect_url = Query.GetString("yonlendir");
        if (string.IsNullOrEmpty(redirect_url))
        {
            GoToDefaultPage();
        }
        else
        {
            redirect_url = redirect_url.Replace("!", "?");
            redirect_url = redirect_url.Replace(",", "&");
            //TODO: remove before production
            redirect_url.Replace("notverin/notverin", "notverin");
            Response.Redirect(Page.ResolveUrl("~/" + redirect_url));          
        }
    }
}
