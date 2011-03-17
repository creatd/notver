﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class SifremiUnuttum : BasePage
{
    protected void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        if (ex != null)
        {
            if (session != null)
            {
                Mesajlar.AdmineHataMesajiGonder(((System.Web.UI.Page)(sender)).Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
            }
            else
            {
                Mesajlar.AdmineHataMesajiGonder(((System.Web.UI.Page)(sender)).Request.Url.ToString(), ex.Message, -1, Enums.SistemHataSeviyesi.Orta);
            }
        }
    }

    protected void Page_Prerender(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (session.IsLoggedIn)
                {
                    GoToDefaultPage();
                }
                pnlBasari.Visible = false;
                pnlHata.Visible = true;
                string kullanici_eposta = Query.GetString("KullaniciEposta");
                string onay_kodu = Query.GetString("Kod");
                if (!string.IsNullOrEmpty(kullanici_eposta) && !string.IsNullOrEmpty(onay_kodu))
                {
                    string dogru_onay_kodu = Uyelik.SifremiUnuttumIcinHashOlustur(kullanici_eposta);
                    if (!string.IsNullOrEmpty(dogru_onay_kodu) && onay_kodu == dogru_onay_kodu)
                    {
                        pnlBasari.Visible = true;
                        pnlHata.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
            pnlHata.Visible = true;
            pnlBasari.Visible = false;
        }
    }

    protected void SifreDegistir(object sender, EventArgs e)
    {
        string kullanici_eposta = Query.GetString("KullaniciEposta");
        if (Uyelik.KullaniciSifreDegistir(kullanici_eposta, txtSifre.Text))
        {
            pnlBasari.Visible = false;
            lblDurum.Text = "Şifreni değiştirdik. Bundan sonra yeni şifrenle giriş yapabilirsin.";
        }
        else
        {
            Mesajlar.AdmineHataMesajiGonder(((System.Web.UI.Page)(sender)).Request.Url.ToString(), 
                "Kullanicinin sifresini degistiremedik. Kullanici eposta:" + kullanici_eposta, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
            lblDurum.Text = "Bir hata oldu, lütfen tekrar deneyin.";
        }
    }

}
