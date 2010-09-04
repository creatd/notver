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
            if (Query.GetInt("DersID") > 0)
            {
                DataTable dtDers = Dersler.DersProfilDondur(Query.GetInt("DersID"));
                
                if (dtDers != null && dtDers.Rows.Count > 0)
                {
                    //Ders kod ve isim
                    if (Util.GecerliString(dtDers.Rows[0]["KOD"]) && Util.GecerliString(dtDers.Rows[0]["ISIM"]))
                    {
                        lblDersIsim.Text = dtDers.Rows[0]["KOD"].ToString() + " - " + dtDers.Rows[0]["ISIM"].ToString();
                        session.DersKod = dtDers.Rows[0]["KOD"].ToString();
                    }
                    //Ders aciklama
                    if (Util.GecerliString(dtDers.Rows[0]["ACIKLAMA"]))
                    {
                        lblDersAciklama.Text = dtDers.Rows[0]["ACIKLAMA"].ToString();
                    }
                    if (Util.GecerliString(dtDers.Rows[0]["OKUL_ISIM"]))
                    {
                        lblDersOkulIsim.Text = dtDers.Rows[0]["OKUL_ISIM"].ToString();
                        session.DersOkulIsim = dtDers.Rows[0]["OKUL_ISIM"].ToString();
                    }
                    if (Util.GecerliStringSayi(dtDers.Rows[0]["OKUL_ID"]))
                    {
                        session.DersOkulID = Convert.ToInt32(dtDers.Rows[0]["OKUL_ID"].ToString());
                    }

                }
                lnkDersDosyalar.NavigateUrl = DersDosyaURLDondur(Query.GetInt("DersID"));
            }
        }
    }
}
