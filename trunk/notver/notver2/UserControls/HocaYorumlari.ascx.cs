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

public partial class UserControls_HocaYorum : BaseUserControl
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



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (session.HocaID <= 0)
            {
                KontroluSakla();
                return;
            }
            MevcutSayfa = 1;
            SayfaBoyutu = Convert.ToInt32(dropSayfaBoyutu.SelectedValue);
            YorumlariDoldur();            
        }
    }

    private void YorumlariDoldur()
    {
        DataTable yorumlar = HocaYorumlariniDondur(session.HocaID);
        if (yorumlar!= null && yorumlar.Rows.Count > 0)
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
        yorumPuan.Text = ((System.Data.DataRowView)(e.Item.DataItem)).Row["ALKIS_PUANI"].ToString();
    }

    protected void rptPager_DataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton but = e.Item.FindControl("lnkSayfa") as LinkButton;
        if (but != null && Convert.ToInt32(but.CommandArgument) == MevcutSayfa)
        {
            but.Enabled = false;
        }
    }

    protected string YorumBasligiOlustur(object KullaniciAdi , object Tarih , object KullaniciPuanAraligi)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            DateTime tarih = Convert.ToDateTime(Tarih.ToString());
            sb.Append(KullaniciAdi + " tarafindan, " + tarih.Day + "/" + tarih.Month + "/" + tarih.Year + " tarihinde yazilmistir.");
            if (KullaniciPuanAraligi != System.DBNull.Value)
            {
                sb.Append(" (" + KullaniciAdi + " hocadan " + KullaniciPuanAraligi.ToString() + "/5 not almis.)");
            }
            return sb.ToString();
        }
        catch
        {
            return "";
        }
    }

    protected string YorumBaslikGenelPuanResmiOlustur(object GenelPuan)
    {
        try
        {
            if (GenelPuan != System.DBNull.Value && Convert.ToInt32(GenelPuan.ToString()) >0)
            {
                int yildizGenisligi = (int)Math.Round((Convert.ToInt32(GenelPuan.ToString()) * 20) * ((float)84 / 100));
                StringBuilder sb = new StringBuilder();
                sb.Append(" <ul class=\"star\" id=\"star1\"> ");
                sb.Append(" <li id=\"puan1\" style=\"BACKGROUND: url('App_Themes/Default/Images/stars.gif') left 25px; FONT-SIZE: 1px; width:" + yildizGenisligi + "px;\" > ");
                sb.Append(" </li></ul> ");
                return sb.ToString();
            }
            else
            {
                return "";
            }
        }
        catch
        {
            return "";
        }
    }

    protected void yorumSev_click(object sender, EventArgs e)
    {
        Literal ltrYorumPuanDurumu = ((LinkButton)sender).Parent.FindControl("yorumPuanDurumu") as Literal;
        if (!BaseUserControl.IsLoggedIn())
        {
            ltrYorumPuanDurumu.Text = "Puan verebilmek icin uye girisi yapmalisiniz!";
            return;
        }
        Literal ltrYorumPuan = ((LinkButton)sender).Parent.FindControl("yorumPuan") as Literal;
        HiddenField hiddenField = ((LinkButton)sender).FindControl("yorumID") as HiddenField;
        int yorumID = Convert.ToInt32(hiddenField.Value);
        int[] result = BasePage.YorumPuanVer(true, session.KullaniciID, yorumID, Enums.YorumTipi.DersYorum);
        if (result == null || result.Length!= 2) //Bir hata olustu
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
        if (!BaseUserControl.IsLoggedIn())
        {
            ltrYorumPuanDurumu.Text = "Puan verebilmek icin uye girisi yapmalisiniz!";
            return;
        }
        Literal ltrYorumPuan = ((LinkButton)sender).Parent.FindControl("yorumPuan") as Literal;
        HiddenField hiddenField = ((LinkButton)sender).FindControl("yorumID") as HiddenField;
        int yorumID = Convert.ToInt32(hiddenField.Value);
        int[] result = BasePage.YorumPuanVer(false, session.KullaniciID, yorumID, Enums.YorumTipi.DersYorum);
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



