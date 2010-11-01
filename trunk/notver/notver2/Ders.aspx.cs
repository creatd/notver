﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Ders : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                int queryDersID = Query.GetInt("DersID");
                if (queryDersID > 0)
                {
                    session.DersYukle(queryDersID);
                    //Ders kod ve isim
                    if (!string.IsNullOrEmpty(session.DersKod) && !string.IsNullOrEmpty(session.DersIsim))
                    {
                        lblDersIsim.Text = session.DersKod + " - " + session.DersIsim;
                    }
                    else
                    {
                        lblDersIsim.Text = "";
                    }
                    //Ders aciklama
                    if (!string.IsNullOrEmpty(session.DersAciklama))
                    {
                        lblDersAciklama.Text = session.DersAciklama;
                    }
                    else
                    {
                        lblDersAciklama.Text = "";
                    }
                    //Ders okul isim (Dersin verildigi okulun ismi)
                    if (!string.IsNullOrEmpty(session.DersOkulIsim))
                    {
                        lblDersOkulIsim.Text = session.DersOkulIsim;
                    }
                    else
                    {
                        lblDersOkulIsim.Text = "";
                    }
                    lnkDersDosyalar.NavigateUrl = DersDosyaURLDondur(queryDersID);
                }
            }
            catch (Exception ex)
            {
                Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
                GoToDefaultPage();
            }
        }
    }
}