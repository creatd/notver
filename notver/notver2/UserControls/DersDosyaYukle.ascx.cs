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
using System.IO;

using Amazon.S3;
using Amazon.S3.Model;
using System.Collections.Specialized;
using System.Net;

public partial class UserControls_DersDosyaYukle : BaseUserControl
{
    public int SeciliDersID
    {
        get
        {
            object o = this.ViewState["_SeciliDersID"];
            if (o == null)
                return -1;
            else
                return (int)o;
        }

        set
        {
            this.ViewState["_SeciliDersID"] = value;
        }
    }

    public int SeciliDersOkulID
    {
        get
        {
            object o = this.ViewState["_SeciliDersOkulID"];
            if (o == null)
                return -1;
            else
                return (int)o;
        }

        set
        {
            this.ViewState["_SeciliDersOkulID"] = value;
        }
    }

    public int MevcutSayfa
    {
        get
        {
            object o = this.ViewState["_MevcutSayfa"];
            if (o == null)
                return 0;
            else
                return (int)o;
        }

        set
        {
            this.ViewState["_MevcutSayfa"] = value;
        }
    }

    public int SayfaBoyutu
    {
        get
        {
            object o = this.ViewState["_SayfaBoyutu"];
            if (o == null)
                return 0;
            else
                return (int)o;
        }

        set
        {
            this.ViewState["_SayfaBoyutu"] = value;
        }
    }

