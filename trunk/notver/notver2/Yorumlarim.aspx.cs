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

public partial class Yorumlarim : BasePage
{
    public Enums.YorumTipi YorumTipi
    {
        get
        {
            object o = this.ViewState["_YorumTipi"];
            if (o == null)
                return Enums.YorumTipi.Gecersiz;
            else
                return (Enums.YorumTipi)o;
        }

        set
        {
            this.ViewState["_YorumTipi"] = value;
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ltrScript.Text = "";
        if (!Page.IsPostBack)
        {
            PanelleriSakla();
            btnDersYorumlarim.Enabled = true;
            btnHocaYorumlarim.Enabled = true;
            btnOkulYorumlarim.Enabled = true;

            if(session.KullaniciAktifYorumSayisi >= 0)
            {
                lblYorumOzeti.Text = "Görüntülenen <strong>" + session.KullaniciAktifYorumSayisi + "</strong> yorumunuz bulunmaktadır";
            }
        }
        if (YorumTipi == Enums.YorumTipi.Gecersiz)
        {
            if (Query.GetInt("HocaID") > 0)
            {
                HocaYorumlariniGoster(null, null);
            }
            else if (Query.GetInt("OkulID") > 0)
            {
                OkulYorumlariniGoster(null, null);
            }
            else if (Query.GetInt("DersID") > 0)
            {
                DersYorumlariniGoster(null, null);
            }
        }
        else
        {
            switch (YorumTipi)
            {
                case Enums.YorumTipi.DersYorum:
                    DersYorumlariniGoster(null, null);
                    break;
                case Enums.YorumTipi.OkulYorum:
                    OkulYorumlariniGoster(null, null);
                    break;
                case Enums.YorumTipi.HocaYorum:
                    HocaYorumlariniGoster(null, null);
                    break;
                default:
                    break;
            }
        }
    }

    protected void HocaYorumlariniGoster(object sender, EventArgs e)
    {
        YorumTipi = Enums.YorumTipi.HocaYorum;
        HocaYorumlariniDoldur();
        btnHocaYorumlarim.Enabled = false;
        btnDersYorumlarim.Enabled = true;
        btnOkulYorumlarim.Enabled = true;
        btnHocaYorumlarim.CssClass = "bold";
        btnDersYorumlarim.CssClass = "";
        btnOkulYorumlarim.CssClass = "";
    }

    protected void OkulYorumlariniGoster(object sender, EventArgs e)
    {
        YorumTipi = Enums.YorumTipi.OkulYorum;
        OkulYorumlariniDoldur();
        btnHocaYorumlarim.Enabled = true;
        btnDersYorumlarim.Enabled = true;
        btnOkulYorumlarim.Enabled = false;
        btnHocaYorumlarim.CssClass = "";
        btnDersYorumlarim.CssClass = "";
        btnOkulYorumlarim.CssClass = "bold";
    }

    protected void DersYorumlariniGoster(object sender, EventArgs e)
    {
        YorumTipi = Enums.YorumTipi.DersYorum;
        DersYorumlariniDoldur();
        btnHocaYorumlarim.Enabled = true;        
        btnDersYorumlarim.Enabled = false;
        btnOkulYorumlarim.Enabled = true;
        btnHocaYorumlarim.CssClass = "";
        btnDersYorumlarim.CssClass = "bold";
        btnOkulYorumlarim.CssClass = "";
    }

    protected void DersYorumSil(object sender, EventArgs e)
    {
        HiddenField yorumIDField = ((ImageButton)sender).FindControl("yorumID") as HiddenField;
        if (yorumIDField == null || !Util.GecerliSayi(yorumIDField.Value))
        {
            return;
        }
        int yorumID = Convert.ToInt32(yorumIDField.Value);
        bool sonuc = Genel.YorumSil(session.KullaniciID, Enums.YorumTipi.DersYorum, yorumID, true);
        if (sonuc)
        {
            ltrScript.Text = "<script type='text/javascript'>alert('Yorumunuz basariyla silinmistir');</script>";
        }
        else
        {
            ltrScript.Text = "<script type='text/javascript'>alert('Bir hata olustu, lutfen tekrar deneyin');</script>";
        }
    }
    
    protected void OkulYorumSil(object sender, EventArgs e)
    {
        HiddenField yorumIDField = ((ImageButton)sender).FindControl("yorumID") as HiddenField;
        if (yorumIDField == null || !Util.GecerliSayi(yorumIDField.Value))
        {
            return;
        }
        int yorumID = Convert.ToInt32(yorumIDField.Value);
        bool sonuc = Genel.YorumSil(session.KullaniciID, Enums.YorumTipi.OkulYorum, yorumID, true);
        if (sonuc)
        {
            ltrScript.Text = "<script type='text/javascript'>alert('Yorumunuz basariyla silinmistir');</script>";
        }
        else
        {
            ltrScript.Text = "<script type='text/javascript'>alert('Bir hata olustu, lutfen tekrar deneyin');</script>";
        }
    }

    protected void HocaYorumSil(object sender, EventArgs e)
    {
        HiddenField yorumIDField = ((ImageButton)sender).FindControl("yorumID") as HiddenField;
        if (yorumIDField == null || !Util.GecerliSayi(yorumIDField.Value))
        {
            return;
        }
        int yorumID = Convert.ToInt32(yorumIDField.Value);
        bool sonuc = Genel.YorumSil(session.KullaniciID, Enums.YorumTipi.HocaYorum, yorumID, true);
        if (sonuc)
        {
            ltrScript.Text = "<script type='text/javascript'>alert('Yorumunuz basariyla silinmistir');</script>";
        }
        else
        {
            ltrScript.Text = "<script type='text/javascript'>alert('Bir hata olustu, lutfen tekrar deneyin');</script>";
        }
    }

    protected void HocaYorumlariniDoldur()
    {
        PanelleriSakla();
        DataTable dtHocaYorumlarim = Hocalar.KullaniciHocaYorumlariniDondur(session.KullaniciID);
        if (dtHocaYorumlarim != null)
        {
            if (dtHocaYorumlarim.Rows.Count > 0)
            {
                repeaterHocaYorumlarim.DataSource = dtHocaYorumlarim;
                repeaterHocaYorumlarim.DataBind();
                pnlHocaYorumlarim.Visible = true;
            }
            else
            {
                pnlYorumYok.Visible = true;
            }
        }
        else
        {
            pnlHata.Visible = true;
        }
    }

    protected void OkulYorum_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            System.Data.DataRowView drv = (System.Data.DataRowView)(e.Item.DataItem);
            if (Util.GecerliStringSayi(drv.Row.ItemArray[4]))
            {
                int yorumDurumuInt = Convert.ToInt32(drv.Row.ItemArray[4]);
                Enums.YorumDurumu yorumDurumu = (Enums.YorumDurumu)yorumDurumuInt;
                switch (yorumDurumu)
                {
                    case Enums.YorumDurumu.KullaniciTarafindanSilinmis:
                    case Enums.YorumDurumu.SistemTarafindanSilinmis:
                        //Silinmisse guncelle ve sil tuslarini sakla
                        //asp:Link kullanip enabled diyerek saklamak daha temiz olurdu
                        Literal ltrHack = e.Item.FindControl("ltrHack") as Literal;
                        Literal ltrHack2 = e.Item.FindControl("ltrHack2") as Literal;
                        ltrHack.Text = "<span style='display:none;'>";
                        ltrHack2.Text = "</span>";
                        ImageButton btnSil = e.Item.FindControl("btnOkulYorumSil") as ImageButton;
                        btnSil.Visible = false;
                        break;
                    case Enums.YorumDurumu.OnayBekliyor:
                        break;
                    case Enums.YorumDurumu.Onaylanmis:
                        break;
                }
            }
        }
    }

    protected void DersYorum_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            System.Data.DataRowView drv = (System.Data.DataRowView)(e.Item.DataItem);
            if (Util.GecerliStringSayi(drv.Row.ItemArray[4]))
            {
                int yorumDurumuInt = Convert.ToInt32(drv.Row.ItemArray[4]);
                Enums.YorumDurumu yorumDurumu = (Enums.YorumDurumu)yorumDurumuInt;
                switch (yorumDurumu)
                {
                    case Enums.YorumDurumu.KullaniciTarafindanSilinmis:
                    case Enums.YorumDurumu.SistemTarafindanSilinmis:
                        //Silinmisse guncelle ve sil tuslarini sakla
                        //asp:Link kullanip enabled diyerek saklamak daha temiz olurdu
                        Literal ltrHack = e.Item.FindControl("ltrHack") as Literal;
                        Literal ltrHack2 = e.Item.FindControl("ltrHack2") as Literal;
                        ltrHack.Text = "<span style='display:none;'>";
                        ltrHack2.Text = "</span>";
                        ImageButton btnSil = e.Item.FindControl("btnDersYorumSil") as ImageButton;
                        btnSil.Visible = false;
                        break;
                    case Enums.YorumDurumu.OnayBekliyor:
                        break;
                    case Enums.YorumDurumu.Onaylanmis:
                        break;
                }
            }
        }
    }

    protected void HocaYorum_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            System.Data.DataRowView drv = (System.Data.DataRowView)(e.Item.DataItem);
            if(Util.GecerliStringSayi(drv.Row.ItemArray[4]))
            {
                int yorumDurumuInt = Convert.ToInt32(drv.Row.ItemArray[4]);
                Enums.YorumDurumu yorumDurumu = (Enums.YorumDurumu)yorumDurumuInt;
                switch (yorumDurumu)
                {
                    case Enums.YorumDurumu.KullaniciTarafindanSilinmis:
                    case Enums.YorumDurumu.SistemTarafindanSilinmis:
                        //Silinmisse guncelle tusunu sakla
                        //asp:Link kullanip enabled diyerek saklamak daha temiz olurdu
                        Literal ltrHack = e.Item.FindControl("ltrHack") as Literal;
                        Literal ltrHack2 = e.Item.FindControl("ltrHack2") as Literal;
                        ltrHack.Text = "<span style='display:none;'>";
                        ltrHack2.Text = "</span>";
                        ImageButton btnSil = e.Item.FindControl("btnHocaYorumSil") as ImageButton;
                        btnSil.Visible = false;
                        break;
                    case Enums.YorumDurumu.OnayBekliyor:
                        break;
                    case Enums.YorumDurumu.Onaylanmis:
                        break;
                }
            }
        }
    }

    protected void PanelleriSakla()
    {
        pnlHocaYorumlarim.Visible = false;
        pnlOkulYorumlarim.Visible = false;
        pnlDersYorumlarim.Visible = false;
        pnlYorumYok.Visible = false;
        pnlHata.Visible = false;
    }
    protected void OkulYorumlariniDoldur()
    {
        PanelleriSakla();
        DataTable dtOkulYorumlarim = Okullar.KullaniciOkulYorumlariniDondur(session.KullaniciID);
        if (dtOkulYorumlarim != null)
        {
            if (dtOkulYorumlarim.Rows.Count > 0)
            {
                repeaterOkulYorumlarim.DataSource = dtOkulYorumlarim;
                repeaterOkulYorumlarim.DataBind();
                pnlOkulYorumlarim.Visible = true;
            }
            else
            {
                pnlYorumYok.Visible = true;
            }
        }
        else
        {
            pnlHata.Visible = true;
        }
    }

    protected void DersYorumlariniDoldur()
    {
        PanelleriSakla();
        DataTable dtDersYorumlarim = Dersler.KullaniciDersYorumlariniDondur(session.KullaniciID);
        if (dtDersYorumlarim != null)
        {
            if (dtDersYorumlarim.Rows.Count > 0)
            {
                repeaterDersYorumlarim.DataSource = dtDersYorumlarim;
                repeaterDersYorumlarim.DataBind();
                pnlDersYorumlarim.Visible = true;
            }
            else
            {
                pnlYorumYok.Visible = true;
            }
        }
        else
        {
            pnlHata.Visible = true;
        }
    }

    protected string YorumDurumunuDondur(object YorumDurumu)
    {
        if (YorumDurumu != null)
        {
            int yorumDurumuInt = Convert.ToInt32(YorumDurumu);
            Enums.YorumDurumu yorumDurumu = (Enums.YorumDurumu)yorumDurumuInt;
            switch (yorumDurumu)
            {
                case Enums.YorumDurumu.KullaniciTarafindanSilinmis:
                    return "<span style='color:Red;' title='Silmissiniz''>X</span>";
                case Enums.YorumDurumu.SistemTarafindanSilinmis:
                    return "<span style='color:Red;' title='Biz sildik'>X</span>";
                case Enums.YorumDurumu.OnayBekliyor:
                    return "<span style='color:Gray;' title='Onay bekliyor'>...</span>";
                case Enums.YorumDurumu.Onaylanmis:
                    return "<span style='color:Green;' title='Goruntuleniyor'>+</span>";
            }
        }
        return "";
    }

    protected string YorumKisalt(object Yorum)
    {
        if (Util.GecerliString(Yorum))
        {
            string yorumStr = Yorum.ToString();
            if (yorumStr.Length > 100)
            {
                return yorumStr.Substring(0, 97) + "...";
            }
            else
            {
                return yorumStr;
            }
        }
        return "";
    }
}
