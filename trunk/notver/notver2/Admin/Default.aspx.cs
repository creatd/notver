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
using System.Collections.Generic;

public partial class Admin_Default : System.Web.UI.Page
{
    Session session;
    List<string> hocaOkullarObj;
    public List<string> hocaOkullar
    {
        get
        {
            List<string> obj = (List<string>)ViewState["hocaOkullar"];
            if (obj != null)
                return obj;
            else
                return new List<string>();
        }
        set
        {
            ViewState["hocaOkullar"] = value;
        }
    }
    List<int> hocaOkullarIDlerObj;
    public List<int> hocaOkullarIDler
    {
        get
        {
            List<int> obj = (List<int>)ViewState["hocaOkullarIDler"];
            if (obj != null)
                return obj;
            else
                return new List<int>();
        }
        set
        {
            ViewState["hocaOkullarIDler"] = value;
        }
    }

    List<string> hocaDerslerObj;
    public List<string> hocaDersler
    {
        get
        {
            List<string> obj = (List<string>)ViewState["hocaDersler"];
            if (obj != null)
                return obj;
            else
                return new List<string>();
        }
        set
        {
            ViewState["hocaDersler"] = value;
        }
    }
    List<int> hocaDerslerIDlerObj;
    public List<int> hocaDerslerIDler
    {
        get
        {
            List<int> obj = (List<int>)ViewState["hocaDerslerIDler"];
            if (obj != null)
                return obj;
            else
                return new List<int>();
        }
        set
        {
            ViewState["hocaDerslerIDler"] = value;
        }
    }

    protected void Page_Prerender(object sender, EventArgs e)
    {
        if (session == null)
        {
            session = new Session();
        }

        if (!Page.IsPostBack)
        {
            /*hocaOkullarObj = hocaOkullar;   //Null donmesin, yeni liste donsun diye
            hocaOkullarObj.Clear();
            hocaOkullar = hocaOkullarObj;*/

            drpHocaOkullar.Items.Clear();
            drpDersOkullar.Items.Clear();
            foreach (DataRow dr in session.dtOkullar.Rows)
            {
                drpHocaOkullar.Items.Add(new ListItem(dr["ISIM"].ToString(), dr["OKUL_ID"].ToString()));
                drpDersOkullar.Items.Add(new ListItem(dr["ISIM"].ToString(), dr["OKUL_ID"].ToString()));
            }
        }
    }

