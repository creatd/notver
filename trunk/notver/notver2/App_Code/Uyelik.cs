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
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Uyelik
/// </summary>
public class Uyelik
{
    /// <summary>
    /// Uyeyi veritabanindan siler. Blok etmek icin kullaniciyi bloke olarak guncelle, burayi kullanma.
    /// </summary>
    /// <param name="UyeID"></param>
    /// <returns></returns>
    public static bool Admin_UyeSil(int UyeID)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Admin_UyeSil");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("UyeID", UyeID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.ExecuteNonQuery(cmd) == 1;
        }
        catch (Exception ex) { }
        return false;
    }
    public static bool Admin_UyeGuncelle(int UyeID, string Eposta, bool Bloke, string BlokNedeni,
        string KullaniciAdi, string Ad, string Soyad, int OkulID, Enums.UyelikDurumu UyelikDurumu,
        Enums.UyelikRol UyelikRol, bool KizMi, int OnayPuani)
    {
        try
        {
            if (UyeID < 0 || string.IsNullOrEmpty(Eposta))
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand("Admin_UyeGuncelle");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("UyeID", UyeID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Eposta", Eposta);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Bloke", Bloke);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Bit;
            cmd.Parameters.Add(param);

            if (!string.IsNullOrEmpty(BlokNedeni))
            {
                param = new SqlParameter("BlokNedeni", BlokNedeni);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
            }

            if (!string.IsNullOrEmpty(KullaniciAdi))
            {
                param = new SqlParameter("KullaniciAdi", KullaniciAdi);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
            }

            param = new SqlParameter("Ad", Ad);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Soyad", Soyad);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            if (OkulID >= 0)
            {
                param = new SqlParameter("OkulID", OkulID);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
            }

            param = new SqlParameter("UyelikDurumu", (int)UyelikDurumu);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("UyelikRol", (int)UyelikRol);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("KizMi", KizMi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Bit;
            cmd.Parameters.Add(param);

            param = new SqlParameter("OnayPuani", OnayPuani);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            return Util.ExecuteNonQuery(cmd) == 1;
        }
        catch (Exception ex) { }
        return false;
    }

    public static DataTable Admin_UyeleriDondur(int OkulID)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Admin_UyeleriDondur");
            cmd.CommandType = CommandType.StoredProcedure;

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

    public static int KullaniciAktifYorumSayisiniDondur(int KullaniciID)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("KullaniciAktifYorumSayisiniDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciID", KullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("YorumSayisi", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            return Convert.ToInt32(Util.GetResult(cmd));
        }
        catch (Exception) { }
        return -1;
    }

    public static bool KullaniciYukle(string Eposta)
    {
        if (string.IsNullOrEmpty(Eposta))
        {
            return false;
        }
        SqlCommand cmd = new SqlCommand("KullaniciYukle");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter param = new SqlParameter("Eposta", Eposta);
        param.Direction = ParameterDirection.Input;
        param.SqlDbType = SqlDbType.NVarChar;
        cmd.Parameters.Add(param);

        DataTable dt = Util.GetDataTable(cmd);
        if(dt != null && dt.Rows.Count == 1)
        {
            DataRow dr = dt.Rows[0];
            int kullaniciID = -1;
            if (Util.GecerliStringSayi(dr["UYE_ID"]))
                kullaniciID = Convert.ToInt32(dr["UYE_ID"].ToString());
            
            int uyelikDurumu = -1;
            if(Util.GecerliStringSayi(dr["UYELIK_DURUMU"]))
                uyelikDurumu = Convert.ToInt32(dr["UYELIK_DURUMU"].ToString());

            int rolID = -1;
            if(Util.GecerliStringSayi(dr["ROL_ID"]))
                rolID = Convert.ToInt32(dr["ROL_ID"].ToString());

            int onayPuani = -1;
            if (Util.GecerliStringSayi(dr["ONAY_PUANI"]))
                onayPuani = Convert.ToInt32(dr["ONAY_PUANI"].ToString());

            Enums.Cinsiyet cinsiyet;
            if(Util.GecerliString(dr["CINSIYET"]))
            {
                if(Convert.ToBoolean(dr["CINSIYET"].ToString()))
                    cinsiyet = Enums.Cinsiyet.Erkek;
                else
                    cinsiyet = Enums.Cinsiyet.Kiz;
            }

            string kullaniciAd = "";
            if (Util.GecerliString(dr["AD"]))
                kullaniciAd = dr["AD"].ToString();

            string kullaniciAdi = "";
            if(Util.GecerliString(dr["KULLANICI_ADI"]))
                kullaniciAdi = dr["KULLANICI_ADI"].ToString();

            Session session = new Session();
            session.IsLoggedIn = true;
            session.KullaniciAdi = kullaniciAdi;
            session.KullaniciID = kullaniciID;
            session.KullaniciUyelikDurumu = (Enums.UyelikDurumu)uyelikDurumu;
            session.KullaniciOnayPuani = onayPuani;
            session.KullaniciAd = kullaniciAd;
            session.KullaniciUyelikRol = (Enums.UyelikRol)rolID;
            return true;
        }
        return false;
    }

    public static void CikisYap()
    {
        //TODO : birden cok kullanici ayni anda giris yaptiginda dogru calisiyor mu kontrol et
        Session session = new Session();
        session.KullaniciID = -1;
        session.IsLoggedIn = false;
        session.KullaniciAdi = "";
        Session.Temizle();
    }

    /// <summary>
    /// Return codes :
    /// -2  Eposta adresi kullanimda
    /// -1  Kullanici adi kullanimda
    /// 0   Bilinmeyen hata
    /// 1   Basari
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public static int KullaniciOlustur(string kullaniciAdi, string ad, string soyad, int okulId, string eposta,
        Enums.UyelikDurumu uyelikDurumu, Enums.UyelikRol uyelikRol, string sifre, Enums.Cinsiyet cinsiyet)
    {
        try
        {
            //Eposta adresi var mi kontrol et
            SqlCommand cmd = new SqlCommand("EpostaAdresiVarMi");
            cmd.CommandType = CommandType.StoredProcedure;

            eposta = eposta.Trim();
            eposta = eposta.ToLowerInvariant();

            SqlParameter param = new SqlParameter("Eposta", eposta);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);
            object obj = Util.ExecuteScalar(cmd);
            if (obj != null)
            {
                if ((int)obj == 1)
                {
                    return -2;
                }
            }
            else
            {
                return 0;
            }

            //Kullanici adi zorunlu degil, ancak girildiyse essiz olmali
            if (!string.IsNullOrEmpty(kullaniciAdi))
            {
                //Kullanici adi var mi kontrol et
                cmd = new SqlCommand("KullaniciAdiVarMi");
                cmd.CommandType = CommandType.StoredProcedure;

                param = new SqlParameter("KullaniciAdi", kullaniciAdi);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);
                obj = Util.ExecuteScalar(cmd);
                if (obj != null)
                {
                    if ((int)obj == 1)
                    {
                        return -1;
                    }
                }
                else
                {
                    return 0;
                }
            }

            cmd = new SqlCommand("KullaniciKaydet");
            cmd.CommandType = CommandType.StoredProcedure;

            param = new SqlParameter("KullaniciAdi", kullaniciAdi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Ad", ad);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Soyad", soyad);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("OkulID",SqlDbType.Int);
            if (okulId > 0)
            {
                param.Value = okulId;
            }
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Eposta", eposta); //Eposta adresini kucuk harf yaptik
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("UyelikDurumu", (int)uyelikDurumu);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("UyelikRol", (int)uyelikRol);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Sifre", Util.HashString(sifre));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Cinsiyet", (bool)((int)cinsiyet==1));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Bit;
            cmd.Parameters.Add(param);

            param = new SqlParameter("OnayPuani", Convert.ToInt32(ConfigurationSettings.AppSettings.Get("UyelikBaslangicOnayPuani")));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            if (Util.ExecuteNonQuery(cmd) != -1)
            {
                return 1;
            }
        }
        catch (Exception)
        {
        }
        return 0;
    }

    public static bool GirisYap(string Eposta, string sifre)
    {
        try
        {
            //Bu kontrol giris kutusunda yapilmis olmali ama yine de burada da yapalim
            if (string.IsNullOrEmpty(Eposta) || string.IsNullOrEmpty(sifre))
            {
                return false;
            }
            Eposta = Eposta.Trim();
            Eposta = Eposta.ToLowerInvariant();
            sifre = sifre.Trim();

            SqlCommand cmd = new SqlCommand("KullaniciSifreDogrula");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("Eposta", Eposta);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Sifre", Util.HashString(sifre));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Sonuc", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            if (Util.ExecuteAndCheckReturnValue(cmd))
            {
                KullaniciYukle(Eposta); 
                return true;
            }
        }
        catch
        {
        }
        return false;
    }

    


}
