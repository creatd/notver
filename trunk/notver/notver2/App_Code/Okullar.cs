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
            SqlCommand cmd = new SqlCommand("OkulProfilDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("OkulID", OkulID);
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
    /// Kayitli tum aktif okullarin ISIM,OKUL_ID 'lerini dondurur
    /// </summary>
    /// <returns></returns>
    public static DataTable OkullariDondur()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("OkullariDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            DataTable dt = Util.GetDataTable(cmd);
            return dt;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
