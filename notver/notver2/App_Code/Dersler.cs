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

    public static DataTable KodaGoreDersleriDondur(string dersIsmi)
    {
        try
        {
            if (string.IsNullOrEmpty(dersIsmi))
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("KodaGoreDersleriDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DersKodu", Util.BuildLikeExpression(dersIsmi));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            DataTable dt = Util.GetDataTable(cmd);
            return dt;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static DataTable IDyeGoreDersiDondur(int dersID)
    {
        try
        {
            if (dersID < 0)
            {
                return null;
            }
            SqlCommand cmd = new SqlCommand("IDyeGoreDersiDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("DersID", dersID);
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

            DataTable dt = Util.GetDataTable(cmd);
            return dt;
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

            DataTable dt = Util.GetDataTable(cmd);
            return dt;
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

}
