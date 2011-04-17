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
/// Summary description for Ders
/// </summary>
public class Dersler
{
    public static bool DersDosyaIndirildi(int DosyaID)
    {
        try
        {
            if (DosyaID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("DersDosyaIndirildi");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DosyaID", DosyaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.ExecuteNonQuery(cmd) == 1;
        }
        catch (Exception) { }
        return false;
    }

    public static bool Admin_KayitsizDersIliskilendir(int DersID, string KayitsizDersIsim)
    {
        try
        {
            if (DersID < 0 || string.IsNullOrEmpty(KayitsizDersIsim))
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_KayitsizDersIliskilendir");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DersID", DersID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("KayitsizDersIsim", Util.ParametreyiTemizle(KayitsizDersIsim));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            return Util.ExecuteNonQuery(cmd) >= 1;
        }
        catch (Exception ex) { }
        return false;
    }

    public static DataTable Admin_KayitsizDersleriDondur()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Admin_KayitsizDersleriDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            return Util.GetDataTable(cmd);
        }
        catch (Exception ex) { }
        return null;
    }

    /// <summary>
    /// Yorumu yayindan kaldirir ve kullanicinin onay puanini dusurur
    /// </summary>
    /// <param name="DersYorumID"></param>
    /// <param name="KullaniciID"></param>
    /// <returns></returns>
    public static bool Admin_DersYorumYayindanKaldir(int DersYorumID, int KullaniciID, string SilinmeNedeni)
    {
        try
        {
            if (DersYorumID < 0 || KullaniciID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_DersYorumYayindanKaldir");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("YorumID", DersYorumID);
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

            int onayDegeri = Convert.ToInt32(ConfigurationManager.AppSettings["DersYorumOnayDegeri"]);
            onayDegeri = onayDegeri * 2;    //Ceza olarak iki kat dusuruyoruz puani
            param = new SqlParameter("OnayDegeri", onayDegeri);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            if (!string.IsNullOrEmpty(SilinmeNedeni))
            {
                param = new SqlParameter("SilinmeNedeni", Util.ParametreyiTemizle(SilinmeNedeni));
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
    /// Yorumu yayindan kaldirir ve kullanicinin onay puanini dusurur
    /// </summary>
    /// <param name="DersYorumID"></param>
    /// <param name="KullaniciID"></param>
    /// <returns></returns>
    public static bool Admin_DersDosyaYayindanKaldir(int DosyaID, int KullaniciID, string SilinmeNedeni)
    {
        try
        {
            if (DosyaID < 0 || KullaniciID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_DersDosyaYayindanKaldir");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DosyaID", DosyaID);
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

            int onayDegeri = Convert.ToInt32(ConfigurationManager.AppSettings["DersYorumOnayDegeri"]);
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
    /// <param name="DersYorumID"></param>
    /// <param name="KullaniciID"></param>
    /// <returns></returns>
    public static bool Admin_DersDosyaOnayla(int DosyaID, int KullaniciID)
    {
        try
        {
            if (DosyaID < 0 || KullaniciID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_DersDosyaOnayla");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DosyaID", DosyaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("KullaniciID", KullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("OnayliDurumID", (int)Enums.DosyaDurumu.Onaylanmis);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            int onayDegeri = Convert.ToInt32(ConfigurationManager.AppSettings["DersDosyaOnayDegeri"]);
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
    /// Yorumu onaylar ve kullanicinin onay puanini yukseltir
    /// </summary>
    /// <param name="DersYorumID"></param>
    /// <param name="KullaniciID"></param>
    /// <returns></returns>
    public static bool Admin_DersYorumOnayla(int DersYorumID, int KullaniciID)
    {
        try
        {
            if (DersYorumID < 0 || KullaniciID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_DersYorumOnayla");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("YorumID", DersYorumID);
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

            int onayDegeri = Convert.ToInt32(ConfigurationManager.AppSettings["DersYorumOnayDegeri"]);
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
    /// Dosyayi veritabanindan siler, Amazon'daki kaynaga dokunmaz.
    /// Dosyayi saklamak icin dosya durumunu guncelleyin.
    /// </summary>
    /// <param name="DosyaID"></param>
    /// <returns></returns>
    public static bool DosyaSil(int DosyaID)
    {
        try
        {
            if (DosyaID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_DersDosyaSil");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DosyaID", DosyaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.ExecuteNonQuery(cmd) == 1;
        }
        catch (Exception ex) { }
        return false;
    }

    public static DataTable Admin_DersDosyalariDondur(int OkulID, int DersID, Enums.DosyaDurumu DosyaDurumu,
        bool HepsiniDondur)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Admin_DersDosyalariDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            if (!HepsiniDondur)
            {
                SqlParameter param = new SqlParameter("Durum", (int)DosyaDurumu);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }

            if(DersID >=0)
            {
                SqlParameter param = new SqlParameter("DersID", DersID);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }
            else if (OkulID >= 0)
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

    public static bool Admin_DersYorumGuncelle(int DersYorumID, string SilinmeNedeni, int DersID, string Yorum,
            DateTime GonderilmeTarihi, int AlkisPuani, int ZorlukPuani)
    {
        try
        {
            if (DersYorumID < 0 || DersID < 0 || string.IsNullOrEmpty(Yorum))
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_DersYorumGuncelle");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DersYorumID", DersYorumID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            if (!string.IsNullOrEmpty(SilinmeNedeni))
            {
                param = new SqlParameter("SilinmeNedeni", Util.ParametreyiTemizle(SilinmeNedeni));
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
            }


            param = new SqlParameter("DersID", DersID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Yorum", Util.HTMLToDB(Util.ParametreyiTemizle(Yorum)));
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

            param = new SqlParameter("ZorlukPuani", ZorlukPuani);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.ExecuteNonQuery(cmd) == 1;
        }
        catch (Exception ex) { }
        return false;
    }

    public static DataTable Admin_DersYorumlariDondur(int OkulID, int DersID, Enums.YorumDurumu YorumDurumu, bool HepsiniGoster)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Admin_DersYorumlariDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            if (!HepsiniGoster)
            {
                SqlParameter param = new SqlParameter("YorumDurumu", (int)YorumDurumu);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }

            if (DersID >= 0)
            {
                SqlParameter param = new SqlParameter("DersID", DersID);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }
            else if (OkulID >= 0)
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

    /// <summary>
    /// Normal silmeleri yorum durumunu degistirerek yap, bu metod veritabanindan tamamen siler
    /// </summary>
    /// <param name="DersYorumID"></param>
    /// <returns></returns>
    public static bool DersYorumSil(int DersYorumID)
    {
        try
        {
            if (DersYorumID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_DersYorumSil");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DersYorumID", DersYorumID);
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

    //Dersi veritabanindan siler, inaktif yapmak icin DersGuncelle'yi kullan
    public static bool DersSil(int DersID)
    {
        try
        {
            if (DersID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_DersSil");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DersID", DersID);
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

    public static bool DersGuncelle(int DersID, int OkulID, int BolumID, bool IsActive, string Kod,
        string Isim, string Aciklama)
    {
        try
        {
            if (DersID < 0 || OkulID < 0 || BolumID <0 || string.IsNullOrEmpty(Kod))
            {
                return false;
            }

            SqlCommand cmd = new SqlCommand("Admin_DersGuncelle");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DersID", DersID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("OkulID", OkulID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("BolumID", BolumID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("IsActive", IsActive);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Kod", Util.ParametreyiTemizle(Kod));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            if (!string.IsNullOrEmpty(Isim))
            {
                param = new SqlParameter("Isim", Util.ParametreyiTemizle(Isim));
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
            }

            if (!string.IsNullOrEmpty(Aciklama))
            {
                param = new SqlParameter("Aciklama", Util.ParametreyiTemizle(Aciklama));
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
    /// Ya tum dersleri (OkulID ve BolumID <0)
    /// Ya bir okuldaki tum dersleri (sadece OkulID >= 0)
    /// Ya da bir bolumdeki tum dersleri (BolumID >=0)
    /// dondurur
    /// </summary>
    /// <param name="OkulID"></param>
    /// <param name="BolumID"></param>
    /// <returns></returns>
    public static DataTable Admin_DersleriDondur(int OkulID, int BolumID)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Admin_DersleriDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            if (BolumID >= 0)
            {
                SqlParameter param = new SqlParameter("BolumID", BolumID);
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
        catch (Exception ex)
        {
            
        }
        return null;
    }

    public static bool DersEkle(int OkulID, int BolumID, bool IsActive, string DersKodu, string DersIsmi,
        string DersAciklama)
    {
        try
        {
            if (OkulID < 0 || BolumID < 0 || string.IsNullOrEmpty(DersKodu))
            {
                return false;
            }
            else if (DersKodu.Length > 50 || DersIsmi.Length > 150 || DersAciklama.Length > 2000)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_DersEkle");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("IsActive", IsActive);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Bit;
            cmd.Parameters.Add(param);

            param = new SqlParameter("OkulID", OkulID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("BolumID", BolumID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Kod", Util.ParametreyiTemizle(DersKodu));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            if (!string.IsNullOrEmpty(DersIsmi))
            {
                param = new SqlParameter("Isim", Util.ParametreyiTemizle(DersIsmi));
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
            }

            if (!string.IsNullOrEmpty(DersAciklama))
            {
                param = new SqlParameter("Aciklama", Util.ParametreyiTemizle(DersAciklama));
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

    public static DataTable IsmeVeyaKodaGoreDersleriDondur(string dersKodu)
    {
        try
        {
            if (string.IsNullOrEmpty(dersKodu))
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("IsmeVeyaKodaGoreDersleriDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("Anahtar", Util.BuildLikeExpression(Util.ParametreyiTemizle(dersKodu)));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            return Util.GetDataTable(cmd);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static DataTable KodaGoreDersleriDondur(string dersKodu)
    {
        try
        {
            if (string.IsNullOrEmpty(dersKodu))
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("KodaGoreDersleriDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DersKodu", Util.BuildLikeExpression(Util.ParametreyiTemizle(dersKodu)));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            return Util.GetDataTable(cmd);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static DataTable IsmeGoreDersleriDondur(string dersIsmi)
    {
        try
        {
            if (string.IsNullOrEmpty(dersIsmi))
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("IsmeGoreDersleriDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DersIsim", Util.BuildLikeExpression(Util.ParametreyiTemizle(dersIsmi)));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            return Util.GetDataTable(cmd);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static DataTable DersProfilDondur(int dersID)
    {
        try
        {
            if (dersID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("DersProfilDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DersID", dersID);
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
    /// Bu ders icin butun onayli dosyalari dondurur
    /// </summary>
    /// <param name="dersID"></param>
    /// <returns></returns>
    public static DataTable DersDosyalariniDondur(int dersID)
    {
        try
        {
            if (dersID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("DersDosyalariniDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DersID", dersID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("DosyaDurumu", (int)Enums.DosyaDurumu.Onaylanmis);
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
    /// Bu ders icin bu kategorideki butun onayli dosyalari dondurur
    /// </summary>
    /// <param name="dersID"></param>
    /// <param name="dosyaKategoriTipi"></param>
    /// <returns></returns>
    public static DataTable DersDosyalariniDondur(int dersID, Enums.DosyaKategoriTipi dosyaKategoriTipi)
    {
        try
        {
            if (dersID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("DersDosyalariniDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DersID", dersID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("DosyaKategoriTipi", (int)dosyaKategoriTipi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("DosyaDurumu", (int)Enums.DosyaDurumu.Onaylanmis);
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

    public static bool DersDosyasiniKaydet(int dersID, int HocaID, Enums.DosyaKategoriTipi dosyaKategoriTipi, 
        string dosyaIsmi, string dosyaAdresi, int yukleyenKullaniciID, string aciklama, int KullaniciOnayPuani,
        int DosyaBoyut)
    {
        try
        {
            if (dersID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("DersDosyasiniKaydet");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DersID", dersID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            if (HocaID >= 0)
            {
                param = new SqlParameter("HocaID", HocaID);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }

            param = new SqlParameter("DosyaKategoriTipi", (int)dosyaKategoriTipi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("DosyaIsmi", dosyaIsmi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("DosyaAdres", Util.ParametreyiTemizle(dosyaAdresi));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("YukleyenKullaniciID", yukleyenKullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            if (!string.IsNullOrEmpty(aciklama))
            {
                param = new SqlParameter("Aciklama", Util.HTMLToDB(Util.ParametreyiTemizle(aciklama)));
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
            }

            param = new SqlParameter("YuklemeTarihi", System.DateTime.Now);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.DateTime;
            cmd.Parameters.Add(param);

            Enums.DosyaDurumu dosyaDurumu = Enums.DosyaDurumu.OnayBekliyor;
            if (KullaniciOnayPuani >= Convert.ToInt32(ConfigurationManager.AppSettings.Get("DersDosyaOnayPuani")))
            {
                dosyaDurumu = Enums.DosyaDurumu.Onaylanmis;
            }
            param = new SqlParameter("DosyaDurumu", (int)dosyaDurumu);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Boyut", DosyaBoyut);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.ExecuteNonQuery(cmd) == 1;
        }
        catch (Exception ex) { }
        return false;
    }

    /// <summary>
    /// Bu isimde bir dosya var mi, bunu kontrol eder
    /// Amazon'da cakisma olmasini engellemek icin kullanilir
    /// </summary>
    /// <param name="DersID"></param>
    /// <param name="DersKategoriTipi"></param>
    /// <param name="DosyaIsmi"></param>
    /// <returns></returns>
    public static bool DersDosyaIsmiVarMi(int DersID, Enums.DosyaKategoriTipi DersKategoriTipi,
        string DosyaIsmi)
    {
        try
        {
            if (DersID < 0)
            {
                return true;
            }
            SqlCommand cmd = new SqlCommand("DersDosyaIsmiVarMi");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DersID", DersID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("DosyaKategoriTipi", (int)DersKategoriTipi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("DosyaIsmi", Util.ParametreyiTemizle(DosyaIsmi));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            return !Util.ExecuteAndCheckReturnValue(cmd);
        }
        catch (Exception)
        {
            return true;
        }
    }

    /// <summary>
    /// Bir bolumdeki tum aktif dersleri dondurur
    /// </summary>
    /// <returns></returns>
    public static DataTable BolumdekiDersleriDondur(int BolumID)
    {
        try
        {
            if (BolumID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("BolumdekiDersleriDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("BolumID", BolumID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.GetDataTable(cmd);
        }
        catch (Exception) { }
        return null;
    }

    /// <summary>
    /// Bir okuldaki tum dersleri dondurur
    /// </summary>
    /// <returns></returns>
    public static DataTable OkuldakiDersleriDondur(int OkulID)
    {
        try
        {
            if (OkulID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("OkuldakiDersleriDondur");
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
    /// Dersi veren tum hocalari dondurur
    /// </summary>
    /// <returns></returns>
    public static DataTable DersiVerenHocalariDondur(int DersID)
    {
        try
        {
            if (DersID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("DersiVerenHocalariDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DersID", DersID);
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
    /// Dersi veren hocalardan, kullanicinin aktif yorumu bulunmayanlari dondurur
    /// </summary>
    /// <returns></returns>
    public static DataTable DersiVerenHocalariKullaniciyaGoreDondur(int DersID, int KullaniciID)
    {
        try
        {
            if (DersID < 0 || KullaniciID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("DersiVerenHocalariKullaniciyaGoreDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DersID", DersID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("KullaniciID", KullaniciID);
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
    /// Bir ders hakkinda yapilan yorumlari dondurur
    /// Eger yorum yoksa null dondurur
    /// </summary>
    /// <param name="DersID"></param>
    /// <returns></returns>
    public static DataTable DersYorumlariniDondur(int DersID)
    {
        try
        {
            if (DersID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("DersYorumlariniDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DersID", DersID);
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
    /// Kullanici derse daha once yorum yaptiysa true dondurur; yoksa false dondurur
    /// Not : Kullanilmiyor
    /// </summary>
    /// <param name="kullaniciID"></param>
    /// <param name="dersID"></param>
    /// <returns></returns>
    public static bool KullaniciDerseYorumYapmis(int kullaniciID, int dersID)
    {
        try
        {
            if (kullaniciID < 0 || dersID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("KullaniciDerseYorumYapmis");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", kullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            SqlParameter param2 = new SqlParameter("DersID", dersID);
            param2.Direction = ParameterDirection.Input;
            param2.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param2);

            return Util.ExecuteAndCheckReturnValue(cmd);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Kullanici derse daha once genel yorum yaptiysa true dondurur; yoksa false dondurur
    /// </summary>
    /// <param name="kullaniciID"></param>
    /// <param name="dersID"></param>
    /// <returns></returns>
    public static bool KullaniciDerseGenelYorumYapmis(int kullaniciID, int dersID)
    {
        try
        {
            if (kullaniciID < 0 || dersID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("KullaniciDerseGenelYorumYapmis");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", kullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            SqlParameter param2 = new SqlParameter("DersID", dersID);
            param2.Direction = ParameterDirection.Input;
            param2.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param2);

            return Util.ExecuteAndCheckReturnValue(cmd);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Kullanicinin yaptigi tum ders yorumlarini dondurur
    /// (Silinmis, onaylanmamis yorumlari da dondurur)
    /// Basarisiz olursa null dondurur
    /// </summary>
    /// <param name="KullaniciID"></param>
    /// <returns></returns>
    public static DataTable KullaniciDersYorumlariniDondur(int KullaniciID)
    {
        try
        {
            if (KullaniciID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("KullaniciDersYorumlariniDondur");
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
    /// ID'si verilen ders yorumunu dondurur
    /// </summary>
    /// <param name="DersYorumID"></param>
    /// <returns></returns>
    public static DataTable DersYorumunuDondur(int DersYorumID)
    {
        try
        {
            if (DersYorumID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("DersYorumunuDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DersYorumID", DersYorumID);
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

    public static bool DersYorumKaydet(int KullaniciID, int DersID, string Yorum, int ZorlukPuani, int HocaID, 
        int TavsiyePuani, string KayitsizHocaIsim, int KullaniciOnayPuani)
    {
        try
        {
            if (DersID < 0 || string.IsNullOrEmpty(Yorum) || KullaniciID < 0 || ZorlukPuani < 1 || ZorlukPuani > 5)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("DersYorumKaydet");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", KullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("DersID", DersID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            if (HocaID >= 0)
            {
                param = new SqlParameter("HocaID", HocaID);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                param = new SqlParameter("TavsiyePuani", TavsiyePuani);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }
            else if (!string.IsNullOrEmpty(KayitsizHocaIsim) && HocaID == -2)
            {
                param = new SqlParameter("KayitsizHocaIsim", Util.ParametreyiTemizle(KayitsizHocaIsim));
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);

                param = new SqlParameter("TavsiyePuani", TavsiyePuani);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }

            param = new SqlParameter("Yorum", Util.HTMLToDB(Util.ParametreyiTemizle(Yorum)));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("ZorlukPuani", ZorlukPuani);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            Enums.YorumDurumu yorumDurumu = Enums.YorumDurumu.OnayBekliyor;
            if (KullaniciOnayPuani >= Convert.ToInt32(ConfigurationManager.AppSettings.Get("DersYorumOnayPuani")))
            {
                yorumDurumu = Enums.YorumDurumu.Onaylanmis;
            }
            param = new SqlParameter("YorumDurumu", (int)yorumDurumu);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.ExecuteAndCheckReturnValue(cmd);
        }
        catch
        {
            return false;
        }
    }

    public static bool DersYorumGuncelle(int DersYorumID, string Yorum, int ZorlukPuani, int HocaID, 
        int TavsiyePuani, string KayitsizHocaIsim, int KullaniciOnayPuani)
    {
        try
        {
            if (DersYorumID < 0 || string.IsNullOrEmpty(Yorum) || ZorlukPuani < 1 || ZorlukPuani > 5 ||
                TavsiyePuani < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("DersYorumGuncelle");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DersYorumID", DersYorumID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            if (HocaID >= 0)
            {
                param = new SqlParameter("HocaID", HocaID);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                param = new SqlParameter("TavsiyePuani", TavsiyePuani);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }
            else if (!string.IsNullOrEmpty(KayitsizHocaIsim) && HocaID == -2)
            {
                param = new SqlParameter("KayitsizHocaIsim", Util.ParametreyiTemizle(KayitsizHocaIsim));
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);

                param = new SqlParameter("TavsiyePuani", TavsiyePuani);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }

            param = new SqlParameter("Yorum", Util.HTMLToDB(Util.ParametreyiTemizle(Yorum)));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("ZorlukPuani", ZorlukPuani);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            Enums.YorumDurumu yorumDurumu = Enums.YorumDurumu.OnayBekliyor;
            if (KullaniciOnayPuani >= Convert.ToInt32(ConfigurationManager.AppSettings.Get("DersYorumOnayPuani")))
            {
                yorumDurumu = Enums.YorumDurumu.Onaylanmis;
            }
            param = new SqlParameter("YorumDurumu", (int)yorumDurumu);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.ExecuteAndCheckReturnValue(cmd);
        }
        catch
        {
            return false;
        }
    }


    public static bool Admin_DersDosyaGuncelle(int DosyaID, int DersID, int HocaID,
        Enums.DosyaKategoriTipi DosyaKategori, string SilinmeNedeni, 
        string DosyaIsmi, string DosyaAdresi, string Aciklama, DateTime EklenmeTarihi, 
        int EkleyenKullanici, int IndirilmeSayisi)
    {
        try
        {
            if (DosyaID < 0 || DersID < 0 || string.IsNullOrEmpty(DosyaIsmi) ||
                string.IsNullOrEmpty(DosyaAdresi))
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_DersDosyaGuncelle");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DosyaID", DosyaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("DersID", DersID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            if (HocaID >= 0)
            {
                param = new SqlParameter("HocaID", HocaID);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }

            param = new SqlParameter("DosyaKategori", (int)DosyaKategori);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            if (!string.IsNullOrEmpty(SilinmeNedeni))
            {
                param = new SqlParameter("SilinmeNedeni", Util.ParametreyiTemizle(SilinmeNedeni));
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
            }

            param = new SqlParameter("DosyaIsmi", Util.ParametreyiTemizle(DosyaIsmi));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("DosyaAdresi", Util.ParametreyiTemizle(DosyaAdresi));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);
            param = new SqlParameter("Aciklama", Util.ParametreyiTemizle(Aciklama));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("EklenmeTarihi", EklenmeTarihi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.SmallDateTime;
            cmd.Parameters.Add(param);

            param = new SqlParameter("EkleyenKullanici", EkleyenKullanici);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("IndirilmeSayisi", IndirilmeSayisi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.ExecuteNonQuery(cmd) == 1;
        }
        catch (Exception ex) { }
        return false;
    }
}
