using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : System.Web.UI.Page
{
    public Session session;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if(session == null)
        {
            session = new Session();
        }
    }

	/*public BasePage()
	{
        if (session == null)
        {
            session = new Session();
        }
	}*/

    /// <summary>
    /// Refreshes current page
    /// </summary>
    public void RefreshPage()
    {
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }

    /// <summary>
    /// Turk alfabesini sirasiyla dondurur
    /// </summary>
    /// <returns></returns>
    public LinkedList<char> Alfabe(bool buyukHarf)
    {
        LinkedList<char> alfabe = new LinkedList<char>();
        if (buyukHarf)
        {
            alfabe.AddLast('A');
            alfabe.AddLast('B');
            alfabe.AddLast('C');
            alfabe.AddLast('Ç');
            alfabe.AddLast('D');
            alfabe.AddLast('E');
            alfabe.AddLast('F');
            alfabe.AddLast('G');
            alfabe.AddLast('Ğ');
            alfabe.AddLast('H');
            alfabe.AddLast('I');
            alfabe.AddLast('İ');
            alfabe.AddLast('J');
            alfabe.AddLast('K');
            alfabe.AddLast('L');
            alfabe.AddLast('M');
            alfabe.AddLast('N');
            alfabe.AddLast('O');
            alfabe.AddLast('Ö');
            alfabe.AddLast('P');
            alfabe.AddLast('R');
            alfabe.AddLast('S');
            alfabe.AddLast('Ş');
            alfabe.AddLast('T');
            alfabe.AddLast('U');
            alfabe.AddLast('Ü');
            alfabe.AddLast('V');
            alfabe.AddLast('Y');
            alfabe.AddLast('Z');
        }
        else
        {
            alfabe.AddLast('a');
            alfabe.AddLast('b');
            alfabe.AddLast('c');
            alfabe.AddLast('ç');
            alfabe.AddLast('d');
            alfabe.AddLast('e');
            alfabe.AddLast('f');
            alfabe.AddLast('g');
            alfabe.AddLast('ğ');
            alfabe.AddLast('h');
            alfabe.AddLast('ı');
            alfabe.AddLast('i');
            alfabe.AddLast('j');
            alfabe.AddLast('k');
            alfabe.AddLast('l');
            alfabe.AddLast('m');
            alfabe.AddLast('n');
            alfabe.AddLast('o');
            alfabe.AddLast('ö');
            alfabe.AddLast('p');
            alfabe.AddLast('r');
            alfabe.AddLast('s');
            alfabe.AddLast('ş');
            alfabe.AddLast('t');
            alfabe.AddLast('u');
            alfabe.AddLast('ü');
            alfabe.AddLast('v');
            alfabe.AddLast('y');
            alfabe.AddLast('z');
        }
        return alfabe;
    }



    /// <summary>
    /// Redirect to default.aspx
    /// </summary>
    public void GoToDefaultPage()
    {
        Response.Redirect( "~\\Default.aspx" , true);
    }

    /// <summary>
    /// Bir hocanin ders vermis bulundugu okullari, okullari link yapmis sekilde tek bir string olarak dondurur
    /// </summary>
    /// <param name="HocaID"></param>
    /// <returns></returns>
    public string HocaOkullariniDondur(string HocaID)
    {
        try
        {
            if (string.IsNullOrEmpty(HocaID))
            {
                return null;
            }
            int hocaID = Convert.ToInt32(HocaID);
            DataTable dt = Hocalar.HocaOkullariniDondur(hocaID);
            if (dt != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("<a href=\"" + Page.ResolveUrl("~/Okul.aspx") + "?OkulID=" + dr["OKUL_ID"] + "\">" + dr["ISIM"] + "</a>");
                }
                string result = sb.ToString().Replace("</a><a", "</a><br /><br /><a");    //Her okul ismi arasina iki tane <br /> koy
                return result;
            }
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public string DersDosyaURLDondur(int DersID)
    {
        return Page.ResolveUrl("~/DersDosya.aspx" + "?DersID=" + DersID);
    }

    public string HocaLinkiniDondur(string HocaIsmi, string HocaID)
    {
        return "<a href=\"" + Page.ResolveUrl("~/Hoca.aspx") + "?HocaID=" + HocaID + "\">" + HocaIsmi + "</a>";
    }

    protected string OkulURLDondur(object okulID)
    {
        if (Util.GecerliString(okulID))
        {
            return Page.ResolveUrl("~/Okul.aspx?OkulID=" + okulID);
        }
        else
        {
            return "";
        }
    }

    protected string OkulLinkiniDondur(string okulIsim, string okulID)
    {
        return "<a href=\"" + Page.ResolveUrl("~/Okul.aspx") + "?OkulID=" + okulID + "\">" + okulIsim + "</a>";
    }

    protected string DersLinkiniDondur(string dersKod, string dersIsim, string dersID)
    {
        return "<a href='" + Page.ResolveUrl("~/Ders.aspx") + "?DersID=" + dersID + "' title='" + dersIsim + "'\">" + dersKod + "</a>";
    }

    protected string DersDosyalarLinkiniDondur(string dersID)
    {
        return "<a href='" + Page.ResolveUrl("~/DersDosya.aspx") + "?DersID=" + dersID + "' title='Dersle ilgili dosyalar'>" +
                "<img src='" + Page.ResolveUrl("~/App_Themes/Default/Images/Diger/disket.gif") + "' /></a>";
    }
}
