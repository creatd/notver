using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;

public partial class UserControls_AraHoca : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void buttonAra_Click(object sender, EventArgs e)
    {
        string searchParams = hocaIsmi.Text.ToString().Trim();

        if(String.IsNullOrEmpty(searchParams) )
        {
            return;
        }
        //Strip whitespaces and replace them with +
        string[] words = searchParams.Trim().Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
        StringBuilder sb = new StringBuilder();
        foreach (string word in words)
        {
            sb.Append(word + "+");
        }
        //Sonda gereksiz bir + kaldi ama onemli degil
        Response.Redirect(Page.ResolveUrl("~/SearchResults.aspx") + "?SearchType=1&SearchParams=" + sb.ToString());
    }

    protected void HocaIsmiGirildi(object sender, EventArgs e)
    {
        buttonAra.Focus();
    }
}
