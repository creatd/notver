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
using System.Text;
using System.Collections.Generic;

/// <summary>
/// Summary description for Hoca
/// </summary>
public class Hocalar
{
    public static bool Admin_KayitsizHocaIliskilendir(int HocaID, string KayitsizHocaIsim)
    {
        try
        {
            if (HocaID < 0 || string.IsNullOrEmpty(KayitsizHocaIsim))
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_KayitsizHocaIliskilendir");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("HocaID", HocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("KayitsizHocaIsim", KayitsizHocaIsim);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            return Util.ExecuteNonQuery(cmd) >= 1;
	    }
	    catch (Exception ex)    {}
        return false;
    }

    public static DataTable Admin_KayitsizHocalariDondur()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Admin_KayitsizHocalariDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            return Util.GetDataTable(cmd);
        }
        catch (Exception ex) { }
        return null;
    }

    /// <summary>
    /// Yorumu yayindan kaldirir ve kullanicinin onay puanini dusurur
    /// </summary>
    /// <param name="HocaYorumID"></param>
    /// <param name="KullaniciID"></param>
    /// <returns></returns>
    public static bool Admin_HocaYorumYayindanKaldir(int HocaYorumID, int KullaniciID, string SilinmeNedeni)
    {
        try
        {
            if (HocaYorumID < 0 || KullaniciID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_HocaYorumYayindanKaldir");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("YorumID", HocaYorumID);
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

            int onayDegeri = Convert.ToInt32(ConfigurationManager.AppSettings["HocaYorumOnayDegeri"]);
            onayDegeri = onayDegeri * 2;    //Ceza olarak iki kat dusuruyoruz puani
            param = new SqlParameter("OnayDegeri", onayDegeri);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            if(!string.IsNullOrEmpty(SilinmeNedeni))
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
    /// Yorumu ve puani onaylar ve kullanicinin onay puanini yukseltir
    /// </summary>
    /// <param name="HocaYorumID"></param>
    /// <param name="KullaniciID"></param>
    /// <returns></returns>
    public static bool Admin_HocaYorumPuanOnayla(int HocaYorumID, int KullaniciID)
    {
        try
        {
            if (HocaYorumID < 0 || KullaniciID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_HocaYorumPuanOnayla");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("YorumID", HocaYorumID);
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

            int onayDegeri = Convert.ToInt32(ConfigurationManager.AppSettings["HocaYorumOnayDegeri"]);
            param = new SqlParameter("OnayDegeri", onayDegeri);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.ExecuteAndCheckReturnValue(cmd);
        }
        catch (Exception ex) { }
        return false;
    }

    public static bool Admin_HocaYorumGuncelle(int HocaYorumID, string SilinmeNedeni, int HocaID, int KullaniciPuanAraligi,
        string Yorum, DateTime GonderilmeTarihi, int AlkisPuani)
    {
        try
        {
            if (HocaYorumID < 0 || HocaID < 0 || string.IsNullOrEmpty(Yorum))
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_HocaYorumGuncelle");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("HocaYorumID", HocaYorumID);
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


            param = new SqlParameter("HocaID", HocaID);
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

            param = new SqlParameter("KullaniciPuanAraligi", KullaniciPuanAraligi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.ExecuteNonQuery(cmd) == 1;
        }
        catch (Exception ex) { }
        return false;
    }

    /// <summary>
    /// Normal silmeleri yorum durumunu degistirerek yap, bu metod veritabanindan tamamen siler
    /// </summary>
    /// <param name="HocaYorumID"></param>
    /// <returns></returns>
    public static bool HocaYorumSil(int HocaYorumID)
    {
        try
        {
            if (HocaYorumID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_HocaYorumSil");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("HocaYorumID", HocaYorumID);
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

    public static DataTable Admin_HocaYorumlariDondur(int HocaID, int OkulID, Enums.YorumDurumu YorumDurumu, bool HepsiniGoster)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Admin_HocaYorumlariDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            if (!HepsiniGoster)
            {
                SqlParameter param = new SqlParameter("YorumDurumu", (int)YorumDurumu);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }

            if (HocaID >= 0)
            {
                SqlParameter param = new SqlParameter("HocaID", HocaID);
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

    //Boyle bir hoca-okul iliskisi daha once eklenmis mi kontrolu prosedur icinde yapiliyor
    public static bool HocaOkulEkle(int HocaID, int OkulID, int BaslangicYili, int BitisYili)
    {
        try
        {
            if (HocaID < 0 || OkulID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_HocaOkulEkle");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("HocaID", HocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("OkulID", OkulID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            if (BaslangicYili > 0)
            {
                param = new SqlParameter("BaslangicYili", BaslangicYili);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }

            if (BitisYili >= 0)
            {
                param = new SqlParameter("BitisYili", BitisYili);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }

            return Util.ExecuteNonQuery(cmd) == 1;
        }
        catch (Exception ex) { }
        return false;
    }

    public static bool HocaOkulSil(int HocaID, int OkulID)
    {
        try
        {
            if (HocaID < 0 || OkulID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_HocaOkulSil");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("HocaID", HocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("OkulID", OkulID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.ExecuteNonQuery(cmd) == 1;
        }
        catch (Exception ex) { }
        return false;
    }

    //Boyle bir hoca-ders iliskisi daha once eklenmis mi kontrolu prosedur icinde yapiliyor
    public static bool HocaDersEkle(int HocaID, int DersID)
    {
        try
        {
            if (HocaID < 0 || DersID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_HocaDersEkle");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("HocaID", HocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("DersID", DersID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.ExecuteNonQuery(cmd) == 1;
        }
        catch (Exception ex) { }
        return false;
    }

    public static bool HocaDersSil(int HocaID, int DersID)
    {
        try
        {
            if (HocaID < 0 || DersID < 0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_HocaDersSil");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("HocaID", HocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("DersID", DersID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.ExecuteNonQuery(cmd) == 1;
        }
        catch (Exception ex) { }
        return false;
    }

    /// <summary>
    /// Hocayi veritabanindan siler, inaktif yapmak icin HocaGuncelle'yi kullan
    /// Onemli not: Hocayi silerken, hocanin ders ve okul iliskilerini de siler
    /// </summary>
    /// <param name="HocaID"></param>
    /// <returns></returns>
    public static bool HocaSil(int HocaID)
    {
        try
        {
            if (HocaID <0)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_HocaSil");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("HocaID", HocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.ExecuteAndCheckReturnValue(cmd);
        }
        catch (Exception ex)
        {
        }
        return false;
    }

    public static bool HocaGuncelle(int HocaID, bool IsActive, string Isim, string Unvan, int YorumSayisi)
    {
        try
        {
            if (HocaID <0 || string.IsNullOrEmpty(Isim))
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_HocaGuncelle");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("HocaID", HocaID);
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

            if (!string.IsNullOrEmpty(Unvan))
            {
                param = new SqlParameter("Unvan", Unvan);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
            }

            if (YorumSayisi >= 0)
            {
                param = new SqlParameter("YorumSayisi", YorumSayisi);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }

            return Util.ExecuteNonQuery(cmd) == 1;
        }
        catch (Exception ex)
        {
        }
        return false;
    }

    public static DataTable Admin_HocalariDondur(int OkulID)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Admin_HocalariDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            if (OkulID > 0)
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

    public static bool HocaEkle(bool IsActive, string Isim, string Unvan, int YorumSayisi,
        List<int> OkulIDler, List<int> OkulBaslangicYillari, List<int> OkulBitisYillari, List<int> DersIDler)
    {
        try
        {
            if (string.IsNullOrEmpty(Isim))
            {
                return false;
            }
            else if (Isim.Length > 50 || Unvan.Length > 50)
            {
                return false;
            }
            else
            {
                int count1 = 0;
                if (OkulIDler != null)
                {
                    count1 = OkulIDler.Count;
                }
                int count2 = 0;
                if (OkulBaslangicYillari != null)
                {
                    count2 = OkulBaslangicYillari.Count;
                }
                int count3 = 0;
                if (OkulBitisYillari != null)
                {
                    count3 = OkulBitisYillari.Count;
                }
                if (count1 != count2 || count1 != count3)
                {
                    return false;
                }
            }

            SqlCommand cmd = new SqlCommand("Admin_HocaEkle");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("IsActive", IsActive);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Bit;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Isim", Isim);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            if (!string.IsNullOrEmpty(Unvan))
            {
                param = new SqlParameter("Unvan", Unvan);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
            }

            if (YorumSayisi >= 0)
            {
                param = new SqlParameter("YorumSayisi", YorumSayisi);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }

            object res = Util.GetResult(cmd);
            int hocaID;
            if (res != null && res != System.DBNull.Value && (int)res > 0)
            {
                hocaID = (int)res;
            }
            else
            {
                return false;
            }

            //Hoca okul baglantilarini ekle
            int i = 0;
            if (OkulIDler != null && OkulBaslangicYillari != null && OkulBitisYillari != null)
            {
                foreach (int okulID in OkulIDler)
                {
                    int baslangicYili = OkulBaslangicYillari[i];
                    int bitisYili = OkulBitisYillari[i];

                    cmd = new SqlCommand("Admin_HocaOkulEkle");
                    cmd.CommandType = CommandType.StoredProcedure;

                    param = new SqlParameter("HocaID", hocaID);
                    param.Direction = ParameterDirection.Input;
                    param.SqlDbType = SqlDbType.Int;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("OkulID", okulID);
                    param.Direction = ParameterDirection.Input;
                    param.SqlDbType = SqlDbType.Int;
                    cmd.Parameters.Add(param);

                    if (baslangicYili > 0)
                    {
                        param = new SqlParameter("BaslangicYili", baslangicYili);
                        param.Direction = ParameterDirection.Input;
                        param.SqlDbType = SqlDbType.Int;
                        cmd.Parameters.Add(param);
                    }

                    //-1 : Bilinmiyor
                    //0 : Hala devam ediyor
                    if (bitisYili >= 0)
                    {
                        param = new SqlParameter("BitisYili", bitisYili);
                        param.Direction = ParameterDirection.Input;
                        param.SqlDbType = SqlDbType.Int;
                        cmd.Parameters.Add(param);
                    }

                    if (Util.ExecuteNonQuery(cmd) != 1)
                    {
                        return false;
                    }
                    i++;
                }
            }

            //Hoca ders baglantilarini ekle
            if (DersIDler != null)
            {
                foreach (int dersID in DersIDler)
                {
                    cmd = new SqlCommand("Admin_HocaDersEkle");
                    cmd.CommandType = CommandType.StoredProcedure;

                    param = new SqlParameter("HocaID", hocaID);
                    param.Direction = ParameterDirection.Input;
                    param.SqlDbType = SqlDbType.Int;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("DersID", dersID);
                    param.Direction = ParameterDirection.Input;
                    param.SqlDbType = SqlDbType.Int;
                    cmd.Parameters.Add(param);

                    if (Util.ExecuteNonQuery(cmd) != 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            
        }
        return false;
    }

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
        Enums.KullaniciPuanAraligi KullaniciPuanaraligi, List<int> DersIDleri, List<string> BilinmeyenDersIsimleri, 
        int KullaniciOnayPuani)
    {
        try
        {
            if (KullaniciID < 0 || HocaID < 0 || Puanlar == null || Yorum == null || string.IsNullOrEmpty(Yorum) ||
                (int)KullaniciPuanaraligi < (int)Enums.KullaniciPuanAraligi.F || 
                (int)KullaniciPuanaraligi > (int)Enums.KullaniciPuanAraligi.A)
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

            param = new SqlParameter("Yorum", Util.HTMLToDB(Yorum));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Kullanici_Puanaraligi", (int)KullaniciPuanaraligi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            Enums.YorumDurumu yorumDurumu = Enums.YorumDurumu.OnayBekliyor;
            if (KullaniciOnayPuani >= Convert.ToInt32(ConfigurationManager.AppSettings.Get("HocaYorumOnayPuani")))
            {
                yorumDurumu = Enums.YorumDurumu.Onaylanmis;
            }
            param = new SqlParameter("YorumDurumu", (int)yorumDurumu);
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
                                param = new SqlParameter("BilinmeyenDersIsmi", BilinmeyenDersIsimleri[i]);
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

    public static bool HocaYorumPuanGuncelle(int HocaYorumID, int[] Puanlar, string Yorum,
            Enums.KullaniciPuanAraligi KullaniciPuanaraligi, List<int> DersIDleri, List<string> BilinmeyenDersIsimleri,
            int KullaniciOnayPuani)
    {
        try
        {
            if (HocaYorumID < 0 || Puanlar == null || Yorum == null || string.IsNullOrEmpty(Yorum) ||
                (int)KullaniciPuanaraligi < (int)Enums.KullaniciPuanAraligi.F ||
                (int)KullaniciPuanaraligi > (int)Enums.KullaniciPuanAraligi.A)
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
            SqlCommand cmd = new SqlCommand("HocaYorumPuanGuncelle");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("HocaYorumID", HocaYorumID);
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

            param = new SqlParameter("Yorum", Util.HTMLToDB(Yorum));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Kullanici_Puanaraligi", (int)KullaniciPuanaraligi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            Enums.YorumDurumu yorumDurumu = Enums.YorumDurumu.OnayBekliyor;
            if (KullaniciOnayPuani >= Convert.ToInt32(ConfigurationManager.AppSettings.Get("HocaYorumOnayPuani")))
            {
                yorumDurumu = Enums.YorumDurumu.Onaylanmis;
            }
            param = new SqlParameter("YorumDurumu", (int)yorumDurumu);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            if (Util.ExecuteAndCheckReturnValue(cmd))
            {
                cmd = new SqlCommand("HocaYorumDersleriSil");
                cmd.CommandType = CommandType.StoredProcedure;

                param = new SqlParameter("HocaYorumID", HocaYorumID);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                if (Util.ExecuteAndCheckReturnValue(cmd))
                {
                    for (int i = 0; i < DersIDleri.Count; i++)
                    {
                        int DersID = DersIDleri[i];
                        cmd = new SqlCommand("HocaYorumDersKaydet");
                        cmd.CommandType = CommandType.StoredProcedure;

                        param = new SqlParameter("HocaYorumID", HocaYorumID);
                        param.Direction = ParameterDirection.Input;
                        param.SqlDbType = SqlDbType.Int;
                        cmd.Parameters.Add(param);
                        if (DersID == -2)  //Bilinmeyen ders ismiyle kaydet
                        {
                            param = new SqlParameter("BilinmeyenDersIsmi", BilinmeyenDersIsimleri[i]);
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
        catch { }
        return false;
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

    //HocaDersleriniDondur'un DataTable donduren hali
    public static DataTable HocaDersleriniDondur_DataTable(int HocaID)
    {
        try
        {
            if (HocaID < 0)
            {
                return null;
            }

            SqlCommand cmd = new SqlCommand("HocaDersleriniDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("HocaID", HocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.GetDataTable(cmd);
        }
        catch (Exception ex) { }
        return null;
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
            if (dt == null || !(dt.Rows.Count > 0))
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
            if (dt != null)
            {
                DataTable dt2 = dt.Clone();
                int prevHocaYorumID = -1;
                DataRow drToAdd = null;
                foreach (DataRow dr in dt.Rows)
                {
                    int currentHocaYorumID = Convert.ToInt32(dr["HOCAYORUM_ID"]);
                    if (currentHocaYorumID == prevHocaYorumID)
                    {
                        if (Util.GecerliString(dr["KAYITSIZ_DERS_ISMI"]))
                        {
                            drToAdd["DERS_KODU"] = drToAdd["DERS_KODU"].ToString() + "," + dr["KAYITSIZ_DERS_ISMI"].ToString();
                        }
                        else if (Util.GecerliString(dr["DERS_KODU"]))
                        {
                            drToAdd["DERS_KODU"] = drToAdd["DERS_KODU"].ToString() + "," + dr["DERS_KODU"].ToString();
                        }
                    }
                    else
                    {
                        if (drToAdd != null)
                        {
                            dt2.Rows.Add(drToAdd);
                        }
                        drToAdd = dt2.NewRow();
                        drToAdd.ItemArray = dr.ItemArray;
                        if (Util.GecerliString(drToAdd["KAYITSIZ_DERS_ISMI"]))
                        {
                            drToAdd["DERS_KODU"] = drToAdd["KAYITSIZ_DERS_ISMI"];
                        }
                        prevHocaYorumID = currentHocaYorumID;
                    }
                }
                if (drToAdd != null)
                {
                    dt2.Rows.Add(drToAdd);
                }
                return dt2;
            }
        }
        catch { }
        return null;
    }

    /// <summary>
    /// Hoca puanlarini dondurur
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
            if (dt == null || dt.Rows.Count <= 0)
            {
                return null;
            }
            DataRow dr = dt.Rows[0];
            float puanSayisi = (float)Convert.ToInt32(dr["PUAN_SAYISI"]);
            if (puanSayisi < 1)
                return null;
            float[] result = new float[6];
            result[0] = Convert.ToInt32(dr["PUAN1"]) / puanSayisi;
            result[1] = Convert.ToInt32(dr["PUAN2"]) / puanSayisi;
            result[2] = Convert.ToInt32(dr["PUAN3"]) / puanSayisi;
            result[3] = Convert.ToInt32(dr["PUAN4"]) / puanSayisi;
            result[4] = Convert.ToInt32(dr["PUAN5"]) / puanSayisi;
            result[5] = Convert.ToInt32(dr["PUAN_SAYISI"]);
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
            if (dt == null)
            {
                return null;
            }
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
    /// Kullanici hocaya daha once yorum yaptiysa yorum ID'sini dondurur; yoksa -1 dondurur
    /// Hata olursa -2 dondurur
    /// </summary>
    /// <param name="kullaniciID"></param>
    /// <param name="hocaID"></param>
    /// <returns></returns>
    public static int KullaniciHocayaYorumYapmis(int kullaniciID, int hocaID)
    {
        try
        {
            if (kullaniciID < 0 || hocaID < 0)
            {
                return -2;
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

            object o = Util.GetResult(cmd);
            if (Util.GecerliSayi(o))
            {
                return Convert.ToInt32(o);
            }
        }
        catch
        {
        }
        return -2;
    }

    /// <summary>
    /// Kullanicinin yaptigi tum hoca yorumlarini dondurur
    /// (Silinmis, onaylanmamis yorumlari da dondurur)
    /// Basarisiz olursa null dondurur
    /// </summary>
    /// <param name="KullaniciID"></param>
    /// <returns></returns>
    public static DataTable KullaniciHocaYorumlariniDondur(int KullaniciID)
    {
        try
        {
            if (KullaniciID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("KullaniciHocaYorumlariniDondur");
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
    /// Hoca yorumunun iliskili oldugu derslerin ID'lerini ve/veya bilinmeyen ders isimlerini dondurur
    /// </summary>
    /// <param name="HocaYorumID"></param>
    /// <returns></returns>
    public static DataTable HocaYorumDersleriDondur(int HocaYorumID)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("HocaYorumDersleriDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("HocaYorumID", HocaYorumID);
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
    /// Kullanicinin hoca icin yaptigi aktif yorum bilgisini dondurur
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
                int hocaYorumID = Convert.ToInt32(dtHocaYorumu.Rows[0]["HOCAYORUM_ID"]);
                //Yorumun ilgili oldugu dersler
                cmd = new SqlCommand("HocaYorumDersleriDondur");
                cmd.CommandType = CommandType.StoredProcedure;

                param = new SqlParameter("HocaYorumID", hocaYorumID);
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
                        //yorumID - yorum - kullanici puan araligi - puan1 - puan2 - puan3 - puan4 - puan5 - { (Ders ID - Ders Kodu)| (-1 - Ders Ismi) }(*)
                        List<object> sonuc = new List<object>();
                        sonuc.Add(hocaYorumID);
                        sonuc.Add(dtHocaYorumu.Rows[0]["YORUM"].ToString());
                        sonuc.Add(dtHocaYorumu.Rows[0]["KULLANICI_PUANARALIGI"].ToString());
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
                                sonuc.Add(dr["OKUL_ISMI"].ToString());
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

#region Eski
/*
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
    }

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
    }
 */
#endregion
