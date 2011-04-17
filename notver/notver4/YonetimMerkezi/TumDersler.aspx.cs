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


public partial class Admin_TumDersler : BasePage
{
    protected void Page_Prerender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            drpOkullar.Items.Clear();
            drpOkullar2.Items.Clear();
            drpOkullar.Items.Add(new ListItem("-", "-1")); //Okul secilir secilmez dersler dolduruldugu icin - ile basliyoruz
            drpOkullar2.Items.Add(new ListItem("-", "-1")); //Okul secilir secilmez dersler dolduruldugu icin - ile basliyoruz
            foreach (DataRow dr in session.dtOkullar.Rows)
            {
                drpOkullar.Items.Add(new ListItem(dr["ISIM"].ToString(), dr["OKUL_ID"].ToString()));
                drpOkullar2.Items.Add(new ListItem(dr["ISIM"].ToString(), dr["OKUL_ID"].ToString()));
            }
            GridDoldur();
            KayitsizDersleriDoldur();
        }
    }

    protected void grid_PageIndexChanged(object sender, DataGridPageChangedEventArgs e)
    {
        gridDersler.CurrentPageIndex = e.NewPageIndex;
        GridDoldur();
    }

    protected void OkulSecildi(object sender, EventArgs e)
    {
        drpBolumler.Items.Clear();
        drpBolumler.Items.Add(new ListItem("-", "-1"));
        // Okuldaki bolumleri doldur
        if (Util.GecerliSayi(drpOkullar.SelectedValue))
        {
            DataTable dtBolumler = Okullar.Admin_BolumleriDondur(Convert.ToInt32(drpOkullar.SelectedValue));
            if (dtBolumler != null)
            {
                foreach (DataRow dr in dtBolumler.Rows)
                {
                    drpBolumler.Items.Add(new ListItem(dr["ISIM"].ToString(), dr["BOLUM_ID"].ToString()));
                }
            }
        }
        GridDoldur();
    }

    protected void BolumSecildi(object sender, EventArgs e)
    {
        GridDoldur();
    }

    protected void BolumSecildi2(object sender, EventArgs e)
    {
        drpOkulDersler.Items.Clear();
        if (Util.GecerliSayi(drpOkulBolumler.SelectedValue))
        {
            int bolumID = Convert.ToInt32(drpOkulBolumler.SelectedValue);
            if (bolumID >= 0)
            {
                DataTable dtDersler = Dersler.BolumdekiDersleriDondur(bolumID);
                if (dtDersler != null)
                {
                    foreach (DataRow dr in dtDersler.Rows)
                    {
                        drpOkulDersler.Items.Add(new ListItem(dr["KOD"].ToString(), dr["DERS_ID"].ToString()));
                    }
                }
            }
            else if (bolumID == -1)
            {
                if (Util.GecerliSayi(drpOkullar2.SelectedValue))
                {
                    int okulID = Convert.ToInt32(drpOkullar2.SelectedValue);
                    DataTable dtDersler = Dersler.OkuldakiDersleriDondur(okulID);
                    if (dtDersler != null)
                    {
                        foreach (DataRow dr in dtDersler.Rows)
                        {
                            drpOkulDersler.Items.Add(new ListItem(dr["KOD"].ToString(), dr["DERS_ID"].ToString()));
                        }
                    }
                }

            }

        }
    }

    protected void GridDoldur()
    {
        int seciliBolumID = -1;
        int seciliOkulID = -1;
        if (!string.IsNullOrEmpty(drpBolumler.SelectedValue) && drpBolumler.SelectedValue != "-1")
        {
            seciliBolumID = Convert.ToInt32(drpBolumler.SelectedValue);
        }
        if (!string.IsNullOrEmpty(drpOkullar.SelectedValue) && drpOkullar.SelectedValue != "-1")
        {
            seciliOkulID = Convert.ToInt32(drpOkullar.SelectedValue);
        }

        DataTable dtDersler = Dersler.Admin_DersleriDondur(seciliOkulID,seciliBolumID);
        if (dtDersler != null)
        {
            if (dtDersler.Rows.Count < gridDersler.CurrentPageIndex * gridDersler.PageSize + 1)
            {
                gridDersler.CurrentPageIndex = 0;
            }
            gridDersler.DataSource = dtDersler;
            gridDersler.DataBind();
        }
        else
        {
            gridDersler.DataSource = null;
            gridDersler.DataBind();
        }
    }

    protected void Edit(object sender, DataGridCommandEventArgs e)
    {
        int sutun_sayisi = ((System.Web.UI.WebControls.DataGrid)(sender)).Items.Count;
        ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[11].Visible = false;

        gridDersler.EditItemIndex = e.Item.ItemIndex;
        GridDoldur();
    }

    protected void Cancel(object sender, DataGridCommandEventArgs e)
    {
        int sutun_sayisi = ((System.Web.UI.WebControls.DataGrid)(sender)).Items.Count;
        ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[11].Visible = false;

        gridDersler.EditItemIndex = -1;
        GridDoldur();
    }

    protected void Update(object sender, DataGridCommandEventArgs e)
    {
        try
        {
            int sutun_sayisi = ((System.Web.UI.WebControls.DataGrid)(sender)).Items.Count;
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[11].Visible = false;

            string dersID = e.Item.Cells[0].Text;
            string isActive = (e.Item.Cells[1].Controls[0] as TextBox).Text;
            string okulID = (e.Item.Cells[2].Controls[0] as TextBox).Text;
            //string okulIsim = e.Item.Cells[3].Text;
            string bolumID = (e.Item.Cells[4].Controls[0] as TextBox).Text;
            //string bolumIsim = e.Item.Cells[3].Text;
            string kod = (e.Item.Cells[6].Controls[0] as TextBox).Text;
            string isim = (e.Item.Cells[7].Controls[0] as TextBox).Text;
            string aciklama = (e.Item.Cells[8].Controls[0] as TextBox).Text;

            int DersID = Convert.ToInt32(dersID);
            int OkulID = Convert.ToInt32(okulID);
            int BolumID = Convert.ToInt32(bolumID);
            bool IsActive = Convert.ToBoolean(isActive);
            if (!string.IsNullOrEmpty(kod))
            {
                if(kod.Length > 50)
                    kod = kod.Substring(0, 50);
            }
            else
            {
                lblDurum1.Text = "Kod eksik";
                lblDurum2.Text = "Kod eksik";
                return;
            }
            if (!string.IsNullOrEmpty(isim))
            {
                if(isim.Length > 150)
                    isim = isim.Substring(0, 150);
            }
            if (!string.IsNullOrEmpty(aciklama))
            {
                if (aciklama.Length > 2000)
                    aciklama = aciklama.Substring(0, 2000);
            }
            if (Dersler.DersGuncelle(DersID,OkulID,BolumID, IsActive, kod, isim, aciklama))
            {
                lblDurum1.Text = "Ders guncellendi";
                lblDurum2.Text = "Ders guncellendi";
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
        gridDersler.EditItemIndex = -1;
        GridDoldur();
    }

    protected void ItemCommand(object sender, DataGridCommandEventArgs e)
    {
        int sutun_sayisi = ((System.Web.UI.WebControls.DataGrid)(sender)).Items.Count;
        if(e.CommandName == "Sil1")
        {
            DataGridItemCollection coll = ((System.Web.UI.WebControls.DataGrid)(sender)).Items;
            for(int i=0 ; i<coll.Count ; i++)
            {
                if (i != e.Item.DataSetIndex)
                {
                    coll[i].Controls[11].Visible = false;
                }
                else
                {
                    coll[i].Controls[11].Visible = true;
                }
            }
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[11].Visible = true;
        }
        else if(e.CommandName == "Sil2")
        {
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[11].Visible = false;

            string ID = e.Item.Cells[0].Text;
            if (Util.GecerliSayi(ID))
            {
                int dersID = Convert.ToInt32(ID);
                if (Dersler.DersSil(dersID))
                {
                    lblDurum1.Text = "Ders silindi";
                    lblDurum2.Text = "Ders silindi";
                }
                else
                {
                    lblDurum1.Text = "Ders silerken bir hata olustu";
                    lblDurum2.Text = "Ders silerken bir hata olustu";
                }
            }
            else
            {
                lblDurum1.Text = "Ders silerken bir hata olustu (ID'yi alamadim)";
                lblDurum2.Text = "Ders silerken bir hata olustu (ID'yi alamadim)";
            }
            GridDoldur();
        }
    }

    protected void OkulSecildi2(object sender, EventArgs e)
    {
        drpOkulBolumler.Items.Clear();
        drpOkulBolumler.Items.Add(new ListItem("-", "-1"));
        drpOkulDersler.Items.Clear();
        // Okuldaki bolumleri doldur
        if (Util.GecerliSayi(drpOkullar2.SelectedValue))
        {
            int okulID = Convert.ToInt32(drpOkullar2.SelectedValue);
            DataTable dtBolumler = Okullar.Admin_BolumleriDondur(okulID);
            if (dtBolumler != null)
            {
                foreach (DataRow dr in dtBolumler.Rows)
                {
                    drpOkulBolumler.Items.Add(new ListItem(dr["ISIM"].ToString(), dr["BOLUM_ID"].ToString()));
                }
            }
            
            DataTable dtDersler = Dersler.OkuldakiDersleriDondur(okulID);
            if (dtDersler != null)
            {
                foreach (DataRow dr in dtDersler.Rows)
                {
                    drpOkulDersler.Items.Add(new ListItem(dr["KOD"].ToString(), dr["DERS_ID"].ToString()));
                }
            }
        }
    }

    protected void KayitsizDersleriDoldur()
    {
        drpKayitsizDersler.Items.Clear();
        DataTable dtKayitsizDersler = Dersler.Admin_KayitsizDersleriDondur();
        if (dtKayitsizDersler != null)
        {
            foreach (DataRow dr in dtKayitsizDersler.Rows)
            {
                drpKayitsizDersler.Items.Add(new ListItem(dr["KAYITSIZ_DERS_ISMI"].ToString() + " (" + 
                    dr["HOCA_ISIM"].ToString() + ") " + ((Enums.YorumDurumu)(Convert.ToInt32(dr["YORUM_DURUMU"]))).ToString())); 
            }
        }
    }

    protected void KayitsizDersIliskilendir(object sender, EventArgs e)
    {
        lblDurum3.Text = "Ders iliskilendirilirken bir hata olustu";
        int seciliDersID = -1;
        if (Util.GecerliSayi(drpOkulDersler.SelectedValue))
        {
            seciliDersID = Convert.ToInt32(drpOkulDersler.SelectedValue);
        }
        if (seciliDersID < 0 && Util.GecerliSayi(txtDersID.Text))
        {
            seciliDersID = Convert.ToInt32(txtDersID.Text);
        }
        if (seciliDersID >= 0)
        {
            string ders_isim = drpKayitsizDersler.SelectedValue;
            ders_isim = ders_isim.Substring(0, ders_isim.LastIndexOf("(") - 1);
            if (Dersler.Admin_KayitsizDersIliskilendir(seciliDersID, ders_isim))
            {
                lblDurum3.Text = "Ders basariyla iliskilendirildi";
                KayitsizDersleriDoldur();
            }
        }
    }
}


