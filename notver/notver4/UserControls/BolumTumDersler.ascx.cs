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


public partial class UserControls_OkulTumDersler : BaseUserControl
{
    private int _bolumID;
    public int _BolumID
    {
        get { return _bolumID; }
        set { _bolumID = value; }
    }

    protected void Page_Prerender(object sender, EventArgs e)
    {
        try
        {
            if (_BolumID >= 0)
            {
                DataTable dtBolumdekiTumDersler = Dersler.BolumdekiDersleriDondur(_BolumID);

                lblDersYok.Visible = false;
                if (dtBolumdekiTumDersler != null)
                {
                    if (dtBolumdekiTumDersler.Rows.Count > 0)
                    {
                        repeaterDersler.DataSource = dtBolumdekiTumDersler;
                        repeaterDersler.DataBind();
                        repeaterDersler.Visible = true;
                    }
                    else
                    {
                        lblDersYok.Visible = true;
                    }
                }
                else
                {
                    repeaterDersler.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
        }
    }
}
