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
}
