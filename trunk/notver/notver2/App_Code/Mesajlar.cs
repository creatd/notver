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
using System.Net.Mail;
using System.IO;

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
            param.SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.Add(param);

            if(Baslik.Length > 50)
                Baslik = Baslik.Substring(0,50);

            param = new SqlParameter("Baslik", Baslik);
            param.Direction = ParameterDirection.Input;
            param.SqlDbType = SqlDbType.NVarChar;
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

    public static bool AdmineHataMesajiGonder(string URL, string Mesaj, int KullaniciID, Enums.SistemHataSeviyesi HataSeviyesi)
    {
        try
        {
            string icerik = Util.TextFileToString(HttpContext.Current.Server.MapPath("~/Admin/Mesajlar/HataMesaji.txt"));
            while (icerik.Contains("||URL||"))
            {
                icerik = icerik.Replace("||URL||", URL);
            }
            while (icerik.Contains("||MESAJ||"))
            {
                icerik = icerik.Replace("||MESAJ||", Mesaj);
            }
            while (icerik.Contains("||KULLANICI_ID||"))
            {
                icerik = icerik.Replace("||KULLANICI_ID||", KullaniciID.ToString());
            }
            while (icerik.Contains("||TARIH||"))
            {
                icerik = icerik.Replace("||TARIH||", DateTime.Now.ToString());
            }
            string baslik = Util.TextFileToString(HttpContext.Current.Server.MapPath("~/Admin/Mesajlar/Baslik/HataMesaji.txt"));
            while (baslik.Contains("||HATA_SEVIYESI||"))
            {
                baslik = baslik.Replace("||HATA_SEVIYESI||", HataSeviyesi.ToString());
            }
            
            return MesajGonder(-1, -1, icerik, baslik, DateTime.Now);
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

    public static bool OnayEpostasiGonder(string KullaniciIsmi, string KullaniciEpostasi, bool UniversiteEpostasi)
    {
        if (string.IsNullOrEmpty(KullaniciIsmi) || string.IsNullOrEmpty(KullaniciEpostasi))
        {
            return false;
        }
        //Kullanici icin bir hash olustur
        string hash = Uyelik.OnayIcinHashOlustur(KullaniciIsmi, KullaniciEpostasi);
        if (UniversiteEpostasi)
        {
            hash = "1" + hash;
        }
        else
        {
            hash = "0" + hash;
        }
        string url = "http://www.notverin.com/EpostaOnayla.aspx?KullaniciEposta=" + KullaniciEpostasi + "&OnayKodu=" + hash;
        string icerik = Util.TextFileToString(HttpContext.Current.Server.MapPath("~/Mesajlar/OnayMesaji.htm"));
        while (icerik.Contains("!!!URL!!!"))
        {
            icerik = icerik.Replace("!!!URL!!!", url);
        }
        while (icerik.Contains("!!!KULLANICI_ISMI!!!"))
        {
            icerik = icerik.Replace("!!!KULLANICI_ISMI!!!", KullaniciIsmi);
        }
        string baslik = "NotVerin - E-posta onay mesaji";
        return Mesajlar.EpostaGonder(KullaniciEpostasi, Enums.EpostaGonderici.bilgi, icerik, baslik, true);
    }

    public static bool HosgeldinEpostasiGonder(string KullaniciIsmi, string KullaniciEpostasi)
    {
        if (string.IsNullOrEmpty(KullaniciIsmi) || string.IsNullOrEmpty(KullaniciEpostasi))
        {
            return false;
        }
        string icerik = Util.TextFileToString(HttpContext.Current.Server.MapPath("~/Mesajlar/Hosgeldin.htm"));
        while (icerik.Contains("!!!KULLANICI_ISMI!!!"))
        {
            icerik = icerik.Replace("!!!KULLANICI_ISMI!!!", KullaniciIsmi);
        }
        string baslik = "NotVerin - Hosgeldin";
        return Mesajlar.EpostaGonder(KullaniciEpostasi, Enums.EpostaGonderici.bilgi, icerik, baslik, true);
    }

    public static bool EpostaGonder(int AliciID, Enums.EpostaGonderici EpostaGonderici, string Icerik, string Baslik,
        bool IsHTML)
    {
        try
        {
            if (AliciID < 0 || string.IsNullOrEmpty(Icerik) || string.IsNullOrEmpty(Baslik))
            {
                return false;
            }
            //Alicinin eposta adresini ogren
            string alici_adres = Uyelik.KullaniciEpostaAdresiniDondur(AliciID);
            if (!string.IsNullOrEmpty(alici_adres))
            {
                return EpostaGonder(alici_adres, EpostaGonderici, Icerik, Baslik, IsHTML);
            }
        }
        catch (Exception ex) { }
        return false;
    }

    public static bool EpostaGonder(string AliciAdres, Enums.EpostaGonderici EpostaGonderici, string Icerik, string Baslik,
        bool IsHTML)
    {
        try
        {
            if (string.IsNullOrEmpty(AliciAdres) || string.IsNullOrEmpty(Icerik) || string.IsNullOrEmpty(Baslik))
            {
                return false;
            }

            string gonderici_adres = "";
            MailMessage message = new MailMessage();
            switch (EpostaGonderici)
            {
                case Enums.EpostaGonderici.bilgi:
                    gonderici_adres = "bilgi";
                    break;
                case Enums.EpostaGonderici.duyuru:
                    gonderici_adres = "duyuru";
                    break;
                case Enums.EpostaGonderici.iletisim:
                    gonderici_adres = "iletisim";
                    break;
                case Enums.EpostaGonderici.uyari:
                    gonderici_adres = "uyari";
                    break;
                default:
                    gonderici_adres="genel";
                    break;
            }
            gonderici_adres += "@notverin.com";

            message.From = new MailAddress(gonderici_adres);

            message.To.Add(new MailAddress(AliciAdres));

            message.Subject = Baslik;
            message.Body = EpostaIcerikOlustur(Icerik);
            message.IsBodyHtml = IsHTML;

            SmtpClient client = new SmtpClient();
            //TODO: enable SSL'i kaldir daha sonra
            client.EnableSsl = true;
            client.Send(message);
            return true;
        }
        catch (Exception ex)
        {
            //TODO: Admin'e haber ver
        }
        return false;
    }

    public static string EpostaIcerikOlustur(string Icerik)
    {
        //Cerceve ekle
        string icerik = Util.TextFileToString(HttpContext.Current.Server.MapPath("~/Mesajlar/Cerceve.htm"));
        while(icerik.Contains("!!!ICERIK!!!"))
        {
            icerik = icerik.Replace("!!!ICERIK!!!" , Icerik);
        }
        return icerik;
    }
}