    protected void rptPager_Command(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "SayfayaGit")
        {
            MevcutSayfa = Convert.ToInt32(e.CommandArgument);
            DersleriDoldur();
        }
    }

    protected void SayfaBoyutuDegisti(object sender, EventArgs e)
    {
        SayfaBoyutu = Convert.ToInt32(dropSayfaBoyutu.SelectedValue);
        MevcutSayfa = 1;
        DersleriDoldur();
    }

    protected void OncekiSayfayaGit(object sender, EventArgs e)
    {
        MevcutSayfa -= 1;
        DersleriDoldur();
    }

    protected void SonrakiSayfayaGit(object sender, EventArgs e)
    {
        MevcutSayfa += 1;
        DersleriDoldur();
    }

    protected void rptPager_DataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton but = e.Item.FindControl("lnkSayfa") as LinkButton;
        if (but != null && Convert.ToInt32(but.CommandArgument) == MevcutSayfa)
        {
            but.Enabled = false;
        }
    }

    protected void KontroluSakla()
    {
        pnlHata.Visible = false;
        pnlDosyaYukle.Visible = false;
        pnlUyeOl.Visible = false;
    }

    protected void Page_Prerender(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                KontroluSakla();
                if (!session.IsLoggedIn)
                {
                    pnlUyeOl.Visible = true;
                }
                else
                {
                    pnlDosyaYukle.Visible = true;
                    //Sayfa İlk acildiginda, hangi sayfadan geldiysen onu aktar
                    if (SeciliDersID <= 0 && Query.GetInt("DersID") > 0)
                    {
                        SeciliDersID = Query.GetInt("DersID");
                        DersSec(null);
                    }
                    MevcutSayfa = 1;
                    SayfaBoyutu = Convert.ToInt32(dropSayfaBoyutu.SelectedValue);
                }
            }
        }
        catch (Exception ex)
        {
            KontroluSakla();
            pnlHata.Visible = true;
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
        }
    }

    protected void DersAra(object sender, EventArgs e)
    {
        DersleriDoldur();
    }

    protected void DersleriDoldur()
    {
        string dersKodu = txtDersKodu.Text.Trim();
        DataTable dt = Dersler.KodaGoreDersleriDondur(dersKodu);
        if (dt != null && dt.Rows.Count > 0)
        {
            pnlDersAramaSonuclari.Visible = true;
            pnlDersAramaSonuclariBos.Visible = false;

            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = dt.DefaultView;
            pds.AllowPaging = true;

            if (SayfaBoyutu == 0)   //Hepsini goster
            {
                pds.PageSize = dt.Rows.Count;
            }
            else
            {
                pds.PageSize = SayfaBoyutu;
            }

            pds.CurrentPageIndex = MevcutSayfa - 1;

            lnkOnceki.Enabled = !pds.IsFirstPage;
            lnkSonraki.Enabled = !pds.IsLastPage;

            ArrayList arrList = new ArrayList(pds.PageCount);
            for (int i = 0; i < pds.PageCount; i++)
            {
                arrList.Add((i + 1).ToString());
            }
            rptPager.DataSource = arrList;
            rptPager.DataBind();

            gridDersAramaSonuclari.DataSource = pds;
            gridDersAramaSonuclari.DataBind();
        }
        else
        {
            pnlDersAramaSonuclari.Visible = false;
            pnlDersAramaSonuclariBos.Visible = true;
        }
    }

    protected void DersSec(string dersIsim)
    {
        drpDersHocalar.Items.Clear();
        drpDersHocalar.Items.Add(new ListItem("-", "-1"));
        lblSecilenDers.Text = "";
        if (SeciliDersID > 0)
        {
            if (string.IsNullOrEmpty(dersIsim))
            {
                DataTable dtDers = Dersler.DersProfilDondur(SeciliDersID);
                if (dtDers != null)
                {
                    lblSecilenDers.Text = dtDers.Rows[0]["KOD"].ToString() + " (" + dtDers.Rows[0]["OKUL_ISIM"].ToString() + ")";
                    SeciliDersOkulID = Convert.ToInt32(dtDers.Rows[0]["OKUL_ID"]);
                }
            }
            else
            {
                lblSecilenDers.Text = dersIsim;
            }
            //Bu dersi veren hocalari doldur
            DataTable dtHocalar = Dersler.DersiVerenHocalariDondur(SeciliDersID);
            if (dtHocalar != null)
            {
                foreach (DataRow dr in dtHocalar.Rows)
                {
                    drpDersHocalar.Items.Add(new ListItem(dr["HOCA_ISIM"].ToString() , dr["HOCA_ID"].ToString()));
                }
            }
        }
    }

    protected void ItemCommand(object sender, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DersSec")
        {
            SeciliDersID = Convert.ToInt32(e.CommandArgument);
            SeciliDersOkulID = Convert.ToInt32(((System.Web.UI.WebControls.TableRow)(e.Item)).Cells[4].Text);
            string seciliDersIsim = ((System.Web.UI.WebControls.TableRow)(e.Item)).Cells[0].Text + " (" + ((System.Web.UI.WebControls.TableRow)(e.Item)).Cells[2].Text + ")";
            DersSec(seciliDersIsim);
        }
    }

    protected void DosyaYukle(object sender, EventArgs e)
    {
        lblYuklemeDurum.Text = "";
        if (SeciliDersID <= 0 || string.IsNullOrEmpty(lblSecilenDers.Text))
        {
            lblYuklemeDurum.Text = "Ders seçmeniz gerekmektedir";
            return;
        }
        if (fileUpload.HasFile)
        {
            try
            {
                /*//Ders icin klasorlerin oldugunu kontrol et, yoksa yarat
                DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/Dosyalar/Dersler/" + SeciliDersID.ToString().Trim()));
                if (!dir.Exists)
                {
                    dir.Create();
                }
                dir = new DirectoryInfo(Server.MapPath("~/Dosyalar/Dersler/" + SeciliDersID.ToString().Trim() + "/" + rbDosyaTipleri.SelectedValue.ToString().Trim()));
                if (!dir.Exists)
                {
                    dir.Create();
                }
                fileUpload.SaveAs(dosyaUzunAdres);*/

                int dosyaBoyut = fileUpload.PostedFile.ContentLength;
                if (dosyaBoyut <= 0)
                {
                    lblYuklemeDurum.Text = "Sectiğiniz dosyanın içeriğinde bir sorun var, lütfen tekrar deneyin";
                }
                else if (dosyaBoyut > 5 * 1024 * 1024)
                {
                    lblYuklemeDurum.Text = "5 GB'tan büyük bir dosya yükleyemezsiniz, lütfen daha küçük bir dosya seçin";
                }

                string dosyaAdres = Path.GetFileName(fileUpload.FileName);
                string dosyaIsim = txtDosyaIsim.Text;
                string dosyaIsimFinal;

                if (string.IsNullOrEmpty(dosyaIsim))
                {
                    dosyaIsimFinal = Util.StringToEnglish_RemoveMarks(dosyaAdres,true);
                }
                else
                {
                    string dosya_uzanti = dosyaAdres.Substring(dosyaAdres.LastIndexOf("."));
                    dosyaIsimFinal = Util.StringToEnglish_RemoveMarks(dosyaIsim + dosya_uzanti,true);
                }

                //Amazon'a yuklemeden once bu isimde bir dosya var mi kontrol et, yoksa uzerine yaziyo
                if( Dersler.DersDosyaIsmiVarMi(SeciliDersID , (Enums.DosyaKategoriTipi)Convert.ToInt32(rbDosyaTipleri.SelectedValue),dosyaIsimFinal)) 
                {
                    lblYuklemeDurum.Text = "Aynı ders için bu isimde bir dosya daha once yüklenmis. Lütfen başka bir isim seçip tekrar deneyin";
                    return;
                }

                //Amazon yukleme                
                string klasorIsim = SeciliDersOkulID.ToString().Trim() + "/" + SeciliDersID.ToString().Trim() + "/" + rbDosyaTipleri.SelectedValue.ToString().Trim() + "/";
                
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
                    if(string.IsNullOrEmpty(dosyaIsim))
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

                if (!Dersler.DersDosyasiniKaydet(SeciliDersID, Convert.ToInt32(drpDersHocalar.SelectedValue),
                    (Enums.DosyaKategoriTipi)Convert.ToInt32(rbDosyaTipleri.SelectedValue), dosyaIsimFinal,
                    klasorIsim + dosyaIsimFinal, session.KullaniciID, txtDosyaAciklama.Text, session.KullaniciOnayPuani,
                    dosyaBoyut))
                {
                    //Amazon'a yukledik, veritabanina yazamadik
                    Mesajlar.AdmineHataMesajiGonder(((System.Web.UI.Page)(sender)).Request.Url.ToString(), 
                        "Dosyayi amazon'a yukledik, veritabanina yazamadik. Klasor isim : " + klasorIsim + " dosya isim : " + dosyaIsimFinal,
                        session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
                }

                lblYuklemeDurum.Text = "Yüklendi! Teşekkürler :)";
                ltrScript.Text = "<script type='text/javascript'>setTimeout('parent.$.fn.colorbox.close()',1500);</script>";
            }
            catch (Exception ex)
            {
                Mesajlar.AdmineHataMesajiGonder(((System.Web.UI.Page)(sender)).Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
                lblYuklemeDurum.Text = "Bir hata oluştu, lütfen tekrar deneyin";
            }
        }
        else
        {
            lblYuklemeDurum.Text = "Dosya seçmeyi unuttunuz";
        }
    }
}
