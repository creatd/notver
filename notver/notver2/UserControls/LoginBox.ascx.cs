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
        if (Uyelik.GirisYap(txtEposta.Text, txtSifre.Text))
        {
            RefreshPage();
            lblDurum.Text = "";
        }
        else
        {
            lblDurum.Text = "tekrar deneyin";
        }
    }
}
