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

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : System.Web.UI.Page
{
    
    public static DataTable dtOkullar;
    
    public static int kullaniciID;
    public static int KullaniciID
    {
        set { kullaniciID = value; }
        get { return kullaniciID; }
    }

	public BasePage()
	{
        if (IsLoggedIn())
        {
            if (kullaniciID <= 0)
            {
                kullaniciID = KullaniciIDDondur();
            }
        }
        else
        {
            kullaniciID = -1;
        }
	}

    //TODO: kontrol et
    private static bool isLoggedIn = false;
    /// <summary>
    /// Returns whether a user is logged in
    /// </summary>
    /// <returns></returns>
    public static bool IsLoggedIn()
    {
        return isLoggedIn || (Membership.GetUser() != null);
    }

    public static int KullaniciIDDondur()
    {
        if (kullaniciID > 0)
        {
            return kullaniciID;
        }
        else
        {
            try
            {
                SqlCommand cmd = new SqlCommand("KullaniciIDDondur");
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("AspID", Membership.GetUser().ProviderUserKey);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.Add(param);

                return Convert.ToInt32(ExecuteScalar(cmd));
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }

    public static DataTable GetDataTable(SqlCommand cmd)
    {
        DataTable dt = new DataTable();
        try
        {
            cmd.Connection = Util.GetSqlConnection();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dt);
        }
        catch (Exception ex)
        {
            return null;
        }
        return dt;
    }

    public static DataTable GetDataTable(string SqlExpression)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand(SqlExpression);
            cmd.Connection = Util.GetSqlConnection();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dt);
        }
        catch (Exception ex)
        {
            return null;
        }
        return dt;
    }

    public static int ExecuteNonQuery(SqlCommand cmd)
    {
        try
        {
            cmd.Connection = Util.GetSqlConnection();
            return cmd.ExecuteNonQuery();
        }
        catch
        {
            return -1;
        }
    }

    public static object ExecuteScalar(SqlCommand cmd)
    {
        try
        {
            cmd.Connection = Util.GetSqlConnection();
            return cmd.ExecuteScalar();
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Asp tablolarinda uye olusturulduktan sonra, bizim tanimladigimiz Uyeler tablosunda uye olusturur.
    /// Basarili olursa kullanicinin ID'sini, aksi takdirde -1 dondurur
    /// </summary>
    /// <param name="aspID"></param>
    /// <param name="kullaniciAdi"></param>
    /// <param name="eposta"></param>
    /// <param name="soru"></param>
    /// <param name="cevap"></param>
    /// <returns></returns>
    public static int UyeOlustur(object aspID, string kullaniciAdi, string eposta, int okulID , string isim)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("UyeOlustur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("AspID", aspID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.UniqueIdentifier;
            cmd.Parameters.Add(param);

            param = new SqlParameter("KullaniciAdi", kullaniciAdi);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Eposta", eposta);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Isim", isim);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            param = new SqlParameter("OkulID", okulID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            object result = ExecuteScalar(cmd);
            if (result == null || result==System.DBNull.Value)
            {
                return -1;
            }
            else
            {
                return Convert.ToInt32(result);
            }
        }
        catch
        {
            return -1; ;
        }
    }

    /// <summary>
    /// Refreshes current page
    /// </summary>
    public void RefreshPage()
    {
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }

    /// <summary>
    /// Returns data from Hocalar table performing the given like expression on their names
    /// Returns null on error
    /// </summary>
    /// <param name="likeExpression"></param>
    /// <returns></returns>
    public DataTable QueryHocalarByName(string likeExpression)
    {
        string sql = "SELECT * FROM HOCALAR WHERE ISIM LIKE " + likeExpression + " AND IS_ACTIVE=1";
        DataTable dt = GetDataTable(sql);

        return dt;
    }

    /// <summary>
    /// Returns data from Hocalar table, given the specific HocaID
    /// Returns null if no active data found/error
    /// </summary>
    /// <param name="hocaID"></param>
    /// <returns></returns>
    public DataTable QueryHocalarByID(int hocaID)
    {
        string sql = "SELECT * FROM HOCALAR WHERE HocaID = " + hocaID + " AND IS_ACTIVE=1";
        DataTable dt = GetDataTable(sql);
        return dt;
    }


    /// <summary>
    /// Turns the given input into a proper LIKE expression
    /// Gereksiz bosluklari ayir
    /// </summary>
    /// <param name="initialInput"></param>
    /// <returns></returns>
    public string BuildLikeExpression(string initialInput)
    {
        string[] words = initialInput.Split(' ');
        StringBuilder sb = new StringBuilder();
        sb.Append("'%");
        foreach (string word in words)
        {
            string latinWord = word;
            latinWord = latinWord.ToUpper();
            latinWord = latinWord.Replace("I", "$1$");
            latinWord = latinWord.Replace("İ", "$1$");
            latinWord = latinWord.Replace("$1$", "[Iİ]");
            latinWord = latinWord.Replace("O", "$2$");
            latinWord = latinWord.Replace("Ö", "$2$");
            latinWord = latinWord.Replace("$2$", "[OÖ]");
            latinWord = latinWord.Replace("U", "$3$");
            latinWord = latinWord.Replace("Ü", "$3$");
            latinWord = latinWord.Replace("$3$", "[UÜ]");
            latinWord = latinWord.Replace("C", "$4$");
            latinWord = latinWord.Replace("Ç", "$4$");
            latinWord = latinWord.Replace("$4$", "[CÇ]");
            latinWord = latinWord.Replace("S", "$5$");
            latinWord = latinWord.Replace("Ş", "$5$");
            latinWord = latinWord.Replace("$5$", "[SŞ]");
            latinWord = latinWord.Replace("G", "$6$");
            latinWord = latinWord.Replace("Ğ", "$6$");
            latinWord = latinWord.Replace("$6$", "[GĞ]");
            sb.Append(latinWord + "%");
        }
        sb.Append("'");

        return sb.ToString();
    }

    /// <summary>
    /// Redirect to default.aspx
    /// </summary>
    public void GoToDefaultPage()
    {
        Response.Redirect( "~\\Default.aspx" , true);
    }


    /// <summary>
    /// Hoca.aspx sayfasindan, bir hocanin profil sayfasindaki veriyi dondurmek icin cagirilir
    /// </summary>
    /// <param name="HocaID"></param>
    /// <returns></returns>
    public DataTable HocaProfilDondur(int HocaID)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("HocaProfilDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("HocaID", HocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            DataTable dt = GetDataTable(cmd);
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
        string SQL = "SELECT OKUL_ID , ISIM FROM OKULLAR WHERE IS_ACTIVE=1";
        return GetDataTable(SQL);
    }

    /// <summary>
    /// Bir hocanin vermis oldugu dersleri ve ders id'lerini dondurur
    /// </summary>
    /// <param name="hocaID"></param>
    /// <returns></returns>
    public string[][] HocaDersleriniDondur(int hocaID)
    {
        try
        {            
            SqlCommand cmd = new SqlCommand("HocaDersleriniDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("HocaID", hocaID);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);


            StringBuilder sb = new StringBuilder();
            DataTable dt = GetDataTable(cmd);
            if (! (dt.Rows.Count > 0) )
            {
                return null;
            }
            else
            {
                string[][] result = new string[dt.Rows.Count][];
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    result[i][0] = Convert.ToString(dr["ISIM"]);
                    result[i][1] = Convert.ToString(dr["DERS_ID"]);
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
    /// Bir hocanin ders vermis bulundugu okullari, okullari link yapmis sekilde tek bir string olarak dondurur
    /// </summary>
    /// <param name="HocaID"></param>
    /// <returns></returns>
    public string HocaOkullariniDondur(string HocaID)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("HocaOkullariniDondur");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("HocaID", Convert.ToInt32(HocaID.Trim()));
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);


            StringBuilder sb = new StringBuilder();
            DataTable dt = GetDataTable(cmd);
            foreach(DataRow dr in dt.Rows)
            {
                sb.Append("<a href=\"" + Page.ResolveUrl("~/Okul.aspx") + "?OkulID=" + dr["OKUL_ID"] + "\">" + dr["ISIM"] + "</a>");
            }
            string result = sb.ToString().Replace("</a><a", "</a><br /><a");    //Her okul ismi arasina <br /> koy
            return result;            
        }
        catch (Exception)
        {
            return null;
        }
    }

    public string HocaLinkiniDondur(string HocaIsmi, string HocaID)
    {
        return "<a href=\"" + Page.ResolveUrl("~/Hoca.aspx") + "?HocaID=" + HocaID + "\">" + HocaIsmi + "</a>";
    }

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

            param = new SqlParameter("YeniPuan",SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            param = new SqlParameter("Return_Value", DbType.Int32);
            param.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(param);

            object alkisPuani = ExecuteScalar(cmd);
            int result = Convert.ToInt32(cmd.Parameters["Return_Value"].Value);
            int yeniPuan = Convert.ToInt32(cmd.Parameters["YeniPuan"].Value);
            if(result == null)
            {
                return null;
            }
            else
            {
                return new int[]{result , yeniPuan};
            }
        }
        catch (Exception)
        {
            return null;
        }
    }


}
