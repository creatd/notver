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
using System.Collections.Generic;

/// <summary>
/// Summary description for Hoca
/// </summary>
public class Hocalar
{
    /// <summary>
    /// Bir okuldaki tum hocalar dondurur
    /// </summary>
    /// <returns></returns>
    public static DataTable OkuldakiHocalariDondur(int OkulID)
    {
        try
        {
            if (OkulID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("OkuldakiHocalariDondur");
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
    /// Verilen hocayla ayni okuldaki diger hocalari dondurur (populerlik sirasina gore)
    /// </summary>
    /// <param name="hocaID"></param>
    /// <param name="sayi"></param>
    /// <returns></returns>
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

    /*
    /// <summary>
    /// Hocaya verilen puanlari kaydeder.
    /// Basarili olursa true, aksi takdirde false dondurur.
    /// </summary>
    /// <param name="KullaniciID"></param>
    /// <param name="HocaID"></param>
    /// <param name="Puanlar"></param>
    /// <returns></returns>
    public static bool HocaPuanKaydet(int KullaniciID, int HocaID, int[] Puanlar)
    {
        try
        {
            if (KullaniciID < 0 || HocaID < 0 || Puanlar == null)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < Puanlar.Length; i++)
                {
                    if (Puanlar[i] < 1 || Puanlar[i] > 5)
                    {
                        return false;
                    }
                }
            }

            SqlCommand cmd = new SqlCommand("HocaPuanKaydet");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", KullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("HocaID", HocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan1", Puanlar[0]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan2", Puanlar[1]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan3", Puanlar[2]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan4", Puanlar[3]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan5", Puanlar[4]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.ExecuteAndCheckReturnValue(cmd);
        }
        catch
        {
            return false;
        }
    }*/
    /*
    public static bool HocaYorumKaydet(int KullaniciID, int HocaID, string Yorum, int KullaniciPuanaraligi, 
        List<int> DersIDleri, List<string> BilinmeyenDersIsimleri)
    {
        try
        {
            if (KullaniciID < 0 || HocaID < 0 || string.IsNullOrEmpty(Yorum) || KullaniciPuanaraligi < 0 
                || KullaniciPuanaraligi > 5)
            {
                return false;
            }
            if (DersIDleri.Count > 0)
            {
                foreach (int ID in DersIDleri)
                {
                    if (ID < 0)
                        return false;
                }
            }
            if (BilinmeyenDersIsimleri.Count > 0)
            {
                foreach (string bilinmeyenDersIsmi in BilinmeyenDersIsimleri)
                {
                    if (string.IsNullOrEmpty(bilinmeyenDersIsmi))
                    {
                        return false;
                    }
                }
            }
            SqlCommand cmd = new SqlCommand("HocaYorumKaydet");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", KullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("HocaID", HocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Yorum", Yorum);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Kullanici_Puanaraligi", KullaniciPuanaraligi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("HocaYorumID", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            if (Util.ExecuteAndCheckReturnValue(cmd))
            {
                if(Util.GecerliStringSayi(cmd.Parameters["HocaYorumID"]))
                {
                    int hocaYorumID = Convert.ToInt32(cmd.Parameters["HocaYorumID"]);
                    if (hocaYorumID > 0)
                    {
                        foreach (int DersID in DersIDleri)
                        {
                            cmd = new SqlCommand("HocaYorumDersKaydet");
                            cmd.CommandType = CommandType.StoredProcedure;

                            param = new SqlParameter("HocaYorumID", hocaYorumID);
                            param.Direction = ParameterDirection.Input;
                            param.SqlDbType = SqlDbType.Int;
                            cmd.Parameters.Add(param);

                            param = new SqlParameter("DersID", DersID);
                            param.Direction = ParameterDirection.Input;
                            param.SqlDbType = SqlDbType.Int;
                            cmd.Parameters.Add(param);

                            if (!Util.ExecuteAndCheckReturnValue(cmd))
                            {
                                return false;
                            }
                        }
                        foreach (string bilinmeyenDersIsmi in BilinmeyenDersIsimleri)
                        {
                            cmd = new SqlCommand("HocaYorumDersKaydet");
                            cmd.CommandType = CommandType.StoredProcedure;

                            param = new SqlParameter("HocaYorumID", hocaYorumID);
                            param.Direction = ParameterDirection.Input;
                            param.SqlDbType = SqlDbType.Int;
                            cmd.Parameters.Add(param);

                            param = new SqlParameter("BilinmeyenDersIsmi", bilinmeyenDersIsmi);
                            param.Direction = ParameterDirection.Input;
                            param.SqlDbType = SqlDbType.NChar;
                            cmd.Parameters.Add(param);

                            if (!Util.ExecuteAndCheckReturnValue(cmd))
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                }
            }
            return false;
        }
        catch
        {
            return false;
        }
    }*/

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

    public static bool HocaYorumPuanKaydet(int KullaniciID, int HocaID, int[] Puanlar, string Yorum,
        int KullaniciPuanaraligi, List<int> DersIDleri, List<string> BilinmeyenDersIsimleri)
    {
        try
        {
            if (KullaniciID < 0 || HocaID < 0 || Puanlar == null || Yorum == null || string.IsNullOrEmpty(Yorum) ||
                KullaniciPuanaraligi < 1 || KullaniciPuanaraligi > 5)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < Puanlar.Length; i++)
                {
                    if (Puanlar[i] < 1 || Puanlar[i] > 5)
                    {
                        return false;
                    }
                }
            }
            foreach (int dersID in DersIDleri)
            {
                if (dersID <= 0 && dersID != -2)
                {
                    return false;
                }
            }
            foreach (string dersIsmi in BilinmeyenDersIsimleri)
            {
                if (string.IsNullOrEmpty(dersIsmi))
                {
                    return false;
                }
            }
            SqlCommand cmd = new SqlCommand("HocaYorumPuanKaydet");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", KullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("HocaID", HocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan1", Puanlar[0]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan2", Puanlar[1]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan3", Puanlar[2]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan4", Puanlar[3]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("puan5", Puanlar[4]);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Yorum", Yorum);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Kullanici_Puanaraligi", KullaniciPuanaraligi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("HocaYorumID", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            if (Util.ExecuteAndCheckReturnValue(cmd))
            {
                if (Util.GecerliStringSayi(cmd.Parameters["HocaYorumID"].Value))
                {
                    int hocaYorumID = Convert.ToInt32(cmd.Parameters["HocaYorumID"].Value);
                    if (hocaYorumID > 0)
                    {
                        for (int i = 0; i < DersIDleri.Count; i++)
                        {
                            int DersID = DersIDleri[i];
                            cmd = new SqlCommand("HocaYorumDersKaydet");
                            cmd.CommandType = CommandType.StoredProcedure;

                            param = new SqlParameter("HocaYorumID", hocaYorumID);
                            param.Direction = ParameterDirection.Input;
                            param.SqlDbType = SqlDbType.Int;
                            cmd.Parameters.Add(param);
                            if (DersID == -2)  //Bilinmeyen ders ismiyle kaydet
                            {
                                param = new SqlParameter("BilinmeyenDersIsmi", bilinmeyenDersIsmi);
                                param.Direction = ParameterDirection.Input;
                                param.SqlDbType = SqlDbType.NChar;
                                cmd.Parameters.Add(param);
                            }
                            else  //DersID ile kaydet
                            {
                                param = new SqlParameter("DersID", DersID);
                                param.Direction = ParameterDirection.Input;
                                param.SqlDbType = SqlDbType.Int;
                                cmd.Parameters.Add(param);
                            }
                            if (!Util.ExecuteAndCheckReturnValue(cmd))
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                }
            }
            return false;
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
    /// Kullanicinin hoca icin yaptigi yorum bilgisini dondurur
    /// </summary>
    /// <param name="KullaniciID"></param>
    /// <param name="HocaID"></param>
    /// <returns></returns>
    public static List<object> KullaniciHocaYorumunuDondur(int KullaniciID, int HocaID)
    {
        try
        {
            if (KullaniciID < 0 || HocaID < 0)
            {
                return null;
            }
            //Ders yorumu
            SqlCommand cmd = new SqlCommand("KullaniciHocaYorumunuDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", KullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("HocaID", HocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            DataTable dtHocaYorumu = Util.GetDataTable(cmd);
            if(dtHocaYorumu != null && dtHocaYorumu.Rows.Count == 1)
            {
                //Yorumun ilgili oldugu dersler
                cmd = new SqlCommand("HocaYorumDersleriDondur");
                cmd.CommandType = CommandType.StoredProcedure;

                param = new SqlParameter("HocaYorumID", Convert.ToInt32(dtHocaYorumu.Rows[0]["HOCAYORUM_ID"]));
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                DataTable dtIlgiliDersler = Util.GetDataTable(cmd);

                if (dtIlgiliDersler != null)  //Not: dtIlgiliDersler bos olabilir ancak null olmamali
                {
                    //Puanlar
                    cmd = new SqlCommand("KullaniciHocaPuanlariniDondur");
                    cmd.CommandType = CommandType.StoredProcedure;

                    param = new SqlParameter("KullaniciID", KullaniciID);
                    param.Direction = ParameterDirection.Input;
                    param.SqlDbType = SqlDbType.Int;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("HocaID", HocaID);
                    param.Direction = ParameterDirection.Input;
                    param.SqlDbType = SqlDbType.Int;
                    cmd.Parameters.Add(param);

                    DataTable dtHocaPuanlari = Util.GetDataTable(cmd);
                    if (dtHocaPuanlari != null && dtHocaPuanlari.Rows.Count == 1)
                    {
                        //List<object> olustur
                        //yorum - puan1 - puan2 - puan3 - puan4 - puan5 - { (Ders ID - Ders Kodu)| (-1 - Ders Ismi) }(*)
                        List<object> sonuc = new List<object>();
                        sonuc.Add(dtHocaYorumu.Rows[0]["YORUM"].ToString());
                        DataRow drPuanlar = dtHocaPuanlari.Rows[0];
                        sonuc.Add(Convert.ToInt32(drPuanlar["PUAN1"]));
                        sonuc.Add(Convert.ToInt32(drPuanlar["PUAN2"]));
                        sonuc.Add(Convert.ToInt32(drPuanlar["PUAN3"]));
                        sonuc.Add(Convert.ToInt32(drPuanlar["PUAN4"]));
                        sonuc.Add(Convert.ToInt32(drPuanlar["PUAN5"]));
                        foreach (DataRow dr in dtIlgiliDersler.Rows)
                        {
                            if(Util.GecerliStringSayi(dr["DERS_ID"]) && Util.GecerliString(dr["DERS_KODU"]))
                            {
                                sonuc.Add(Convert.ToInt32(dr["DERS_ID"]));
                                sonuc.Add(dr["DERS_KODU"].ToString());
                            }
                            else if (Util.GecerliString(dr["KAYITSIZ_DERS_ISMI"]))
                            {
                                sonuc.Add(-1);
                                sonuc.Add(dr["KAYITSIZ_DERS_ISMI"].ToString());
                            }
                        }
                        return sonuc;
                    }
                }
            }
            return null;
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
