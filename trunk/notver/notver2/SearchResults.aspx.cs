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

public partial class SearchResults : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int searchType = -1;
            string searchParameters = "";
            try 
	        {	        
        		searchType = Convert.ToInt32(Request.QueryString["SearchType"].ToString().Trim());
                searchParameters = Request.QueryString["SearchParams"].ToString().Trim();
                switch (searchType)
                {
                    case 1: //Hoca
                        BindGridHoca(searchParameters);
                        pnlHocalar.Visible = true;
                        pnlDersler.Visible = false;
                        break;
                    case 2: //Ders
                        BindGridDers(searchParameters);
                        pnlHocalar.Visible = false;
                        pnlDersler.Visible = true;
                        break;
                }
	        }
	        catch (Exception)
	        {        		
		        GoToDefaultPage();
	        }
            


        }
    }

    void BindGridHoca(string expression)
    {
        string likeExpression = Util.BuildLikeExpression(expression);
        DataTable dt = Hocalar.IsmeGoreHocalariDondur(likeExpression);
        DataSet ds = new DataSet();
        ds.Tables.Add(dt);

        dataGridHoca.DataSource = ds;
        dataGridHoca.DataBind();
    }

    void BindGridDers(string expression)
    {
        DataTable dt = Dersler.KodaGoreDersleriDondur(expression);
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
        }
        
        DataSet ds = new DataSet();
        if (dtSonuc != null)
        {
            ds.Tables.Add(dtSonuc);
        }

        dataGridDersler.DataSource = ds;
        dataGridDersler.DataBind();
    }

    
}
