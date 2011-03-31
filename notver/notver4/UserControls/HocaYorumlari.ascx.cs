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
        try
        {
            int dummy = 0;
            //int dummy2 = 5 / dummy;
        }
        catch (Exception ex)
        {
            new DivideByZeroException();
            //throw;
        }
        
    }

    protected void Page_Prerender(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Query.GetInt("HocaID") <= 0)
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
        DataTable yorumlar = Hocalar.HocaYorumlariniDondur(Query.GetInt("HocaID"));
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
        if (Util.GecerliSayi(((System.Data.DataRowView)(e.Item.DataItem)).Row["ALKIS_PUANI"]))
        {
            int alkis_puani = Convert.ToInt32(((System.Data.DataRowView)(e.Item.DataItem)).Row["ALKIS_PUANI"]);
            if(alkis_puani > 0)
            {
                yorumPuan.Text = "+" + alkis_puani + "&nbsp;";
            }
            else
            {
                yorumPuan.Text = alkis_puani + "&nbsp;";
            }
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

    protected string YorumBasligiOlustur(object KullaniciAdi , object KullaniciIsim, object Tarih , object KullaniciPuanAraligi, object DersKodu)
    {
        try
        {
            DateTime tarih = Convert.ToDateTime(Tarih.ToString());
            string str = "";
            if (Util.GecerliString(KullaniciAdi))
            {
                str = KullaniciAdi.ToString(); ;
            }
            else
            {
                str = KullaniciIsim.ToString();
            }
            str += "&nbsp;&nbsp;" + tarih.Day + "/" + tarih.Month + "/" + tarih.Year;
            if (Util.GecerliSayi(KullaniciPuanAraligi))
            {
                Enums.KullaniciPuanAraligi puanAraligi = (Enums.KullaniciPuanAraligi)Convert.ToInt32(KullaniciPuanAraligi);
                str += "&nbsp;&nbsp;&nbsp;&nbsp;<span style='color:#05d4b4'>(Aldigi Not : " + puanAraligi.ToString() + ")</span>";
            }
            if (Util.GecerliString(DersKodu))
            {
                str += "<br /><span style='font-weight:normal; font-size:12px; line-height:150%; display:block; margin-top:7px;'>" + DersKodu.ToString() + "</span>";
            }
            
            return str;
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
                int yildizGenisligi = (int)Math.Round((Convert.ToInt32(GenelPuan.ToString()) * 20) * ((float)100 / 100));
                StringBuilder sb = new StringBuilder();
                sb.Append("<ul class='star' id='star1' title='Kullanıcının verdiği genel puan'> ");
                sb.Append(" <li id='puan1' style=\"BACKGROUND: url('App_Themes/Default/Images/yildizlar2.png') left 34px; FONT-SIZE: 1px; width:" + yildizGenisligi + "px;\" > ");
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
        System.Threading.Thread.Sleep(2000);
        Label lblYorumPuanDurumu = ((ImageButton)sender).Parent.FindControl("yorumPuanDurumu") as Label;
        if (!session.IsLoggedIn)
        {
            lblYorumPuanDurumu.Text = "Puan verebilmek icin üye girişi yapmalısın";
            return;
        }
        Literal ltrYorumPuan = ((ImageButton)sender).Parent.FindControl("yorumPuan") as Literal;
        HiddenField hiddenField = ((ImageButton)sender).FindControl("yorumID") as HiddenField;
        int yorumID = Convert.ToInt32(hiddenField.Value);
        int[] result = Genel.YorumPuanVer(true, session.KullaniciID, yorumID, Enums.YorumTipi.HocaYorum);
        if (result == null || result.Length!= 2) //Bir hata olustu
        {
            lblYorumPuanDurumu.Text = "Bir hata oluştu, lütfen tekrar deneyin";
        }
        else
        {
            if (result[0] == 1) //İlk defa puan verildi.
            {
                lblYorumPuanDurumu.Text = "Puanınız kaydedildi";
            }
            else if (result[0] == 2)    //Daha once puan verilmis. Puan guncellendi.
            {
                lblYorumPuanDurumu.Text = "Puanınız güncellendi";
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
            lblYorumPuanDurumu.Text = "Puan verebilmek için üye girişi yapmalısın";
            return;
        }
        Literal ltrYorumPuan = ((ImageButton)sender).Parent.FindControl("yorumPuan") as Literal;
        HiddenField hiddenField = ((ImageButton)sender).FindControl("yorumID") as HiddenField;
        int yorumID = Convert.ToInt32(hiddenField.Value);
        int[] result = Genel.YorumPuanVer(false, session.KullaniciID, yorumID, Enums.YorumTipi.HocaYorum);
        if (result == null || result.Length != 2) //Bir hata olustu
        {
            lblYorumPuanDurumu.Text = "Bir hata oluştu, lütfen tekrar deneyin";
        }
        else
        {
            if (result[0] == 1) //İlk defa puan verildi.
            {
                lblYorumPuanDurumu.Text = "Puanınız kaydedildi";
            }
            else if (result[0] == 2)    //Daha once puan verilmis. Puan guncellendi.
            {
                lblYorumPuanDurumu.Text = "Puanınız güncellendi";
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



