﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class Admin_TumDosyalar : BasePage
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

            drpDosyaDurum.Items.Clear();
            drpDosyaDurum.Items.Add(new ListItem("-", ""));
            foreach (string durum in Enum.GetNames(typeof(Enums.DosyaDurumu)))
            {
                drpDosyaDurum.Items.Add(new ListItem(durum, durum));
            }
            drpDosyaDurum.SelectedValue = "OnayBekliyor";  //Baslarken onay bekleyen dosyalari goster
            GridDoldur();
        }
    }

    protected void grid_PageIndexChanged(object sender, DataGridPageChangedEventArgs e)
    {
        gridDosyalar.CurrentPageIndex = e.NewPageIndex;
        GridDoldur();
    }

    protected void grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (e.Item.Cells != null && e.Item.Cells.Count > 19 && e.Item.Cells[18].Controls.Count > 0
                && e.Item.Cells[19].Controls.Count > 0)
            {
                string yorumDurumuStr = e.Item.Cells[8].Text; //Gorunmez int cekiyorum
                if (Util.GecerliSayi(yorumDurumuStr))
                {
                    int yorumDurumu = Convert.ToInt32(yorumDurumuStr);
                    e.Item.Cells[18].Controls[0].Visible = false;   //Onayla
                    e.Item.Cells[19].Controls[0].Visible = false;   //Kaldir
                    if (yorumDurumu == (int)Enums.YorumDurumu.OnayBekliyor)
                    {
                        e.Item.Cells[18].Controls[0].Visible = true;
                        e.Item.Cells[19].Controls[0].Visible = true;
                    }
                    else if (yorumDurumu == (int)Enums.YorumDurumu.Onaylanmis)
                    {
                        e.Item.Cells[19].Controls[0].Visible = true;
                    }
                }
            }
        }
    }

    protected void OkulSecildi(object sender, EventArgs e)
    {
        //Secilen okula gore dersleri doldur
        drpDersler.Items.Clear();
        if (Util.GecerliSayi(drpOkullar.SelectedValue))
        {
            int okulID = Convert.ToInt32(drpOkullar.SelectedValue);
            DataTable dtDersler = Dersler.OkuldakiDersleriDondur(okulID);
            if (dtDersler != null)
            {
                drpDersler.Items.Add(new ListItem("-", ""));
                foreach (DataRow dr in dtDersler.Rows)
                {
                    drpDersler.Items.Add(new ListItem(dr["KOD"].ToString(), dr["DERS_ID"].ToString()));
                }
            }
        }
        GridDoldur();
    }

    protected void DersSecildi(object sender, EventArgs e)
    {
        GridDoldur();
    }

    protected void DurumSecildi(object sender, EventArgs e)
    {
        GridDoldur();
    }

    protected void GridDoldur()
    {
        int dersID = -1;
        if (Util.GecerliSayi(drpDersler.SelectedValue))
        {
            dersID = Convert.ToInt32(drpDersler.SelectedValue);
        }
        int okulID = -1;
        if (Util.GecerliSayi(drpOkullar.SelectedValue))
        {
            okulID = Convert.ToInt32(drpOkullar.SelectedValue);
        }
        Enums.DosyaDurumu durum = Enums.DosyaDurumu.OnayBekliyor;
        bool hepsiniDondur = false;
        if (string.IsNullOrEmpty(drpDosyaDurum.SelectedValue))
        {
            hepsiniDondur = true;
        }
        else
        {
            durum = (Enums.DosyaDurumu)Enum.Parse(typeof(Enums.DosyaDurumu), drpDosyaDurum.SelectedValue);
        }
        DataTable dtDosyalar = Dersler.Admin_DersDosyalariDondur(okulID, dersID, durum, hepsiniDondur);
        if (dtDosyalar != null)
        {
            if (dtDosyalar.Rows.Count < gridDosyalar.CurrentPageIndex * gridDosyalar.PageSize + 1)
            {
                gridDosyalar.CurrentPageIndex = 0;
            }
            gridDosyalar.DataSource = dtDosyalar;
            gridDosyalar.DataBind();
        }
        else
        {
            gridDosyalar.DataSource = null;
            gridDosyalar.DataBind();
        }
    }

    protected void Edit(object sender, DataGridCommandEventArgs e)
    {
        ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[22].Visible = false;

        gridDosyalar.EditItemIndex = e.Item.ItemIndex;
        GridDoldur();
    }

    protected void Cancel(object sender, DataGridCommandEventArgs e)
    {
        ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[22].Visible = false;

        gridDosyalar.EditItemIndex = -1;
        GridDoldur();
    }

    protected void Update(object sender, DataGridCommandEventArgs e)
    {
        try
        {
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[22].Visible = false;
            string ID = e.Item.Cells[0].Text;
            string dersID = (e.Item.Cells[1].Controls[0] as TextBox).Text;
            string hocaID = (e.Item.Cells[4].Controls[0] as TextBox).Text;
            string kategoriID = (e.Item.Cells[6].Controls[0] as TextBox).Text;
            string durumID = (e.Item.Cells[8].Controls[0] as TextBox).Text; //Gizli int'ten cekiyorum
            string silinmeNedeni = (e.Item.Cells[10].Controls[0] as TextBox).Text;
            string dosyaIsmi = (e.Item.Cells[11].Controls[0] as TextBox).Text;
            string dosyaAdresi = (e.Item.Cells[12].Controls[0] as TextBox).Text;
            string aciklama = (e.Item.Cells[13].Controls[0] as TextBox).Text;
            string eklenmeTarihi = (e.Item.Cells[14].Controls[0] as TextBox).Text;
            string ekleyenKullanici = (e.Item.Cells[15].Controls[0] as TextBox).Text;
            string indirilmeSayisi = (e.Item.Cells[16].Controls[0] as TextBox).Text;

            int DosyaID = -1;
            if (Util.GecerliSayi(ID))
            {
                DosyaID = Convert.ToInt32(ID);
            }
            else
            {
                lblDurum1.Text = "Hata: Dosya ID'yi alamadim";
                lblDurum2.Text = "Hata: Dosya ID'yi alamadim";
                return;
            }
            int DersID = -1;
            if (Util.GecerliSayi(dersID))
            {
                DersID = Convert.ToInt32(dersID);
                if (DersID < 0)
                {
                    lblDurum1.Text = "Ders ID negatif olamaz";
                    lblDurum2.Text = "Ders ID negatif olamaz";
                    return;
                }
            }
            else
            {
                lblDurum1.Text = "Ders ID sayi olmali";
                lblDurum2.Text = "Ders ID sayi olmali";
                return;
            }
            int HocaID = -1;
            if (Util.GecerliSayi(hocaID))
            {
                HocaID = Convert.ToInt32(hocaID);
            }
            int KategoriID = -1;
            Enums.DosyaKategoriTipi dosyaKategori;
            if (Util.GecerliSayi(kategoriID))
            {
                KategoriID = Convert.ToInt32(kategoriID);
                if (KategoriID < 0 || KategoriID > 5)
                {
                    lblDurum1.Text = "Kategori ID 0 ile 5 arasinda olmali";
                    lblDurum2.Text = "Kategori ID 0 ile 5 arasinda olmali";
                    return;
                }
                dosyaKategori = (Enums.DosyaKategoriTipi)KategoriID;
            }
            else
            {
                lblDurum1.Text = "Kategori ID sayi olmali";
                lblDurum2.Text = "Kategori ID sayi olmali";
                return;
            }
            if(Util.GecerliSayi(durumID))
            {
                int DurumID = Convert.ToInt32(durumID);
                if(!string.IsNullOrEmpty(silinmeNedeni))
                {
                    if(DurumID == (int)Enums.DosyaDurumu.SistemTarafindanSilinmis)
                    {
                        if(silinmeNedeni.Length > 256)
                        {
                            lblDurum1.Text = "Silinme nedeni 256 karakterden uzun olamaz";
                            lblDurum2.Text = "Silinme nedeni 256 karakterden uzun olamaz";
                            return;
                        }
                    }
                    else
                    {
                        lblDurum1.Text = "Sadece sistem tarafindan silinen dosyalar icin silinme nedeni girilebilir";
                        lblDurum2.Text = "Sadece sistem tarafindan silinen dosyalar icin silinme nedeni girilebilir";
                        return;
                    }
                }
            }
            else
            {
                lblDurum1.Text = "Hata: Dosya durum ID'yi cekemedim";
                lblDurum1.Text = "Hata: Dosya durum ID'yi cekemedim";
                return;
            }
            if (!string.IsNullOrEmpty(dosyaIsmi))
            {
                if (dosyaIsmi.Length > 256)
                {
                    lblDurum1.Text = "Dosya ismi 256 karakterden uzun olamaz";
                    lblDurum2.Text = "Dosya ismi 256 karakterden uzun olamaz";
                    return;
                }
            }
            else
            {
                lblDurum1.Text = "Isim eksik";
                lblDurum2.Text = "Isim eksik";
                return;
            }
            if (!string.IsNullOrEmpty(dosyaAdresi))
            {
                if (dosyaAdresi.Length > 256)
                {
                    lblDurum1.Text = "Dosya adresi 256 karakterden uzun olamaz";
                    lblDurum2.Text = "Dosya adresi 256 karakterden uzun olamaz";
                    return;
                }
            }
            else
            {
                lblDurum1.Text = "Dosya adresi eksik";
                lblDurum2.Text = "Dosya adresi eksik";
                return;
            }
            if (!string.IsNullOrEmpty(aciklama))
            {
                if (aciklama.Length > 256)
                {
                    lblDurum1.Text = "Aciklama 256 karakterden uzun olamaz";
                    lblDurum2.Text = "Aciklama 256 karakterden uzun olamaz";
                    return;
                }
            }
            DateTime EklenmeTarihi = Convert.ToDateTime(eklenmeTarihi);
            int EkleyenKullanici;
            if (Util.GecerliSayi(ekleyenKullanici))
            {
                EkleyenKullanici = Convert.ToInt32(ekleyenKullanici);
            }
            else
            {
                lblDurum1.Text = "Ekleyen kullanici sayi olmali";
                lblDurum2.Text = "Ekleyen kullanici sayi olmali";
                return;
            }
            int IndirilmeSayisi;
            if (Util.GecerliSayi(indirilmeSayisi))
            {
                IndirilmeSayisi = Convert.ToInt32(indirilmeSayisi);
            }
            else
            {
                lblDurum1.Text = "Indirilme sayisi sayi olmali";
                lblDurum2.Text = "Indirilme sayisi sayi olmali";
                return;
            }
            
            if (Dersler.Admin_DersDosyaGuncelle(DosyaID, DersID, HocaID, dosyaKategori, silinmeNedeni,
                dosyaIsmi, dosyaAdresi, aciklama, EklenmeTarihi, EkleyenKullanici, IndirilmeSayisi))
            {
                lblDurum1.Text = "Dosya guncellendi";
                lblDurum2.Text = "Dosya guncellendi";
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
        gridDosyalar.EditItemIndex = -1;
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
                    coll[i].Controls[22].Visible = false;
                }
                else
                {
                    coll[i].Controls[22].Visible = true;
                }
            }
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[22].Visible = true;
        }
        else if(e.CommandName == "Sil2")
        {
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[22].Visible = false;

            string ID = e.Item.Cells[0].Text;
            if (Util.GecerliSayi(ID))
            {
                int dosyaID = Convert.ToInt32(ID);
                if (Dersler.DosyaSil(dosyaID))
                {
                    lblDurum1.Text = "Dosya veritabanindan silindi. Amazon'a dokunmadim!";
                    lblDurum2.Text = "Dosya veritabanindan silindi. Amazon'a dokunmadim!";
                }
                else
                {
                    lblDurum1.Text = "Dosya silerken bir hata olustu";
                    lblDurum2.Text = "Dosya silerken bir hata olustu";
                }
            }
            else
            {
                lblDurum1.Text = "Dosya silerken bir hata olustu (ID'yi alamadim)";
                lblDurum2.Text = "Dosya silerken bir hata olustu (ID'yi alamadim)";
            }
            GridDoldur();
        }
        else if (e.CommandName == "Onayla")
        {
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[22].Visible = false;

            string ID = e.Item.Cells[0].Text;
            string kullaniciID = e.Item.Cells[15].Text;
            if (Util.GecerliSayi(ID) && Util.GecerliSayi(kullaniciID))
            {
                int dosyaID = Convert.ToInt32(ID);
                int KullaniciID = Convert.ToInt32(kullaniciID);
                if (Dersler.Admin_DersDosyaOnayla(dosyaID, KullaniciID))
                {
                    lblDurum1.Text = "Dosya onaylandi";
                    lblDurum2.Text = "Dosya onaylandi";
                    GridDoldur();
                }
                else
                {
                    lblDurum1.Text = "Dosyayi onaylarken bir hata olustu";
                    lblDurum2.Text = "Dosyayi onaylarken bir hata olustu";
                }
            }
            else
            {
                lblDurum1.Text = "Dosyayi onaylarken bir hata olustu (ID'yi alamadim)";
                lblDurum2.Text = "Dosyayi onaylarken bir hata olustu (ID'yi alamadim)";
            }
        }
        else if (e.CommandName == "Kaldir")
        {
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[22].Visible = false;

            string ID = e.Item.Cells[0].Text;
            string kullaniciID = e.Item.Cells[15].Text;
            if (Util.GecerliSayi(ID) && Util.GecerliSayi(kullaniciID))
            {
                int dosyaID = Convert.ToInt32(ID);
                int KullaniciID = Convert.ToInt32(kullaniciID);
                if (Dersler.Admin_DersDosyaYayindanKaldir(dosyaID, KullaniciID, txtSilinmeNedeni.Text))
                {
                    string dosyaIsmi = e.Item.Cells[11].Text;
                    string gonderilmeTarihi = e.Item.Cells[8].Text;
                    //DateTime donusumu patlayabilir ama patlamiycagini umabiliriz
                    if (Mesajlar.KullaniciyaDosyaSilindiEpostasiGonder(KullaniciID, dosyaIsmi, txtSilinmeNedeni.Text,
                        Convert.ToDateTime(gonderilmeTarihi)))
                    {
                        lblDurum1.Text = "Dosya yayindan kaldirildi";
                        lblDurum2.Text = "Dosya yayindan kaldirildi";
                    }
                    else
                    {
                        lblDurum1.Text = "Dosya yayindan kaldirildi - Kullaniciya mesaj gonderilemedi";
                        lblDurum2.Text = "Dosya yayindan kaldirildi - Kullaniciya mesaj gonderilemedi";
                    }
                    GridDoldur();
                }
                else
                {
                    lblDurum1.Text = "Dosyayi yayindan kaldirirken bir hata olustu";
                    lblDurum2.Text = "Dosyayi yayindan kaldirirken bir hata olustu";
                }
            }
            else
            {
                lblDurum1.Text = "Dosyayi yayindan kaldirirken bir hata olustu (ID'yi alamadim)";
                lblDurum2.Text = "Dosyayi yayindan kaldirirken bir hata olustu (ID'yi alamadim)";
            }
        }
    }
}


