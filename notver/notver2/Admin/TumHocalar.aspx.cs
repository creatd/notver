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
    protected void Page_Prerender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            drpOkullar.Items.Add(new ListItem("-", "-1")); //Okul secilir secilmez dersler dolduruldugu icin - ile basliyoruz
            foreach (DataRow dr in session.dtOkullar.Rows)
            {
                drpOkullar.Items.Add(new ListItem(dr["ISIM"].ToString(), dr["OKUL_ID"].ToString()));
            }
            GridDoldur();
        }
    }

    protected void grid_PageIndexChanged(object sender, DataGridPageChangedEventArgs e)
    {
        gridHocalar.CurrentPageIndex = e.NewPageIndex;
        GridDoldur();
    }

    protected void OkulSecildi(object sender, EventArgs e)
    {
        GridDoldur();
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
        ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[7].Visible = false;

        gridHocalar.EditItemIndex = e.Item.ItemIndex;
        GridDoldur();
    }

    protected void Cancel(object sender, DataGridCommandEventArgs e)
    {
        ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[7].Visible = false;

        gridHocalar.EditItemIndex = -1;
        GridDoldur();
    }

    protected void Update(object sender, DataGridCommandEventArgs e)
    {
        try
        {
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[7].Visible = false;

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

    protected void ItemCommand(object sender, DataGridCommandEventArgs e)
    {
        if(e.CommandName == "Sil1")
        {
            DataGridItemCollection coll = ((System.Web.UI.WebControls.DataGrid)(sender)).Items;
            for(int i=0 ; i<coll.Count ; i++)
            {
                if (i != e.Item.DataSetIndex)
                {
                    coll[i].Controls[7].Visible = false;
                }
                else
                {
                    coll[i].Controls[7].Visible = true;
                }
            }
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[7].Visible = true;
        }
        else if(e.CommandName == "Sil2")
        {
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[7].Visible = false;

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
    }
}


