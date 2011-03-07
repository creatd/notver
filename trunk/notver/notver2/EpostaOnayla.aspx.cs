using System;
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

public partial class EpostaOnayla : BasePage
{
    protected void Page_Prerender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            pnlOnayBasari.Visible = false;
            pnlOnayBasari_UniEposta.Visible = false;
            pnlOnayHata.Visible = true;
            string kullanici_eposta = Query.GetString("KullaniciEposta");
            string onay_kodu = Query.GetString("OnayKodu");
            if (!string.IsNullOrEmpty(kullanici_eposta) && !string.IsNullOrEmpty(onay_kodu))
            {
                bool universite_epostasi = false;
                if (onay_kodu[0] == '1')
                {
                    universite_epostasi = true;
                }
                onay_kodu = onay_kodu.Substring(1);
                if (onay_kodu == Uyelik.OnayIcinHashOlustur(kullanici_eposta))
                {
                    if (!Uyelik.KullaniciEpostaOnayla(kullanici_eposta,universite_epostasi))
                    {
                        //TODO: admin'e mesaj
                        //Kullanicinin epostasini onaylamamiz gerekirken veritabanina yazamadik
                    }
                    pnlOnayHata.Visible = false;
                    if (universite_epostasi)
                    {
                        pnlOnayBasari_UniEposta.Visible = true;
                    }
                    else
                    {
                        pnlOnayBasari.Visible = true;
                    }
                }
            }
        }

    }
}
