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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                //s: Puan aciklamalarini doldur
                if (session.hocaPuanAciklamalari.Length == 5)
                {
                    Aciklama1.Text = session.hocaPuanAciklamalari[0];
                    Aciklama2.Text = session.hocaPuanAciklamalari[1];
                    Aciklama3.Text = session.hocaPuanAciklamalari[2];
                    Aciklama4.Text = session.hocaPuanAciklamalari[3];
                    Aciklama5.Text = session.hocaPuanAciklamalari[4];
                }
                else
                {
                    //TODO: admin mesaj
                }
                //e: Puan aciklamalarini doldur

                //s: Hoca puanlarini doldur
                //javascript kullanacagiz
                if (session.HocaID <= 0)
                {
                    KontroluSakla();
                    return;
                }
                float[] puanlar = Hocalar.HocaPuanlariniDondur(session.HocaID);
                StringBuilder sb = new StringBuilder();
                if (puanlar == null) //Hata olustu ya da hocanin hic puani yok
                {
                    pnlNotYok.Visible = true;
                }
                else if (puanlar.Length != 5)   //Hata
                {
                }
                else
                {
                    pnlNotYok.Visible = false;
                    //Puanlari yildizlarin genisligine gore orantilamaliyiz, 5 yildizin genisligi 84px
                    for (int i = 0; i < 5; i++)
                    {
                        puanlar[i] = (puanlar[i] * 20) * ((float)84/100);
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
                }

                //e: Hoca puanlarini doldur
            }
            catch
            {
                panelPuanlar.Visible = false;
            }
        }
    }

    void KontroluSakla()
    {
        panelPuanlar.Visible = false;
        pnlNotYok.Visible = false;
    }
}
