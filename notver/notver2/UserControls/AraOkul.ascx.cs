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

public partial class UserControls_AraOkul : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (dtOkullar == null)
            {
                dtOkullar = OkullariDondur();                
            }

            foreach (DataRow dr in dtOkullar.Rows)
            {
                okulIsmi.Items.Add(new ListItem(dr["ISIM"].ToString(), dr["OKUL_ID"].ToString()));
            }
        }
    }

    protected void OkulSecildi(object sender, EventArgs e)
    {
        OkulaGit(okulIsmi.SelectedValue.ToString());
    }
}
