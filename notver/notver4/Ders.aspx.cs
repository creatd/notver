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


public partial class Ders : BasePage
{
    protected void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        if (ex != null)
        {
            if (session != null)
            {
                Mesajlar.AdmineHataMesajiGonder(((System.Web.UI.Page)(sender)).Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
            }
            else
            {
                Mesajlar.AdmineHataMesajiGonder(((System.Web.UI.Page)(sender)).Request.Url.ToString(), ex.Message, -1, Enums.SistemHataSeviyesi.Orta);
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                int queryDersID = Query.GetInt("DersID");
                if (queryDersID > 0)
                {
                    session.DersYukle(queryDersID);
                    //Ders kod ve isim
                    if (!string.IsNullOrEmpty(session.DersKod) && !string.IsNullOrEmpty(session.DersIsim))
                    {
                        lblDersIsim.Text = session.DersKod + " - " + session.DersIsim;
                        Page.Title = "NotVerin - " + session.DersKod + " (" + session.DersIsim + ")";
                    }
                    else
                    {
                        lblDersIsim.Text = "";
                    }
                    //Ders aciklama
                    if (!string.IsNullOrEmpty(session.DersAciklama))
                    {
                        lblDersAciklama.Text = session.DersAciklama;
                    }
                    else
                    {
                        lblDersAciklama.Text = "";
                    }
                    //Ders okul isim (Dersin verildigi okulun ismi)
                    if (!string.IsNullOrEmpty(session.DersOkulIsim) && !string.IsNullOrEmpty(session.DersBolumIsim))
                    {
                        lblDersOkulIsim.Text = session.DersBolumIsim + " - " + session.DersOkulIsim;
                    }
                    else
                    {
                        lblDersOkulIsim.Text = "";
                    }
                    lnkDersDosyalar.NavigateUrl = DersDosyaURLDondur(queryDersID);
                    lnkYorumum.NavigateUrl = Page.ResolveUrl("~/DersYorumYap.aspx?DersID=" + queryDersID);
                    
                }
            }
        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
            GoToDefaultPage();
        }
    }
}
