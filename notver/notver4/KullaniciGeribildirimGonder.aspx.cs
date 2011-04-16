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

public partial class KullaniciGeribildirimGonder : BasePage
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
        try
        {
            KontroluSakla();
          
            if (session.IsLoggedIn && session.KullaniciID >= 0)
            {
                pnlGeriBildirim.Visible = true;
            }
            else
            {
                pnlUyeOl.Visible = true;
            }
        }
        catch (Exception ex)
        {
            KontroluSakla();
            pnlHata.Visible = true;
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
        }
    }

    protected void GeriBildirimGonder(object sender, EventArgs e)
    {
        string GeriBildirim = textGeriBildirim.Text ;
        string userID = session.KullaniciAdi ;

        textGeriBildirim.Text = userID;
        if (!Mesajlar.GeriBildirimEpostaGonder(GeriBildirim, session.KullaniciAdi))
        {
            ltrDurum.Text = "Şikayet iletirken bir hata oldu, lütfen tekrar deneyin";
        }
        else
        {
            ltrDurum.Text = "Geri bildirimin için teşekkürler!";
        }

    }

    void KontroluSakla()
    {
        pnlGeriBildirim.Visible = false;
        pnlUyeOl.Visible = false;
        pnlHata.Visible = false;
    }


}