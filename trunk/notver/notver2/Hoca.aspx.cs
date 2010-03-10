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
                DataTable dtProfil = HocaProfilDondur(HocaID);
                if(dtProfil == null || dtProfil.Rows.Count ==0)    //Hoca bulunamadi ya da hata olustu
                {
                    hocaIsim.Text = "Hoca bulunamadi";                    
                    return;
                }
                //TODO: null donmez ama okul bilgisi donmeyebilir
                HocaIsim = dtProfil.Rows[0]["HOCA_ISIM"].ToString();
                hocaIsim.Text = HocaIsim;
                //Hocanin kayitli oldugu bir okul yok!
                if (dtProfil.Rows[0]["OKUL_ISIM"] == null || dtProfil.Rows[0]["OKUL_ISIM"] == System.DBNull.Value)
                {
                    hocaOkullar.Text = "(Okul bilgisi bulunamadi!)";
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (DataRow dr in dtProfil.Rows)
                    {

                        sb.Append(dr["OKUL_ISIM"] + " (" + dr["START_YEAR"] + " - ");
                        if (dr["END_YEAR"] != System.DBNull.Value)
                        {
                            sb.Append(dr["END_YEAR"]);
                        }
                        else
                        {
                            sb.Append("...");
                        }
                        sb.Append(") <br />");
                    }
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
