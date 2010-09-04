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

public partial class UserControls_HocaAyniOkul : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = Hocalar.AyniOkuldakiHocalariDondur(Query.GetInt("HocaID"), 4);
            if (dt != null)
            {
                rptHocalar.DataSource = dt;
                rptHocalar.DataBind();
            }
        }
    }

    protected void ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Literal ltr = e.Item.FindControl("ltrHoca") as Literal;
        if (ltr != null)
        {
            string hocaID = ((System.Data.DataRowView)(e.Item.DataItem)).Row["HOCA_ID"].ToString();
            string isim = ((System.Data.DataRowView)(e.Item.DataItem)).Row["ISIM"].ToString();
            ltr.Text = HocaLinkiniDondur(isim, hocaID);
        }
    }
}
