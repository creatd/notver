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
using System.Data.SqlClient;

/// <summary>
/// Summary description for Okullar
/// </summary>
public class Okullar
{
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
            if (dr["YORUM"] != null && dr["YORUM"] != System.DBNull.Value)
            {
                return dr["YORUM"].ToString();
            }
            return null;
        }
        catch
        {
            return null;
        }
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

            param = new SqlParameter("Yorum", yorum);
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

            return (Util.ExecuteNonQuery(cmd) > 0);
        }
        catch
        {
            return false;
        }
    }

    public static bool OkulYorumGuncelle(int kullaniciID, int okulID, string yorum)
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

            param = new SqlParameter("Yorum", yorum);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            return Util.ExecuteAndCheckReturnValue(cmd);
        }
        catch
        {
            return false;
        }
    }
}
