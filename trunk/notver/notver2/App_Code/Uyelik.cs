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

    public static bool KullaniciYukle(string kullaniciAdi)
    {
        if (string.IsNullOrEmpty(kullaniciAdi))
        {
            return false;
        }
        SqlCommand cmd = new SqlCommand("KullaniciYukle");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter param = new SqlParameter("KullaniciAdi", kullaniciAdi);
        param.Direction = ParameterDirection.Input;
        param.SqlDbType = SqlDbType.VarChar;
        cmd.Parameters.Add(param);

        DataTable dt = Util.GetDataTable(cmd);
        if(dt != null && dt.Rows.Count == 1)
        {
            DataRow dr = dt.Rows[0];
            int kullaniciID = -1;
            if (Util.GecerliStringSayi(dr["UYE_ID"]))
                kullaniciID = Convert.ToInt32(dr["UYE_ID"].ToString());
            string isim = "";
            if(Util.GecerliString(dr["ISIM"]))
                isim = dr["ISIM"].ToString();
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
            string eposta = "";
            if(Util.GecerliString(dr["EPOSTA"]))
                eposta = dr["EPOSTA"].ToString();
            Session session = new Session();
            session.IsLoggedIn = true;
            session.KullaniciAdi = kullaniciAdi;
            session.KullaniciID = kullaniciID;
            session.KullaniciUyelikDurumu = (Enums.UyelikDurumu)uyelikDurumu;
            session.KullaniciOnayPuani = onayPuani;
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
    public static int KullaniciOlustur(string kullaniciAdi, string isim, int okulId, string eposta,
        Enums.UyelikDurumu uyelikDurumu, Enums.UyelikRol uyelikRol, string sifre, Enums.Cinsiyet cinsiyet)
    {
        try
        {
            //Kullanici adi var mi kontrol et
            SqlCommand cmd = new SqlCommand("KullaniciAdiVarMi");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciAdi", kullaniciAdi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.Add(param);
            object obj = Util.ExecuteScalar(cmd);
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

            //Eposta adresi var mi kontrol et
            cmd = new SqlCommand("EpostaAdresiVarMi");
            cmd.CommandType = CommandType.StoredProcedure;

            param = new SqlParameter("Eposta", eposta);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.Add(param);
            obj = Util.ExecuteScalar(cmd);
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

            cmd = new SqlCommand("KullaniciKaydet");
            cmd.CommandType = CommandType.StoredProcedure;

            param = new SqlParameter("KullaniciAdi", kullaniciAdi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Isim", isim);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("OkulID",SqlDbType.Int);
            if (okulId > 0)
            {
                param.Value = okulId;
            }
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Eposta", eposta);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.VarChar;
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
            param.SqlDbType = SqlDbType.VarChar;
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

    public static bool GirisYap(string kullaniciAdi, string sifre)
    {
        try
        {
            //Bu kontrol giris kutusunda yapilmis olmali ama yine de burada da yapalim
            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(sifre))
            {
                return false;
            }
            kullaniciAdi = kullaniciAdi.Trim();
            sifre = sifre.Trim();

            SqlCommand cmd = new SqlCommand("KullaniciSifreDogrula");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("KullaniciAdi", kullaniciAdi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Sifre", Util.HashString(sifre));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Sonuc", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            object obj = Util.GetResult(cmd);
            if (obj != null)
            {
                if ((int)obj == 1)
                {
                    KullaniciYukle(kullaniciAdi); 
                    return true;
                }
            }                       
        }
        catch
        {
        }
        return false;
    }

    


}
