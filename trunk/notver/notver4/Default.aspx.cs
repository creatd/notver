﻿using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class _Default : BasePage
{
    protected void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        if (ex != null)
        {
            if (session != null)
            {
                Mesajlar.AdmineHataMesajiGonder(Page.Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
            }
            else
            {
                Mesajlar.AdmineHataMesajiGonder(Page.Request.Url.ToString(), ex.Message, -1, Enums.SistemHataSeviyesi.Orta);
            }
        }
    }

    protected void Page_Prerender(object sender, EventArgs e)
    {
        pnlTanitim.Visible = true;
        pnlHosgeldin.Visible = false;
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Query.GetString("hosgeldin")))
            {
                pnlTanitim.Visible = false;
                pnlHosgeldin.Visible = true;
            }
        }
    }
}
