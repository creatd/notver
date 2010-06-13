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

/// <summary>
/// Hem sayfalarda, hem user control'lerde kullanilabilecek metodlarin bulundugu sinif
/// </summary>
public class Genel
{
    /// <summary>
    /// Kayitli tum aktif okullarin ISIM,OKUL_ID 'lerini dondurur
    /// </summary>
    /// <returns></returns>
    public static DataTable OkullariDondur()
    {
        string SQL = "SELECT OKUL_ID , ISIM FROM OKULLAR WHERE IS_ACTIVE=1";
        return Util.GetDataTable(SQL);
    }

}
