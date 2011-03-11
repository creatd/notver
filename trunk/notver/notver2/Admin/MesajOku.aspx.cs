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

public partial class Admin_MesajOku : BasePage
{
    protected void KontroluSakla()
    {
        pnlGirisYap.Visible = false;
        pnlHata.Visible = false;
        pnlMesaj.Visible = false;
    }
    protected void Page_Prerender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                KontroluSakla();
                if (session.IsLoggedIn && (session.KullaniciUyelikRol == Enums.UyelikRol.Admin || session.KullaniciUyelikRol == Enums.UyelikRol.Moderator))
                {
                    //Mesaji yukle

                    int mesajID = Query.GetInt("MesajID");
                    if (mesajID > 0)
                    {
                        DataTable dtMesaj = Mesajlar.MesajYukle(mesajID);
                        if (dtMesaj != null && dtMesaj.Rows.Count == 1)
                        {
                            DataRow dr = dtMesaj.Rows[0];
                            lblBaslik.Text = dr["BASLIK"].ToString();
                            lblGonderen.Text = dr["GONDEREN_ID"].ToString();
                            lblGonderilmeTarihi.Text = dr["GONDERME_ZAMANI"].ToString();
                            lblMesaj.Text = dr["ICERIK"].ToString();
                            if (!Convert.ToBoolean(dr["OKUNDU"].ToString()))
                            {
                                if (Mesajlar.Admin_MesajOkunduIsaretle(mesajID))
                                {
                                    pnlMesaj.Visible = true;
                                    return;
                                }
                            }
                            else
                            {
                                pnlMesaj.Visible = true;
                                return;
                            }
                        }
                    }
                }
                else
                {
                    pnlGirisYap.Visible = true;
                    return;
                }
            }
            catch (Exception ex) { }
            pnlHata.Visible = true;
        }
        
    }

    protected void MesajSil(object sender, EventArgs e)
    {
        int mesajID = Query.GetInt("MesajID");
        if (mesajID >= 0)
        {
            if (Mesajlar.Admin_MesajSil(mesajID))
            {
                lblDurum.Text = "Mesaj silindi";
            }
            else
            {
                lblDurum.Text = "Mesaj silerken bir hata olustu";
            }
        }
        else
        {
            lblDurum.Text = "Hata - ID'yi alamadim";
        }
    }
}
