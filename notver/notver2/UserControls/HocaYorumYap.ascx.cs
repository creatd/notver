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

public partial class UserControls_HocaYorumYap : BaseUserControl
{
    static string[] dersIsimleri;
    List<string> hocaKullaniciDerslerObj;
    public List<string> hocaKullaniciDersler
    {
        get
        {
            List<string> obj = (List<string>)ViewState["hocaKullaniciDersler"];
            if (obj != null)
                return obj;
            else
                return new List<string>();
        }
        set
        {
            ViewState["hocaKullaniciDersler"] = value;
        }
    }
    List<int> hocaKullaniciDerslerIDlerObj;
    public List<int> hocaKullaniciDerslerIDler
    {
        get
        {
            List<int> obj = (List<int>)ViewState["hocaKullaniciDerslerIDler"];
            if (obj != null)
                return obj;
            else
                return new List<int>();
        }
        set
        {
            ViewState["hocaKullaniciDerslerIDler"] = value;
        }
    }



    protected void Page_Prerender(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                KontroluSakla();

                //TODO: kontrol et
                /*hocaKullaniciDerslerObj = hocaKullaniciDersler; //Null donmesin, yeni liste donsun diye
                hocaKullaniciDerslerObj.Clear();
                hocaKullaniciDersler = hocaKullaniciDerslerObj;*/
            }

