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
            lblTimeout.Visible = false;
            if (session.IsLoggedIn)
            {
                GirisiGoster();
            }
            else
            {
                CikisiGoster();
                if (Context.Session != null && Context.Session.IsNewSession)
                {
                    string cookie = Request.Headers["Cookie"];
                    if (!string.IsNullOrEmpty(cookie) && cookie.IndexOf("ASP.NET_SessionId") >= 0)
                    {
                        lblTimeout.Visible = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
        }
    }

    protected void GirisiGoster()
    {
        pnlNoLogin.Visible = false;
        pnlLogin.Visible = true;
    }

    protected void CikisiGoster()
    {
        pnlNoLogin.Visible = true;
        pnlLogin.Visible = false;
    }

    protected void GirisYap(object sender, EventArgs e)
    {
        if (Uyelik.GirisYap(txtEposta.Text, txtSifre.Text))
        {
            //session.LoggedIn = true;
            //session.KullaniciAdi = txtUsername.Text.Trim();
            lblDurum.Text = "";
        }
        else
        {
            lblDurum.Text = "Giris yapilamadi. Lutfen kullanici adi/sifrenizi kontrol edin.";
        }
    }

    protected void CikisYap(object sender, EventArgs e)
    {
        Uyelik.CikisYap();

    }
    


}
