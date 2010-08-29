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
/// Hem sayfalarda, hem user control'lerde kullanilabilecek metodlarin bulundugu sinif
/// </summary>
public class Genel
{
    /// <summary>
    /// Ilk int degeri :
    /// Eger ilk defa puan veriliyorsa 1,
    /// Daha once puan verilmisse 2
    /// 
    /// Ikinci int degeri : yorumun su anki alkis puani
    /// 
    /// Bir hata olusursa null dondurur
    /// </summary>
    /// <param name="olumluPuan"></param>
    /// <param name="kullaniciID"></param>
    /// <param name="yorumID"></param>
    /// <param name="yaziTipi"></param>
    /// <returns></returns>
    public static int[] YorumPuanVer(bool olumluPuan, int kullaniciID, int yorumID, Enums.YorumTipi yorumTipi)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("YorumPuanVer");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("OlumluPuan", olumluPuan);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Bit;
            cmd.Parameters.Add(param);

            param = new SqlParameter("YorumID", yorumID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("KullaniciID", kullaniciID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("YorumTipi", Convert.ToInt32(yorumTipi));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter("YeniPuan", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Return_Value", DbType.Int32);
            param.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(param);

            object alkisPuani = Util.ExecuteScalar(cmd);
            int result = Convert.ToInt32(cmd.Parameters["Return_Value"].Value);
            int yeniPuan = Convert.ToInt32(cmd.Parameters["YeniPuan"].Value);
            if (result == null)
            {
                return null;
            }
            else
            {
                return new int[] { result, yeniPuan };
            }
        }
        catch (Exception)
        {
            return null;
        }
    }

}
