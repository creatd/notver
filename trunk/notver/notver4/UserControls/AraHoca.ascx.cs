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

    protected void Ara(object sender, EventArgs e)
    {
        try
        {
            string searchParams = hocaIsmi.Text.ToString().Trim();

            if (string.IsNullOrEmpty(searchParams))
            {
                return;
            }
            else if (searchParams.StartsWith("Hoca ismi"))
            {
                return;
            }
            //Strip whitespaces and replace them with +
            string[] words = searchParams.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            foreach (string word in words)
            {
                sb.Append(word + "+");
            }
            //Sonda gereksiz bir + kaldi ama onemli degil
            //Mesajlar.EpostaGonder("emir.neftci@boun.edu.tr", Enums.EpostaGonderici.uyari, "uyari icerik", "bilgi baslik", true);
            Response.Redirect(Page.ResolveUrl("~/SearchResults.aspx") + "?SearchType=1&SearchParams=" + sb.ToString());
        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
        }
    }
}
