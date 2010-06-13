﻿using System;
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
    public static bool KullaniciYukle(string kullaniciAdi)
    {
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
            
            int kullaniciID = Convert.ToInt32(dr["UYE_ID"].ToString());
            //string kullaniciAdi = dr["KULLANICI_ADI"].ToString();
            string isim = dr["ISIM"].ToString();
            int uyelikDurumu = Convert.ToInt32(dr["UYELIK_DURUMU"].ToString());
            int rolID = Convert.ToInt32(dr["ROL_ID"].ToString());
            int cinsiyet;
            bool erkek = Convert.ToBoolean(dr["CINSIYET"].ToString());
            if(erkek)
                cinsiyet = (int)Enums.Cinsiyet.Erkek;
            else
                cinsiyet = (int)Enums.Cinsiyet.Kiz;
            string eposta = dr["EPOSTA"].ToString();
            Session session = new Session();
            session.KullaniciAdi = kullaniciAdi;
            session.KullaniciID = kullaniciID;
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
    /// -1  Username exists
    /// 0   Unknown error
    /// 1   Success
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public static int KullaniciOlustur(string kullaniciAdi, string sifre)
    {
        try
        {
            kullaniciAdi = kullaniciAdi.Trim();
            sifre = sifre.Trim();

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

            cmd = new SqlCommand("KullaniciKaydet");
            cmd.CommandType = CommandType.StoredProcedure;

            param = new SqlParameter("KullaniciAdi", kullaniciAdi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Sifre", Util.HashString(sifre));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.Add(param);

            if (Util.ExecuteNonQuery(cmd) != -99)
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

            param = new SqlParameter("Result", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            object obj = Util.GetResult(cmd);
            if (obj != null)
            {
                if ((int)obj == 1)
                {
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
