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

using System.Text;

public partial class Hoca : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                //s: HocaProfil
                DataTable dtProfil = Hocalar.HocaProfilDondur(Query.GetInt("HocaID"));
                if(dtProfil == null || dtProfil.Rows.Count ==0)    //Hoca bulunamadi ya da hata olustu
                {
                    hocaIsim.Text = "Hoca bulunamadi";                    
                    return;
                }
                if (Util.GecerliString(dtProfil.Rows[0]["HOCA_ISIM"]))
                {
                    session.HocaIsim = dtProfil.Rows[0]["HOCA_ISIM"].ToString();
                    hocaIsim.Text = session.HocaIsim;
                    if (Util.GecerliString(dtProfil.Rows[0]["HOCA_UNVAN"]))
                    {
                        string hocaUnvan = dtProfil.Rows[0]["HOCA_UNVAN"].ToString();
                        if (!string.IsNullOrEmpty(hocaUnvan))
                        {
                            hocaIsim.Text = hocaUnvan + " " + session.HocaIsim;
                        }
                    }    
                }
                
                //Hocanin kayitli oldugu bir okul yok!
                if (!Util.GecerliString(dtProfil.Rows[0]["OKUL_ISIM"]))
                {
                    hocaOkullar.Text = "<span class=\"HocaOkullar\"(Okul bilgisi bulunamadi!)</span>";
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<span class=\"HocaOkullar\">");
                    foreach (DataRow dr in dtProfil.Rows)
                    {

                        sb.Append(dr["OKUL_ISIM"] + "<br />(" + dr["START_YEAR"] + " - ");
                        if (dr["END_YEAR"] != System.DBNull.Value)
                        {
                            sb.Append(dr["END_YEAR"]);
                        }
                        else
                        {
                            sb.Append("...");
                        }
                        sb.Append(") <br /><br />");
                    }
                    sb.Append("</span>");
                    hocaOkullar.Text = sb.ToString();
                }
                //e: HocaProfil

                Page.Title = "NotVer.com - " + session.HocaIsim;
            }
            catch
            {
                GoToDefaultPage();
            }
        }
    }
}
