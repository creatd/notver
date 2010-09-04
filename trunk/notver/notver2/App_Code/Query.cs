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
/// Query string'deki degiskenlere erisme sinifi
/// </summary>
public static class Query
{
    public static int GetInt(string anahtar)
    {
        try
        {
            var obj = HttpContext.Current.Request.QueryString.Get(anahtar);
            if (obj != null && Util.GecerliStringSayi(obj))
            {
                return Convert.ToInt32(obj.ToString());
            }
            else
            {
                return -1;
            }
        }
        catch (Exception)
        {
            return -1;   
        }

    }

    public static string GetString(string anahtar)
    {
        try
        {
            var obj = HttpContext.Current.Request.QueryString.Get(anahtar);
            if (obj != null && Util.GecerliString(obj))
            {
                return obj.ToString();
            }
            else
            {
                return null;
            }
        }
        catch (Exception)
        {
            return null;   
        }
    }
}
   /* 
    public int DersID
    {
        get
        {
            var obj = HttpContext.Current.Request.QueryString.Get("DersID");
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
                int dersID = Convert.ToInt32(obj.ToString());
                DersID = dersID;
                return dersID;
            }
            else
            {
                return -1;
            }
        }
        set
        {
            HttpContext.Current.Session["DersID"] = value;
        }
    }

    public int HocaID
    {
        get
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
        set
        {
            HttpContext.Current.Session["HocaID"] = value;
        }
    }

    public int OkulID
    {
        get
        {
            var obj = HttpContext.Current.Request.QueryString.Get("OkulID");
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
                int okulID = Convert.ToInt32(obj.ToString());
                OkulID = okulID;
                return okulID;
            }
            else
            {
                return -1;
            }
        }
        set
        {
            HttpContext.Current.Session["OkulID"] = value;
        }
    }
}*/
