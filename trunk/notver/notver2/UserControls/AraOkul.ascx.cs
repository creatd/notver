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
        try
        {
            if (!Page.IsPostBack)
            {
                okulIsmi.Items.Add(new ListItem("Okul secin", "-1"));
                foreach (DataRow dr in session.dtOkullar.Rows)
                {
                    okulIsmi.Items.Add(new ListItem(dr["ISIM"].ToString(), dr["OKUL_ID"].ToString()));
                }
            }
        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
        }
    }

    protected void OkulSecildi(object sender, EventArgs e)
    {
        try
        {
            int secilenOkulID = Convert.ToInt32(okulIsmi.SelectedValue);
            if (Query.GetInt("OkulID") != secilenOkulID)
            {
                OkulaGit(okulIsmi.SelectedValue.ToString());
            }
        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
        }
    }
}
