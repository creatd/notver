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
                DataTable dt = Okullar.OkullariDondur();
                HttpContext.Current.Session["dtOkullar"] = dt;
                return dt;
            }
        }
        set
        {
            HttpContext.Current.Session["dtOkullar"] = value;
        }        
    }

    public string[] hocaPuanAciklamalari
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["hocaPuanAciklamalari"] != null)
            {
                return (string[])HttpContext.Current.Session["hocaPuanAciklamalari"];
            }
            else
            {
                string[] aciklamalar = Hocalar.HocaPuanAciklamalariniDondur();
                HttpContext.Current.Session["hocaPuanAciklamalari"] = aciklamalar;
                return aciklamalar;
            }
        }
        set
        {
            HttpContext.Current.Session["hocaPuanAciklamalari"] = value;
        }   
    }

    public string DersKod
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["DersKod"] != null)
            {
                return HttpContext.Current.Session["DersKod"].ToString();
            }
            else
            {
                return "";
            }
        }
        set
        {
            HttpContext.Current.Session["DersKod"] = value;
        }
    }

    public int DersOkulID
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["DersOkulID"] != null)
            {
                if (Util.GecerliStringSayi(HttpContext.Current.Session["DersOkulID"]))
                {
                    return Convert.ToInt32(HttpContext.Current.Session["DersOkulID"]);
                }
            }
            return -1;
        }
        set
        {
            HttpContext.Current.Session["DersOkulID"] = value;
        }
    }

    public string DersOkulIsim
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["DersOkulIsim"] != null)
            {
                return HttpContext.Current.Session["DersOkulIsim"].ToString();
            }
            else
            {
                return "";
            }
        }
        set
        {
            HttpContext.Current.Session["DersOkulIsim"] = value;
        }
    }

    public string HocaIsim
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["HocaIsim"] != null)
            {
                return HttpContext.Current.Session["HocaIsim"].ToString();
            }
            else
            {
                return "";
            }
        }
        set
        {
            HttpContext.Current.Session["HocaIsim"] = value;
        }
    }

    public string OkulIsim
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["OkulIsim"] != null)
            {
                return HttpContext.Current.Session["OkulIsim"].ToString();
            }
            else
            {
                return "";
            }
        }
        set
        {
            HttpContext.Current.Session["OkulIsim"] = value;
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

    public Enums.UyelikDurumu KullaniciUyelikDurumu
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["KullaniciUyelikDurumu"] != null)
            {
                return (Enums.UyelikDurumu)Convert.ToInt32(HttpContext.Current.Session["KullaniciUyelikDurumu"]);
            }
            else
            {
                return Enums.UyelikDurumu.Gecersiz;
            }
        }
        set
        {
            HttpContext.Current.Session["KullaniciUyelikDurumu"] = value;
        }
    }

    public int KullaniciOnayPuani
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["KullaniciOnayPuani"] != null)
            {
                return Convert.ToInt32(HttpContext.Current.Session["KullaniciOnayPuani"]);
            }
            else
            {
                return -1;
            }
        }
        set
        {
            HttpContext.Current.Session["KullaniciOnayPuani"] = value;
        }
    }

    public int KullaniciAktifYorumSayisi
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["KullaniciAktifYorumSayisi"] != null)
            {
                return Convert.ToInt32(HttpContext.Current.Session["KullaniciAktifYorumSayisi"]);
            }
            else
            {
                int aktifYorumSayisi = Uyelik.KullaniciAktifYorumSayisiniDondur(KullaniciID);
                if (aktifYorumSayisi >= 0)
                {
                    KullaniciAktifYorumSayisi = aktifYorumSayisi;
                    return aktifYorumSayisi;
                }
                else
                {
                    return -1;
                }
            }
        }
        set
        {
            HttpContext.Current.Session["KullaniciAktifYorumSayisi"] = value;
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
