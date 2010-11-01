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

public partial class UserControls_OkulTumDersler : BaseUserControl
{
    private int _okulID;
    public int _OkulID
    {
        get { return _okulID; }
        set { _okulID = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (_OkulID > 0)
                {
                    DataTable dtOkuldakiTumDersler = Dersler.OkuldakiDersleriDondur(_OkulID);

                    lblDersYok.Visible = false;
                    if (dtOkuldakiTumDersler != null)
                    {
                        if (dtOkuldakiTumDersler.Rows.Count > 0)
                        {
                            repeaterDersler.DataSource = dtOkuldakiTumDersler;
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
        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
        }
    }
}
