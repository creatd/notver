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
/// Summary description for Enums
/// </summary>
public class Enums
{
    public enum YorumTipi
    {
        HocaYorum = 0,      
        DersYorum = 1,      
        HocaDersYorum = 2,  
        OkulYorum = 3       
    }

    //Uyeler
    public enum AdminMesajTipi
    {
        YeniHocaTalebi = 0,
        YeniDersTalebi = 1,
        YeniOkulTalebi = 2
    }

    public enum DosyaKategoriTipi
    {
        SinavVeCozum = 0,
        DersNotu = 1,
        Odev = 2,
        Proje = 3,
        YararliKaynak = 4,
        Diger = 5
    }

    //s: Uyelik
    public enum UyelikDurumu
    {
        EpostaOnayBekliyor = 0, 
        EpostaOnayli = 1,       
        UniEpostaOnayli = 2     
    }

    public enum UyelikRol
    {
        Kullanici = 0,
        Moderator = 1,
        Admin =2
    }
    //e: Uyelik

    public enum Cinsiyet
    {
        Erkek = 0,  
        Kiz = 1     
    }
}
