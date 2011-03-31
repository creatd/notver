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


public partial class SearchResults : BasePage
{
    protected void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        if (ex != null)
        {
            if (session != null)
            {
                Mesajlar.AdmineHataMesajiGonder(((System.Web.UI.Page)(sender)).Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
            }
            else
            {
                Mesajlar.AdmineHataMesajiGonder(((System.Web.UI.Page)(sender)).Request.Url.ToString(), ex.Message, -1, Enums.SistemHataSeviyesi.Orta);
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                lblBaslik.Text = "";
                int searchType = -1;
                string searchParameters = "";
                searchType = Convert.ToInt32(Request.QueryString["SearchType"].ToString().Trim());
                searchParameters = Request.QueryString["SearchParams"].ToString().Trim();
                switch (searchType)
                {
                    case 1: //Hoca
                        if (BindGridHoca(searchParameters))
                        {
                            pnlHocalar.Visible = true;
                            pnlDersler.Visible = false;
                            pnlSonucYok.Visible = false;
                        }
                        else
                        {
                            pnlHocalar.Visible = false;
                            pnlDersler.Visible = false;
                            pnlSonucYok.Visible = true;
                            lblBaslik.Text = "Hoca Arama Sonucu";
                            lblSonucYok.Text = "İsminde <strong>\'" + searchParameters + "\'</strong> geçen bir hoca henüz bilmiyoruz";
                        }
                        break;
                    case 2: //Ders
                        if (BindGridDers(searchParameters))
                        {
                            pnlHocalar.Visible = false;
                            pnlDersler.Visible = true;
                            pnlSonucYok.Visible = false;
                        }
                        else
                        {
                            pnlHocalar.Visible = false;
                            pnlDersler.Visible = false;
                            pnlSonucYok.Visible = true;
                            lblBaslik.Text = "Ders Arama Sonucu";
                            lblSonucYok.Text = "Kodunda veya isminde <strong>\'" + searchParameters + "\'</strong> geçen ders bulamadık";
                        }
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
            GoToDefaultPage();
        }
    }

    bool BindGridHoca(string expression)
    {
        DataTable dt = Hocalar.IsmeGoreHocalariDondur(expression);
        if (dt != null && dt.Rows.Count > 0)
        {
            repeaterHocalar.DataSource = dt;
            repeaterHocalar.DataBind();
            return true;
        }
        return false;
    }

    bool BindGridDers(string expression)
    {
        /*DataTable dt = Dersler.KodaGoreDersleriDondur(expression);
        DataTable dt2 = Dersler.IsmeGoreDersleriDondur(expression);
        DataTable dtSonuc = null;
        if (dt != null && dt2 != null)
        {
            dt2.Merge(dt);
            dtSonuc = dt2;
        }
        else if (dt != null)
        {
            dtSonuc = dt;
        }
        else if (dt2 != null)
        {
            dtSonuc = dt2;
        }*/

        DataTable dtSonuc = Dersler.IsmeVeyaKodaGoreDersleriDondur(expression);
        
        if (dtSonuc != null && dtSonuc.Rows.Count>0 )
        {
            repeaterDersler.DataSource = dtSonuc;
            repeaterDersler.DataBind();            
            return true;
        }
        return false;
    }

    
}
