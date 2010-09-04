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

public partial class TumDersler : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataTable dtOkullar = null;
            //Query string'de OkulID varsa o okul icin dondurecegiz
            var obj = HttpContext.Current.Request.QueryString.Get("OkulID");
            if (Util.GecerliString(obj))
            {
                try
                {
                    int okulID = Convert.ToInt32(obj.ToString());
                    dtOkullar = Okullar.OkulProfilDondur(okulID);
                    if (dtOkullar != null && dtOkullar.Rows.Count > 0)
                    {
                        dtOkullar.Columns.Add(new DataColumn("OKUL_ID", System.Type.GetType("System.Int32")));
                        dtOkullar.Rows[0]["OKUL_ID"] = okulID;
                    }
                }
                catch (Exception ex)
                {   //TODO: admin'e haber ver. Gerci adam kendi de bozmus olabilir query string'i
                    GoToDefaultPage();
                }
            }
            else  //Query string'de OkulID yok, tum okullar icin dondurecegiz
            {
                dtOkullar = Okullar.OkullariDondur();
            }
            
            if(dtOkullar != null)
            {
                repeaterOkullar.DataSource = dtOkullar;
                repeaterOkullar.DataBind();
                repeaterOkullar.Visible = true;
                HarfDiziniOlustur(dtOkullar);
            }
            else
            {
                repeaterOkullar.Visible = false;
            }
            
        }
    }

    protected void HarfDiziniOlustur(DataTable dtOkullar)
    {
        Hashtable harfSayimi = new Hashtable();
        foreach (DataRow dr in dtOkullar.Rows)
        {
            harfSayimi[dr["ISIM"].ToString()[0]] = true;
        }
        char curChar = 'A';
        StringBuilder sb = new StringBuilder();
        sb.Append("<ol class='dizin'>");
        for (int i = 0; i < 29; i++)
        {
            if(harfSayimi.ContainsKey(curChar))
            {
                sb.Append("<li><b><a href='#" + curChar + "'>" + curChar + "</a></b></li>");
            }
            else
            {
                sb.Append("<li>" + curChar + "</li>");
            }
            curChar++;
        }
        sb.Append("</ol>");
        ltrHarfDizini.Text = sb.ToString();
    }

    protected string OkulLinkBaslikDondur(object OkulIsim, object OkulID)
    {
        if (Util.GecerliString(OkulIsim) && Util.GecerliString(OkulID))
        {
            return "<a href='" + OkulURLDondur(OkulID) + "' name='" + OkulIsim.ToString()[0] + "'>" 
                + OkulIsim.ToString() + "</a>";
        }
        else
        {
            return "";
        }
    }
}
