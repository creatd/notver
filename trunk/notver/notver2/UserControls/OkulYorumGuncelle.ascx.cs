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

public partial class UserControls_OkulYorumGuncelle : BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (session.IsLoggedIn)
            {
                pnlYorum.Visible = true;
                pnlUyeOl.Visible = false;
                //Kullanicinin daha once yapmis oldugu yorumu yukle
                string eskiYorum = Okullar.KullaniciOkulYorumunuDondur(session.KullaniciID, Query.GetInt("OkulID"));
                if (Util.GecerliString(eskiYorum))
                {
                    textYorum.Text = eskiYorum;
                }

            }
            else
            {
                pnlYorum.Visible = false;
                pnlUyeOl.Visible = true;
            }
        }
    }

    protected void YorumGuncelle(object sender, EventArgs e)
    {
        if (Okullar.OkulYorumGuncelle(session.KullaniciID, Query.GetInt("OkulID"), textYorum.Text))
        {
            ltrDurum.Text = "Yorumunuz basariyla guncellendi";
            ltrScript.Text = "<script type='text/javascript'>setTimeout('self.parent.tb_remove()',1500);</script>";
        }
        else
        {
            ltrDurum.Text = "Bir hata olustu, lutfen tekrar deneyin.";
        }
    }
}
