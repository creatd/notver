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
using Amazon.S3;
using System.Collections.Specialized;
using Amazon.S3.Model;

public partial class UserControls_DersDosyalar : BaseUserControl
{
    
    public bool TumKategoriler
    {
        get { 
            object o = this.ViewState["_TumKategoriler"];
            if (o == null)
                return false;
            else
                return (bool)o; 
        }
        set { this.ViewState["_TumKategoriler"] = value; }
    }

    public int DosyaKategoriTipi
    {
        get
        {
            object o = this.ViewState["_DosyaKategoriTipi"];
            if (o == null)
                return -1;
            else
                return (int)o;
        }

        set
        {
            this.ViewState["_DosyaKategoriTipi"] = value;
        }
    }

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
            GridDoldur();
        }
    }

    protected void SayfaBoyutuDegisti(object sender, EventArgs e)
    {
        SayfaBoyutu = Convert.ToInt32(dropSayfaBoyutu.SelectedValue);
        MevcutSayfa = 1;
        GridDoldur();
    }

    protected void OncekiSayfayaGit(object sender, EventArgs e)
    {
        MevcutSayfa -= 1;
        GridDoldur();
    }

    protected void SonrakiSayfayaGit(object sender, EventArgs e)
    {
        MevcutSayfa += 1;
        GridDoldur();
    }

    protected void rptPager_DataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton but = e.Item.FindControl("lnkSayfa") as LinkButton;
        if (but != null && Convert.ToInt32(but.CommandArgument) == MevcutSayfa)
        {
            but.Enabled = false;
        }
    }

    protected void Page_Prerender(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                MevcutSayfa = 1;
                DosyaKategoriTipi = (int)Enums.DosyaKategoriTipi.SinavVeCozum;
                TumKategoriler = true;
                SayfaBoyutu = Convert.ToInt32(dropSayfaBoyutu.SelectedValue);
                GridDoldur();
            }

            butKlasor0.Enabled = true;
            butKlasor1.Enabled = true;
            butKlasor2.Enabled = true;
            butKlasor3.Enabled = true;
            butKlasor4.Enabled = true;
            butKlasor5.Enabled = true;
            butKlasor6.Enabled = true;
            butKlasor0.CssClass = "";
            butKlasor1.CssClass = "";
            butKlasor2.CssClass = "";
            butKlasor3.CssClass = "";
            butKlasor4.CssClass = "";
            butKlasor5.CssClass = "";
            butKlasor6.CssClass = "";
            if (TumKategoriler)
            {
                butKlasor6.Enabled = false;
                butKlasor6.CssClass = "secili";
            }
            else if(DosyaKategoriTipi == 0)
            {
                butKlasor0.Enabled = false;
                butKlasor0.CssClass = "secili";
            }
            else if (DosyaKategoriTipi == 1)
            {
                butKlasor1.Enabled = false;
                butKlasor1.CssClass = "secili";
            }
            else if (DosyaKategoriTipi == 2)
            {
                butKlasor2.Enabled = false;
                butKlasor2.CssClass = "secili";
            }
            else if (DosyaKategoriTipi == 3)
            {
                butKlasor3.Enabled = false;
                butKlasor3.CssClass = "secili";
            }
            else if (DosyaKategoriTipi == 4)
            {
                butKlasor4.Enabled = false;
                butKlasor4.CssClass = "secili";
            }
            else if (DosyaKategoriTipi == 5)
            {
                butKlasor5.Enabled = false;
                butKlasor5.CssClass = "secili";
            }

        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
        }
    }

    protected void GridDoldur()
    {
        DataTable dt = null;
        if (TumKategoriler)
        {
            dt = Dersler.DersDosyalariniDondur(Query.GetInt("DersID"));
        }
        else
        {
            dt = Dersler.DersDosyalariniDondur(Query.GetInt("DersID"), (Enums.DosyaKategoriTipi)DosyaKategoriTipi);
        }

        if (dt!= null && dt.Rows.Count > 0)
        {
            pnlDosyalar.Visible = true;
            pnlDosyaYok.Visible = false;

            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = dt.DefaultView;
            pds.AllowPaging = true;

            if (SayfaBoyutu == 0)   //Hepsini goster
            {
                pds.PageSize = dt.Rows.Count;
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

            gridDosyalar.DataSource = pds;
            gridDosyalar.DataBind();
        }
        else
        {
            pnlDosyalar.Visible = false;
            pnlDosyaYok.Visible = true;
        }
    }

    protected void KlasorSec(object sender, CommandEventArgs e)
    {
        int argument = Convert.ToInt32(e.CommandArgument);
        if(argument == 6)   //Hepsi secenegi
        {
            TumKategoriler = true;
        }
        else
        {
            TumKategoriler = false;
            DosyaKategoriTipi = argument;
        }
        
        MevcutSayfa = 1;
        SayfaBoyutu = Convert.ToInt32(dropSayfaBoyutu.SelectedValue);
        GridDoldur();
    }

    protected void gridDosyalar_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (e.Item.Cells.Count > 3)
            {
                string dosyaTooltip = "";
                System.Data.DataRowView drv = (System.Data.DataRowView)(e.Item.DataItem);
                if( drv.Row.ItemArray.Length > 5 )
                {                    
                    dosyaTooltip = DosyaTooltipDondur(drv.Row.ItemArray[6].ToString() , drv.Row.ItemArray[4].ToString());
                }
                /*//1 isim 2 adres
                if (string.IsNullOrEmpty(drv.Row.ItemArray[1].ToString()))
                {
                    e.Item.Cells[0].Text = drv.Row.ItemArray[2].ToString();
                    //e.Item.Cells[3].Text = drv.Row.ItemArray[2].ToString();
                }*/
                //Tooltip ekle linke
                //string imgText = ((System.Web.UI.WebControls.LinkButton)(((System.Web.UI.WebControls.DataGridLinkButton)(e.Item.Cells[4].Controls[0])))).Text;
                //((System.Web.UI.WebControls.LinkButton)(((System.Web.UI.WebControls.DataGridLinkButton)(e.Item.Cells[4].Controls[0])))).Text = imgText.Replace("<img ", "img tooltip='" + dosyaTooltip + "' ");
                //e.Item.Cells[4].Text = "deneme";
                //e.Item.Cells[4].Controls[0];
                string imgText = ((System.Web.UI.WebControls.LinkButton)(e.Item.Cells[4].Controls[0])).Text;
                ((System.Web.UI.WebControls.LinkButton)(e.Item.Cells[4].Controls[0])).Text = imgText.Replace("<img ", "<img tooltip='" + dosyaTooltip + "' ");
            }
        }
    }

    protected void gridDosyalar_ItemCommand(object sender, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DosyaIndir")
        {
            string dosya_anahtar = ((System.Web.UI.WebControls.TableRow)(e.Item)).Cells[3].Text;

            //Amazon'dan 1 saat sureli URL olustur
            NameValueCollection appConfig = ConfigurationManager.AppSettings;
            string accessKeyID = appConfig["AWSAccessKey"];
            string secretAccessKeyID = appConfig["AWSSecretKey"];
            string bucketName = appConfig["AWSBucketName"];

            using (AmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(accessKeyID, secretAccessKeyID))
            {
                GetPreSignedUrlRequest url_request = new GetPreSignedUrlRequest()
                {
                    BucketName = bucketName,
                    Key = dosya_anahtar,
                    Protocol = Protocol.HTTP,
                    Expires = DateTime.Now.AddHours(1)
                };
                string url = client.GetPreSignedURL(url_request);
                string response = "<script>window.open('" + url + "','_blank')</script>";
                ltrScript.Text = response;
                //Response.Write(response);
            }

        }
    }

    protected string DosyaTooltipDondur(string Aciklama, string IndirilmeSayisi)
    {
        string tooltip = "";
        if(!string.IsNullOrEmpty(Aciklama))
            tooltip += "\"" + Aciklama + "\"<br />";
        tooltip += "<b>" + IndirilmeSayisi + "</b> kere indirilmis";
        return tooltip;
    }
    
    /*protected string DosyaAdresDondur(string dosyaAdres, string dosyaTooltip, int dosyaKategoriTipi)
    {
        if (DosyaKategoriTipi >=0)
        {
            return "<a href='" + Page.ResolveUrl("~/Dosyalar/Dersler/" + Query.GetInt("DersID").ToString() + "/" + dosyaKategoriTipi.ToString() + "/" + dosyaAdres.Trim()) + 
                "' tooltip='" + dosyaTooltip +"'><img src='" +
                Page.ResolveUrl("~/App_Themes/Default/Images/diger/disket.gif") + "' /></a>";
        }
        return "";
    }*/
}