            int queryHocaID = Query.GetInt("HocaID");
            if (queryHocaID < 0)
            {
                pnlHata.Visible = true;
                return;
            }
            if (session.IsLoggedIn && session.KullaniciID >= 0)
            {
                pnlPuanYorum.Visible = true;    

                if (session.hocaPuanAciklamalari.Length == 5)
                {
                    Aciklama1.Text = session.hocaPuanAciklamalari[0];
                    Aciklama2.Text = session.hocaPuanAciklamalari[1];
                    Aciklama3.Text = session.hocaPuanAciklamalari[2];
                    Aciklama4.Text = session.hocaPuanAciklamalari[3];
                    Aciklama5.Text = session.hocaPuanAciklamalari[4];
                }
                else
                {
                    pnlPuanYorum.Visible = false;
                    pnlHata.Visible = true;
                    return;
                    //TODO: admin mesaj
                }

                if (!Page.IsPostBack || dropHocaDersler.Items.Count == 0)  //Items.Count ==0 'i, sayfa acildiktan sonra login yapilirsa
                //girsin diye koydum
                {
                    dropHocaDersler.Items.Clear();
                    //Hocanin verdigi dersleri yukleyip dropdown'a yukle
                    dropHocaDersler.Items.Add(new ListItem("", "-1"));
                    string[][] hocaDersler = Hocalar.HocaDersleriniDondur(queryHocaID);

                    if (hocaDersler != null)
                    {
                        dersIsimleri = new string[hocaDersler.Length];
                        for (int i = 0; i < hocaDersler.Length; i++)
                        {
                            //Okul ismi uzunsa kisalt
                            string okulIsmi = hocaDersler[i][3];
                            if (okulIsmi.Length > 20)
                            {
                                dropHocaDersler.Items.Add(new ListItem(hocaDersler[i][0] + " (" + okulIsmi.Substring(0, 18) + "..)", hocaDersler[i][1]));
                            }
                            else
                            {
                                dropHocaDersler.Items.Add(new ListItem(hocaDersler[i][0] + " (" + okulIsmi + ")", hocaDersler[i][1]));
                            }
                            dersIsimleri[i] = hocaDersler[i][2];
                        }
                    }
                    dropHocaDersler.Items.Add(new ListItem("Diger", "-2")); //-2 degeri Hocalar sinifinda da kullaniliyor
                }
                lnkKullaniciYorumlar.NavigateUrl = "javascript:parent.document.location='" + HocaYorumlarimURLDondur(queryHocaID) + "';";

            }
            else  //Giris yapmamis
            {
                pnlUyeOl.Visible = true;
            }
        }
        catch (Exception ex)
        {
            KontroluSakla();
            pnlHata.Visible = true;
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
        }
    }

    protected void dropHocaDersler_Secildi(object sender, EventArgs e)
    {
        dersIsim.Text = "";
        int selectedIndex = dropHocaDersler.SelectedIndex;
        int seciliDeger = Convert.ToInt32(dropHocaDersler.SelectedValue);
        dropDersEkle.Visible = false;
        txtDersKodDiger.Visible = false;
        if (seciliDeger >= 0)
        {
            dropDersEkle.Visible = true;
            //Ders ismini getir
            if (selectedIndex > 0 && dersIsimleri != null)
            {
                if (dersIsimleri.Length > selectedIndex - 1)
                {
                    dersIsim.Text = dersIsimleri[selectedIndex - 1];
                }
            }
        }
        else if (seciliDeger == -2)  //Diger
        {
            //TODO: Admin'e mesaj gonder
            dropDersEkle.Visible = true;
            txtDersKodDiger.Visible = true;
        }
        else
        {
            dropDersEkle.Visible = false;
        }         
    }

    
    protected void dropDersEkle_Click(object sender, EventArgs e)
    {
        dersIsim.Text = "";
        dropDersEkle.Visible = false;
        txtDersKodDiger.Visible = false;
        int seciliDeger = Convert.ToInt32(dropHocaDersler.SelectedValue);
        if (seciliDeger >= 0)
        {
            if (hocaKullaniciDerslerIDler.Contains(seciliDeger))
            {
                dersIsim.Text = "Bu dersi zaten eklediniz";
                return;
            }
            else
            {
                hocaKullaniciDerslerObj = hocaKullaniciDersler;
                hocaKullaniciDerslerObj.Add(dropHocaDersler.SelectedItem.Text);
                hocaKullaniciDersler = hocaKullaniciDerslerObj;

                hocaKullaniciDerslerIDlerObj = hocaKullaniciDerslerIDler;
                hocaKullaniciDerslerIDlerObj.Add(seciliDeger);
                hocaKullaniciDerslerIDler = hocaKullaniciDerslerIDlerObj;

                repeaterDersler.DataSource = hocaKullaniciDersler;
                repeaterDersler.DataBind();
            }
        }
        else if (seciliDeger == -2 && !string.IsNullOrEmpty(txtDersKodDiger.Text))
        {
            if (hocaKullaniciDersler.Contains(txtDersKodDiger.Text))
            {
                dersIsim.Text = "Bu dersi zaten eklediniz";
                return;
            }
            hocaKullaniciDerslerObj = hocaKullaniciDersler;
            hocaKullaniciDerslerObj.Add(txtDersKodDiger.Text.Trim());
            hocaKullaniciDersler = hocaKullaniciDerslerObj;

            //Her "Diger" secenegi icin ID'lere de ekleyelim ki Dersler ve DerslerIDler'in index'leri ayni olsun
            hocaKullaniciDerslerIDlerObj = hocaKullaniciDerslerIDler;
            hocaKullaniciDerslerIDlerObj.Add(-2);   //-2 degeri Hocalar sinifinda da kullaniliyor
            hocaKullaniciDerslerIDler = hocaKullaniciDerslerIDlerObj;

            repeaterDersler.DataSource = hocaKullaniciDersler;
            repeaterDersler.DataBind();
        }

    }

    protected void repeaterDersSil(object sender, RepeaterCommandEventArgs e)
    {
        hocaKullaniciDerslerObj = hocaKullaniciDersler;
        hocaKullaniciDerslerObj.RemoveAt(e.Item.ItemIndex);
        hocaKullaniciDersler = hocaKullaniciDerslerObj;

        hocaKullaniciDerslerIDlerObj = hocaKullaniciDerslerIDler;
        hocaKullaniciDerslerIDlerObj.RemoveAt(e.Item.ItemIndex);
        hocaKullaniciDerslerIDler = hocaKullaniciDerslerIDlerObj;        

        repeaterDersler.DataSource = hocaKullaniciDersler;
        repeaterDersler.DataBind();
    }

    /// <summary>
    /// Yorum ve puanlari kaydeder
    /// Hem yorum hem puan girmek zorunlu degil ancak bir puan girerse tum 5 puani da girmeli kullanici
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void PuanYorumKaydet(object sender, EventArgs e)
    {
        if (!(Puan1.CurrentRating > 0 && Puan2.CurrentRating > 0 && Puan3.CurrentRating > 0 && Puan4.CurrentRating > 0 && Puan5.CurrentRating > 0))    //Puanlarin hepsini girmedi
        {
            ltrDurum.Text = "Puanlariniz eksik. Her kategoride puan girmelisiniz";
            return;
        }
        int[] puanlar = new int[5];
        puanlar[0] = Puan1.CurrentRating;
        puanlar[1] = Puan2.CurrentRating;
        puanlar[2] = Puan3.CurrentRating;
        puanlar[3] = Puan4.CurrentRating;
        puanlar[4] = Puan5.CurrentRating;
        int kullaniciPuanAraligi = Convert.ToInt32(dropGenelPuan.SelectedValue);
        if (Hocalar.HocaYorumPuanKaydet(session.KullaniciID, Query.GetInt("HocaID"), puanlar,textYorum.Text,
            kullaniciPuanAraligi,(List<int>) hocaKullaniciDerslerIDler , (List<string>)hocaKullaniciDersler, session.KullaniciOnayPuani))
        {
            ltrDurum.Text = "Puan ve yorumlariniz basariyla kaydedildi!";
            ltrScript.Text = "<script type='text/javascript'>setTimeout('parent.$.fn.colorbox.close()',1500);</script>";
        }
        else
        {
            ltrDurum.Text = "Puan ve yorumlarinizi kaydederken bir hata olustu, lutfen tekrar deneyin.";
        }
    }

    void KontroluSakla()
    {
        pnlPuanYorum.Visible = false;
        pnlUyeOl.Visible = false;
        pnlHata.Visible = false;
        dropDersEkle.Visible = false;
    }
}