    protected void Refresh()
    {
        Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void OkulEkle(object sender, EventArgs e)
    {
        lblDurumOkulEkle.Text = "";
        //TODO: Gerekli kontrolleri yap burda
        if (Okullar.OkulEkle(Convert.ToBoolean(Convert.ToInt32(drpOkulEkleActive.SelectedValue)),
            txtOkulIsim.Text, txtOkulAdres.Text, Convert.ToInt32(txtOkulKurulusTarihi.Text),
            Convert.ToInt32(txtOkulOgrenciSayisi.Text), Convert.ToInt32(txtOkulAkademikSayisi.Text),
            txtOkulWebAdresi.Text))
        {
            OkulSifirla();
            lblDurumOkulEkle.Text = "Okul basariyla eklendi";
            if (session == null)
            {
                session = new Session();
            }
            session.dtOkullar = null;   //Yeniden yuklensin diye
            Refresh();
        }
        else
        {
            lblDurumOkulEkle.Text = "Bir hata olustu";
        }
        
    }

    protected void HocaEkle(object sender, EventArgs e)
    {
        List<int> okulBaslangicYillari = new List<int>();
        List<int> okulBitisYillari = new List<int>();
        foreach (string str in hocaOkullar)
	    {
		    string tarih = str.Substring(str.LastIndexOf("(")+1);   //orn. ?-? ?-... sayi-... sayi-sayi
            int baslangicYili = -1;
            int bitisYili = -1;
            if(!tarih.StartsWith("?"))
            {
                baslangicYili = Convert.ToInt32(tarih.Substring(0, tarih.IndexOf("-")));
            }
            tarih = tarih.Substring(tarih.IndexOf("-") + 1);
            tarih = tarih.Substring(0, tarih.Length - 1);   //Sondaki )'den kurtul
            if(!tarih.StartsWith("?"))
            {
                if(tarih.StartsWith("..."))
                {
                    bitisYili = 0;
                }
                else
                {
                    bitisYili = Convert.ToInt32(tarih);
                }
            }
            okulBaslangicYillari.Add(baslangicYili);
            okulBitisYillari.Add(bitisYili);
	    }

        if (Hocalar.HocaEkle(Convert.ToBoolean(Convert.ToInt32(drpHocaEkleActive.SelectedValue)),
            txtHocaIsim.Text, txtHocaUnvan.Text, Convert.ToInt32(txtHocaYorumSayisi.Text),
            hocaOkullarIDler, okulBaslangicYillari, okulBitisYillari, hocaDerslerIDler))
        {
            lblDurumHocaEkle.Text = "Hoca basariyla eklendi";
            HocaSifirla();
        }
        else
        {
            lblDurumHocaEkle.Text = "Hoca eklerken bir hata olustu!";
        }
    }

    protected void HocaOkulEkle(object sender, EventArgs e)
    {
        lblHocaOkulEkleDurum.Text = "";
        int seciliDeger = Convert.ToInt32(drpHocaOkullar.SelectedValue);
        int baslangic_yili = -1;
        int bitis_yili = -1;
        if (!string.IsNullOrEmpty(txtHocaOkulBaslangicYili.Text))
        {
            baslangic_yili = Convert.ToInt32(txtHocaOkulBaslangicYili.Text);
        }
        if (!string.IsNullOrEmpty(txtHocaOkulBitisYili.Text))
        {
            bitis_yili = Convert.ToInt32(txtHocaOkulBitisYili.Text);
        }

        if (bitis_yili > 0 && bitis_yili < baslangic_yili)
        {
            lblHocaOkulEkleDurum.Text = "Bitis yili baslangic yilindan once olamaz";
            return;
        }

        if (seciliDeger >= 0)
        {
            if (hocaOkullarIDler.Contains(seciliDeger))
            {
                lblHocaOkulEkleDurum.Text = "Bu okul zaten ekli";
                return;
            }
            else
            {
                string yazi = drpHocaOkullar.SelectedItem.Text;
                if (baslangic_yili > 0)
                {
                    yazi += " (" + baslangic_yili + "-";
                }
                else
                {
                    yazi += " (?-";
                }
                if (bitis_yili == 0)
                {
                    yazi += "...";
                }
                else if (bitis_yili > 0)
                {
                    yazi += bitis_yili;
                }
                else
                {
                    yazi += "?";
                }
                yazi += ")";

                hocaOkullarObj = hocaOkullar;
                hocaOkullarObj.Add(yazi);
                hocaOkullar = hocaOkullarObj;

                hocaOkullarIDlerObj = hocaOkullarIDler;
                hocaOkullarIDlerObj.Add(seciliDeger);
                hocaOkullarIDler = hocaOkullarIDlerObj;

                repeaterHocaOkullar.DataSource = hocaOkullar;
                repeaterHocaOkullar.DataBind();
            }
        }
    }

    protected void HocaSifirla()
    {
        drpHocaEkleActive.SelectedIndex = 0;
        txtHocaIsim.Text = "";
        txtHocaUnvan.Text = "";
        txtHocaYorumSayisi.Text = "0";

        hocaDerslerObj = hocaDersler;
        hocaDerslerObj.Clear();
        hocaDersler = hocaDerslerObj;

        hocaDerslerIDlerObj = hocaDerslerIDler;
        hocaDerslerIDlerObj.Clear();
        hocaDerslerIDler = hocaDerslerIDlerObj;
        
        repeaterHocaDersler.DataSource = hocaDersler;
        repeaterHocaDersler.DataBind();

        hocaOkullarObj = hocaOkullar;
        hocaOkullarObj.Clear();
        hocaOkullar = hocaOkullarObj;

        hocaOkullarIDlerObj = hocaOkullarIDler;
        hocaOkullarIDlerObj.Clear();
        hocaOkullarIDler = hocaOkullarIDlerObj;

        repeaterHocaOkullar.DataSource = hocaOkullar;
        repeaterHocaOkullar.DataBind();
    }

    protected void RepeaterHocaOkullar_OkulSil(object sender, RepeaterCommandEventArgs e)
    {
        hocaOkullarObj = hocaOkullar;
        hocaOkullarObj.RemoveAt(e.Item.ItemIndex);
        hocaOkullar = hocaOkullarObj;

        hocaOkullarIDlerObj = hocaOkullarIDler;
        hocaOkullarIDlerObj.RemoveAt(e.Item.ItemIndex);
        hocaOkullarIDler = hocaOkullarIDlerObj;

        repeaterHocaOkullar.DataSource = hocaOkullar;
        repeaterHocaOkullar.DataBind();
    }

    protected void HocaDersEkle(object sender, EventArgs e)
    {
        lblHocaDersEkleDurum.Text = "";
        int seciliDeger = Convert.ToInt32(drpHocaDersler.SelectedValue);

        if (seciliDeger >= 0)
        {
            if (hocaDerslerIDler.Contains(seciliDeger))
            {
                lblHocaDersEkleDurum.Text = "Bu ders zaten ekli";
                return;
            }
            else
            {
                hocaDerslerObj = hocaDersler;
                hocaDerslerObj.Add(drpHocaDersler.SelectedItem.Text);
                hocaDersler = hocaDerslerObj;

                hocaDerslerIDlerObj = hocaDerslerIDler;
                hocaDerslerIDlerObj.Add(seciliDeger);
                hocaDerslerIDler = hocaDerslerIDlerObj;

                repeaterHocaDersler.DataSource = hocaDersler;
                repeaterHocaDersler.DataBind();
            }
        }
    }

    protected void RepeaterHocaDersler_DersSil(object sender, RepeaterCommandEventArgs e)
    {
        hocaDerslerObj = hocaDersler;
        hocaDerslerObj.RemoveAt(e.Item.ItemIndex);
        hocaDersler = hocaDerslerObj;

        hocaDerslerIDlerObj = hocaDerslerIDler;
        hocaDerslerIDlerObj.RemoveAt(e.Item.ItemIndex);
        hocaDerslerIDler = hocaDerslerIDlerObj;

        repeaterHocaDersler.DataSource = hocaDersler;
        repeaterHocaDersler.DataBind();
    }

    protected void HocaDerslerGuncelle(object sender, EventArgs e)
    {
        drpHocaDersler.Items.Clear();
        if (hocaOkullarIDler.Count > 0)
        {
            //Her ekli okul icin, o okulda verilen dersleri dropdown'a ekle

            if (hocaOkullarIDler.Count == 1)
            {
                //Dropdown'a 'DersKodu' olarak ekle
                DataTable dtOkuldakiDersler = Dersler.OkuldakiDersleriDondur(hocaOkullarIDler[0]);
                if (dtOkuldakiDersler != null)
                {
                    foreach (DataRow dr in dtOkuldakiDersler.Rows)
                    {
                        drpHocaDersler.Items.Add(new ListItem(dr["KOD"].ToString(), dr["DERS_ID"].ToString()));
                    }
                }
                
            }
            else
            {
                //Dropdown'a 'OkulAdi - DersKodu' olarak ekle
                foreach (int okulID in hocaOkullarIDler)
                {
                    DataTable dtOkuldakiDersler = Dersler.OkuldakiDersleriDondur(okulID);
                    if (dtOkuldakiDersler != null)
                    {
                        foreach (DataRow dr in dtOkuldakiDersler.Rows)
                        {
                            drpHocaDersler.Items.Add(new ListItem(dr["OKUL_ISIM"].ToString() + 
                                " - " + dr["KOD"].ToString(), dr["DERS_ID"].ToString()));
                        }
                    }
                }
            }
        }
    }

    protected void DersEkle(object sender, EventArgs e)
    {
        if (Dersler.DersEkle(Convert.ToInt32(drpDersOkullar.SelectedValue), Convert.ToBoolean(Convert.ToInt32(drpDersIsActive.SelectedValue)),
            txtDersKod.Text, txtDersIsim.Text, txtDersAciklama.Text))
        {
            lblDurumDersEkle.Text = "Ders basariyla eklendi";
            DersSifirla();
        }
        else
        {
            lblDurumDersEkle.Text = "Ders eklenirken bir hata olustu!";
        }
    }

    protected void DersSifirla()
    {
        drpDersOkullar.SelectedIndex = 0;
        drpDersIsActive.SelectedIndex = 0;
        txtDersKod.Text = "";
        txtDersIsim.Text = "";
        txtDersAciklama.Text = "";
    }

    protected void OkulSifirla()
    {
        drpOkulEkleActive.SelectedIndex = 0;
        txtOkulIsim.Text = "";
        txtOkulAdres.Text = "";
        txtOkulKurulusTarihi.Text = "";
        txtOkulOgrenciSayisi.Text = "";
        txtOkulAkademikSayisi.Text = "";
        txtOkulWebAdresi.Text = "";
    }
}
