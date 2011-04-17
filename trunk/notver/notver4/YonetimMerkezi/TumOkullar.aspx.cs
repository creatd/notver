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


public partial class Admin_TumOkullar : BasePage
{
    protected void Page_Prerender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GridDoldur();
            // Okullari dropdown'ini doldur
            drpOkullar.Items.Add(new ListItem("-", "-1")); //Okul secilir secilmez dersler dolduruldugu icin - ile basliyoruz
            foreach (DataRow dr in session.dtOkullar.Rows)
            {
                drpOkullar.Items.Add(new ListItem(dr["ISIM"].ToString(), dr["OKUL_ID"].ToString()));
            }
        }
    }

    protected void grid_PageIndexChanged(object sender, DataGridPageChangedEventArgs e)
    {
        gridOkullar.CurrentPageIndex = e.NewPageIndex;
        GridDoldur();
    }

    protected void gridBolumler_PageIndexChanged(object sender, DataGridPageChangedEventArgs e)
    {
        gridBolumler.CurrentPageIndex = e.NewPageIndex;
        OkulSecildi(null,null);
    }

    protected void GridDoldur()
    {
        DataTable dtOkullar = Okullar.Admin_OkullariDondur();
        if (dtOkullar != null)
        {
            if (dtOkullar.Rows.Count < gridOkullar.CurrentPageIndex * gridOkullar.PageSize + 1)
            {
                gridOkullar.CurrentPageIndex = 0;
            }
            gridOkullar.DataSource = dtOkullar;
            gridOkullar.DataBind();
        }
        else
        {
            gridOkullar.DataSource = null;
            gridOkullar.DataBind();
        }
    }

    protected void Edit(object sender, DataGridCommandEventArgs e)
    {
        ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[10].Visible = false;

        gridOkullar.EditItemIndex = e.Item.ItemIndex;
        GridDoldur();
    }

    protected void Edit2(object sender, DataGridCommandEventArgs e)
    {
        ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[5].Visible = false;

        gridBolumler.EditItemIndex = e.Item.ItemIndex;
        OkulSecildi(null,null);
    }

    protected void Cancel(object sender, DataGridCommandEventArgs e)
    {
        ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[10].Visible = false;

        gridOkullar.EditItemIndex = -1;
        GridDoldur();
    }

    protected void Cancel2(object sender, DataGridCommandEventArgs e)
    {
        ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[5].Visible = false;

        gridBolumler.EditItemIndex = -1;
        OkulSecildi(null,null);
    }

    protected void Update2(object sender, DataGridCommandEventArgs e)
    {
        try
        {
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[5].Visible = false;

            string ID = e.Item.Cells[0].Text;
            string isActive = (e.Item.Cells[1].Controls[0] as TextBox).Text;
            string isim = (e.Item.Cells[2].Controls[0] as TextBox).Text;

            int BolumID = Convert.ToInt32(ID);
            bool IsActive = Convert.ToBoolean(isActive);
            if (!string.IsNullOrEmpty(isim))
            {
                if (isim.Length > 256)
                    isim = isim.Substring(0, 256);
            }
            else
            {
                lblDurum1.Text = "Isim eksik";
                lblDurum2.Text = "Isim eksik";
                return;
            }
             
            if (Okullar.BolumGuncelle(BolumID, IsActive, isim))
            {
                lblDurum1.Text = "Bolum guncellendi";
                lblDurum2.Text = "Bolum guncellendi";
            }
            else
            {
                lblDurum1.Text = "Bir hata olustu";
                lblDurum2.Text = "Bir hata olustu";
            }
        }
        catch (Exception ex)
        {
            lblDurum1.Text = "Hata (detay en altta)";
            lblDurum2.Text = "Hata : " + ex.ToString();
        }
        gridBolumler.EditItemIndex = -1;
        OkulSecildi(null,null);
    }

    protected void Update(object sender, DataGridCommandEventArgs e)
    {
        try
        {
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[10].Visible = false;

            string ID = e.Item.Cells[0].Text;
            string isActive = (e.Item.Cells[1].Controls[0] as TextBox).Text;
            string isim = (e.Item.Cells[2].Controls[0] as TextBox).Text;
            string adres = (e.Item.Cells[3].Controls[0] as TextBox).Text;
            string kurulusTarihi = (e.Item.Cells[4].Controls[0] as TextBox).Text;
            string ogrenciSayisi = (e.Item.Cells[5].Controls[0] as TextBox).Text;
            string akademikSayisi = (e.Item.Cells[6].Controls[0] as TextBox).Text;
            string webAdresi = (e.Item.Cells[7].Controls[0] as TextBox).Text;

            int OkulID = Convert.ToInt32(ID);
            bool IsActive = Convert.ToBoolean(isActive);
            if (!string.IsNullOrEmpty(isim))
            {
                if(isim.Length > 100)
                    isim = isim.Substring(0, 100);
            }
            else
            {
                lblDurum1.Text = "Isim eksik";
                lblDurum2.Text = "Isim eksik";
                return;
            }
            if (!string.IsNullOrEmpty(adres))
            {
                if(adres.Length > 50)
                    adres = adres.Substring(0, 50);
            }
            if (!string.IsNullOrEmpty(webAdresi))
            {
                if(webAdresi.Length > 250)
                    webAdresi = webAdresi.Substring(0, 250);
            }
            int KurulusTarihi = -1;
            if(Util.GecerliSayi(kurulusTarihi))
            {
                KurulusTarihi = Convert.ToInt32(kurulusTarihi);
            }
            int OgrenciSayisi = -1;
            if(Util.GecerliSayi(ogrenciSayisi))
            {
                OgrenciSayisi = Convert.ToInt32(ogrenciSayisi);
            }
            int AkademikSayisi = -1;
            if(Util.GecerliSayi(akademikSayisi))
            {
                AkademikSayisi = Convert.ToInt32(akademikSayisi);
            }
            if (Okullar.OkulGuncelle(OkulID,IsActive, isim, adres, KurulusTarihi, OgrenciSayisi,
                AkademikSayisi, webAdresi))
            {
                lblDurum1.Text = "Okul guncellendi";
                lblDurum2.Text = "Okul guncellendi";
            }
            else
            {
                lblDurum1.Text = "Bir hata olustu";
                lblDurum2.Text = "Bir hata olustu";
            }            
        }
        catch (Exception ex)
        {
            lblDurum1.Text = "Hata (detay en altta)";
            lblDurum2.Text = "Hata : " + ex.ToString();
        }
        gridOkullar.EditItemIndex = -1;
        GridDoldur();
    }

    protected void ItemCommand2(object sender, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Sil1")
        {
            DataGridItemCollection coll = ((System.Web.UI.WebControls.DataGrid)(sender)).Items;
            for (int i = 0; i < coll.Count; i++)
            {
                if (i != e.Item.DataSetIndex)
                {
                    coll[i].Controls[5].Visible = false;
                }
                else
                {
                    coll[i].Controls[5].Visible = true;
                }
            }
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[5].Visible = true;
        }
        else if (e.CommandName == "Sil2")
        {
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[5].Visible = false;

            string ID = e.Item.Cells[0].Text;
            if (Util.GecerliSayi(ID))
            {
                int bolumID = Convert.ToInt32(ID);
                if (Okullar.BolumSil(bolumID))
                {
                    lblDurum1.Text = "Bolum silindi";
                    lblDurum2.Text = "Bolum silindi";
                }
                else
                {
                    lblDurum1.Text = "Bolum silerken bir hata olustu";
                    lblDurum2.Text = "Bolum silerken bir hata olustu";
                }
            }
            else
            {
                lblDurum1.Text = "Bolum silerken bir hata olustu (ID'yi alamadim)";
                lblDurum2.Text = "Bolum silerken bir hata olustu (ID'yi alamadim)";
            }
            OkulSecildi(null,null);
        }
    }

    protected void ItemCommand(object sender, DataGridCommandEventArgs e)
    {
        if(e.CommandName == "Sil1")
        {
            DataGridItemCollection coll = ((System.Web.UI.WebControls.DataGrid)(sender)).Items;
            for(int i=0 ; i<coll.Count ; i++)
            {
                if (i != e.Item.DataSetIndex)
                {
                    coll[i].Controls[10].Visible = false;
                }
                else
                {
                    coll[i].Controls[10].Visible = true;
                }
            }
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[10].Visible = true;
        }
        else if(e.CommandName == "Sil2")
        {
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[10].Visible = false;

            string ID = e.Item.Cells[0].Text;
            if (Util.GecerliSayi(ID))
            {
                int okulID = Convert.ToInt32(ID);
                if (Okullar.OkulSil(okulID))
                {
                    lblDurum1.Text = "Okul silindi";
                    lblDurum2.Text = "Okul silindi";
                    session.dtOkullar = null;   //Sifirlanmasi icin
                }
                else
                {
                    lblDurum1.Text = "Okul silerken bir hata olustu";
                    lblDurum2.Text = "Okul silerken bir hata olustu";
                }
            }
            else
            {
                lblDurum1.Text = "Okul silerken bir hata olustu (ID'yi alamadim)";
                lblDurum2.Text = "Okul silerken bir hata olustu (ID'yi alamadim)";
            }
            GridDoldur();
        }
    }

    protected void OkulSecildi(object sender, EventArgs e)
    {
        if (Util.Gecerli(drpOkullar.SelectedValue) && Convert.ToInt32(drpOkullar.SelectedValue) >= 0)
        {
            DataTable dtBolumler = Okullar.Admin_BolumleriDondur(Convert.ToInt32(drpOkullar.SelectedValue));
            if (dtBolumler != null)
            {
                gridBolumler.DataSource = dtBolumler;
                gridBolumler.DataBind();
            }
            else
            {
                gridBolumler.DataSource = null;
                gridBolumler.DataBind();
            }
        }
        else
        {
            gridBolumler.DataSource = null;
            gridBolumler.DataBind();
        }
    }
}


