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

public partial class Ders : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (session.DersID > 0)
            {
                DataTable dtDers = Dersler.DersProfilDondur(session.DersID);
                if (dtDers != null && dtDers.Rows.Count > 0)
                {
                    //Ders kod ve isim
                    if (Util.GecerliString(dtDers.Rows[0]["KOD"]) && Util.GecerliString(dtDers.Rows[0]["ISIM"]))
                    {
                        lblDersIsim.Text = dtDers.Rows[0]["KOD"].ToString() + " - " + dtDers.Rows[0]["ISIM"].ToString();
                    }
                    //Ders aciklama
                    if (Util.GecerliString(dtDers.Rows[0]["ACIKLAMA"]))
                    {
                        lblDersAciklama.Text = dtDers.Rows[0]["ACIKLAMA"].ToString();
                    }
                    if (Util.GecerliString(dtDers.Rows[0]["OKUL_ISIM"]))
                    {
                        lblDersOkulIsim.Text = dtDers.Rows[0]["OKUL_ISIM"].ToString();
                    }
                }
            }
        }
    }
}
