using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Session degiskenlerinin tutuldugu sinif
/// </summary>
public class Session
{
    public DataTable dtOkullar
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["dtOkullar"] != null)
            {
                return (DataTable)HttpContext.Current.Session["dtOkullar"];
            }
            else
            {
                DataTable dt = Genel.OkullariDondur();
                HttpContext.Current.Session["dtOkullar"] = dt;
                return dt;
            }
        }
        set
        {
            HttpContext.Current.Session["dtOkullar"] = value;
        }        
    }

    public int HocaID
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["HocaID"] != null)
            {
                return Convert.ToInt32(HttpContext.Current.Session["HocaID"]);
            }
            else
            {
                var obj = HttpContext.Current.Request.QueryString.Get("HocaID");
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                {
                    int hocaID = Convert.ToInt32(obj.ToString());
                    HocaID = hocaID;
                    return hocaID;
                }
                else
                {
                    return -1;
                }
            }
        }
        set
        {
            HttpContext.Current.Session["HocaID"] = value;
        }  
    }

    public int KullaniciID
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["KullaniciID"] != null)
            {
                return Convert.ToInt32(HttpContext.Current.Session["KullaniciID"]);
            }
            else
            {
                return -1;
            }
        }
        set
        {
            HttpContext.Current.Session["KullaniciID"] = value;
        }
    }

    public string KullaniciAdi
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["KullaniciAdi"] != null)
            {
                return HttpContext.Current.Session["KullaniciAdi"].ToString();
            }
            else
            {
                return "";
            }
        }
        set
        {
            HttpContext.Current.Session["KullaniciAdi"] = value;
        }
    }

    public bool IsLoggedIn
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["IsLoggedIn"] != null)
            {
                return Convert.ToBoolean(HttpContext.Current.Session["IsLoggedIn"]);
            }
            else
            {
                return false;
            }
        }
        set
        {
            HttpContext.Current.Session["IsLoggedIn"] = value;
        }
    }

    public static void Temizle()
    {
        if (HttpContext.Current.Session != null)
        {
            HttpContext.Current.Session.Clear();
        }
    }
}
