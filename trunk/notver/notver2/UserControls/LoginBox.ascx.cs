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
        if (session.IsLoggedIn)
        {
            GirisiGoster();
        }
        else
        {
            CikisiGoster();
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
        if (Uyelik.GirisYap(txtKullaniciAdi.Text, txtSifre.Text))
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
