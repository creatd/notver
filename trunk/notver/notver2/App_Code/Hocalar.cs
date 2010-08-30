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
using System.Text;

/// <summary>
/// Summary description for Hoca
/// </summary>
public class Hocalar
{

    public static DataTable AyniOkuldakiHocalariDondur(int hocaID, int sayi)
    {
        try
        {
            if (hocaID < 0 || sayi < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("AyniOkuldakiHocalariDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("HocaID", hocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Sayi", sayi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            DataTable dt = Util.GetDataTable(cmd);
            return dt;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Hoca.aspx sayfasindan, bir hocanin profil sayfasindaki veriyi dondurmek icin cagirilir
    /// </summary>
    /// <param name="HocaID"></param>
    /// <returns></returns>
    public static DataTable HocaProfilDondur(int HocaID)
    {
        try
        {
            if (HocaID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("HocaProfilDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("HocaID", HocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            DataTable dt = Util.GetDataTable(cmd);
            return dt;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Hocaya verilen puanlari kaydeder.
    /// Basarili olursa true, aksi takdirde false dondurur.
    /// </summary>
    /// <param name="kullaniciID"></param>
    /// <param name="hocaID"></param>
    /// <param name="puanlar"></param>
    /// <returns></returns>
    public static bool HocaPuanKaydet(int kullaniciID, int hocaID, int[] puanlar)
    {
        try
        {
            if (kullaniciID < 0 || hocaID < 0 || puanlar == null)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("HocaPuanKaydet");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", kullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("HocaID", hocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan1", puanlar[0]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan2", puanlar[1]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan3", puanlar[2]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan4", puanlar[3]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan5", puanlar[4]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return (Util.ExecuteNonQuery(cmd) > 0);
        }
        catch
        {
            return false;
        }
    }

    public static bool HocaYorumKaydet(int kullaniciID, int hocaID, string[] yorumlar, int kullaniciPuanaraligi)
    {
        try
        {
            if (kullaniciID < 0 || hocaID < 0 || yorumlar == null || kullaniciPuanaraligi < 0 || kullaniciPuanaraligi > 5)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("HocaYorumKaydet");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", kullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("HocaID", hocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            if (yorumlar[0].Length > 0)
            {
                param = new SqlParameter("Yorum_Olumlu", yorumlar[0]);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
            }
            if (yorumlar[1].Length > 0)
            {
                param = new SqlParameter("Yorum_Olumsuz", yorumlar[1]);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
            }
            if (yorumlar[2].Length > 0)
            {
                param = new SqlParameter("Yorum_Ozet", yorumlar[2]);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
            }

            param = new SqlParameter("Kullanici_Puanaraligi", kullaniciPuanaraligi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return (Util.ExecuteNonQuery(cmd) > 0);
        }
        catch
        {
            return false;
        }
    }

    public static bool HocaYorumGuncelle(int kullaniciID, int hocaID, string[] yorumlar, int kullaniciPuanaraligi)
    {
        try
        {
            if (kullaniciID < 0 || hocaID < 0 || yorumlar == null || kullaniciPuanaraligi < 0 || kullaniciPuanaraligi > 5)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("HocaYorumGuncelle");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", kullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("HocaID", hocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            if (yorumlar[0].Length > 0)
            {
                param = new SqlParameter("YorumOlumlu", yorumlar[0]);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
            }
            if (yorumlar[1].Length > 0)
            {
                param = new SqlParameter("YorumOlumsuz", yorumlar[1]);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
            }
            if (yorumlar[2].Length > 0)
            {
                param = new SqlParameter("YorumOzet", yorumlar[2]);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
            }

            param = new SqlParameter("KullaniciPuanaraligi", kullaniciPuanaraligi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return (Util.ExecuteNonQuery(cmd) > 0);
        }
        catch
        {
            return false;
        }
    }

    public static bool HocaYorumPuanKaydet(int kullaniciID, int hocaID, int[] puanlar, string[] yorumlar, int kullaniciPuanaraligi)
    {
        try
        {
            if (kullaniciID < 0 || hocaID < 0 || puanlar == null || yorumlar == null || kullaniciPuanaraligi < 0 || kullaniciPuanaraligi > 5)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("HocaYorumPuanKaydet");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", kullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("HocaID", hocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan1", puanlar[0]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan2", puanlar[1]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan3", puanlar[2]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan4", puanlar[3]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan5", puanlar[4]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            if (yorumlar[0].Length > 0)
            {
                param = new SqlParameter("Yorum_Olumlu", yorumlar[0]);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
            }
            if (yorumlar[1].Length > 0)
            {
                param = new SqlParameter("Yorum_Olumsuz", yorumlar[1]);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
            }
            if (yorumlar[2].Length > 0)
            {
                param = new SqlParameter("Yorum_Ozet", yorumlar[2]);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
            }

            param = new SqlParameter("Kullanici_Puanaraligi", kullaniciPuanaraligi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return (Util.ExecuteNonQuery(cmd) > 0);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Hocanin ders vermis oldugu okullari dondurur
    /// </summary>
    /// <param name="hocaID"></param>
    /// <returns></returns>
    public static DataTable HocaOkullariniDondur(int hocaID)
    {
        try 
	    {
            if (hocaID < 0)
            {
                return null;
            }

            SqlCommand cmd = new SqlCommand("HocaOkullariniDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("HocaID", hocaID);
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


    public static bool HocaPuanGuncelle(int kullaniciID, int hocaID, int[] puanlar)
    {
        try
        {
            if (kullaniciID < 0 || hocaID < 0 || puanlar == null)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("HocaPuanGuncelle");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", kullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("HocaID", hocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan1", puanlar[0]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan2", puanlar[1]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan3", puanlar[2]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan4", puanlar[3]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan5", puanlar[4]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return (Util.ExecuteNonQuery(cmd) > 0);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Bir hocanin vermis oldugu dersleri ve ders id'lerini dondurur
    /// </summary>
    /// <param name="hocaID"></param>
    /// <returns></returns>
    public static string[][] HocaDersleriniDondur(int hocaID)
    {
        try
        {
            if (hocaID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("HocaDersleriniDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("HocaID", hocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);


            StringBuilder sb = new StringBuilder();
            DataTable dt = Util.GetDataTable(cmd);
            if (!(dt.Rows.Count > 0))
            {
                return null;
            }
            else
            {
                string[][] result = new string[dt.Rows.Count][];
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    result[i] = new string[4];
                    result[i][0] = Convert.ToString(dr["KOD"]);
                    result[i][1] = Convert.ToString(dr["DERS_ID"]);
                    result[i][2] = Convert.ToString(dr["DERS_ISIM"]);
                    result[i][3] = Convert.ToString(dr["OKUL_ISIM"]);
                    i++;
                }
                return result;
            }
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Bir hoca hakkinda yapilan yorumlari dondurur
    /// Eger yorum yoksa null dondurur
    /// </summary>
    /// <param name="HocaID"></param>
    /// <returns></returns>
    public static DataTable HocaYorumlariniDondur(int HocaID)
    {
        try
        {
            if (HocaID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("HocaYorumlariniDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("HocaID", HocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            DataTable dt = Util.GetDataTable(cmd);
            return dt;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Hoca puanlarini dondur
    /// Eger hoca puanlari yoksa veya hata olusursa null dondurur
    /// </summary>
    /// <param name="HocaID"></param>
    /// <returns></returns>
    public static float[] HocaPuanlariniDondur(int HocaID)
    {
        try
        {
            if (HocaID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("HocaPuanlariniDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("HocaID", HocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            DataTable dt = Util.GetDataTable(cmd);
            DataRow dr = dt.Rows[0];
            float puanSayisi = (float)Convert.ToInt32(dr["PUAN_SAYISI"]);
            if (puanSayisi < 1)
                return null;
            float[] result = new float[5];
            result[0] = Convert.ToInt32(dr["PUAN1"]) / puanSayisi;
            result[1] = Convert.ToInt32(dr["PUAN2"]) / puanSayisi;
            result[2] = Convert.ToInt32(dr["PUAN3"]) / puanSayisi;
            result[3] = Convert.ToInt32(dr["PUAN4"]) / puanSayisi;
            result[4] = Convert.ToInt32(dr["PUAN5"]) / puanSayisi;
            return result;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Hoca sayfasindaki puan aciklamalarini, sirali bir sekilde dondurur
    /// Not : Sira kontrolu stored procedure'de order by PUAN_NUMARASI seklinde saglaniyor
    /// </summary>
    /// <returns></returns>
    public static string[] HocaPuanAciklamalariniDondur()
    {
        string[] result;
        try
        {
            SqlCommand cmd = new SqlCommand("HocaPuanAciklamalariniDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            StringBuilder sb = new StringBuilder();
            DataTable dt = Util.GetDataTable(cmd);
            result = new string[dt.Rows.Count];
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                result[i] = dr["ACIKLAMA"].ToString();
                i++;
            }
            return result;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Kullanici hocaya daha once yorum yaptiysa true dondurur; yoksa false dondurur
    /// </summary>
    /// <param name="kullaniciID"></param>
    /// <param name="hocaID"></param>
    /// <returns></returns>
    public static bool KullaniciHocayaYorumYapmis(int kullaniciID, int hocaID)
    {
        try
        {
            if (kullaniciID < 0 || hocaID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("KullaniciHocayaYorumYapmis");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", kullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            SqlParameter param2 = new SqlParameter("HocaID", hocaID);
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
    /// Kullanicinin hocaya daha once verdigi puanlari int[5] olarak dondurur
    /// </summary>
    public static int[] KullaniciHocaPuanlariniDondur(int kullaniciID, int hocaID)
    {
        try
        {
            if (kullaniciID < 0 || hocaID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("KullaniciHocaPuanlariniDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", kullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("HocaID", hocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            DataTable dt = Util.GetDataTable(cmd);
            int[] puanlar = new int[5];
            for (int i = 0; i < 5; i++)
            {
                string indexString = "PUAN" + (i + 1);
                puanlar[i] = Convert.ToInt32(dt.Rows[0][indexString]);
            }
            return puanlar;
        }
        catch
        {
            return null;
        }
    }


    /// <summary>
    /// Kullanicinin hoca icin yaptigi yorumlari string[3] olarak dondurur
    /// </summary>
    /// <param name="kullaniciID"></param>
    /// <param name="hocaID"></param>
    /// <returns></returns>
    public static string[] KullaniciHocaYorumlariniDondur(int kullaniciID, int hocaID)
    {
        try
        {
            if (kullaniciID < 0 || hocaID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("KullaniciHocaYorumlariniDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", kullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("HocaID", hocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            DataRow dr = (Util.GetDataTable(cmd)).Rows[0];
            string[] yorumlar = new string[3];

            if (dr["YORUM_OLUMLU"] != null && dr["YORUM_OLUMLU"] != System.DBNull.Value)
            {
                yorumlar[0] = dr["YORUM_OLUMLU"].ToString();
            }
            if (dr["YORUM_OLUMSUZ"] != null && dr["YORUM_OLUMSUZ"] != System.DBNull.Value)
            {
                yorumlar[1] = dr["YORUM_OLUMSUZ"].ToString();
            }
            if (dr["YORUM_OZET"] != null && dr["YORUM_OZET"] != System.DBNull.Value)
            {
                yorumlar[2] = dr["YORUM_OZET"].ToString();
            }
            return yorumlar;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Kullanici hocaya daha once puan verdiyse true dondurur; yoksa false dondurur
    /// </summary>
    /// <param name="kullaniciID"></param>
    /// <param name="hocaID"></param>
    /// <returns></returns>
    public static bool KullaniciHocayaPuanVermis(int kullaniciID, int hocaID)
    {
        try
        {
            if (kullaniciID < 0 || hocaID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("KullaniciHocayaPuanVermis");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", kullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("HocaID", hocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

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
    /// Returns data from Hocalar table performing the given like expression on their names
    /// Returns null on error
    /// </summary>
    /// <param name="likeExpression"></param>
    /// <returns></returns>
    public static DataTable IsmeGoreHocalariDondur(string hocaIsmi)
    {
        try
        {
            if (string.IsNullOrEmpty(hocaIsmi))
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("IsmeGoreHocalariDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("HocaIsim", Util.BuildLikeExpression(hocaIsmi));
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
}
