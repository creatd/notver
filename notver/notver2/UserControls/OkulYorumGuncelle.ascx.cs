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

public partial class UserControls_OkulYorumGuncelle : BaseUserControl
{
    protected void Page_Prerender(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (session.IsLoggedIn)
                {
                    pnlYorum.Visible = true;
                    pnlUyeOl.Visible = false;
                    //Kullanicinin daha once yapmis oldugu yorumu yukle
                    string eskiYorum = Okullar.KullaniciOkulYorumunuDondur(session.KullaniciID, Query.GetInt("OkulID"));
                    if (Util.GecerliString(eskiYorum))
                    {
                        textYorum.Text = Util.DBToHTML(eskiYorum);
                    }

                }
                else
                {
                    pnlYorum.Visible = false;
                    pnlUyeOl.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
        }
    }

    protected void YorumGuncelle(object sender, EventArgs e)
    {
        if (Okullar.OkulYorumGuncelle(session.KullaniciID, Query.GetInt("OkulID"), textYorum.Text, session.KullaniciOnayPuani))
        {
            ltrDurum.Text = "Yorumunuz basariyla guncellendi";
            ltrScript.Text = "<script type='text/javascript'>setTimeout('self.parent.tb_remove()',1500);</script>";
        }
        else
        {
            ltrDurum.Text = "Bir hata olustu, lutfen tekrar deneyin.";
        }
    }
}
