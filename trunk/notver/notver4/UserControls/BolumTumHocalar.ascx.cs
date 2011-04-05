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


public partial class UserControls_OkulTumHocalar : BaseUserControl
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
                DataTable dtBolumdekiTumHocalar = Hocalar.BolumdekiHocalariDondur(_BolumID);

                lblHocaYok.Visible = false;
                if (dtBolumdekiTumHocalar != null)
                {
                    if (dtBolumdekiTumHocalar.Rows.Count > 0)
                    {
                        repeaterHocalar.DataSource = dtBolumdekiTumHocalar;
                        repeaterHocalar.DataBind();
                        repeaterHocalar.Visible = true;
                    }
                    else
                    {
                        lblHocaYok.Visible = true;
                    }
                }
                else
                {
                    repeaterHocalar.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
        }
    }
}
