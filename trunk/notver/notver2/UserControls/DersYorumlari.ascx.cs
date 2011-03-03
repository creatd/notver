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

public partial class UserControls_DersYorumlari : BaseUserControl
{
    public int MevcutSayfa
    {
        get
        {
            object o = this.ViewState["_MevcutSayfa"];
            if (o == null)
                return 0;
            else
                return (int)o;
        }

        set
        {
            this.ViewState["_MevcutSayfa"] = value;
        }
    }

    public int SayfaBoyutu
    {
        get
        {
            object o = this.ViewState["_SayfaBoyutu"];
            if (o == null)
                return 0;
            else
                return (int)o;
        }

        set
        {
            this.ViewState["_SayfaBoyutu"] = value;
        }
    }

    protected void rptPager_Command(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "SayfayaGit")
        {
            MevcutSayfa = Convert.ToInt32(e.CommandArgument);
            YorumlariDoldur();
        }
    }

    protected void SayfaBoyutuDegisti(object sender, EventArgs e)
    {
        SayfaBoyutu = Convert.ToInt32(dropSayfaBoyutu.SelectedValue);
        MevcutSayfa = 1;
        YorumlariDoldur();
    }

    protected void OncekiYorumlaraGit(object sender, EventArgs e)
    {
        MevcutSayfa -= 1;
        YorumlariDoldur();
    }

    protected void SonrakiYorumlaraGit(object sender, EventArgs e)
    {
        MevcutSayfa += 1;
        YorumlariDoldur();
    }



    protected void Page_Prerender(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Query.GetInt("DersID") <= 0)
                {
                    KontroluSakla();
                    return;
                }
                MevcutSayfa = 1;
                SayfaBoyutu = Convert.ToInt32(dropSayfaBoyutu.SelectedValue);
                YorumlariDoldur();
            }
        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
        }
    }

    private void YorumlariDoldur()
    {
        DataTable yorumlar = Dersler.DersYorumlariniDondur(Query.GetInt("DersID"));
        if (yorumlar != null && yorumlar.Rows.Count > 0)
        {
            pnlYorumlar.Visible = true;
            pnlYorumYok.Visible = false;

            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = yorumlar.DefaultView;
            pds.AllowPaging = true;

            if (SayfaBoyutu == 0)   //Hepsini goster
            {
                pds.PageSize = yorumlar.Rows.Count;
            }
            else
            {
                pds.PageSize = SayfaBoyutu;
            }

            pds.CurrentPageIndex = MevcutSayfa - 1;
            lnkOnceki.Enabled = !pds.IsFirstPage;
            lnkSonraki.Enabled = !pds.IsLastPage;

            ArrayList arrList = new ArrayList(pds.PageCount);
            for (int i = 0; i < pds.PageCount; i++)
            {
                arrList.Add((i + 1).ToString());
            }
            rptPager.DataSource = arrList;
            rptPager.DataBind();

            repeaterYorumlar.DataSource = pds;
            repeaterYorumlar.DataBind();
        }
        else
        {
            pnlYorumlar.Visible = false;
            pnlYorumYok.Visible = true;
        }
    }

    protected void repeaterYorumlar_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        yorumPuan.Text = ((System.Data.DataRowView)(e.Item.DataItem)).Row["ALKIS_PUANI"].ToString() + "&nbsp;";
    }

    protected void rptPager_DataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton but = e.Item.FindControl("lnkSayfa") as LinkButton;
        if (but != null && Convert.ToInt32(but.CommandArgument) == MevcutSayfa)
        {
            but.Enabled = false;
        }
    }

    protected string YorumBasligiOlustur(object KullaniciAdi, object Tarih, object KullaniciPuanAraligi)
    {
        try
        {
            DateTime tarih = Convert.ToDateTime(Tarih.ToString());
            string str = KullaniciAdi + " - " + tarih.Day + "/" + tarih.Month + "/" + tarih.Year;
            if (KullaniciPuanAraligi != System.DBNull.Value)
            {
                str += " (Hocadan aldigi not : " + KullaniciPuanAraligi.ToString() + "/5)";
            }
            return str;
        }
        catch
        {
            return "";
        }
    }

    protected string YorumBasligiOlustur(object KullaniciAdi, object KullaniciIsim, object Tarih, object HocaIsim, object KayitsizHocaIsim)
    {
        try
        {
            DateTime tarih = Convert.ToDateTime(Tarih.ToString());
            string str = "";
            if (Util.GecerliString(KullaniciAdi))
            {
                str = KullaniciAdi.ToString();;
            }
            else
            {
                str = KullaniciIsim.ToString();
            }
            str += "&nbsp;&nbsp;" + tarih.Day + "/" + tarih.Month + "/" + tarih.Year;
            if(Util.GecerliString(HocaIsim))
            {
                str += "&nbsp;&nbsp&nbsp;&nbsp<span style='color:#05d4b4;'>" + HocaIsim.ToString() + "</span>";
            }
            else if (Util.GecerliString(KayitsizHocaIsim))
            {
                str += "&nbsp;&nbsp&nbsp;&nbsp<span style='color:#05d4b4;'>" + KayitsizHocaIsim.ToString() + "</span>";
            }
            return str;
        }
        catch
        {
            return "";
        }
    }

    protected void yorumSev_click(object sender, EventArgs e)
    {
        Label lblYorumPuanDurumu = ((ImageButton)sender).Parent.FindControl("yorumPuanDurumu") as Label;
        if (!session.IsLoggedIn)
        {
            lblYorumPuanDurumu.Text = "Puan verebilmek icin uye girisi yapmalisiniz!";
            return;
        }
        Literal ltrYorumPuan = ((ImageButton)sender).Parent.FindControl("yorumPuan") as Literal;
        HiddenField hiddenField = ((ImageButton)sender).FindControl("yorumID") as HiddenField;
        int yorumID = Convert.ToInt32(hiddenField.Value);
        int[] result = Genel.YorumPuanVer(true, session.KullaniciID, yorumID, Enums.YorumTipi.DersYorum);
        if (result == null || result.Length != 2) //Bir hata olustu
        {
            lblYorumPuanDurumu.Text = "Bir hata olustu, lutfen tekrar deneyin";
        }
        else
        {
            if (result[0] == 1) //Ilk defa puan verildi.
            {
                lblYorumPuanDurumu.Text = "Puaniniz kaydedildi";
            }
            else if (result[0] == 2)    //Daha once puan verilmis. Puan guncellendi.
            {
                lblYorumPuanDurumu.Text = "Puaniniz guncellendi";
            }
            if (result[1] > 0)
            {
                ltrYorumPuan.Text = "<strong>+" + result[1] + "&nbsp;</strong>";
            }
            else
            {
                ltrYorumPuan.Text = "<strong>" + result[1] + "&nbsp;</strong>";
            }
        }
    }

    protected void yorumSevme_click(object sender, EventArgs e)
    {
        Label lblYorumPuanDurumu = ((ImageButton)sender).Parent.FindControl("yorumPuanDurumu") as Label;
        if (!session.IsLoggedIn)
        {
            lblYorumPuanDurumu.Text = "Puan verebilmek icin uye girisi yapmalisiniz!";
            return;
        }
        Literal ltrYorumPuan = ((ImageButton)sender).Parent.FindControl("yorumPuan") as Literal;
        HiddenField hiddenField = ((ImageButton)sender).FindControl("yorumID") as HiddenField;
        int yorumID = Convert.ToInt32(hiddenField.Value);
        int[] result = Genel.YorumPuanVer(false, session.KullaniciID, yorumID, Enums.YorumTipi.DersYorum);
        if (result == null || result.Length != 2) //Bir hata olustu
        {
            lblYorumPuanDurumu.Text = "Bir hata olustu, lutfen tekrar deneyin";
        }
        else
        {
            if (result[0] == 1) //Ilk defa puan verildi.
            {
                lblYorumPuanDurumu.Text = "Puaniniz kaydedildi";
            }
            else if (result[0] == 2)    //Daha once puan verilmis. Puan guncellendi.
            {
                lblYorumPuanDurumu.Text = "Puaniniz guncellendi";
            }
            if (result[1] > 0)
            {
                ltrYorumPuan.Text = "<strong>+" + result[1] + "&nbsp;</strong>";
            }
            else
            {
                ltrYorumPuan.Text = "<strong>" + result[1] + "&nbsp;</strong>";
            }
        }
    }

    void KontroluSakla()
    {
        pnlYorumlar.Visible = false;
        pnlYorumlar.Visible = false;
    }
}
