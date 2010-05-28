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
/// Summary description for Mesajlar
/// </summary>
public static class Mesajlar
{

    public static bool MesajGonder(int AliciID, int GonderenID, string Icerik, string Baslik, DateTime GondermeZamani)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("MesajGonder");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("AliciID", AliciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("GonderenID", GonderenID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            if (Icerik.Length > 1024)
                Icerik = Icerik.Substring(0, 1024);

            param = new SqlParameter("Icerik", Icerik);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.Add(param);

            if(Baslik.Length > 50)
                Baslik = Baslik.Substring(0,50);

            param = new SqlParameter("Baslik", Baslik);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("GondermeZamani", GondermeZamani);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.DateTime;
            cmd.Parameters.Add(param);

            cmd.Connection = Util.GetSqlConnection();
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool AdmineDersTalebiGonder(string DersIsmi, string OkulIsimleri, string Aciklama, int GonderenID)
    {
        try
        {
            string icerik = Util.TextFileToString(HttpContext.Current.Server.MapPath("~/Admin/Mesajlar/YeniDersTalebi.txt"));
            while (icerik.Contains("||DERS_ISMI||"))
            {
                icerik = icerik.Replace("||DERS_ISMI||", DersIsmi);
            }
            while (icerik.Contains("||OKUL_ISIMLERI||"))
            {
                icerik = icerik.Replace("||OKUL_ISIMLERI||", OkulIsimleri);
            }
            while (icerik.Contains("||ACIKLAMA||"))
            {
                icerik = icerik.Replace("||ACIKLAMA||", Aciklama);
            }
            while (icerik.Contains("||TALEP_EDEN||"))
            {
                icerik = icerik.Replace("||TALEP_EDEN||", GonderenID + " no.lu kullanici");
            }
            while (icerik.Contains("||TALEP_TARIHI||"))
            {
                icerik = icerik.Replace("||TALEP_TARIHI||", DateTime.Now.ToString());
            }
            string baslik = Util.TextFileToString(HttpContext.Current.Server.MapPath("~/Admin/Mesajlar/Baslik/YeniDersTalebi.txt"));
            return MesajGonder(-1, GonderenID, icerik, baslik, DateTime.Now);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool AdmineHocaTalebiGonder(string HocaIsmi, string OkulIsimleri, string Aciklama, int GonderenID)
    {
        try
        {
            string icerik = Util.TextFileToString(HttpContext.Current.Server.MapPath("~/Admin/Mesajlar/YeniHocaTalebi.txt"));
            while (icerik.Contains("||HOCA_ISMI||"))
            {
                icerik = icerik.Replace("||HOCA_ISMI||", HocaIsmi);
            }
            while (icerik.Contains("||OKUL_ISIMLERI||"))
            {
                icerik = icerik.Replace("||OKUL_ISIMLERI||", OkulIsimleri);
            }
            while (icerik.Contains("||ACIKLAMA||"))
            {
                icerik = icerik.Replace("||ACIKLAMA||", Aciklama);
            }
            while (icerik.Contains("||TALEP_EDEN||"))
            {
                icerik = icerik.Replace("||TALEP_EDEN||", GonderenID + " no.lu kullanici");
            }
            while (icerik.Contains("||TALEP_TARIHI||"))
            {
                icerik = icerik.Replace("||TALEP_TARIHI||", DateTime.Now.ToString());
            }
            string baslik = Util.TextFileToString(HttpContext.Current.Server.MapPath("~/Admin/Mesajlar/Baslik/YeniHocaTalebi.txt"));
            return MesajGonder(-1, GonderenID, icerik, baslik, DateTime.Now);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool AdmineOkulTalebiGonder(string OkulIsmi, string Aciklama, int GonderenID)
    {
        try
        {
            string icerik = Util.TextFileToString(HttpContext.Current.Server.MapPath("~/Admin/Mesajlar/YeniOkulTalebi.txt"));
            while(icerik.Contains("||OKUL_ISMI||"))
            {
                icerik = icerik.Replace("||OKUL_ISMI||", OkulIsmi);
            }
            while (icerik.Contains("||ACIKLAMA||"))
            {
                icerik = icerik.Replace("||ACIKLAMA||", Aciklama);
            }
            while (icerik.Contains("||TALEP_EDEN||"))
            {
                icerik = icerik.Replace("||TALEP_EDEN||", GonderenID + " no.lu kullanici");
            }
            while (icerik.Contains("||TALEP_TARIHI||"))
            {
                icerik = icerik.Replace("||TALEP_TARIHI||", DateTime.Now.ToString());
            }
            string baslik = Util.TextFileToString(HttpContext.Current.Server.MapPath("~/Admin/Mesajlar/Baslik/YeniOkulTalebi.txt"));
            return MesajGonder(-1, GonderenID, icerik, baslik, DateTime.Now);
        }
        catch (Exception)
        {
            return false;
        }
    }
}
