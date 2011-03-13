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

using System.Text;

public partial class UserControls_HocaPuanlari : BaseUserControl
{
    protected void Page_Prerender(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //s: Puan aciklamalarini doldur
                if (session.hocaPuanAciklamalari.Length == 5)
                {
                    Aciklama1.Text = session.hocaPuanAciklamalari[0] +":";
                    Aciklama2.Text = session.hocaPuanAciklamalari[1] + ":";
                    Aciklama3.Text = session.hocaPuanAciklamalari[2] + ":";
                    Aciklama4.Text = session.hocaPuanAciklamalari[3] + ":";
                    Aciklama5.Text = session.hocaPuanAciklamalari[4] + ":";
                }
                else
                {
                    Mesajlar.AdmineHataMesajiGonder(((System.Web.UI.Page)(sender)).Request.Url.ToString(), 
                        "Hoca puan aciklamalari 5 tane degil", session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
                }
                //e: Puan aciklamalarini doldur

                //s: Hoca puanlarini doldur
                //javascript kullanacagiz
                if (Query.GetInt("HocaID") <= 0)
                {
                    KontroluSakla();
                    return;
                }
                float[] puanlar = Hocalar.HocaPuanlariniDondur(Query.GetInt("HocaID"));
                StringBuilder sb = new StringBuilder();
                if (puanlar == null) //Hata olustu ya da hocanin hic puani yok
                {
                    pnlNotYok.Visible = true;
                }
                else if (puanlar.Length != 6)   //Hata, 5 puan + Puan sayisi olmali
                {
                    Mesajlar.AdmineHataMesajiGonder(((System.Web.UI.Page)(sender)).Request.Url.ToString(), 
                        "Hoca puanlari 6 adet olmali, degil", session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
                }
                else
                {
                    pnlNotYok.Visible = false;
                    //Puanlari yildizlarin genisligine gore orantilamaliyiz, 5 yildizin genisligi 100px
                    for (int i = 0; i < 5; i++)
                    {
                        puanlar[i] = (puanlar[i] * 20) * ((float)100 / 100);
                        puanlar[i] = (float)Math.Round(puanlar[i]);
                    }

                    sb.Append("<script type='text/javascript'>");
                    sb.Append("setRating(" + puanlar[0] + ",1);");
                    sb.Append("setRating(" + puanlar[1] + ",2);");
                    sb.Append("setRating(" + puanlar[2] + ",3);");
                    sb.Append("setRating(" + puanlar[3] + ",4);");
                    sb.Append("setRating(" + puanlar[4] + ",5);");
                    sb.Append("</script>");
                    script.Text = sb.ToString();
                    //   Puan1.

                    lblToplamPuanSayisi.Text = "(Toplam <strong>" + puanlar[5] + "</strong> kisi puan vermis)";
                }
                //e: Hoca puanlarini doldur
            }
        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
        }
    }

    void KontroluSakla()
    {
        panelPuanlar.Visible = false;
        pnlNotYok.Visible = false;
    }
}
