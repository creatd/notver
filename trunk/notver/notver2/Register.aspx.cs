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

public partial class Register : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (session.IsLoggedIn)
            {
                GoToDefaultPage();
            }

            DropDownList Okullar = ddOkullar as DropDownList;
            Okullar.Items.Add(new ListItem("-", "-1"));
            foreach (DataRow dr in session.dtOkullar.Rows)
            {
                Okullar.Items.Add(new ListItem(dr["ISIM"].ToString(), dr["OKUL_ID"].ToString()));
            }
        }
    }


    protected void KullaniciOlustur(object sender, EventArgs e)
    {
        string kullaniciAdi = txtKullaniciAdi.Text.Trim();
        string sifre = txtSifre.Text.Trim();
        int result = Uyelik.KullaniciOlustur(kullaniciAdi, sifre);
        lblDurum.Text = "";
        if (result == -1)
        {
            lblDurum.Text = "Kullanici adi alinmis, lutfen baska bir kullanici adi secin.";
        }
        else if (result == 0)
        {
            lblDurum.Text = "Bilinmeyen hata, lutfen tekrar deneyin.";
        }
        else if (result == 1)
        {
            Uyelik.GirisYap(kullaniciAdi, sifre);
            Response.Redirect(Page.ResolveUrl("~/Default.aspx"));
        }
    }
}
