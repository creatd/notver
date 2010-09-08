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
            pnlDersYorumlarim.Visible = false;
            pnlHocaYorumlarim.Visible = false;
            pnlOkulYorumlarim.Visible = false;
            pnlYorumYok.Visible = false;
            btnDersYorumlarim.Enabled = true;
            btnHocaYorumlarim.Enabled = true;
            btnOkulYorumlarim.Enabled = true;

            if(session.KullaniciAktifYorumSayisi > 0)
            {
                lblYorumOzeti.Text = session.KullaniciAktifYorumSayisi + " aktif yorumunuz bulunmaktadir";
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
    }

    protected void OkulYorumlariniGoster(object sender, EventArgs e)
    {
        YorumTipi = Enums.YorumTipi.OkulYorum;
        OkulYorumlariniDoldur();
        btnHocaYorumlarim.Enabled = true;
        btnDersYorumlarim.Enabled = true;
        btnOkulYorumlarim.Enabled = false;
    }

    protected void DersYorumlariniGoster(object sender, EventArgs e)
    {
        YorumTipi = Enums.YorumTipi.DersYorum;
        DersYorumlariniDoldur();
        btnHocaYorumlarim.Enabled = true;
        btnDersYorumlarim.Enabled = false;
        btnOkulYorumlarim.Enabled = true;
    }


    protected void HocaYorumGuncelle(object sender, EventArgs e)
    {
    }

    protected void HocaYorumSil(object sender, EventArgs e)
    {
        HiddenField yorumIDField = ((LinkButton)sender).FindControl("yorumID") as HiddenField;
        HiddenField yorumDurumField = ((LinkButton)sender).FindControl("yorumDurum") as HiddenField;
        if (yorumIDField == null || !Util.GecerliSayi(yorumIDField.Value) || yorumDurumField == null || !Util.GecerliSayi(yorumDurumField.Value))
        {
            return;
        }
        int yorumID = Convert.ToInt32(yorumIDField.Value);
        int yorumDurum = Convert.ToInt32(yorumDurumField.Value);
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
                repeaterHocaYorumlarim.DataSource = dtOkulYorumlarim;
                repeaterHocaYorumlarim.DataBind();
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
                repeaterHocaYorumlarim.DataSource = dtDersYorumlarim;
                repeaterHocaYorumlarim.DataBind();
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

}
