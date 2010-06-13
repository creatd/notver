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
using System.Collections.Generic;

public partial class UserControls_HocaYorumYap : BaseUserControl
{
    static string[] dersIsimleri;
    static List<string> hocaKullaniciDersler = new List<string>();
    //static List<int> hocaKullaniciDersler_ID = new List<int>();
    //static Hashtable hocaKullaniciDersler = new Hashtable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            KontroluSakla();

            if (hocaKullaniciDersler != null)
            {
                hocaKullaniciDersler.Clear();
            }
            else
            {
                hocaKullaniciDersler = new List<string>();
                //hocaKullaniciDersler = new Hashtable();
            }
            pnlPuanYorum.Visible = true;
            if (session.HocaID <= 0)
            {
                return;
            }
            if (IsLoggedIn() && session.KullaniciID > 0)
            {
                baslikPuanYorum.Visible = true;
                
                if (hocaPuanAciklamalari == null)
                {
                    hocaPuanAciklamalari = HocaPuanAciklamalariniDondur();
                }
                if (hocaPuanAciklamalari.Length == 5)
                {
                    Aciklama1.Text = hocaPuanAciklamalari[0];
                    Aciklama2.Text = hocaPuanAciklamalari[1];
                    Aciklama3.Text = hocaPuanAciklamalari[2];
                    Aciklama4.Text = hocaPuanAciklamalari[3];
                    Aciklama5.Text = hocaPuanAciklamalari[4];
                }

                bool yorumVar = false;
                bool puanVar = false;

                if (!KullaniciHocayaPuanVermis(session.KullaniciID, session.HocaID))
                {
                    //baslikPuan.Text = "Benim de puanlarim var!";
                    //pnlPuanVer.Visible = true;
                    puanVar = false;
                }
                else
                {
                    puanVar = true;
                    //Kullanicinin daha once vermis oldugu puanlari yukle
                    int[] eskiPuanlar = KullaniciHocaPuanlariniDondur(session.KullaniciID, session.HocaID);
                    Puan1.CurrentRating = eskiPuanlar[0];
                    Puan2.CurrentRating = eskiPuanlar[1];
                    Puan3.CurrentRating = eskiPuanlar[2];
                    Puan4.CurrentRating = eskiPuanlar[3];
                    Puan5.CurrentRating = eskiPuanlar[4];

                    //pnlPuanVer.Visible = true;
                    //baslikPuan.Text = "Verdiginiz puanlari degistirmek icin tiklayin";
                    //pnlPuanDegistir.Visible = true;
                }

                if (!KullaniciHocayaYorumYapmis(session.KullaniciID, session.HocaID))
                {
                    //baslikYorum.Text = "Benim de soyleyeceklerim var!";
                    dugmeYorumGonder.Visible = true;
                }
                else
                {
                    yorumVar = true;
                    //Kullanicinin daha once yaptigi yorumlari yukle
                    string[] eskiYorumlar = KullaniciHocaYorumlariniDondur(session.KullaniciID, session.HocaID);
                    textOlumlu.Text = eskiYorumlar[0];
                    textOlumsuz.Text = eskiYorumlar[1];
                    textOzet.Text = eskiYorumlar[2];

                    //baslikYorum.Text = "Yaptiginiz yorumu degistirmek icin tiklayin";
                    //pnlYorumYap.Visible = true;
                    //dugmeYorumGuncelle.Visible = true;
                    //pnlYorumDegistir.Visible = true;
                }

                if (yorumVar)
                {
                    if (puanVar)
                    {
                        baslikPuanYorum.Text = "Puan/yorumlarimi degistirecegim";
                        dugmeYorumGuncelle.Visible = true;
                    }
                    else
                    {
                        baslikPuanYorum.Text = "Puan verecegim";
                        dugmeYorumGuncelle.Visible = true;
                    }
                }
                else if (puanVar)
                {
                    baslikPuanYorum.Text = "Yorum yapacagim";
                    dugmeYorumGuncelle.Visible = true;
                }
                else
                {
                    baslikPuanYorum.Text = "Benim de diyeceklerim var";
                    dugmeYorumGonder.Visible = true;
                }
                //Hocanin verdigi dersleri yukleyip dropdown'a yukle
                dropHocaDersler.Items.Add(new ListItem("", "-1"));
                dropHocaDersler.Items.Add(new ListItem("Diger" , "0"));
                string[][] hocaDersler = HocaDersleriniDondur(session.HocaID);

                if(hocaDersler != null)
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
            }
            else
            {
                pnlUyeOl.Visible = true;
            }


        }
    }

    protected void dropHocaDersler_Secildi(object sender, EventArgs e)
    {
        dersIsim.Text = "";
        int selectedIndex = dropHocaDersler.SelectedIndex;

        if (selectedIndex > 0)
        {
            dropDersEkle.Visible = true;
            //Ders ismini getir
            if (selectedIndex > 1 && dersIsimleri != null)
            {
                if (dersIsimleri.Length > selectedIndex - 2)
                {
                    dersIsim.Text = dersIsimleri[selectedIndex - 2];
                }
            }
            else if (selectedIndex == 1) //Diger secildi
            {

            }
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
        if (hocaKullaniciDersler.Contains(dropHocaDersler.SelectedItem.Text))
        {
            dersIsim.Text = "Bu dersi zaten eklediniz";
            return;
        }
        else
        {
            hocaKullaniciDersler.Add(dropHocaDersler.SelectedItem.Text);

            repeaterDersler.DataSource = hocaKullaniciDersler;
            repeaterDersler.DataBind();
        }
    }

    protected void repeaterDersSil(object sender, RepeaterCommandEventArgs e)
    {
        //hocaKullaniciDersler.Remove(e.Item.ItemIndex);
        hocaKullaniciDersler.RemoveAt(e.Item.ItemIndex);

        repeaterDersler.DataSource = hocaKullaniciDersler;
        repeaterDersler.DataBind();
        //hocaDersler.RemoveAt
    }

    /// <summary>
    /// Hoca icin daha once girilen puani gunceller
    /// </summary>
    protected bool PuanGuncelle()
    {
        int[] puanlar = null;
        puanlar = new int[5];
        puanlar[0] = Puan1.CurrentRating;
        puanlar[1] = Puan2.CurrentRating;
        puanlar[2] = Puan3.CurrentRating;
        puanlar[3] = Puan4.CurrentRating;
        puanlar[4] = Puan5.CurrentRating;
        if (HocaPuanGuncelle(session.KullaniciID, session.HocaID, puanlar))
        {
            ltrDurum.Text = "Puaniniz guncellenmistir";
            return true;
        }
        else
        {
            ltrDurum.Text = "Puan guncellenirken bir hata olustu, lutfen tekrar deneyin";
            return false;
        }
    }

    /// <summary>
    /// Yorum ve puanlari kaydeder
    /// Hem yorum hem puan girmek zorunlu degil ancak bir puan girerse tum 5 puani da girmeli kullanici
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void PuanYorumKaydet(object sender, EventArgs e)
    {
        bool puanKaydet = false;
        bool yorumKaydet = false;
        if(Puan1.CurrentRating == 0 && Puan2.CurrentRating == 0 && Puan3.CurrentRating == 0 && Puan4.CurrentRating == 0 && Puan5.CurrentRating ==0) //Hic puan girmedi
        {
        }
        else if (!(Puan1.CurrentRating > 0 && Puan2.CurrentRating > 0 && Puan3.CurrentRating > 0 && Puan4.CurrentRating > 0 && Puan5.CurrentRating > 0))    //Puanlarin hepsini girmedi
        {
            ltrDurum.Text = "Puanlariniz eksik. Her kategoride puan girmelisiniz";
            return;
        }
        else
        {
            puanKaydet = true;
        }

        if (textOlumlu.Text.Length > 0 || textOlumsuz.Text.Length > 0 || textOzet.Text.Length > 0)
        {
            yorumKaydet = true;
        }

        if (puanKaydet || yorumKaydet)
        {
            //Yorum/puanlarin yonelik oldugu derslerin listesini olustur
            //hocaKullaniciDersler

            if (puanKaydet)
            {
                //Puanlari kaydet
                int[] puanlar = new int[5];
                puanlar[0] = Puan1.CurrentRating;
                puanlar[1] = Puan2.CurrentRating;
                puanlar[2] = Puan3.CurrentRating;
                puanlar[3] = Puan4.CurrentRating;
                puanlar[4] = Puan5.CurrentRating;
                if (HocaPuanKaydet(session.KullaniciID, session.HocaID, puanlar))
                {
                    ltrDurum.Text = "Puanlariniz basariyla kaydedildi!";
                }
                else
                {
                    ltrDurum.Text = "Puan kaydederken bir hata olustu, lutfen tekrar deneyiniz.";
                }
            }
            if (yorumKaydet)
            {
                string[] yorumlar = new string[3];
                yorumlar[0] = textOlumlu.Text;
                yorumlar[1] = textOlumsuz.Text;
                yorumlar[2] = textOzet.Text;
                int kullaniciPuanaraligi = Convert.ToInt32(dropGenelPuan.SelectedValue);
                if (!HocaYorumKaydet(session.KullaniciID, session.HocaID, yorumlar, kullaniciPuanaraligi))
                {
                    ltrDurum.Text = ltrDurum.Text + "<br />Yorum kaydederken bir hata olustu. Lutfen tekrar deneyiniz.";
                }
                else
                {
                    ltrDurum.Text = ltrDurum.Text + "<br />Yorumunuz basariyla kaydedildi!";
                }  
            }
        }
   
    }

    protected void PuanYorumGuncelle(object sender, EventArgs e)
    {
        int[] puanlar = null;
        puanlar = new int[5];
        puanlar[0] = Puan1.CurrentRating;
        puanlar[1] = Puan2.CurrentRating;
        puanlar[2] = Puan3.CurrentRating;
        puanlar[3] = Puan4.CurrentRating;
        puanlar[4] = Puan5.CurrentRating;
        if (HocaPuanGuncelle(session.KullaniciID, session.HocaID, puanlar))
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
        if (!HocaYorumGuncelle(session.KullaniciID, session.HocaID, yorumlar, kullaniciPuanaraligi))
        {
            ltrDurum.Text = ltrDurum.Text + "<br />Yorum guncellerken bir hata olustu, lutfen tekrar deneyin";
        }
        else
        {
            ltrDurum.Text = ltrDurum.Text + "<br />Yorumunuz guncellendi!";
        }
    }

    void KontroluSakla()
    {
        pnlPuanYorum.Visible = false;
        pnlUyeOl.Visible = false;
        baslikPuanYorum.Visible = false;
        dugmeYorumGonder.Visible = false;
        dugmeYorumGuncelle.Visible = false;
        dropDersEkle.Visible = false;
    }
}

