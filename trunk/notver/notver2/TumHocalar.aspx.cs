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
using System.Collections.Generic;

public partial class TumHocalar : BasePage
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
                DataTable dtOkullar = null;
                //Query string'de OkulID varsa o okul icin dondurecegiz
                if (Query.GetInt("OkulID") > 0)
                {
                    int okulID = Query.GetInt("OkulID");
                    dtOkullar = Okullar.OkulProfilDondur(okulID);
                    if (dtOkullar != null && dtOkullar.Rows.Count > 0)
                    {
                        dtOkullar.Columns.Add(new DataColumn("OKUL_ID", System.Type.GetType("System.Int32")));
                        dtOkullar.Rows[0]["OKUL_ID"] = okulID;
                    }

                }
                else  //Query string'de OkulID yok, tum okullar icin dondurecegiz
                {
                    dtOkullar = Okullar.OkullariDondur();
                }

                if (dtOkullar != null)
                {
                    repeaterHocalar.DataSource = dtOkullar;
                    repeaterHocalar.DataBind();
                    repeaterHocalar.Visible = true;
                    HarfDiziniOlustur(dtOkullar);
                }
                else
                {
                    repeaterHocalar.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
            GoToDefaultPage();
        }

    }

    protected void HarfDiziniOlustur(DataTable dtOkullar)
    {
        Hashtable harfSayimi = new Hashtable();
        foreach (DataRow dr in dtOkullar.Rows)
        {
            harfSayimi[dr["ISIM"].ToString()[0]] = true;
        }

        LinkedList<char> alfabe = Alfabe(true);
        char curChar;
        StringBuilder sb = new StringBuilder();
        sb.Append("<ol class='dizin' style='font-weight:normal; padding:10px; text-align:center;'>");
        foreach (char ch in alfabe)
        {
            if (harfSayimi.ContainsKey(ch))
            {
                sb.Append("<li><b><a href='#" + ch + "'>" + ch + "</a></b></li>");
            }
            else
            {
                sb.Append("<li class='sessiz'>" + ch + "</li>");
            }
        }
        sb.Append("</ol>");
        ltrHarfDizini.Text = sb.ToString();
    }

    protected string OkulLinkBaslikDondur(object OkulIsim, object OkulID)
    {
        if (Util.GecerliString(OkulIsim) && Util.GecerliString(OkulID))
        {
            return "<a href='" + OkulURLDondur(OkulID) + "' name='" + OkulIsim.ToString()[0] + "'>"
                + OkulIsim.ToString() + "</a>";
        }
        else
        {
            return "";
        }
    }
}
