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
                        PanelHocalar.Visible = true;
                        PanelOkullar.Visible = false;
                        PanelDersler.Visible = false;
                        break;
                    case 2: //Okul
                        BindGridOkul(searchParameters);
                        PanelHocalar.Visible = false;
                        PanelOkullar.Visible = true;
                        PanelDersler.Visible = false;
                        break;
                    case 3: //Ders
                        BindGridDers(searchParameters);
                        PanelHocalar.Visible = false;
                        PanelOkullar.Visible = false;
                        PanelDersler.Visible = true;
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
        string likeExpression = BuildLikeExpression(expression);
        DataTable dt = QueryHocalarByName(likeExpression);
        DataSet ds = new DataSet();
        ds.Tables.Add(dt);

        dataGridHoca.DataSource = ds;
        dataGridHoca.DataBind();
    }

    void BindGridOkul(string expression)
    {
    }

    void BindGridDers(string expression)
    {
    }
}