/***
 * 
 * 
    /// <summary>
    /// Puan girildigi zaman cagirilir. Tum puanlar da girildiginde veritabanina kaydeder.
    /// </summary>
    bool puan5Girildi = false;
    static bool puanKaydiVar = false;
    protected void PuanGirildi(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        if (puanKaydiVar) //Puan daha once kaydedildi, puani guncellemeye yonlen
        {
            PuanGuncelle();
            return;
        }
        else if (puan5Girildi)    //Puan5 daha once girilmisti, hepsi tamam mi diye kontrol et
        {
            if (!(Puan1.CurrentRating > 0 && Puan2.CurrentRating > 0 && Puan3.CurrentRating > 0 && Puan4.CurrentRating > 0 && Puan5.CurrentRating > 0))
            {
                return;
            }
        }
        else if (!((sender as Control).ID.Contains("Puan5")))
        {
            return;
        }
        else  //Puan5 ilk defa girildi
        {
            puan5Girildi = true;
            //Butun puanlar doldurulmamis
            if (!(Puan1.CurrentRating > 0 && Puan2.CurrentRating > 0 && Puan3.CurrentRating > 0 && Puan4.CurrentRating > 0 && Puan5.CurrentRating > 0))
            {
                ltrPuanDurum.Text = "Puanlariniz eksik. Lutfen bes kategoride de puan veriniz";
                return;
            }
        }
        //Puanlari kaydet
        int[] puanlar = null;
        puanlar = new int[5];
        puanlar[0] = Puan1.CurrentRating;
        puanlar[1] = Puan2.CurrentRating;
        puanlar[2] = Puan3.CurrentRating;
        puanlar[3] = Puan4.CurrentRating;
        puanlar[4] = Puan5.CurrentRating;
        if (HocaPuanKaydet(KullaniciID, HocaID, puanlar))
        {
            ltrPuanDurum.Text = "Puanlariniz basariyla kaydedildi";
            puanKaydiVar = true;
        }
        else
        {
            ltrPuanDurum.Text = "Puan kaydederken bir hata olustu, lutfen tekrar deneyiniz.";
        }
    }
*/

