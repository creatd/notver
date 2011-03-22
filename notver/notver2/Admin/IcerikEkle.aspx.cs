using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Collections.Generic;
using Amazon.S3.Model;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using Amazon.S3;

public partial class Admin_IcerikEkle : BasePage
{
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

        try
        {
            if (!Page.IsPostBack)
            {
                /*hocaOkullarObj = hocaOkullar;   //Null donmesin, yeni liste donsun diye
                hocaOkullarObj.Clear();
                hocaOkullar = hocaOkullarObj;*/

                drpHocaOkullar.Items.Clear();
                drpDersOkullar.Items.Clear();
                drpDosyaOkullar.Items.Clear();
                drpDosyaOkullar.Items.Add(new ListItem("-", "-1")); //Okul secilir secilmez dersler dolduruldugu icin - ile basliyoruz
                foreach (DataRow dr in session.dtOkullar.Rows)
                {
                    drpHocaOkullar.Items.Add(new ListItem(dr["ISIM"].ToString(), dr["OKUL_ID"].ToString()));
                    drpDersOkullar.Items.Add(new ListItem(dr["ISIM"].ToString(), dr["OKUL_ID"].ToString()));
                    drpDosyaOkullar.Items.Add(new ListItem(dr["ISIM"].ToString(), dr["OKUL_ID"].ToString()));
                }

                drpDosyaDurum.Items.Clear();
                foreach (string durum in Enum.GetNames(typeof(Enums.DosyaDurumu)))
                {
                    drpDosyaDurum.Items.Add(new ListItem(durum, durum));
                }
                drpDosyaDurum.SelectedValue = Enum.GetName(typeof(Enums.DosyaDurumu), Enums.DosyaDurumu.Onaylanmis);

                drpDosyaTipler.Items.Clear();
                foreach (string dosyaTipi in Enum.GetNames(typeof(Enums.DosyaKategoriTipi)))
                {
                    drpDosyaTipler.Items.Add(new ListItem(dosyaTipi, ((int)((Enums.DosyaKategoriTipi)(Enum.Parse(typeof(Enums.DosyaKategoriTipi), dosyaTipi)))).ToString()));
                }
            }
        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(((System.Web.UI.Page)(sender)).Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
        }
        
    }

