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
    public int HocaID = -1;
    public string HocaIsim = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       // dummy2.Text = "ege";
        if (!Page.IsPostBack)
        {
            try
            {
                HocaID = Convert.ToInt32(Request.Params["HocaID"].ToString());
                
                //s: HocaProfil
                DataTable dtProfil = Hocalar.HocaProfilDondur(HocaID);
                if(dtProfil == null || dtProfil.Rows.Count ==0)    //Hoca bulunamadi ya da hata olustu
                {
                    hocaIsim.Text = "Hoca bulunamadi";                    
                    return;
                }
                HocaIsim = dtProfil.Rows[0]["HOCA_ISIM"].ToString();
                if (dtProfil.Rows[0]["HOCA_UNVAN"] != System.DBNull.Value)
                {
                    string hocaUnvan = dtProfil.Rows[0]["HOCA_UNVAN"].ToString();
                    if (!string.IsNullOrEmpty(hocaUnvan))
                    {
                        HocaIsim = hocaUnvan + " " + HocaIsim;
                    }
                }                
                hocaIsim.Text = HocaIsim;
                //Hocanin kayitli oldugu bir okul yok!
                if (dtProfil.Rows[0]["OKUL_ISIM"] == null || dtProfil.Rows[0]["OKUL_ISIM"] == System.DBNull.Value)
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

                Page.Title = "NotVer.com - " + HocaIsim;
                //HocaResmi1.HocaID = HocaID;
                //HocaPuanlari1.HocaID = HocaID;
                //HocaYorumlari1.HocaID = HocaID;
                //HocaYorumYap1.HocaID = HocaID;
                BaseUserControl.HocaID = HocaID;
                BaseUserControl.KullaniciID = kullaniciID;
                //HocaPuanAciklamalariniDondur();
            }
            catch
            {
                GoToDefaultPage();
            }
        }
    }
}
