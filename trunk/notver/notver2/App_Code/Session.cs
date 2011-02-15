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

    //Ders ile ilgili butun bilgiler
    //Not : Birini degistirince, digerlerini de degistirmek gerekiyor cunku butun degiskenlerin DersID'ye gore oldugu varsayiliyor
    public void DersYukle(int DersID)
    {
        if (DersID != this.DersID)
        {
            DataTable dtDers = Dersler.DersProfilDondur(DersID);
            if (dtDers != null && dtDers.Rows.Count > 0)
            {
                this.DersID = DersID;
                if (Util.GecerliString(dtDers.Rows[0]["KOD"]))
                {
                    this.DersKod = dtDers.Rows[0]["KOD"].ToString();
                }
                else
                {
                    this.DersKod = "";
                }
                if (Util.GecerliStringSayi(dtDers.Rows[0]["OKUL_ID"]))
                {
                    this.DersOkulID = Convert.ToInt32(dtDers.Rows[0]["OKUL_ID"]);
                }
                else
                {
                    this.DersOkulID = -1;
                }
                if (Util.GecerliString(dtDers.Rows[0]["OKUL_ISIM"]))
                {
                    this.DersOkulIsim = dtDers.Rows[0]["OKUL_ISIM"].ToString();
                }
                else
                {
                    this.DersOkulIsim = "";
                }
                if (Util.GecerliString(dtDers.Rows[0]["ISIM"]))
                {
                    this.DersIsim = dtDers.Rows[0]["ISIM"].ToString();
                }
                else
                {
                    this.DersIsim = "";
                }
                if (Util.GecerliString(dtDers.Rows[0]["ACIKLAMA"]))
                {
                    this.DersAciklama = dtDers.Rows[0]["ACIKLAMA"].ToString();
                }
                else
                {
                    this.DersAciklama = "";
                }
            }
            else  //Profil yuklerken bir hata olustu
            {
                //Dersle ilgili butun degiskenleri sifirla
                this.DersID = -1;
                this.DersKod = "";
                this.DersOkulID = -1;
                this.DersOkulIsim = "";
                this.DersIsim = "";
                this.DersAciklama = "";
            }
        }
    }

    public int DersID
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["DersID"] != null)
            {
                return Convert.ToInt32(HttpContext.Current.Session["DersID"]);
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

    public string DersIsim
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["DersIsim"] != null)
            {
                return HttpContext.Current.Session["DersIsim"].ToString();
            }
            else
            {
                return "";
            }
        }
        set
        {
            HttpContext.Current.Session["DersIsim"] = value;
        }
    }

    public string DersAciklama
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["DersAciklama"] != null)
            {
                return HttpContext.Current.Session["DersAciklama"].ToString();
            }
            else
            {
                return "";
            }
        }
        set
        {
            HttpContext.Current.Session["DersAciklama"] = value;
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

    public void HocaYukle(int HocaID)
    {
        if (HocaID != this.HocaID)
        {
            DataTable dtHoca = Hocalar.HocaProfilDondur(HocaID);
            if (dtHoca != null && dtHoca.Rows.Count > 0)
            {
                this.HocaID = HocaID;
                if (dtHoca != null && dtHoca.Rows.Count > 0)    
                {
                    if (Util.GecerliString(dtHoca.Rows[0]["HOCA_ISIM"]))
                    {
                        this.HocaIsim = dtHoca.Rows[0]["HOCA_ISIM"].ToString();
                    }
                    else
                    {
                        this.HocaIsim = "";
                    }

                    if (Util.GecerliString(dtHoca.Rows[0]["HOCA_UNVAN"]))
                    {
                        this.HocaUnvan = dtHoca.Rows[0]["HOCA_UNVAN"].ToString();
                    }
                    else
                    {
                        this.HocaUnvan = "";
                    }

                    //Hocanin kayitli oldugu bir okul yok!
                    if (!Util.GecerliString(dtHoca.Rows[0]["OKUL_ISIM"]))
                    {
                        this.HocaOkulIsimleri = null;
                        this.HocaOkulBaslangicYillari = null;
                        this.HocaOkulBitisYillari = null;
                    }
                    else
                    {
                        string[] hocaOkulIsimleri = new string[dtHoca.Rows.Count];
                        int[] hocaOkulBaslangicYillari = new int[dtHoca.Rows.Count];
                        int[] hocaOkulBitisYillari = new int[dtHoca.Rows.Count];
                        int[] hocaOkulIDleri = new int[dtHoca.Rows.Count];
                        int i=0;
                        foreach (DataRow dr in dtHoca.Rows)
                        {
                            if(Util.GecerliString(dtHoca.Rows[i]["OKUL_ISIM"]) && 
                                Util.GecerliStringSayi(dtHoca.Rows[i]["START_YEAR"]) &&
                                Util.GecerliStringSayi(dtHoca.Rows[i]["OKUL_ID"]))
                            {
                                hocaOkulIsimleri[i] = dtHoca.Rows[i]["OKUL_ISIM"].ToString();
                                hocaOkulBaslangicYillari[i] = Convert.ToInt32(dtHoca.Rows[i]["START_YEAR"]);
                                if (Util.GecerliStringSayi(dtHoca.Rows[i]["END_YEAR"]))
                                {
                                    hocaOkulBitisYillari[i] = Convert.ToInt32(dtHoca.Rows[i]["END_YEAR"]);
                                }
                                else
                                {
                                    hocaOkulBitisYillari[i] = -1;
                                }
                                hocaOkulIDleri[i] = Convert.ToInt32(dtHoca.Rows[i]["OKUL_ID"]);
                            }
                            i++;
                        }
                        this.HocaOkulIsimleri = hocaOkulIsimleri;
                        this.HocaOkulBaslangicYillari = hocaOkulBaslangicYillari;
                        this.HocaOkulBitisYillari = hocaOkulBitisYillari;
                        this.HocaOkulIDleri = hocaOkulIDleri;
                    }
                }
            }
            else  //Profil yuklerken bir hata olustu
            {
                //Hoca ile ilgili butun degiskenleri sifirla
                this.HocaID = -1;
                this.HocaIsim = "";
                this.HocaOkulBaslangicYillari = null;
                this.HocaOkulBitisYillari = null;
                this.HocaOkulIDleri = null;
                this.HocaOkulIsimleri = null;
                this.HocaUnvan = "";
            }
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
                return -1;
            }
        }
        set
        {
            HttpContext.Current.Session["HocaID"] = value;
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

    public string HocaUnvan
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["HocaUnvan"] != null)
            {
                return HttpContext.Current.Session["HocaUnvan"].ToString();
            }
            else
            {
                return "";
            }
        }
        set
        {
            HttpContext.Current.Session["HocaUnvan"] = value;
        }
    }

    public int[] HocaOkulIDleri
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["HocaOkulID"] != null)
            {
                return (int[])HttpContext.Current.Session["HocaOkulID"];
            }
            else
            {
                return null;
            }
        }
        set
        {
            HttpContext.Current.Session["HocaOkulID"] = value;
        }
    }

    public int[] HocaOkulBaslangicYillari
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["HocaOkulBaslangicYillari"] != null)
            {
                return (int[])HttpContext.Current.Session["HocaOkulBaslangicYillari"];
            }
            else
            {
                return null;
            }
        }
        set
        {
            HttpContext.Current.Session["HocaOkulBaslangicYillari"] = value;
        }
    }

    public int[] HocaOkulBitisYillari
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["HocaOkulBitisYillari"] != null)
            {
                return (int[])HttpContext.Current.Session["HocaOkulBitisYillari"];
            }
            else
            {
                return null;
            }
        }
        set
        {
            HttpContext.Current.Session["HocaOkulBitisYillari"] = value;
        }
    }

    public string[] HocaOkulIsimleri
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["HocaOkulIsimleri"] != null)
            {
                return (string[])HttpContext.Current.Session["HocaOkulIsimleri"];
            }
            else
            {
                return null;
            }
        }
        set
        {
            HttpContext.Current.Session["HocaOkulIsimleri"] = value;
        }
    }

    public void OkulYukle(int OkulID)
    {
        if (OkulID != this.OkulID)
        {
            DataTable dtOkul = Okullar.OkulProfilDondur(OkulID);
            if (dtOkul != null && dtOkul.Rows.Count > 0)
            {
                this.OkulID = OkulID;
                if (Util.GecerliString(dtOkul.Rows[0]["ISIM"]))
                {
                    this.OkulIsim = dtOkul.Rows[0]["ISIM"].ToString();
                }
                else
                {
                    this.OkulIsim = "";
                }

                //Kurulus tarihi
                if (Util.GecerliSayi(dtOkul.Rows[0]["KURULUS_TARIHI"]))
                {
                    this.OkulKurulusTarihi = Convert.ToInt32(dtOkul.Rows[0]["KURULUS_TARIHI"]);
                }
                else
                {
                    this.OkulKurulusTarihi = -1;
                }

                //Okul adresi
                if (Util.GecerliString(dtOkul.Rows[0]["ADRES"]))
                {
                    this.OkulAdres = dtOkul.Rows[0]["ADRES"].ToString();
                }
                else
                {
                    this.OkulAdres = "";
                }

                //Ogrenci sayisi
                if (Util.GecerliStringSayi(dtOkul.Rows[0]["OGRENCI_SAYISI"]))
                {
                    this.OkulOgrenciSayisi = Convert.ToInt32(dtOkul.Rows[0]["OGRENCI_SAYISI"]);
                }
                else
                {
                    this.OkulOgrenciSayisi = -1;
                }

                //Akademik personel sayisi
                if (Util.GecerliStringSayi(dtOkul.Rows[0]["AKADEMIK_SAYISI"]))
                {
                    this.OkulAkademikSayisi = Convert.ToInt32(dtOkul.Rows[0]["AKADEMIK_SAYISI"]);
                }
                else
                {
                    this.OkulAkademikSayisi = -1;
                }

                //Web adresi
                if (Util.GecerliString(dtOkul.Rows[0]["WEB_ADRESI"]))
                {
                    this.OkulWebAdresi = dtOkul.Rows[0]["WEB_ADRESI"].ToString();
                }
                else
                {
                    this.OkulWebAdresi = "";
                }
            }
            else  //Profil yuklerken bir hata olustu
            {
                //Okulla ilgili butun degiskenleri sifirla
                this.OkulAdres = "";
                this.OkulAkademikSayisi = -1;
                this.OkulID = -1;
                this.OkulIsim = "";
                this.OkulKurulusTarihi = -1;
                this.OkulOgrenciSayisi = -1;
                this.OkulWebAdresi = "";
            }
        }
    }

    public int OkulKurulusTarihi
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["OkulKurulusTarihi"] != null)
            {
                return Convert.ToInt32(HttpContext.Current.Session["OkulKurulusTarihi"]);
            }
            else
            {
                return -1;
            }
        }
        set
        {
            HttpContext.Current.Session["OkulKurulusTarihi"] = value;
        }
    }

    public int OkulAkademikSayisi
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["OkulAkademikSayisi"] != null)
            {
                return Convert.ToInt32(HttpContext.Current.Session["OkulAkademikSayisi"]);
            }
            else
            {
                return -1;
            }
        }
        set
        {
            HttpContext.Current.Session["OkulAkademikSayisi"] = value;
        }
    }

    public int OkulOgrenciSayisi
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["OkulOgrenciSayisi"] != null)
            {
                return Convert.ToInt32(HttpContext.Current.Session["OkulOgrenciSayisi"]);
            }
            else
            {
                return -1;
            }
        }
        set
        {
            HttpContext.Current.Session["OkulOgrenciSayisi"] = value;
        }
    }

    public int OkulID
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["OkulID"] != null)
            {
                return Convert.ToInt32(HttpContext.Current.Session["OkulID"]);
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

    public string OkulAdres
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["OkulAdres"] != null)
            {
                return HttpContext.Current.Session["OkulAdres"].ToString();
            }
            else
            {
                return "";
            }
        }
        set
        {
            HttpContext.Current.Session["OkulAdres"] = value;
        }
    }

    public string OkulWebAdresi
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["OkulWebAdresi"] != null)
            {
                return HttpContext.Current.Session["OkulWebAdresi"].ToString();
            }
            else
            {
                return "";
            }
        }
        set
        {
            HttpContext.Current.Session["OkulWebAdresi"] = value;
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

    public string KullaniciAd
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["KullaniciAd"] != null)
            {
                return HttpContext.Current.Session["KullaniciAd"].ToString();
            }
            else
            {
                return "";
            }
        }
        set
        {
            HttpContext.Current.Session["KullaniciAd"] = value;
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

    public Enums.UyelikRol KullaniciUyelikRol
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["KullaniciUyelikRol"] != null)
            {
                return (Enums.UyelikRol)Convert.ToInt32(HttpContext.Current.Session["KullaniciUyelikRol"]);
            }
            else
            {
                return Enums.UyelikRol.Kullanici;
            }
        }
        set
        {
            HttpContext.Current.Session["KullaniciUyelikRol"] = value;
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
