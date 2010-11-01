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

public partial class UserControls_OkulYorumlari : BaseUserControl
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
                if (Query.GetInt("OkulID") <= 0)
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
        DataTable yorumlar = Okullar.OkulYorumlariniDondur(Query.GetInt("OkulID"));
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
        if (e.Item.DataItem != null)
        {
            yorumPuan.Text = ((System.Data.DataRowView)(e.Item.DataItem)).Row["ALKIS_PUANI"].ToString();
        }
    }

    protected void rptPager_DataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton but = e.Item.FindControl("lnkSayfa") as LinkButton;
        if (but != null && Convert.ToInt32(but.CommandArgument) == MevcutSayfa)
        {
            but.Enabled = false;
        }
    }

    protected string YorumBasligiOlustur(object KullaniciAdi, object Tarih)
    {
        try
        {
            DateTime tarih = Convert.ToDateTime(Tarih.ToString());
            return KullaniciAdi + " - " + tarih.Day + "/" + tarih.Month + "/" + tarih.Year;
        }
        catch
        {
            return "";
        }
    }

    protected void yorumSev_click(object sender, EventArgs e)
    {
        Literal ltrYorumPuanDurumu = ((LinkButton)sender).Parent.FindControl("yorumPuanDurumu") as Literal;
        if (!session.IsLoggedIn)
        {
            ltrYorumPuanDurumu.Text = "Puan verebilmek icin uye girisi yapmalisiniz!";
            return;
        }
        Literal ltrYorumPuan = ((LinkButton)sender).Parent.FindControl("yorumPuan") as Literal;
        HiddenField hiddenField = ((LinkButton)sender).FindControl("yorumID") as HiddenField;
        int yorumID = Convert.ToInt32(hiddenField.Value);
        int[] result = Genel.YorumPuanVer(true, session.KullaniciID, yorumID, Enums.YorumTipi.OkulYorum);
        if (result == null || result.Length != 2) //Bir hata olustu
        {
            ltrYorumPuanDurumu.Text = "Bir hata olustu, lutfen tekrar deneyin";
        }
        else
        {
            if (result[0] == 1) //Ilk defa puan verildi.
            {
                ltrYorumPuanDurumu.Text = "Puaniniz kaydedildi";
            }
            else if (result[0] == 2)    //Daha once puan verilmis. Puan guncellendi.
            {
                ltrYorumPuanDurumu.Text = "Puaniniz guncellendi";
            }
            ltrYorumPuan.Text = "<strong>" + result[1] + "</strong>";
        }
    }

    protected void yorumSevme_click(object sender, EventArgs e)
    {
        Literal ltrYorumPuanDurumu = ((LinkButton)sender).Parent.FindControl("yorumPuanDurumu") as Literal;
        if (!session.IsLoggedIn)
        {
            ltrYorumPuanDurumu.Text = "Puan verebilmek icin uye girisi yapmalisiniz!";
            return;
        }
        Literal ltrYorumPuan = ((LinkButton)sender).Parent.FindControl("yorumPuan") as Literal;
        HiddenField hiddenField = ((LinkButton)sender).FindControl("yorumID") as HiddenField;
        int yorumID = Convert.ToInt32(hiddenField.Value);
        int[] result = Genel.YorumPuanVer(false, session.KullaniciID, yorumID, Enums.YorumTipi.OkulYorum);
        if (result == null || result.Length != 2) //Bir hata olustu
        {
            ltrYorumPuanDurumu.Text = "Bir hata olustu, lutfen tekrar deneyin";
        }
        else
        {
            if (result[0] == 1) //Ilk defa puan verildi.
            {
                ltrYorumPuanDurumu.Text = "Puaniniz kaydedildi";
            }
            else if (result[0] == 2)    //Daha once puan verilmis. Puan guncellendi.
            {
                ltrYorumPuanDurumu.Text = "Puaniniz guncellendi";
            }
            ltrYorumPuan.Text = "<strong>" + result[1] + "</strong>";
        }
    }

    void KontroluSakla()
    {
        pnlYorumlar.Visible = false;
        pnlYorumlar.Visible = false;
    }
}
