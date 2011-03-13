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

public partial class Admin_TumHocalar : BasePage
{
    public int SeciliHocaID
    {
        get
        {
            object o = ViewState["_SeciliHocaID"];
            if (o != null)
                return (int)o;
            else
                return -1;
        }
        set
        {
            ViewState["_SeciliHocaID"] = value;
        }
    }

    protected void Page_Prerender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            drpOkullar.Items.Clear();
            drpOkullar2.Items.Clear();
            drpOkullar3.Items.Clear();
            drpOkullar4.Items.Clear();

            drpOkullar.Items.Add(new ListItem("-", "-1")); //Okul secilir secilmez dersler dolduruldugu icin - ile basliyoruz
            drpOkullar2.Items.Add(new ListItem("-", "-1")); //Okul secilir secilmez dersler dolduruldugu icin - ile basliyoruz
            drpOkullar4.Items.Add(new ListItem("-", "-1")); //Okul secilir secilmez dersler dolduruldugu icin - ile basliyoruz
            foreach (DataRow dr in session.dtOkullar.Rows)
            {
                drpOkullar.Items.Add(new ListItem(dr["ISIM"].ToString(), dr["OKUL_ID"].ToString()));
                drpOkullar2.Items.Add(new ListItem(dr["ISIM"].ToString(), dr["OKUL_ID"].ToString()));
                drpOkullar3.Items.Add(new ListItem(dr["ISIM"].ToString(), dr["OKUL_ID"].ToString()));
                drpOkullar4.Items.Add(new ListItem(dr["ISIM"].ToString(), dr["OKUL_ID"].ToString()));
            }
            GridDoldur();
            KayitsizHocalariDoldur();
        }
    }

    protected void grid_PageIndexChanged(object sender, DataGridPageChangedEventArgs e)
    {
        gridHocalar.CurrentPageIndex = e.NewPageIndex;
        GridDoldur();
    }

    protected void grid2_PageIndexChanged(object sender, DataGridPageChangedEventArgs e)
    {
        gridHocaDersler.CurrentPageIndex = e.NewPageIndex;
        GridDoldur2();
    }

    protected void grid3_PageIndexChanged(object sender, DataGridPageChangedEventArgs e)
    {
        //gridHocaOkullar.CurrentPageIndex = e.NewPageIndex;
        //GridDoldur3();
    }

    protected void OkulSecildi(object sender, EventArgs e)
    {
        GridDoldur();
    }

    protected void KayitsizHocalariDoldur()
    {
        drpKayitsizHocalar.Items.Clear();
        DataTable dtKayitsizHocalar = Hocalar.Admin_KayitsizHocalariDondur();
        if (dtKayitsizHocalar != null)
        {
            foreach (DataRow dr in dtKayitsizHocalar.Rows)
            {
                drpKayitsizHocalar.Items.Add(new ListItem(dr["KAYITSIZ_HOCA_ISIM"].ToString() + " (" + 
                    dr["KAYITSIZ_HOCA_OKUL"].ToString() + ") " + ((Enums.YorumDurumu)(Convert.ToInt32(dr["YORUM_DURUMU"]))).ToString())); 
            }
        }
    }



    protected void GridDoldur()
    {
        int seciliOkulID = -1;
        if (!string.IsNullOrEmpty(drpOkullar.SelectedValue) && drpOkullar.SelectedValue != "-")
        {
            seciliOkulID = Convert.ToInt32(drpOkullar.SelectedValue);
        }

        DataTable dtHocalar = Hocalar.Admin_HocalariDondur(seciliOkulID);
        if (dtHocalar != null)
        {
            if (dtHocalar.Rows.Count < gridHocalar.CurrentPageIndex * gridHocalar.PageSize + 1)
            {
                gridHocalar.CurrentPageIndex = 0;
            }
            gridHocalar.DataSource = dtHocalar;
            gridHocalar.DataBind();
        }
        else
        {
            gridHocalar.DataSource = null;
            gridHocalar.DataBind();
        }
    }

    protected void Edit(object sender, DataGridCommandEventArgs e)
    {
        ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[8].Visible = false;

        gridHocalar.EditItemIndex = e.Item.ItemIndex;
        GridDoldur();
    }

    protected void Cancel(object sender, DataGridCommandEventArgs e)
    {
        ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[8].Visible = false;

        gridHocalar.EditItemIndex = -1;
        GridDoldur();
    }

    protected void Update(object sender, DataGridCommandEventArgs e)
    {
        try
        {
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[8].Visible = false;

            string ID = e.Item.Cells[0].Text;
            string isActive = (e.Item.Cells[1].Controls[0] as TextBox).Text;
            string isim = (e.Item.Cells[2].Controls[0] as TextBox).Text;
            string unvan = (e.Item.Cells[3].Controls[0] as TextBox).Text;
            string yorumSayisi = e.Item.Cells[4].Text;

            int HocaID = Convert.ToInt32(ID);
            bool IsActive = Convert.ToBoolean(isActive);
            if (!string.IsNullOrEmpty(isim))
            {
                if(isim.Length > 50)
                    isim = isim.Substring(0, 50);
            }
            else
            {
                lblDurum1.Text = "Isim eksik";
                lblDurum2.Text = "Isim eksik";
                return;
            }
            if (!string.IsNullOrEmpty(unvan))
            {
                if (unvan.Length > 50)
                    unvan = unvan.Substring(0, 50);
            }
            int YorumSayisi = -1;
            if (Util.GecerliSayi(yorumSayisi))
            {
                YorumSayisi = Convert.ToInt32(yorumSayisi);
            }
            if (Hocalar.HocaGuncelle(HocaID, IsActive, isim, unvan, YorumSayisi))
            {
                lblDurum1.Text = "Hoca guncellendi";
                lblDurum2.Text = "Hoca guncellendi";
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
        gridHocalar.EditItemIndex = -1;
        GridDoldur();
    }

    //Bu ders daha once ekli mi kontrolu prosedur icinde yapiliyor
    protected void HocaDersEkle(object sender, EventArgs e)
    {
        if (drpDersler.Items.Count > 0 && Util.GecerliSayi(drpDersler.SelectedValue))
        {
            int dersID = Convert.ToInt32(drpDersler.SelectedValue);
            if (Hocalar.HocaDersEkle(SeciliHocaID, dersID))
            {
                lblDurum3.Text = "Hoca ders iliskisi eklendi";
                GridDoldur2();
            }
            else
            {
                lblDurum3.Text = "Hoca ders iliskisi eklerken hata olustu";
            }
        }
        else
        {
            lblDurum3.Text = "Ders secimi gecersiz";
        }
    }

    //Bu okul daha once ekli mi kontrolu prosedur icinde yapiliyor
    protected void HocaOkulEkle(object sender, EventArgs e)
    {
        if (drpOkullar3.Items.Count > 0 && Util.GecerliSayi(drpOkullar3.SelectedValue))
        {
            int okulID = Convert.ToInt32(drpOkullar3.SelectedValue);
            int baslangicYili = -1;
            int bitisYili = -1;
            if (Util.GecerliSayi(txtOkulBaslangicYili.Text))
            {
                baslangicYili = Convert.ToInt32(txtOkulBaslangicYili.Text);
            }
            if (Util.GecerliSayi(txtOkulBitisYili.Text))
            {
                bitisYili = Convert.ToInt32(txtOkulBitisYili.Text);
            }
            if (Hocalar.HocaOkulEkle(SeciliHocaID, okulID,baslangicYili,bitisYili))
            {
                lblDurum1.Text = "Hoca okul iliskisi eklendi";
                lblDurum2.Text = "Hoca okul iliskisi eklendi";
                GridDoldur3();
            }
            else
            {
                lblDurum1.Text = "Hoca okul iliskisi eklerken hata olustu";
                lblDurum2.Text = "Hoca okul iliskisi eklerken hata olustu";
            }
        }
        else
        {
            lblDurum1.Text = "Okul secimi gecersiz";
            lblDurum2.Text = "Okul secimi gecersiz";
        }
    }

    protected void OkulSecildi2(object sender, EventArgs e)
    {
        int okulID = Convert.ToInt32(drpOkullar2.SelectedValue);
        drpDersler.Items.Clear();
        if (okulID >= 0)
        {
            DataTable dtDersler = Dersler.OkuldakiDersleriDondur(okulID);
            if (dtDersler != null)
            {
                foreach (DataRow dr in dtDersler.Rows)
                {
                    drpDersler.Items.Add(new ListItem(dr["KOD"].ToString(), dr["DERS_ID"].ToString()));
                }
            }
        }
    }

    protected void OkulSecildi4(object sender, EventArgs e)
    {
        int okulID = Convert.ToInt32(drpOkullar4.SelectedValue);
        drpOkulHocalar.Items.Clear();
        if (okulID >= 0)
        {
            DataTable dtHocalar = Hocalar.OkuldakiHocalariDondur(okulID);
            if (dtHocalar != null)
            {
                foreach (DataRow dr in dtHocalar.Rows)
                {
                    drpOkulHocalar.Items.Add(new ListItem(dr["HOCA_ISIM"].ToString(), dr["HOCA_ID"].ToString()));
                }
            }
        }
    }

    protected void KayitsizHocaIliskilendir(object sender, EventArgs e)
    {
        lblDurum4.Text = "Hoca iliskilendirilirken bir hata olustu";
        int seciliHocaID = -1;
        if (Util.GecerliSayi(drpOkulHocalar.SelectedValue))
        {
            seciliHocaID = Convert.ToInt32(drpOkulHocalar.SelectedValue);
        }
        if(seciliHocaID < 0 && Util.GecerliSayi(txtHocaID.Text))
        {
            seciliHocaID = Convert.ToInt32(txtHocaID.Text);
        }
        if (seciliHocaID >= 0)
        {
            string hoca_isim = drpKayitsizHocalar.SelectedValue;
            hoca_isim = hoca_isim.Substring(0, hoca_isim.LastIndexOf("(")-1);
            if (Hocalar.Admin_KayitsizHocaIliskilendir(seciliHocaID, hoca_isim))
            {
                lblDurum4.Text = "Hoca basariyla iliskilendirildi";
                KayitsizHocalariDoldur();
            }
        }
    }

    protected void ItemCommand(object sender, DataGridCommandEventArgs e)
    {
        pnlHocaDersler.Visible = false;
        pnlHocaOkullar.Visible = false;
        if(e.CommandName == "Sil1")
        {
            DataGridItemCollection coll = ((System.Web.UI.WebControls.DataGrid)(sender)).Items;
            for(int i=0 ; i<coll.Count ; i++)
            {
                if (i != e.Item.DataSetIndex)
                {
                    coll[i].Controls[8].Visible = false;
                }
                else
                {
                    coll[i].Controls[8].Visible = true;
                }
            }
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[8].Visible = true;
        }
        else if(e.CommandName == "Sil2")
        {
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[8].Visible = false;

            string ID = e.Item.Cells[0].Text;
            if (Util.GecerliSayi(ID))
            {
                int hocaID = Convert.ToInt32(ID);
                if (Hocalar.HocaSil(hocaID))
                {
                    lblDurum1.Text = "Hoca silindi";
                    lblDurum2.Text = "Hoca silindi";
                }
                else
                {
                    lblDurum1.Text = "Hoca silerken bir hata olustu";
                    lblDurum2.Text = "Hoca silerken bir hata olustu";
                }
            }
            else
            {
                lblDurum1.Text = "Hoca silerken bir hata olustu (ID'yi alamadim)";
                lblDurum2.Text = "Hoca silerken bir hata olustu (ID'yi alamadim)";
            }
            GridDoldur();
        }
        else if (e.CommandName == "Detay")
        {
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[8].Visible = false;
            pnlHocaDersler.Visible = true;
            pnlHocaOkullar.Visible = true;
            string ID = e.Item.Cells[0].Text;
            if (Util.GecerliSayi(ID))
            {
                SeciliHocaID = Convert.ToInt32(ID);
                GridDoldur2();
                GridDoldur3();
            }
        }
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
                    coll[i].Controls[4].Visible = false;
                }
                else
                {
                    coll[i].Controls[4].Visible = true;
                }
            }
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[4].Visible = true;
        }
        else if (e.CommandName == "Sil2")
        {
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[4].Visible = false;

            string ID = e.Item.Cells[0].Text;
            if (Util.GecerliSayi(ID))
            {
                int dersID = Convert.ToInt32(ID);
                if (Hocalar.HocaDersSil(SeciliHocaID,dersID))
                {
                    lblDurum3.Text = "Hoca ders iliskisi silindi";
                }
                else
                {
                    lblDurum3.Text = "Hoca ders iliskisini silerken bir hata olustu";
                }
            }
            else
            {
                lblDurum3.Text = "Hoca ders iliskisini silerken bir hata olustu (ID'yi alamadim)";
            }
            GridDoldur2();
        }
    }

    protected void ItemCommand3(object sender, DataGridCommandEventArgs e)
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
                int okulID = Convert.ToInt32(ID);
                if (Hocalar.HocaOkulSil(SeciliHocaID, okulID))
                {
                    lblDurum1.Text = "Hoca okul iliskisi silindi";
                    lblDurum2.Text = "Hoca okul iliskisi silindi";
                }
                else
                {
                    lblDurum1.Text = "Hoca okul iliskisini silerken bir hata olustu";
                    lblDurum2.Text = "Hoca okul iliskisini silerken bir hata olustu";
                }
            }
            else
            {
                lblDurum1.Text = "Hoca okul iliskisini silerken bir hata olustu (ID'yi alamadim)";
                lblDurum2.Text = "Hoca okul iliskisini silerken bir hata olustu (ID'yi alamadim)";
            }
            GridDoldur3();
        }
    }

    protected void GridDoldur2()
    {
        DataTable dtHocaDersler = Hocalar.HocaDersleriniDondur_DataTable(SeciliHocaID);
        if (dtHocaDersler != null)
        {
            if (dtHocaDersler.Rows.Count < gridHocaDersler.CurrentPageIndex * gridHocaDersler.PageSize + 1)
            {
                gridHocaDersler.CurrentPageIndex = 0;
            }
            gridHocaDersler.DataSource = dtHocaDersler;
            gridHocaDersler.DataBind();
        }
        else
        {
            gridHocaDersler.DataSource = null;
            gridHocaDersler.DataBind();
        }
    }

    protected void GridDoldur3()
    {
        DataTable dtHocaOkullar = Hocalar.HocaOkullariniDondur(SeciliHocaID);
        if (dtHocaOkullar != null)
        {
            if (dtHocaOkullar.Rows.Count < gridHocaOkullar.CurrentPageIndex * gridHocaOkullar.PageSize + 1)
            {
                gridHocaOkullar.CurrentPageIndex = 0;
            }
            gridHocaOkullar.DataSource = dtHocaOkullar;
            gridHocaOkullar.DataBind();
        }
        else
        {
            gridHocaOkullar.DataSource = null;
            gridHocaOkullar.DataBind();
        }
    }
}


