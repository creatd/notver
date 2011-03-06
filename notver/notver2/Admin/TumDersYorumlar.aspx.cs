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

public partial class Admin_TumDersYorumlar : BasePage
{
    protected void Page_Prerender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            drpDersler.Items.Clear();

            drpOkullar.Items.Clear();
            drpOkullar.Items.Add(new ListItem("-", "-1")); //Okul secilir secilmez dersler dolduruldugu icin - ile basliyoruz
            foreach (DataRow dr in session.dtOkullar.Rows)
            {
                drpOkullar.Items.Add(new ListItem(dr["ISIM"].ToString(), dr["OKUL_ID"].ToString()));
            }

            drpYorumDurumu.Items.Clear();
            drpYorumDurumu.Items.Add(new ListItem("-", ""));
            foreach (string durum in Enum.GetNames(typeof(Enums.YorumDurumu)))
            {
                drpYorumDurumu.Items.Add(new ListItem(durum, durum));
            }
            drpYorumDurumu.SelectedValue = "OnayBekliyor";  //Baslarken onay bekleyen yorumlari goster
            GridDoldur();
        }
    }

    protected void grid_PageIndexChanged(object sender, DataGridPageChangedEventArgs e)
    {
        gridDersYorumlar.CurrentPageIndex = e.NewPageIndex;
        GridDoldur();
    }

    protected void DersSecildi(object sender, EventArgs e)
    {
        GridDoldur();
    }

    protected void OkulSecildi(object sender, EventArgs e)
    {
        //Okula gore dersleri doldur
        if(Util.GecerliSayi(drpOkullar.SelectedValue))
        {
            int okulID = Convert.ToInt32(drpOkullar.SelectedValue);
            DataTable dtDersler = Dersler.OkuldakiDersleriDondur(okulID);
            if (dtDersler != null)
            {
                drpDersler.Items.Clear();
                drpDersler.Items.Add(new ListItem("-", ""));
                foreach (DataRow dr in dtDersler.Rows)
                {
                    drpDersler.Items.Add(new ListItem(dr["KOD"].ToString(), dr["DERS_ID"].ToString()));
                }
            }
        }
        
        GridDoldur();
    }

    protected void DurumSecildi(object sender, EventArgs e)
    {
        GridDoldur();
    }

    protected void GridDoldur()
    {
        string yorumDurumu = drpYorumDurumu.SelectedValue;
        bool hepsiniGoster = false;
        Enums.YorumDurumu durum = Enums.YorumDurumu.OnayBekliyor;
        if (string.IsNullOrEmpty(yorumDurumu))
        {
            hepsiniGoster = true;
        }
        else
        {
            durum = (Enums.YorumDurumu)(Enum.Parse(typeof(Enums.YorumDurumu), yorumDurumu));
        }
        int seciliOkulID = -1;
        if (Util.GecerliSayi(drpOkullar.SelectedValue))
        {
            seciliOkulID = Convert.ToInt32(drpOkullar.SelectedValue);
        }
        int seciliDersID = -1;
        if (Util.GecerliSayi(drpDersler.SelectedValue))
        {
            seciliDersID = Convert.ToInt32(drpDersler.SelectedValue);
        }
        DataTable dtDersYorumlar = Dersler.Admin_DersYorumlariDondur(seciliOkulID, seciliDersID, durum, hepsiniGoster);
        if (dtDersYorumlar != null)
        {
            if (dtDersYorumlar.Rows.Count < gridDersYorumlar.CurrentPageIndex * gridDersYorumlar.PageSize + 1)
            {
                gridDersYorumlar.CurrentPageIndex = 0;
            }
            gridDersYorumlar.DataSource = dtDersYorumlar;
            gridDersYorumlar.DataBind();
        }
        else
        {
            gridDersYorumlar.DataSource = null;
            gridDersYorumlar.DataBind();
        }
    }

    protected void Edit(object sender, DataGridCommandEventArgs e)
    {
        ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[15].Visible = false;

        gridDersYorumlar.EditItemIndex = e.Item.ItemIndex;
        GridDoldur();
    }

    protected void Cancel(object sender, DataGridCommandEventArgs e)
    {
        ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[15].Visible = false;

        gridDersYorumlar.EditItemIndex = -1;
        GridDoldur();
    }

    protected void Update(object sender, DataGridCommandEventArgs e)
    {
        try
        {
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[15].Visible = false;

            string ID = e.Item.Cells[0].Text;
            string yorumDurumu = (e.Item.Cells[2].Controls[0] as TextBox).Text; //Gorunmez int cekiyorum
            string silinmeNedeni = (e.Item.Cells[3].Controls[0] as TextBox).Text;
            string dersID = (e.Item.Cells[4].Controls[0] as TextBox).Text;
            string yorum = (e.Item.Cells[8].Controls[0] as TextBox).Text;
            string gonderilmeTarihi = (e.Item.Cells[9].Controls[0] as TextBox).Text;
            string alkisPuani = (e.Item.Cells[10].Controls[0] as TextBox).Text;
            string zorlukPuani = (e.Item.Cells[11].Controls[0] as TextBox).Text;

            int DersYorumID = -1;
            int DersID = -1;
            if (Util.GecerliSayi(ID))
            {
                DersYorumID = Convert.ToInt32(ID);
            }
            else
            {
                lblDurum1.Text = "Hata: Ders yorum ID'sini alamadim";
                lblDurum2.Text = "Hata: Ders yorum ID'sini alamadim";
                return;
            }
            if (Util.GecerliSayi(dersID))
            {
                DersID = Convert.ToInt32(dersID);
            }
            else
            {
                lblDurum1.Text = "Ders ID'si sayi olmali";
                lblDurum2.Text = "Ders ID'si sayi olmali";
                return;
            }
            if (!string.IsNullOrEmpty(yorum))
            {
                if (yorum.Length > 2000)
                    yorum = yorum.Substring(0, 2000);
            }
            else
            {
                lblDurum1.Text = "Yorum eksik";
                lblDurum2.Text = "Yorum eksik";
                return;
            }
            int YorumDurumu = -1;
            if (Util.GecerliSayi(yorumDurumu))
            {
                YorumDurumu = Convert.ToInt32(yorumDurumu);
            }
            else
            {
                lblDurum1.Text = "Hata: Yorum durum ID'yi cekemedim";
                lblDurum2.Text = "Hata: Yorum durum ID'yi cekemedim";
                return;
            }

            if (YorumDurumu == (int)Enums.YorumDurumu.SistemTarafindanSilinmis)
            {
                if (!string.IsNullOrEmpty(silinmeNedeni))
                {
                    if (silinmeNedeni.Length > 256)
                        silinmeNedeni = silinmeNedeni.Substring(0, 256);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(silinmeNedeni))
                {
                    lblDurum1.Text = "Silinmeyen yorum icin silinme nedeni girilemez";
                    lblDurum2.Text = "Silinmeyen yorum icin silinme nedeni girilemez";
                    return;
                }
            }
            DateTime GonderilmeTarihi = Convert.ToDateTime(gonderilmeTarihi);
            int AlkisPuani = -1;
            if (Util.GecerliSayi(alkisPuani))
            {
                AlkisPuani = Convert.ToInt32(alkisPuani);
            }
            else
            {
                lblDurum1.Text = "Alkis puani sayi olmali";
                lblDurum2.Text = "Alkis puani sayi olmali";
                return;
            }

            int ZorlukPuani = -1;
            if (Util.GecerliSayi(zorlukPuani))
            {
                ZorlukPuani = Convert.ToInt32(zorlukPuani);
                if (ZorlukPuani > 5 || ZorlukPuani < 1)
                {
                    lblDurum1.Text = "Zorluk puani 1 ile 5 arasinda olmali";
                    lblDurum2.Text = "Zorluk puani 1 ile 5 arasinda olmali";
                    return;
                }
            }
            else
            {
                lblDurum1.Text = "Zorluk puani 1 ile 5 arasinda olmali";
                lblDurum2.Text = "Zorluk puani 1 ile 5 arasinda olmali";
                return;
            }

            if (Dersler.Admin_DersYorumGuncelle(DersYorumID, silinmeNedeni, DersID, yorum, GonderilmeTarihi, AlkisPuani,
                ZorlukPuani))
            {
                lblDurum1.Text = "Ders yorumu guncellendi";
                lblDurum2.Text = "Ders yorumu guncellendi";
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
        gridDersYorumlar.EditItemIndex = -1;
        GridDoldur();
    }

    protected void ItemCommand(object sender, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Sil1")
        {
            DataGridItemCollection coll = ((System.Web.UI.WebControls.DataGrid)(sender)).Items;
            for (int i = 0; i < coll.Count; i++)
            {
                if (i != e.Item.DataSetIndex)
                {
                    coll[i].Controls[15].Visible = false;
                }
                else
                {
                    coll[i].Controls[15].Visible = true;
                }
            }
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[15].Visible = true;
        }
        else if (e.CommandName == "Sil2")
        {
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[15].Visible = false;

            string ID = e.Item.Cells[0].Text;
            if (Util.GecerliSayi(ID))
            {
                int dersYorumID = Convert.ToInt32(ID);
                if (Dersler.DersYorumSil(dersYorumID))
                {
                    lblDurum1.Text = "Ders yorumu silindi";
                    lblDurum2.Text = "Ders yorumu silindi";
                }
                else
                {
                    lblDurum1.Text = "Ders yorumunu silerken bir hata olustu";
                    lblDurum2.Text = "Ders yorumunu silerken bir hata olustu";
                }
            }
            else
            {
                lblDurum1.Text = "Ders yorumunu silerken bir hata olustu (ID'yi alamadim)";
                lblDurum2.Text = "Ders yorumunu silerken bir hata olustu (ID'yi alamadim)";
            }
            GridDoldur();
        }
        else if (e.CommandName == "Onayla")
        {
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[15].Visible = false;

            string ID = e.Item.Cells[0].Text;
            string kullaniciID = e.Item.Cells[7].Text;
            if (Util.GecerliSayi(ID) && Util.GecerliSayi(kullaniciID))
            {
                int dersYorumID = Convert.ToInt32(ID);
                int KullaniciID = Convert.ToInt32(kullaniciID);
                if (Dersler.Admin_DersYorumOnayla(dersYorumID, KullaniciID))
                {
                    lblDurum1.Text = "Ders yorumu onaylandi";
                    lblDurum2.Text = "Ders yorumu onaylandi";
                    GridDoldur();
                }
                else
                {
                    lblDurum1.Text = "Ders yorumunu onaylarken bir hata olustu";
                    lblDurum2.Text = "Ders yorumunu onaylarken bir hata olustu";
                }
            }
            else
            {
                lblDurum1.Text = "Ders yorumunu onaylarken bir hata olustu (ID'yi alamadim)";
                lblDurum2.Text = "Ders yorumunu onaylarken bir hata olustu (ID'yi alamadim)";
            }
        }
        else if (e.CommandName == "Kaldir")
        {
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[15].Visible = false;

            string ID = e.Item.Cells[0].Text;
            string kullaniciID = e.Item.Cells[7].Text;
            if (Util.GecerliSayi(ID) && Util.GecerliSayi(kullaniciID))
            {
                int dersYorumID = Convert.ToInt32(ID);
                int KullaniciID = Convert.ToInt32(kullaniciID);
                if (Dersler.Admin_DersYorumYayindanKaldir(dersYorumID, KullaniciID, txtSilinmeNedeni.Text))
                {
                    lblDurum1.Text = "Ders yorumu yayindan kaldirildi";
                    lblDurum2.Text = "Ders yorumu yayindan kaldirildi";
                    GridDoldur();
                }
                else
                {
                    lblDurum1.Text = "Ders yorumunu yayindan kaldirirken bir hata olustu";
                    lblDurum2.Text = "Ders yorumunu yayindan kaldirirken bir hata olustu";
                }
            }
            else
            {
                lblDurum1.Text = "Ders yorumunu yayindan kaldirirken bir hata olustu (ID'yi alamadim)";
                lblDurum2.Text = "Ders yorumunu yayindan kaldirirken bir hata olustu (ID'yi alamadim)";
            }
        }
    }

    protected void grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (e.Item.Cells != null && e.Item.Cells.Count > 13 && e.Item.Cells[12].Controls.Count > 0
                && e.Item.Cells[13].Controls.Count > 0)
            {
                string yorumDurumuStr = e.Item.Cells[2].Text; //Gorunmez int cekiyorum
                if (Util.GecerliSayi(yorumDurumuStr))
                {
                    int yorumDurumu = Convert.ToInt32(yorumDurumuStr);
                    e.Item.Cells[12].Controls[0].Visible = false;   //Onayla
                    e.Item.Cells[13].Controls[0].Visible = false;   //Kaldir
                    if (yorumDurumu == (int)Enums.YorumDurumu.OnayBekliyor)
                    {
                        e.Item.Cells[12].Controls[0].Visible = true;
                        e.Item.Cells[13].Controls[0].Visible = true;
                    }
                    else if (yorumDurumu == (int)Enums.YorumDurumu.Onaylanmis)
                    {
                        e.Item.Cells[13].Controls[0].Visible = true;
                    }
                }
            }
        }
    }
}


