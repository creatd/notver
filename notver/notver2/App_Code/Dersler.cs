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

}
