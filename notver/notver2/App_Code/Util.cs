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
using System.IO;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// Summary description for Util
/// </summary>
public class Util
{
    static SqlConnection connection;
    public static SqlConnection GetSqlConnection()
    {
        if (connection == null)
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
            connection.Open();
        }
        else if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }
        return connection;
    }

    public static string TextFileToString(string filePath)
    {
        StreamReader sr = new StreamReader(filePath);
        return sr.ReadToEnd();
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
    /// Prosedure verilen son parametrenin degerini dondurur (son parametre output parametre olmali)
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>
    public static object GetResult(SqlCommand cmd)
    {
        try
        {
            cmd.Connection = Util.GetSqlConnection();
            cmd.ExecuteNonQuery();
            return cmd.Parameters[cmd.Parameters.Count - 1].Value;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Prosedurden return ile donen deger 0 ise true, degilse false dondurur
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>
    public static bool ExecuteAndCheckReturnValue(SqlCommand cmd)
    {
        try
        {
            SqlParameter param = new SqlParameter("Return_Value", DbType.Int32);
            param.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(param);

            cmd.Connection = Util.GetSqlConnection();
            cmd.ExecuteNonQuery();
            int result = Convert.ToInt32(cmd.Parameters["Return_Value"].Value);
            if (result == null)
            {
                return false;
            }
            else
            {
                return (result == 0);
            }
        }
        catch
        {
            return false;
        }
    }

    public static string HashString(string input)
    {
        MD5 md5Hasher = MD5.Create();
        byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            sb.Append(data[i].ToString("x2"));
        }
        return sb.ToString();
    }

    //TODO: gereksiz bosluklari ayir ne demek ya?
    /// <summary>
    /// Turns the given input into a proper LIKE expression
    /// Gereksiz bosluklari ayir
    /// Not : Basa ve sona tirnak isareti koymaz
    /// </summary>
    /// <param name="initialInput"></param>
    /// <returns></returns>
    public static string BuildLikeExpression(string initialInput)
    {
        string[] words = initialInput.Split(' ');
        StringBuilder sb = new StringBuilder();
        sb.Append("%");
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

        return sb.ToString();
    }

    public static bool Gecerli(object obj)
    {
        try
        {
            if (obj != null && obj != System.DBNull.Value)
            {
                return true;
            }
        }
        catch (Exception)
        {
        }
        return false;
    }

    public static bool GecerliString(object obj)
    {
        try
        {
            if (obj != null && obj != System.DBNull.Value && !string.IsNullOrEmpty(obj.ToString()))
            {
                return true;
            }
        }
        catch (Exception)
        {
        }
        return false;
    }
}
