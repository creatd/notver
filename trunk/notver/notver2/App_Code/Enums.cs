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
    //Admin_OnayBekleyenYorumlariDondur
    //TODO: Gecersiz YorumTipi'ni kaldir
    public enum YorumTipi
    {
        Gecersiz = -1,
        HocaYorum = 0,      
        DersYorum = 1,      
        OkulYorum = 2
    }

    //Admin_OnayBekleyenYorumlariDondur
    public enum YorumDurumu
    {
        OnayBekliyor = 0,
        Onaylanmis = 1,
        KullaniciTarafindanSilinmis = 2,
        SistemTarafindanSilinmis = 3
    }

    public enum DosyaDurumu
    {
        OnayBekliyor = 0,
        Onaylanmis = 1,
        KullaniciTarafindanSilinmis = 2,
        SistemTarafindanSilinmis = 3
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
        Gecersiz = -1,  //Bir hata olusunca bunu dondurur (session expire oldugunda mesela)
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

    public enum SistemHataSeviyesi
    {
        Dusuk = 1,
        Orta = 2,
        Yuksek = 3
    }
}
