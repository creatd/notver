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

public partial class UserControls_DersDosyalar : BaseUserControl
{
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
                SayfaBoyutu = Convert.ToInt32(dropSayfaBoyutu.SelectedValue);
                GridDoldur();
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
        if (DosyaKategoriTipi >= 0 && DosyaKategoriTipi < (int)Enums.DosyaKategoriTipi.Hepsi)
        {
            dt = Dersler.DersDosyalariniDondur(Query.GetInt("DersID"), (Enums.DosyaKategoriTipi)DosyaKategoriTipi);
        }
        else if (DosyaKategoriTipi == (int)Enums.DosyaKategoriTipi.Hepsi)
        {
            dt = Dersler.DersDosyalariniDondur(Query.GetInt("DersID"));
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
        DosyaKategoriTipi = Convert.ToInt32(e.CommandArgument);
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
                //1 isim 2 adres
                if (string.IsNullOrEmpty(drv.Row.ItemArray[1].ToString()))
                {
                    e.Item.Cells[0].Text = drv.Row.ItemArray[2].ToString();
                    e.Item.Cells[3].Text = drv.Row.ItemArray[2].ToString();
                }
                e.Item.Cells[3].Text = DosyaAdresDondur(e.Item.Cells[3].Text,dosyaTooltip,Convert.ToInt32(drv.Row.ItemArray[7]));
            }
        }
    }

    protected string DosyaTooltipDondur(string Aciklama, string IndirilmeSayisi)
    {
        string tooltip = "";
        if(!string.IsNullOrEmpty(Aciklama))
            tooltip += "\"" + Aciklama + "\"";
        tooltip += "<br /><b>" + IndirilmeSayisi + "</b> kere indirilmis";
        return tooltip;
    }

    protected string DosyaAdresDondur(string dosyaAdres, string dosyaTooltip, int dosyaKategoriTipi)
    {
        if (DosyaKategoriTipi >=0)
        {
            return "<a href='" + Page.ResolveUrl("~/Dosyalar/Dersler/" + Query.GetInt("DersID").ToString() + "/" + dosyaKategoriTipi.ToString() + "/" + dosyaAdres.Trim()) + 
                "' tooltip='" + dosyaTooltip +"'><img src='" +
                Page.ResolveUrl("~/App_Themes/Default/Images/diger/disket.gif") + "' /></a>";
        }
        return "";
    }
}
