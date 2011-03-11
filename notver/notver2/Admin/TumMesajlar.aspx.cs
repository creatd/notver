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

public partial class Admin_TumMesajlar : BasePage
{
    protected void Page_Prerender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GridDoldur();
        }
    }

    protected void chk_changed(object sender, EventArgs e)
    {
        GridDoldur();
    }

    protected void GridDoldur()
    {
        bool tumu = chkTumu.Checked;
        DataTable dtMesajlar = Mesajlar.Admin_MesajlariDondur(tumu);
        if (dtMesajlar != null)
        {
            if (dtMesajlar.Rows.Count < gridMesajlar.CurrentPageIndex * gridMesajlar.PageSize + 1)
            {
                gridMesajlar.CurrentPageIndex = 0;
            }
            gridMesajlar.DataSource = dtMesajlar;
            gridMesajlar.DataBind();
        }
        else
        {
            gridMesajlar.DataSource = null;
            gridMesajlar.DataBind();
        }
    }

    protected void grid_PageIndexChanged(object sender, DataGridPageChangedEventArgs e)
    {
        gridMesajlar.CurrentPageIndex = e.NewPageIndex;
        GridDoldur();
    }

    protected string IcerikOzetDondur(object Icerik)
    {
        if (Util.GecerliString(Icerik))
        {
            string icerik = Icerik.ToString();
            if (icerik.Length > 200)
            {
                icerik = icerik.Substring(0, 197) + "...";
            }
            return icerik;
        }
        return "";
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
                    coll[i].Controls[8].Visible = false;
                }
                else
                {
                    coll[i].Controls[8].Visible = true;
                }
            }
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[8].Visible = true;
        }
        else if (e.CommandName == "Sil2")
        {
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[8].Visible = false;

            string ID = e.Item.Cells[0].Text;
            if (Util.GecerliSayi(ID))
            {
                int mesajID = Convert.ToInt32(ID);
                if (Mesajlar.Admin_MesajSil(mesajID))
                {
                    lblDurum1.Text = "Mesaj silindi";
                    lblDurum2.Text = "Mesaj silindi";
                }
                else
                {
                    lblDurum1.Text = "Mesaj silerken bir hata olustu";
                    lblDurum2.Text = "Mesaj silerken bir hata olustu";
                }
            }
            else
            {
                lblDurum1.Text = "Mesaj silerken bir hata olustu (ID'yi alamadim)";
                lblDurum2.Text = "Mesaj silerken bir hata olustu (ID'yi alamadim)";
            }
            GridDoldur();
        }
        else if (e.CommandName == "OkunduIsaretle")
        {
            ((System.Web.UI.WebControls.DataGrid)(sender)).Columns[8].Visible = false;

            string ID = e.Item.Cells[0].Text;
            if (Util.GecerliSayi(ID))
            {
                int mesajID = Convert.ToInt32(ID);
                if (Mesajlar.Admin_MesajOkunduIsaretle(mesajID))
                {
                    lblDurum1.Text = "Mesaj okundu olarak isaretlendi";
                    lblDurum2.Text = "Mesaj okundu olarak isaretlendi";
                }
                else
                {
                    lblDurum1.Text = "Mesaji okundu olarak isaretlerken bir hata olustu";
                    lblDurum2.Text = "Mesaji okundu olarak isaretlerken bir hata olustu";
                }
            }
            else
            {
                lblDurum1.Text = "Mesaji okundu olarak isaretlerken bir hata olustu (ID'yi alamadim)";
                lblDurum2.Text = "Mesaji okundu olarak isaretlerken bir hata olustu (ID'yi alamadim)";
            }
            GridDoldur();
        }
    }
}
