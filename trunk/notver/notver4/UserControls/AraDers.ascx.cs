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

using System.Text;

public partial class UserControls_AraDers : BaseUserControl
{
    protected void Ara(object sender, EventArgs e)
    {
        try
        {

            string searchParams = dersIsmi.Text.ToString().Trim();
            if (string.IsNullOrEmpty(searchParams))
            {
                return;
            }
            else if (searchParams.StartsWith("Hoca ismini"))
            {
                return;
            }
            string aramaKriterleri = dersKoduAyir(searchParams);

            //Strip whitespaces and replace them with +
            string[] words = aramaKriterleri.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length < 1)   //Hic kelime yok arama parametrelerinde (ornegin bosluk girilmis)
                return;
            StringBuilder sb = new StringBuilder();
            foreach (string word in words)
            {
                sb.Append(word + "+");
            }

            //Sonda gereksiz bir + kaldi ama onemli degil
            Response.Redirect(Page.ResolveUrl("~/SearchResults.aspx") + "?SearchType=2&SearchParams=" + sb.ToString());
        }
        catch (Exception ex)
        {
            Mesajlar.AdmineHataMesajiGonder(Request.Url.ToString(), ex.Message, session.KullaniciID, Enums.SistemHataSeviyesi.Orta);
        }
    }

    //gelen string icinde ders kodunu arar
    protected string dersKodu(string searchParams)
    {
        int counter = 0;
        int ascii;
        string dersKodu = "";

        CharEnumerator charEnum = searchParams.GetEnumerator();

        while (charEnum.MoveNext())
        {
            ascii = Convert.ToInt32(searchParams[counter]);

            if ((ascii >= 48) && (ascii <= 57))
            {
                //ders kodunun 3 haneli oldugu varsayimi var burda
                //arama parametreleri arasinda ilk rakami gordugunde sonraki iki karakterle beraber
                //return eder
                dersKodu += Convert.ToString(searchParams[counter]);
                dersKodu += Convert.ToString(searchParams[counter + 1]);
                dersKodu += Convert.ToString(searchParams[counter + 2]);
                break;
            }

            counter++;
        }

        return dersKodu;
    }

    protected string dersKoduAyir(string searchParams)
    {
        int ascii;
        int counter = 0;
        int length = searchParams.Length;
        string dersKodu = "";
        string dersIsmi = "";
        string sonuc = searchParams;

        CharEnumerator charEnum = searchParams.GetEnumerator();

        while (charEnum.MoveNext())
        {
            ascii = Convert.ToInt32(searchParams[counter]);

            if ((ascii >= 48) && (ascii <= 57))
            {
                for (int i = 0; i < length - counter; i++)
                {
                    dersKodu += Convert.ToString(searchParams[counter + i]);
                }
                break;
            }
            else
            {
                dersIsmi += Convert.ToString(searchParams[counter]);
            }
            counter++;
        }

        sonuc = dersIsmi + " " + dersKodu;

        return sonuc;
    }
}
