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

/// <summary>
/// Summary description for Okullar
/// </summary>
public class Okullar
{
    public static bool BolumGuncelle(int BolumID, bool IsActive, string BolumIsim)
    {
        try
        {
            if (BolumID < 0 || string.IsNullOrEmpty(BolumIsim))
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_BolumGuncelle");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("BolumID", BolumID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("BolumIsim", BolumIsim);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("IsActive", IsActive);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Bit;
            cmd.Parameters.Add(param);

            return Util.ExecuteNonQuery(cmd) == 1;
        }
        catch (Exception ex) { }
        return false;
    }

    public static bool BolumEkle(int OkulID, string BolumIsim, bool IsActive)
    {
        try
        {
            if (OkulID < 0 || string.IsNullOrEmpty(BolumIsim))
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_OkulBolumEkle");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("OkulID", OkulID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("BolumIsim", BolumIsim);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("IsActive", IsActive);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Bit;
            cmd.Parameters.Add(param);

            return Util.ExecuteNonQuery(cmd) == 1;
        }
        catch (Exception) { }
        return false;
    }

    public static DataTable BolumDondur(int BolumID)
    {
        try
        {
            //TODO : -99 Bogazici icin gecici cozum, bunu v3'te duzelt
            if (BolumID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("OkulBolumDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("BolumID", BolumID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.GetDataTable(cmd);
        }
        catch (Exception ex) { }
        return null;
    }

    public static DataTable Admin_BolumleriDondur(int OkulID)
    {
        try
        {
            //TODO : -99 Bogazici icin gecici cozum, bunu v3'te duzelt
            if (OkulID < 0 && OkulID != -99)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("Admin_OkulBolumleriDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("OkulID", OkulID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.GetDataTable(cmd);
        }
        catch (Exception ex) { }
        return null;
    }

    public static DataTable BolumleriDondur(int OkulID)
    {
        try
        {
            //TODO : -99 Bogazici icin gecici cozum, bunu v3'te duzelt
            if (OkulID < 0 && OkulID != -99)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("OkulBolumleriDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("OkulID", OkulID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.GetDataTable(cmd);
        }
        catch (Exception ex) { }
        return null;
    }

    //Universite epostasi mi kontrolu icin kullanilir
    public static string OkulUrlDondur(int OkulID)
    {
        try
        {
            if (OkulID < 0)
            {
                return "";
            }
            SqlCommand cmd = new SqlCommand("OkulUrlDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("OkulID", OkulID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            DataTable dt = Util.GetDataTable(cmd);
            if (dt != null && dt.Rows.Count > 0 && dt.Rows[0].ItemArray.Length > 0 && Util.GecerliString(dt.Rows[0][0]))
            {
                return dt.Rows[0][0].ToString();
            }
        }
        catch (Exception ex) { }
        return "";
    }

    /// <summary>
    /// Yorumu yayindan kaldirir ve kullanicinin onay puanini dusurur
    /// </summary>
    /// <param name="OkulYorumID"></param>
    /// <param name="KullaniciID"></param>
    /// <returns></returns>
    public static bool Admin_OkulYorumYayindanKaldir(int OkulYorumID, int KullaniciID, string SilinmeNedeni)
    {
        try
        {
            if (OkulYorumID < 0 || KullaniciID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_OkulYorumYayindanKaldir");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("YorumID", OkulYorumID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("KullaniciID", KullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("YeniDurumID", (int)Enums.YorumDurumu.SistemTarafindanSilinmis);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            int onayDegeri = Convert.ToInt32(ConfigurationManager.AppSettings["OkulYorumOnayDegeri"]);
            onayDegeri = onayDegeri * 2;    //Ceza olarak iki kat dusuruyoruz puani
            param = new SqlParameter("OnayDegeri", onayDegeri);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            if (!string.IsNullOrEmpty(SilinmeNedeni))
            {
                param = new SqlParameter("SilinmeNedeni", SilinmeNedeni);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
            }

            return Util.ExecuteAndCheckReturnValue(cmd);
        }
        catch (Exception ex) { }
        return false;
    }

    /// <summary>
    /// Yorumu onaylar ve kullanicinin onay puanini yukseltir
    /// </summary>
    /// <param name="OkulYorumID"></param>
    /// <param name="KullaniciID"></param>
    /// <returns></returns>
    public static bool Admin_OkulYorumOnayla(int OkulYorumID, int KullaniciID)
    {
        try
        {
            if (OkulYorumID < 0 || KullaniciID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_OkulYorumOnayla");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("YorumID", OkulYorumID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("KullaniciID", KullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("OnayliDurumID", (int)Enums.YorumDurumu.Onaylanmis);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            int onayDegeri = Convert.ToInt32(ConfigurationManager.AppSettings["OkulYorumOnayDegeri"]);
            param = new SqlParameter("OnayDegeri", onayDegeri);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.ExecuteAndCheckReturnValue(cmd);
        }
        catch (Exception ex) { }
        return false;
    }

    /// <summary>
    /// Normal silmeleri yorum durumunu degistirerek yap, bu metod veritabanindan tamamen siler
    /// </summary>
    /// <param name="OkulYorumID"></param>
    /// <returns></returns>
    public static bool OkulYorumSil(int OkulYorumID)
    {
        try
        {
            if (OkulYorumID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_OkulYorumSil");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("OkulYorumID", OkulYorumID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.ExecuteNonQuery(cmd) == 1;
        }
        catch (Exception ex)
        {
        }
        return false;
    }

    public static DataTable Admin_OkulYorumlariDondur(int OkulID, Enums.YorumDurumu YorumDurumu, bool HepsiniGoster)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Admin_OkulYorumlariDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            if (!HepsiniGoster)
            {
                SqlParameter param = new SqlParameter("YorumDurumu", (int)YorumDurumu);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }

            if (OkulID >= 0)
            {
                SqlParameter param = new SqlParameter("OkulID", OkulID);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }

            return Util.GetDataTable(cmd);
        }
        catch (Exception ex) { }
        return null;
    }


    //Admin
    //Bolumun kaydini tamamen siler, inaktif yapmak icin OkulGuncelle'yi kullan
    public static bool BolumSil(int BolumID)
    {
        try
        {
            if (BolumID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_BolumSil");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("BolumID", BolumID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.ExecuteNonQuery(cmd) == 1;
        }
        catch (Exception ex)
        {
        }
        return false;
    }

    //Admin
    //Okulun kaydini tamamen siler, inaktif yapmak icin OkulGuncelle'yi kullan
    public static bool OkulSil(int OkulID)
    {
        try
        {
            if (OkulID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_OkulSil");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("OkulID", OkulID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.ExecuteNonQuery(cmd) == 1;
        }
        catch (Exception ex)
        {
        }
        return false;
    }

    //Admin
    public static bool OkulGuncelle(int OkulID, bool IsActive, string Isim, string Adres, int KurulusTarihi,
        int OgrenciSayisi, int AkademikSayisi, string WebAdresi)
    {
        try
        {
            //Uzunluk kontrolleri bu metod cagirilmadan yapilmali
            if (string.IsNullOrEmpty(Isim) || OkulID < 0)
            {
                return false;
            }

            SqlCommand cmd = new SqlCommand("Admin_OkulGuncelle");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("OkulID", OkulID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("IsActive", IsActive);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Bit;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Isim", Isim);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            if (!string.IsNullOrEmpty(Adres))
            {
                param = new SqlParameter("Adres", Adres);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
            }

            if (KurulusTarihi > 0)
            {
                param = new SqlParameter("KurulusTarihi", KurulusTarihi);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }

            if (OgrenciSayisi > 0)
            {
                param = new SqlParameter("OgrenciSayisi", OgrenciSayisi);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }

            if (AkademikSayisi > 0)
            {
                param = new SqlParameter("AkademikSayisi", AkademikSayisi);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }

            if (!string.IsNullOrEmpty(WebAdresi))
            {
                param = new SqlParameter("WebAdresi", WebAdresi);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
            }

            return Util.ExecuteNonQuery(cmd) == 1;
        }
        catch (Exception ex)
        {
        }
        return false;
    }

    public static bool OkulEkle(bool IsActive, string Isim, string Adres, int KurulusTarihi,
        int OgrenciSayisi, int AkademikSayisi, string WebAdresi)
    {
        try
        {
            //Uzunluk kontrolleri bu metod cagirilmadan yapilmali
            if (string.IsNullOrEmpty(Isim))
            {
                return false;
            }
            

            SqlCommand cmd = new SqlCommand("Admin_OkulEkle");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("IsActive", IsActive);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Bit;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Isim", Isim);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            if (!string.IsNullOrEmpty(Adres))
            {
                param = new SqlParameter("Adres", Adres);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
            }

            if(KurulusTarihi > 0)
            {
                param = new SqlParameter("KurulusTarihi", KurulusTarihi);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }

            if (OgrenciSayisi > 0)
            {
                param = new SqlParameter("OgrenciSayisi", OgrenciSayisi);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }

            if (AkademikSayisi > 0)
            {
                param = new SqlParameter("AkademikSayisi", AkademikSayisi);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }

            if (!string.IsNullOrEmpty(WebAdresi))
            {
                param = new SqlParameter("WebAdresi", WebAdresi);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
            }

            return Util.ExecuteNonQuery(cmd) == 1;
        }
        catch (Exception ex)
        {
            
        }
        return false;
    }

    /// <summary>
    /// Okul.aspx sayfasindan, bir okulun profil bilgilerini dondurmek icin cagirilir
    /// </summary>
    /// <param name="OkulID"></param>
    /// <returns></returns>
    public static DataTable OkulProfilDondur(int OkulID)
    {
        try
        {
            if (OkulID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("OkulProfilDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("OkulID", OkulID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.GetDataTable(cmd);
        }
        catch (Exception)
        {
            return null;
        }
    }


    /// <summary>
    /// Kayitli tum aktif okullarin ISIM,OKUL_ID 'lerini dondurur
    /// </summary>
    /// <returns></returns>
    public static DataTable OkullariDondur()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("OkullariDondur");
            cmd.CommandType = CommandType.StoredProcedure;

           return Util.GetDataTable(cmd);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static DataTable Admin_OkullariDondur()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Admin_OkullariDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            return Util.GetDataTable(cmd);
        }
        catch (Exception)
        {
            return null;
        }
    }


    /// <summary>
    /// Bir okul hakkinda yapilan yorumlari dondurur
    /// Eger yorum yoksa null dondurur
    /// </summary>
    /// <param name="OkulID"></param>
    /// <returns></returns>
    public static DataTable OkulYorumlariniDondur(int OkulID)
    {
        try
        {
            if (OkulID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("OkulYorumlariniDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("OkulID", OkulID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.GetDataTable(cmd);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Kullanici okula daha once yorum yaptiysa true dondurur; yoksa false dondurur
    /// </summary>
    /// <param name="kullaniciID"></param>
    /// <param name="hocaID"></param>
    /// <returns></returns>
    public static bool KullaniciOkulaYorumYapmis(int kullaniciID, int okulID)
    {
        try
        {
            if (kullaniciID < 0 || okulID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("KullaniciOkulaYorumYapmis");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", kullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            SqlParameter param2 = new SqlParameter("OkulID", okulID);
            param2.Direction = ParameterDirection.Input;
            param2.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param2);

            DataTable dt = Util.GetDataTable(cmd);
            if (Convert.ToInt32(dt.Rows[0][0]) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Kullanicinin yaptigi tum okul yorumlarini dondurur
    /// (Silinmis, onaylanmamis yorumlari da dondurur)
    /// Basarisiz olursa null dondurur
    /// </summary>
    /// <param name="KullaniciID"></param>
    /// <returns></returns>
    public static DataTable KullaniciOkulYorumlariniDondur(int KullaniciID)
    {
        try
        {
            if (KullaniciID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("KullaniciOkulYorumlariniDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", KullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.GetDataTable(cmd);
        }
        catch (Exception) { }
        return null;
    }

    /// <summary>
    /// Kullanicinin okul icin yaptigi yorumu dondurur
    /// </summary>
    /// <param name="kullaniciID"></param>
    /// <param name="okulID"></param>
    /// <returns></returns>
    public static string KullaniciOkulYorumunuDondur(int kullaniciID, int okulID)
    {
        try
        {
            if (kullaniciID < 0 || okulID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("KullaniciOkulYorumunuDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", kullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("OkulID", okulID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            DataRow dr = (Util.GetDataTable(cmd)).Rows[0];
            if(Util.GecerliString(dr["YORUM"]))
            {
                return dr["YORUM"].ToString();
            }
        }
        catch   {}
        return null;
    }

    public static bool OkulYorumKaydet(int kullaniciID, int okulID, string yorum, int KullaniciOnayPuani)
    {
        try
        {
            if (okulID < 0 || string.IsNullOrEmpty(yorum) || kullaniciID<0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("OkulYorumKaydet");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", kullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("OkulID", okulID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Yorum", Util.HTMLToDB(yorum));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            Enums.YorumDurumu yorumDurumu = Enums.YorumDurumu.OnayBekliyor;
            if (KullaniciOnayPuani >= Convert.ToInt32(ConfigurationManager.AppSettings.Get("OkulYorumOnayPuani")))
            {
                yorumDurumu = Enums.YorumDurumu.Onaylanmis;
            }
            param = new SqlParameter("YorumDurumu", (int)yorumDurumu);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return (Util.ExecuteNonQuery(cmd) == 1);
        }
        catch
        {
            return false;
        }
    }

    public static bool OkulYorumGuncelle(int kullaniciID, int okulID, string yorum, int KullaniciOnayPuani)
    {
        try
        {
            if (okulID < 0 || string.IsNullOrEmpty(yorum) || kullaniciID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("OkulYorumGuncelle");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", kullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("OkulID", okulID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Yorum", Util.HTMLToDB(yorum));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            Enums.YorumDurumu yorumDurumu = Enums.YorumDurumu.OnayBekliyor;
            if (KullaniciOnayPuani >= Convert.ToInt32(ConfigurationManager.AppSettings.Get("OkulYorumOnayPuani")))
            {
                yorumDurumu = Enums.YorumDurumu.Onaylanmis;
            }
            param = new SqlParameter("YorumDurumu", (int)yorumDurumu);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return (Util.ExecuteNonQuery(cmd) == 1);
        }
        catch
        {
            return false;
        }
    }

    public static bool Admin_OkulYorumGuncelle(int OkulYorumID, string SilinmeNedeni, int OkulID, string Yorum, 
        DateTime GonderilmeTarihi, int AlkisPuani)
    {
        try
        {
            if (OkulYorumID < 0 || OkulID <0 || string.IsNullOrEmpty(Yorum))
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_OkulYorumGuncelle");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("OkulYorumID", OkulYorumID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            if (!string.IsNullOrEmpty(SilinmeNedeni))
            {
                param = new SqlParameter("SilinmeNedeni", SilinmeNedeni);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
            }


            param = new SqlParameter("OkulID", OkulID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Yorum", Util.HTMLToDB(Yorum));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("GonderilmeTarihi", GonderilmeTarihi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.SmallDateTime;
            cmd.Parameters.Add(param);

            param = new SqlParameter("AlkisPuani", AlkisPuani);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.ExecuteNonQuery(cmd) == 1;
        }
        catch (Exception ex) { }
        return false;
    }
}