    protected void OkulEkle(object sender, EventArgs e)
    {
        lblDurumOkulEkle.Text = "";

        //Uzunluk ve null kontrollerini js ile yap

        int kurulusTarihi = -1;
        if (Util.GecerliSayi(txtOkulKurulusTarihi.Text))
        {
            kurulusTarihi = Convert.ToInt32(txtOkulKurulusTarihi.Text);
        }
        int ogrenciSayisi = -1;
        if (Util.GecerliSayi(txtOkulOgrenciSayisi.Text))
        {
            ogrenciSayisi = Convert.ToInt32(txtOkulOgrenciSayisi.Text);
        }
        int akademikSayisi = -1;
        if (Util.GecerliSayi(txtOkulAkademikSayisi.Text))
        {
            akademikSayisi = Convert.ToInt32(txtOkulAkademikSayisi.Text);
        }

        //TODO: Gerekli kontrolleri yap burda
        if (Okullar.OkulEkle(Convert.ToBoolean(Convert.ToInt32(drpOkulEkleActive.SelectedValue)),
            txtOkulIsim.Text, txtOkulAdres.Text, kurulusTarihi,ogrenciSayisi, akademikSayisi,
            txtOkulWebAdresi.Text))
        {
            OkulSifirla();
            lblDurumOkulEkle.Text = "Okul başarıyla eklendi";
            if (session == null)
            {
                session = new Session();
            }
            session.dtOkullar = null;   //Yeniden yuklensin diye
            RefreshPage();
        }
        else
        {
            lblDurumOkulEkle.Text = "Bir hata oldu";
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

        int yorumSayisi = -1;
        if (Util.GecerliSayi(txtHocaYorumSayisi.Text))
        {
            yorumSayisi = Convert.ToInt32(txtHocaYorumSayisi.Text);
        }
        if (Hocalar.HocaEkle(Convert.ToBoolean(Convert.ToInt32(drpHocaEkleActive.SelectedValue)),
            txtHocaIsim.Text, txtHocaUnvan.Text, yorumSayisi,
            hocaOkullarIDler, okulBaslangicYillari, okulBitisYillari, hocaDerslerIDler))
        {
            lblDurumHocaEkle.Text = "Hoca başarıyla eklendi";
            HocaSifirla();
        }
        else
        {
            lblDurumHocaEkle.Text = "Hoca eklerken bir hata oldu!";
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
            lblHocaOkulEkleDurum.Text = "Bitiş yılı başlangıç yılından önce olamaz";
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
            lblDurumDersEkle.Text = "Ders başarıyla eklendi";
            DersSifirla();
        }
        else
        {
            lblDurumDersEkle.Text = "Ders eklenirken bir hata oldu!";
        }
    }

    protected void drpDosyaOkullar_OkulSecildi(object sender, EventArgs e)
    {
        lblDurumDersEkle.Text = "";
        //Secili okula gore dersleri doldur
        int okulID = Convert.ToInt32(drpDosyaOkullar.SelectedValue);
        if (okulID < 0) //Baslangic secenegi
            return;
        DataTable dtDersler = Dersler.OkuldakiDersleriDondur(okulID);
        if (dtDersler != null)
        {
            drpDosyaDersler.Items.Clear();
            foreach (DataRow dr in dtDersler.Rows)
            {
                drpDosyaDersler.Items.Add(new ListItem(dr["KOD"].ToString(), dr["DERS_ID"].ToString()));
            }
        }
        else
        {
            lblDurumDersEkle.Text = "Okuldaki dersleri döndürürken bir hata oldu!";
        }
        //Secili okula gore hocalari doldur
        DataTable dtHocalar = Hocalar.OkuldakiHocalariDondur(okulID);
        if (dtHocalar != null)
        {
            drpDosyaHocalar.Items.Clear();
            drpDosyaHocalar.Items.Add(new ListItem("-", "-1"));
            foreach (DataRow dr in dtHocalar.Rows)
            {
                drpDosyaHocalar.Items.Add(new ListItem(dr["HOCA_ISIM"].ToString(), dr["HOCA_ID"].ToString()));
            }
        }
        else
        {
            lblDurumDersEkle.Text = "Okuldaki hocaları döndürürken bir hata oldu!";
        }
    }

    protected void DersDosyaYukle(object sender, EventArgs e)
    {
        if (session == null)
        {
            session = new Session();
        }

        lblDurumDosyaYukle.Text = "";
        int seciliDersID = Convert.ToInt32(drpDosyaDersler.SelectedValue);
        if (seciliDersID <= 0)
        {
            lblDurumDosyaYukle.Text = "Ders seçmen gerekli";
            return;
        }
        if (fileUpload.HasFile)
        {
            try
            {
                int dosyaBoyut = fileUpload.PostedFile.ContentLength;
                if (dosyaBoyut <= 0)
                {
                    lblDurumDosyaYukle.Text = "Seçtiğin dosyanın içeriğinde bir sorun var, tekrar dene";
                }
                else if (dosyaBoyut > 5 * 1024 * 1024)
                {
                    lblDurumDosyaYukle.Text = "YUH! 5 GB'tan büyük bir dosya yükleyemezsin, daha küçük bir dosya seç";
                }

                string dosyaAdres = Path.GetFileName(fileUpload.FileName);
                string dosyaIsim = txtDosyaIsim.Text;
                string dosyaIsimFinal;

                if (string.IsNullOrEmpty(dosyaIsim))
                {
                    dosyaIsimFinal = Util.StringToEnglish_RemoveMarks(dosyaAdres, true);
                }
                else
                {
                    string dosya_uzanti = dosyaAdres.Substring(dosyaAdres.LastIndexOf("."));
                    dosyaIsimFinal = Util.StringToEnglish_RemoveMarks(dosyaIsim + dosya_uzanti, true);
                }

                //Amazon'a yuklemeden once bu isimde bir dosya var mi kontrol et, yoksa uzerine yaziyo
                if (Dersler.DersDosyaIsmiVarMi(seciliDersID, (Enums.DosyaKategoriTipi)Convert.ToInt32(drpDosyaTipler.SelectedValue), dosyaIsimFinal))
                {
                    lblDurumDosyaYukle.Text = "Aynı ders için bu isimde bir dosya daha önce yüklenmiş. Lütfen başka bir isim seçip tekrar deneyin";
                    return;
                }

                //Amazon yukleme                
                string klasorIsim = drpDosyaOkullar.SelectedValue.ToString().Trim() + "/" + seciliDersID.ToString().Trim() + "/" + 
                    drpDosyaTipler.SelectedValue.ToString().Trim() + "/";

                NameValueCollection appConfig = ConfigurationManager.AppSettings;

                string accessKeyID = appConfig["AWSAccessKey"];
                string secretAccessKeyID = appConfig["AWSSecretKey"];
                string bucketName = appConfig["AWSBucketName"];

                using (AmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(accessKeyID, secretAccessKeyID))
                {
                    PutObjectRequest titledRequest = new PutObjectRequest();
                    titledRequest.WithBucketName(bucketName)
                        .WithGenerateChecksum(true)
                        .WithInputStream(fileUpload.FileContent);

                    string md5 = titledRequest.MD5Digest;
                    if (string.IsNullOrEmpty(dosyaIsim))
                    {
                        titledRequest.WithKey(klasorIsim + dosyaIsimFinal);
                    }
                    else
                    {
                        string dosya_uzanti = dosyaAdres.Substring(dosyaAdres.LastIndexOf("."));
                        titledRequest.WithKey(klasorIsim + dosyaIsimFinal);
                    }

                    using (PutObjectResponse response = client.PutObject(titledRequest))
                    {
                        WebHeaderCollection headers = response.Headers;
                        foreach (string key in headers.Keys)
                        {
                            Console.WriteLine("Response Header: {0}, Value: {1}", key, headers.Get(key));
                        }
                    }
                }

                int hocaID = -1;
                if (Util.GecerliSayi(drpDosyaHocalar.SelectedValue))
                {
                    hocaID = Convert.ToInt32(drpDosyaHocalar.SelectedValue);
                }
                Dersler.DersDosyasiniKaydet(seciliDersID, hocaID,
                    (Enums.DosyaKategoriTipi)Convert.ToInt32(drpDosyaTipler.SelectedValue), dosyaIsimFinal,
                    klasorIsim + dosyaIsimFinal, session.KullaniciID, txtDosyaAciklama.Text, session.KullaniciOnayPuani,
                    dosyaBoyut);

                lblDurumDosyaYukle.Text = "Yüklendi! Teşekkürler :)";
                DersDosyaSifirla();
            }
            catch (Exception ex)
            {
                Mesajlar.AdmineHataMesajiGonder(Page.Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
                lblDurumDosyaYukle.Text = "Bir hata oluştu, lütfen tekrar deneyin";
            }
        }
        else
        {
            lblDurumDosyaYukle.Text = "Dosya seçmeyi unuttun";
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

    protected void DersDosyaSifirla()
    {
        drpDosyaDersler.SelectedIndex = 0;
        if(drpDosyaHocalar.Items.Count > 0)
            drpDosyaHocalar.SelectedIndex = 0;
        //Universite dropdown'ina dokunma
        drpDosyaDurum.SelectedValue = Enum.GetName(typeof(Enums.DosyaDurumu), Enums.DosyaDurumu.Onaylanmis);

        drpDosyaTipler.SelectedIndex = 0;
        txtDosyaAciklama.Text = "";
        txtDosyaIsim.Text = "";
    }
}
