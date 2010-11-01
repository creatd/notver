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

using System.IO;

public partial class UserControls_HocaResmi : BaseUserControl
{
    protected void Page_Prerender(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                string imageRelativePath = "~/Images/Hocalar/p" + Query.GetInt("HocaID") + ".jpg";
                string imageFilePath = Server.MapPath(imageRelativePath);
                if (File.Exists(imageFilePath))
                {
                    profilResmi.ImageUrl = imageRelativePath;
                }
                else
                {
                    profilResmi.ImageUrl = "~/Images/Hocalar/p_bay.jpg";
                }
            }
        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
        }

    }
}
