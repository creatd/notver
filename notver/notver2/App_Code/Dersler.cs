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
using System.Data.SqlClient;

/// <summary>
/// Summary description for Ders
/// </summary>
public class Dersler
{

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

            SqlParameter param = new SqlParameter("DersKodu", Util.BuildLikeExpression(dersKodu));
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

            SqlParameter param = new SqlParameter("DersIsim", Util.BuildLikeExpression(dersIsmi));
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

            return Util.GetDataTable(cmd);
        }
        catch (Exception)
        {
            return null;
        }
    }

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

            return Util.GetDataTable(cmd);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static void DersDosyasiniKaydet(int dersID, Enums.DosyaKategoriTipi dosyaKategoriTipi, string dosyaIsmi, string dosyaAdresi, int yukleyenKullaniciID, string aciklama)
    {
        try
        {
            if (dersID < 0)
            {
                //TODO: admine msj (dosyayi kaydettik ama veritabanina yazamadik)
                return;
            }
            SqlCommand cmd = new SqlCommand("DersDosyasiniKaydet");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DersID", dersID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("DosyaKategoriTipi", (int)dosyaKategoriTipi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("DosyaIsmi",SqlDbType.NVarChar);
            if (string.IsNullOrEmpty(dosyaIsmi))
            {
                param.Value = dosyaIsmi;
            }
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter("DosyaAdres", dosyaAdresi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("YukleyenKullaniciID", yukleyenKullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Aciklama", aciklama);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("YuklemeTarihi", System.DateTime.Now);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.DateTime;
            cmd.Parameters.Add(param);

            if (Util.ExecuteNonQuery(cmd) == -1)
            {
                //TODO: ciddi sorun, admine haber ver (dosyayi kaydettik ama veritabanina yazamadik)
            }
        }
        catch (Exception)
        {
            //TODO: ciddi sorun, admine haber ver (dosyayi kaydettik ama veritabanina yazamadik)
        }
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
    /// Kullanicinin ders icin yaptigi yorumu dondurur
    /// </summary>
    /// <param name="kullaniciID"></param>
    /// <param name="okulID"></param>
    /// <returns></returns>
    public static DataTable KullaniciDersYorumunuDondur(int kullaniciID, int dersID)
    {
        try
        {
            if (kullaniciID < 0 || dersID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("KullaniciDersYorumunuDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", kullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("DersID", dersID);
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

    public static bool DersYorumKaydet(int kullaniciID, int dersID, string yorum, int ZorlukPuani, int hocaID, int TavsiyePuani)
    {
        try
        {
            if (dersID < 0 || string.IsNullOrEmpty(yorum) || kullaniciID < 0 || ZorlukPuani < 1 || ZorlukPuani > 5)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("DersYorumKaydet");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", kullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("DersID", dersID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            if (hocaID > 0)
            {
                param = new SqlParameter("HocaID", hocaID);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                param = new SqlParameter("TavsiyePuani", TavsiyePuani);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }

            param = new SqlParameter("Yorum", yorum);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("ZorlukPuani", ZorlukPuani);
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

    public static bool DersYorumGuncelle(int kullaniciID, int dersID, string yorum, int ZorlukPuani, int hocaID, int TavsiyePuani)
    {
        try
        {
            if (dersID < 0 || string.IsNullOrEmpty(yorum) || kullaniciID < 0 || ZorlukPuani < 1 || ZorlukPuani > 5)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("DersYorumGuncelle");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", kullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("DersID", dersID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            if (hocaID > 0 && TavsiyePuani >= 1 && TavsiyePuani <= 5)
            {
                param = new SqlParameter("HocaID", hocaID);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                param = new SqlParameter("TavsiyePuani", TavsiyePuani);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }

            param = new SqlParameter("Yorum", yorum);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("ZorlukPuani", ZorlukPuani);
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

}
