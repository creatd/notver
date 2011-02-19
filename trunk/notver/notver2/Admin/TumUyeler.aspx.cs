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

public partial class Admin_TumUyeler : BasePage
{
    protected void Page_Prerender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            drpOkullar.Items.Clear();
            drpOkullar.Items.Add(new ListItem("-", "")); //Okul secilir secilmez dersler dolduruldugu icin - ile basliyoruz
            foreach (DataRow dr in session.dtOkullar.Rows)
            {
                drpOkullar.Items.Add(new ListItem(dr["ISIM"].ToString(), dr["OKUL_ID"].ToString()));
            }
            GridDoldur();

            //Enum'larin degerleri hakkinda bilgi ver
            StringBuilder sb = new StringBuilder();
            sb.Append("Uyelik durumu<br/>");
            foreach (int sayi in Enum.GetValues(typeof(Enums.UyelikDurumu)))
            {
                sb.Append(sayi + " : " + Enum.GetName(typeof(Enums.UyelikDurumu) , sayi) + "<br/>");
            }
            sb.Append("<br/>Uyelik rol<br/>");
            foreach (int sayi in Enum.GetValues(typeof(Enums.UyelikRol)))
            {
                sb.Append(sayi + " : " + Enum.GetName(typeof(Enums.UyelikRol), sayi) + "<br/>");
            }
            sb.Append("<br/>Cinsiyet<br/>");
            foreach (int sayi in Enum.GetValues(typeof(Enums.Cinsiyet)))
            {
                sb.Append(sayi + " : " + Enum.GetName(typeof(Enums.Cinsiyet), sayi) + "<br/>");
            }
            lblAciklama.Text = sb.ToString();
        }
    }

    protected void GridDoldur()
    {
        int okulID = -1;
        if (Util.GecerliSayi(drpOkullar.SelectedValue))
        {
            okulID = Convert.ToInt32(drpOkullar.SelectedValue);
        }
        DataTable dtUyeler = Uyelik.Admin_UyeleriDondur(okulID);
        if (dtUyeler != null)
        {
            if (dtUyeler.Rows.Count < gridUyeler.CurrentPageIndex * gridUyeler.PageSize + 1)
            {
                gridUyeler.CurrentPageIndex = 0;
            }
            gridUyeler.DataSource = dtUyeler;
            gridUyeler.DataBind();
        }
        else
        {
            gridUyeler.DataSource = null;
            gridUyeler.DataBind();
        }
    }

    protected void grid_PageIndexChanged(object sender, DataGridPageChangedEventArgs e)
    {
        gridUyeler.CurrentPageIndex = e.NewPageIndex;
        GridDoldur();
    }

    protected void OkulSecildi(object sender, EventArgs e)
    {
        GridDoldur();
    }

    protected void Edit(object sender, DataGridCommandEventArgs e)
    {
        ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[18].Visible = false;

        gridUyeler.EditItemIndex = e.Item.ItemIndex;
        GridDoldur();
    }

    protected void Cancel(object sender, DataGridCommandEventArgs e)
    {
        ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[18].Visible = false;

        gridUyeler.EditItemIndex = -1;
        GridDoldur();
    }

    protected void Update(object sender, DataGridCommandEventArgs e)
    {
        try
        {
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[18].Visible = false;

            string ID = e.Item.Cells[0].Text;
            string eposta = (e.Item.Cells[1].Controls[0] as TextBox).Text;
            string bloke = (e.Item.Cells[2].Controls[0] as TextBox).Text;
            string blokNedeni = (e.Item.Cells[3].Controls[0] as TextBox).Text;
            string kullaniciAdi = (e.Item.Cells[4].Controls[0] as TextBox).Text;
            string ad = (e.Item.Cells[5].Controls[0] as TextBox).Text;
            string soyad = (e.Item.Cells[6].Controls[0] as TextBox).Text;
            string okulID = (e.Item.Cells[7].Controls[0] as TextBox).Text;
            string uyelikDurumID = (e.Item.Cells[9].Controls[0] as TextBox).Text;
            string uyelikRolID = (e.Item.Cells[11].Controls[0] as TextBox).Text;
            string cinsiyet = (e.Item.Cells[13].Controls[0] as TextBox).Text;
            string onayPuani = (e.Item.Cells[15].Controls[0] as TextBox).Text;

            int uyeID = -1;
            if (Util.GecerliSayi(ID))
            {
                uyeID = Convert.ToInt32(ID);
            }
            else
            {
                lblDurum1.Text = "Hata: Uye ID'yi alamadim";
                lblDurum2.Text = "Hata: Uye ID'yi alamadim";
                return;
            }

            if (!string.IsNullOrEmpty(eposta))
            {
                if (eposta.Length > 256)
                {
                    lblDurum1.Text = "Eposta 256 karakterden uzun olamaz";
                    lblDurum2.Text = "Eposta 256 karakterden uzun olamaz";
                    return;
                }
            }
            else
            {
                lblDurum1.Text = "Eposta bos olamaz";
                lblDurum2.Text = "Eposta bos olamaz";
                return;
            }

            bool Bloke = Convert.ToBoolean(bloke);

            if (!string.IsNullOrEmpty(kullaniciAdi))
            {
                if (kullaniciAdi.Length > 256)
                {
                    lblDurum1.Text = "Kullanici adi 256 karakterden uzun olamaz";
                    lblDurum2.Text = "Kullanici adi 256 karakterden uzun olamaz";
                    return;
                }
            }

            if (!string.IsNullOrEmpty(ad))
            {
                if (ad.Length > 50)
                {
                    lblDurum1.Text = "Ad 50 karakterden uzun olamaz";
                    lblDurum2.Text = "Ad 50 karakterden uzun olamaz";
                    return;
                }
            }
            else
            {
                lblDurum1.Text = "Ad bos olamaz";
                lblDurum2.Text = "Ad bos olamaz";
                return;
            }

            if (!string.IsNullOrEmpty(soyad))
            {
                if (soyad.Length > 50)
                {
                    lblDurum1.Text = "Soyad 50 karakterden uzun olamaz";
                    lblDurum2.Text = "Soyad 50 karakterden uzun olamaz";
                    return;
                }
            }
            else
            {
                lblDurum1.Text = "Soyad bos olamaz";
                lblDurum2.Text = "Soyad bos olamaz";
                return;
            }

            int OkulID = -1;
            if (Util.GecerliSayi(okulID))
            {
                OkulID = Convert.ToInt32(okulID);
            }

            Enums.UyelikDurumu UyelikDurum = Enums.UyelikDurumu.EpostaOnayBekliyor;
            if (Util.GecerliSayi(uyelikDurumID))
            {
                UyelikDurum = (Enums.UyelikDurumu)Convert.ToInt32(uyelikDurumID);
            }
            else
            {
                lblDurum1.Text = "Uyelik durum ID gecersiz";
                lblDurum2.Text = "Uyelik durum ID gecersiz";
                return;
            }

            Enums.UyelikRol UyelikRol = Enums.UyelikRol.Kullanici;
            if (Util.GecerliSayi(uyelikRolID))
            {
                UyelikRol = (Enums.UyelikRol)Convert.ToInt32(uyelikRolID);
            }
            else
            {
                lblDurum1.Text = "Uyelik rol ID gecersiz";
                lblDurum2.Text = "Uyelik rol ID gecersiz";
                return;
            }

            bool kizMi = Convert.ToBoolean(cinsiyet);

            int OnayPuani = -1;
            if (Util.GecerliSayi(onayPuani))
            {
                OnayPuani = Convert.ToInt32(onayPuani);
            }
            else
            {
                lblDurum1.Text = "Onay puani sayi olmali";
                lblDurum2.Text = "Onay puani sayi olmali";
                return;
            }
            
            if (Uyelik.Admin_UyeGuncelle(uyeID, eposta, Bloke, blokNedeni, kullaniciAdi, ad, soyad,
                OkulID, UyelikDurum, UyelikRol, kizMi, OnayPuani))
            {
                lblDurum1.Text = "Uye guncellendi";
                lblDurum2.Text = "Uye guncellendi";
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
        gridUyeler.EditItemIndex = -1;
        GridDoldur();
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
                    coll[i].Controls[18].Visible = false;
                }
                else
                {
                    coll[i].Controls[18].Visible = true;
                }
            }
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[18].Visible = true;
        }
        else if(e.CommandName == "Sil2")
        {
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[18].Visible = false;

            string ID = e.Item.Cells[0].Text;
            if (Util.GecerliSayi(ID))
            {
                int uyeID = Convert.ToInt32(ID);
                if (Uyelik.Admin_UyeSil(uyeID))
                {
                    lblDurum1.Text = "Uye silindi";
                    lblDurum2.Text = "Uye silindi";
                }
                else
                {
                    lblDurum1.Text = "Uye silerken bir hata olustu";
                    lblDurum2.Text = "Uye silerken bir hata olustu";
                }
            }
            else
            {
                lblDurum1.Text = "Uye silerken bir hata olustu (ID'yi alamadim)";
                lblDurum2.Text = "Uye silerken bir hata olustu (ID'yi alamadim)";
            }
            GridDoldur();
        }
    }
}


