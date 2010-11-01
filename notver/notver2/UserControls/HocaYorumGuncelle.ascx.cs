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

public partial class UserControls_HocaYorumGuncelle : BaseUserControl
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

    public int hocaYorumID
    {
        get
        {
            var obj = ViewState["hocaYorumID"];
            if(obj != null)
                return (int)obj;
            return  -1;
        }
        set
        {
            ViewState["hocaYorumID"] = value;
        }

    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        try
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

                if (!Page.IsPostBack)
                {
                    //Kullanicinin daha once yaptigi yorumu yukle
                    List<object> listEskiYorum = Hocalar.KullaniciHocaYorumunuDondur(session.KullaniciID, Query.GetInt("HocaID"));
                    if (listEskiYorum != null)
                    {
                        //yorumID - yorum - kullanici puan araligi - puan1 - puan2 - puan3 - puan4 - puan5 - { (Ders ID - Ders Kodu - OkulIsmi)| (-1 - Ders Ismi) }(*)
                        hocaYorumID = (int)listEskiYorum[0];
                        textYorum.Text = (string)listEskiYorum[1];
                        dropGenelPuan.SelectedValue = Convert.ToString(listEskiYorum[2]);
                        Puan1.CurrentRating = (int)listEskiYorum[3];
                        Puan2.CurrentRating = (int)listEskiYorum[4];
                        Puan3.CurrentRating = (int)listEskiYorum[5];
                        Puan4.CurrentRating = (int)listEskiYorum[6];
                        Puan5.CurrentRating = (int)listEskiYorum[7];
                        hocaKullaniciDerslerObj = hocaKullaniciDersler;
                        hocaKullaniciDerslerIDlerObj = hocaKullaniciDerslerIDler;
                        for (int i = 8; i < listEskiYorum.Count - 1; )
                        {
                            if ((int)listEskiYorum[i] == -1)
                            {
                                hocaKullaniciDerslerObj.Add((string)listEskiYorum[i + 1]);
                                //Her "Diger" secenegi icin ID'lere de ekleyelim ki Dersler ve DerslerIDler'in index'leri ayni olsun
                                hocaKullaniciDerslerIDlerObj.Add(-2);   //-2 degeri Hocalar sinifinda da kullaniliyor
                                i += 2;
                            }
                            else
                            {
                                string okulIsmi = (string)listEskiYorum[i + 2];
                                if (okulIsmi.Length > 20)
                                {
                                    hocaKullaniciDerslerObj.Add((string)listEskiYorum[i + 1] + " (" + okulIsmi.Substring(0, 18) + "..)");
                                }
                                else
                                {
                                    hocaKullaniciDerslerObj.Add((string)listEskiYorum[i + 1] + " (" + okulIsmi.Substring(0, 18) + "..)");
                                    dropHocaDersler.Items.Add((string)listEskiYorum[i + 1] + " (" + okulIsmi.Substring(0, 20) + ")");
                                }
                                hocaKullaniciDerslerIDlerObj.Add((int)listEskiYorum[i]);
                                i += 3;
                            }
                        }
                        hocaKullaniciDersler = hocaKullaniciDerslerObj;
                        hocaKullaniciDerslerIDler = hocaKullaniciDerslerIDlerObj;

                        repeaterDersler.DataSource = hocaKullaniciDersler;
                        repeaterDersler.DataBind();
                    }
                    else
                    {
                        //TODO: eski yorumu yukleyemedik, pop-up'i kapatmaliyiz ya da uyari mesaji vermeliyiz
                    }
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
                pnlUyeOl.Visible = true;
            }
        }
        catch (Exception ex)
        {
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
    /// Yorum ve puanlari gunceller
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void PuanYorumGuncelle(object sender, EventArgs e)
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
        if (Hocalar.HocaYorumPuanGuncelle(hocaYorumID, puanlar, textYorum.Text,
            kullaniciPuanAraligi, (List<int>)hocaKullaniciDerslerIDler, (List<string>)hocaKullaniciDersler))
        {
            ltrDurum.Text = "Puan ve yorumlariniz basariyla guncellendi!";
            ltrScript.Text = "<script type='text/javascript'>setTimeout('self.parent.tb_remove()',1500);</script>";
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
        dropDersEkle.Visible = false;
        txtDersKodDiger.Visible = false;
    }
}
