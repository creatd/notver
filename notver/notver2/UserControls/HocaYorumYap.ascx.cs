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



    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            KontroluSakla();
            hocaKullaniciDerslerObj = hocaKullaniciDersler;
            hocaKullaniciDerslerObj.Clear();
            hocaKullaniciDersler = hocaKullaniciDerslerObj;
        }
        
        if (Query.GetInt("HocaID") <= 0)
        {
            return;
        }
        if (session.IsLoggedIn && session.KullaniciID > 0)
        {
            pnlPuanYorum.Visible = true;
            baslikPuanYorum.Visible = true;
            pnlUyeOl.Visible = false;

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
                //TODO: admin mesaj
            }

            bool yorumVar = false;

            if (Hocalar.KullaniciHocayaYorumYapmis(session.KullaniciID, Query.GetInt("HocaID")))
            {
                yorumVar = true;
                //Kullanicinin daha once yaptigi yorumlari yukle
                /*
                List<object> listEskiYorum = Hocalar.KullaniciHocaYorumunuDondur(session.KullaniciID, Query.GetInt("HocaID"));
                //yorum - puan1 - puan2 - puan3 - puan4 - puan5 - { (Ders ID - Ders Kodu)| (-1 - Ders Ismi) }(*)
                textYorum.Text = (string)listEskiYorum[0];
                Puan1.CurrentRating = (int)listEskiYorum[1];
                Puan2.CurrentRating = (int)listEskiYorum[2];
                Puan3.CurrentRating = (int)listEskiYorum[3];
                Puan4.CurrentRating = (int)listEskiYorum[4];
                Puan5.CurrentRating = (int)listEskiYorum[5];
                hocaKullaniciDerslerObj = hocaKullaniciDersler;
                hocaKullaniciDerslerIDlerObj = hocaKullaniciDerslerIDler;
                for (int i = 6; i < listEskiYorum.Count-1; i+=2)
                {
                    if ((int)listEskiYorum[i] == -1)
                    {
                        hocaKullaniciDerslerObj.Add((string)listEskiYorum[i+1]);
                        //Her "Diger" secenegi icin ID'lere de ekleyelim ki Dersler ve DerslerIDler'in index'leri ayni olsun
                        hocaKullaniciDerslerIDlerObj.Add(-2);   //-2 degeri Hocalar sinifinda da kullaniliyor
                    }
                    else
                    {
                        hocaKullaniciDerslerIDlerObj.Add((int)listEskiYorum[i]);
                        hocaKullaniciDerslerObj.Add((string)listEskiYorum[i + 1]);
                    }
                }
                hocaKullaniciDersler = hocaKullaniciDerslerObj;
                hocaKullaniciDerslerIDler = hocaKullaniciDerslerIDlerObj;

                dropHocaDersler.DataSource = hocaKullaniciDersler;
                dropHocaDersler.DataBind();*/
            }

            if (yorumVar)
            {
                baslikPuanYorum.Text = "Puan/yorumlarimi degistirecegim";
                dugmeYorumGuncelle.Visible = true;
                dugmeYorumGonder.Visible = false;
                lnkKullaniciYorumlar.Visible = true;
                lnkKullaniciYorumlar.NavigateUrl = HocaYorumlarimURLDondur(Query.GetInt("HocaID"));
            }
            else
            {
                baslikPuanYorum.Text = "Puan verecegim";
                dugmeYorumGuncelle.Visible = false;
                dugmeYorumGonder.Visible = true;
                lnkKullaniciYorumlar.Visible = false;
            }

            if (!Page.IsPostBack || dropHocaDersler.Items.Count == 0)  //Items.Count ==0 'i, sayfa acildiktan sonra login yapilirsa
                //girsin diye koydum
            {
                dropHocaDersler.Items.Clear();
                //Hocanin verdigi dersleri yukleyip dropdown'a yukle
                dropHocaDersler.Items.Add(new ListItem("", "-1"));
                string[][] hocaDersler = Hocalar.HocaDersleriniDondur(Query.GetInt("HocaID"));

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
                            dropHocaDersler.Items.Add(new ListItem(hocaDersler[i][0] + " (" + okulIsmi.Substring(0, 20) + ")", hocaDersler[i][1]));
                        }
                        dersIsimleri[i] = hocaDersler[i][2];
                    }
                }
                dropHocaDersler.Items.Add(new ListItem("Diger", "-2")); //-2 degeri Hocalar sinifinda da kullaniliyor
            }

        }
        else  //Giris yapmamis
        {
            pnlPuanYorum.Visible = false;
            baslikPuanYorum.Visible = false;
            pnlUyeOl.Visible = true;
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
            kullaniciPuanAraligi,(List<int>) hocaKullaniciDerslerIDler , (List<string>)hocaKullaniciDersler))
        {
            ltrDurum.Text = "Puan ve yorumlariniz basariyla kaydedildi!";
        }
        else
        {
            ltrDurum.Text = "Puan ve yorumlarinizi kaydederken bir hata olustu, lutfen tekrar deneyin.";
        }
    }
    /*
    protected void PuanYorumGuncelle(object sender, EventArgs e)
    {
        int[] puanlar = null;
        puanlar = new int[5];
        puanlar[0] = Puan1.CurrentRating;
        puanlar[1] = Puan2.CurrentRating;
        puanlar[2] = Puan3.CurrentRating;
        puanlar[3] = Puan4.CurrentRating;
        puanlar[4] = Puan5.CurrentRating;
        if (Hocalar.HocaPuanGuncelle(session.KullaniciID, Query.GetInt("HocaID"), puanlar))
        {
            ltrDurum.Text = "Puaniniz guncellendi!";
        }
        else
        {
            ltrDurum.Text = "Puan guncellenirken bir hata olustu, lutfen tekrar deneyin";
        }

        string[] yorumlar = null;

        if (textOlumlu.Text.Length > 0 || textOlumsuz.Text.Length > 0 || textOzet.Text.Length > 0)
        {
            yorumlar = new string[3];
            yorumlar[0] = textOlumlu.Text;
            yorumlar[1] = textOlumsuz.Text;
            yorumlar[2] = textOzet.Text;
        }

        int kullaniciPuanaraligi = Convert.ToInt32(dropGenelPuan.SelectedValue);
        if (!Hocalar.HocaYorumGuncelle(session.KullaniciID, Query.GetInt("HocaID"), yorumlar, kullaniciPuanaraligi))
        {
            ltrDurum.Text = ltrDurum.Text + "<br />Yorum guncellerken bir hata olustu, lutfen tekrar deneyin";
        }
        else
        {
            ltrDurum.Text = ltrDurum.Text + "<br />Yorumunuz guncellendi!";
        }
    }*/

    void KontroluSakla()
    {
        pnlPuanYorum.Visible = false;
        pnlUyeOl.Visible = false;
        baslikPuanYorum.Visible = false;
        dugmeYorumGonder.Visible = false;
        dugmeYorumGuncelle.Visible = false;
        dropDersEkle.Visible = false;
        txtDersKodDiger.Visible = false;
        lnkKullaniciYorumlar.Visible = false;
    }
}


