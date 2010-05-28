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
            if (IsLoggedIn())
            {
                GoToDefaultPage();
            }
            if (dtOkullar == null)
            {
                dtOkullar = OkullariDondur();
            }

            DropDownList Okullar = (DropDownList)CreateUserWizardStep1.ContentTemplateContainer.FindControl("Okullar");
            Okullar.Items.Add(new ListItem("-", "-1"));
            foreach (DataRow dr in dtOkullar.Rows)
            {
                Okullar.Items.Add(new ListItem(dr["ISIM"].ToString(), dr["OKUL_ID"].ToString()));
            }
        }
    }

    protected void KullaniciOlustur(object sender, EventArgs e)
    {
        string userName = CreateUserWizard1.UserName;
        string email = CreateUserWizard1.Email;
        string question = CreateUserWizard1.Question;
        string answer = CreateUserWizard1.Answer;
        int okulID = Convert.ToInt32(((DropDownList) CreateUserWizardStep1.ContentTemplateContainer.FindControl("Okullar") ).SelectedValue);
        string isim = ((TextBox)CreateUserWizardStep1.ContentTemplateContainer.FindControl("Isim")).Text;
        KullaniciID = UyeOlustur(Membership.GetUser(userName).ProviderUserKey, userName, email, okulID, isim);
    }
}
