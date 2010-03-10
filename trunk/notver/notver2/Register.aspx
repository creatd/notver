<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register"
    MasterPageFile="~/Masters/Giris.master" %>

<asp:Content ContentPlaceHolderID="content" runat="server">
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" AnswerLabelText="Gizli Cevap:"
        AnswerRequiredErrorMessage="Gizli cevap belirtmeniz gereklidir" CancelButtonText="İptal"
        CompleteSuccessText="Hesabınız oluşturulmuştur. Eposta adresinize onay mesajı gönderilmiştir"
        ConfirmPasswordCompareErrorMessage="Girdiğiniz iki şifre de aynı olmalıdır" ConfirmPasswordLabelText="Şifre (tekrar):"
        ConfirmPasswordRequiredErrorMessage="Şifre tekrar girilmelidir" ContinueDestinationPageUrl="Default.aspx"
        ContinueButtonText="Devam" CreateUserButtonText="Beni de üye yap!" DuplicateEmailErrorMessage="Girmiş olduğunuz e-posta adresi sistemde kayıtlıdır"
        DuplicateUserNameErrorMessage="Seçtiğiniz kullanıcı adı sistemde kayıtlıdır"
        EmailRegularExpressionErrorMessage="E-posta adresiniz geçersizdir, lütfen geçerli bir e-posta adresi giriniz"
        EmailRequiredErrorMessage="E-posta adresi girmeniz gereklidir" FinishCompleteButtonText="Tamaml"
        FinishPreviousButtonText="Geri" PasswordLabelText="Şifre:" PasswordRegularExpressionErrorMessage="Lütfen farklı bir şifre giriniz"
        PasswordRequiredErrorMessage="Şifre girmeniz gereklidir" QuestionLabelText="Güvenlik sorusu:"
        QuestionRequiredErrorMessage="Güvenlik sorusu girmeniz gereklidir" StartNextButtonText="İleri"
        StepPreviousButtonText="Geri" UnknownErrorMessage="Bilinmeyen bir hata oluştu, lütfen tekrar deneyiniz"
        UserNameLabelText="Kullanıcı adı:" UserNameRequiredErrorMessage="Kullanıcı adı girmeniz gereklidir" OnCreatedUser="KullaniciOlustur"> 
        <WizardSteps>
            <asp:CreateUserWizardStep runat="server" ID="CreateUserWizardStep1">
                <ContentTemplate>
                    <table border="0">
                        <tr>
                            <td align="center" colspan="2">
                                Aramıza katılmak için aşağıdaki 6 kutuyu doldurmanız yeterli!
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Kullanıcı 
                                adı:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                    ErrorMessage="Kullanıcı adı girmeniz gereklidir" ToolTip="Kullanıcı adı girmeniz gereklidir"
                                    ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Şifre:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                    ErrorMessage="Şifre girmeniz gereklidir" ToolTip="Şifre girmeniz gereklidir"
                                    ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Şifre (tekrar):</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                    ErrorMessage="Şifre tekrar girilmelidir" ToolTip="Şifre tekrar girilmelidir"
                                    ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="IsimLabel" runat="server" AssociatedControlID="Isim">Isminiz (Ad Soyad):</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Isim" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="IsimRequired" runat="server" ControlToValidate="Isim" ErrorMessage="Isim girilmelidir" ToolTip="Isim girilmelidir" 
                                ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>                                
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="OkulLabel" runat="server" AssociatedControlID="Okullar">Okulunuz (opsiyonel):</asp:Label>
                                <br />
                                <asp:Label ID="OkulNot" runat="server">(Okulunuza ait eposta adresinizle kayit olarak okulunuzu onaylatabilirsiniz)</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="Okullar" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-posta adresi:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                                    ErrorMessage="E-posta adresi girmeniz gereklidir" ToolTip="E-posta adresi girmeniz gereklidir"
                                    ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question">Güvenlik 
                                sorusu:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Question" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" ControlToValidate="Question"
                                    ErrorMessage="Güvenlik sorusu girmeniz gereklidir" ToolTip="Güvenlik sorusu girmeniz gereklidir"
                                    ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">Gizli 
                                Cevap:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Answer" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer"
                                    ErrorMessage="Gizli cevap belirtmeniz gereklidir" ToolTip="Gizli cevap belirtmeniz gereklidir"
                                    ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                                    ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="Girdiğiniz iki şifre de aynı olmalıdır"
                                    ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="color: Red;">
                                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep runat="server">
                <ContentTemplate>
                    <table border="0">
                        <tr>
                            <td align="center" colspan="2">
                                Hesabınız oluşturulmuştur. Eposta adresinize onay mesajı gönderilmiştir. Üye ayrıcalıklarından yararlanabilmek için bu onay
                                mesajındaki linke tıklayarak üyeliğinizi onaylamanız gereklidir.
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="ContinueButton" runat="server" CausesValidation="False" CommandName="Continue"
                                    Text="Devam" ValidationGroup="CreateUserWizard1" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
</asp:Content>
