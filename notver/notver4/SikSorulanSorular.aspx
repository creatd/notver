<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SikSorulanSorular.aspx.cs" 
Inherits="SikSorulanSorular" MasterPageFile="~/Masters/Giris.master" %>

<asp:Content runat="server" ContentPlaceHolderID="content">
<div style="padding-top:30px;" id="SSS">
    <h2>Sık Sorulan Sorular</h2>
    
    <br />
    <p style="font-weight:bold;">Sistemdeki yorumları okuyabilmem için üye olmam gerekli mi ?</p>
    <p>Hayır, üye olmadan da sistemdeki ders, hoca, okul sayfalarını inceleyebilirsin. Fakat hocalar, 
    dersler ve okullar için yorum yapabilmek için üye olmalısın. </p>
    
    <br />
    <p style="font-weight:bold;">Sistemdeki ders arşivlerini indirip kullanmam için üye olmam gerekli mi ?</p>
    <p>Hayır, üye olmadan da derslerin arşivlerine erişebilirsin. 
    <br />
    Üye olup dosya paylaşarak sistemin gelişmesine ve canlı kalmasına katkıda bulunabilirsin. 
    Yüklediğin her dosya için 5 puan kazanırsın.</p>
    
    <br />
    <p style="font-weight:bold;">Puan nedir, ne işe yarar ?</p>
    <p>Her NotVerin kullanıcısının bir puanı vardır. Dosyaların yorumların onaylandıkça puan kazanır, 
    uygunsuz bulunursa da puan kaybedersin. Puanı belli bir seviyeye erişen kullanıcıların yaptıkları 
    yorumlar anında sistemde yayınlanır (onay beklemez). </p>
    
    <br />
    <p style="font-weight:bold;">Yorum yaptım fakat yaptığım yorumu ilgili sayfada göremiyorum. Yorumumu çöpe mi attınız ?</p>
    <p>Yeni üyelerin yaptıkları yorumlar onayımızdan geçtikten sonra yayınlanmaktadır. Yorumların onaylandıkça 
    puanın yükselir ve kısa bir sürede yorumların anında sistemde yayınlanmaya başlar (onay beklemeden). Tüm 
    yorumlarının durumunu “Hesabım” sayfasından kontrol edebilirsin (Üst menüde bulunan “Hesabım” tuşundan ulaşabilirsin)</p>
    
    <br />
    <p style="font-weight:bold;">Daha önce yayında olan yorumumu bulamıyorum, nerede gördünüz mü?</p>
    <p>NotVerin yayın kurallarıyla bağdaşmayan (örn. hakaret içeren) yorumlar tarafımızdan silinmektedir. 
    Her yorumun yanında yer alan şikayet tuşu aracılığıyla diğer kullanıcılarımız da sistemden silinmesi 
    gerektiğini düşündükleri yorumları bize iletmektedir. Tüm yorumlarının durumunu “Hesabım” sayfasından 
    kontrol edebilirsin (Üst menüde bulunan “Hesabım” tuşundan ulaşabilirsin)</p>
    
    <br />
    <p style="font-weight:bold;">Daha önce yorumlarım onay beklemeden yayınlanırdı; 
    fakat şimdi yaptığım yorum anında yayınlanmadı. Böyle mi olduk şimdi ?</p>
    <p>Onaylanan dosya ve yorumlarınla puan kazandığın gibi uygunsuz bulunduğu için kaldırılan dosya ve yorumlarından 
    dolayı da puan kaybedersin. Yorum gönderdiğindeki puanın düşükse yorum ve dosyaların onayımızdan geçtikten 
    sonra yayınlanacaktır. Tüm yorumlarının durumunu 
    “Hesabım” sayfasından kontrol edebilirsiz (Üst menüde bulunan “Hesabım” tuşundan ulaşabilirsin)</p>
    
    <br />
    <p style="font-weight:bold;">Sistemdeki bir hocaya daha önce yorum yapmıştım, şimdi tekrar yorum yapmak istiyorum fakat, 
    sadece bir önceki yorumumu güncelleyebiliyorum. Neden ?</p>
    <p>Sistemdeki her hoca için sadece bir yorum hakkın bulunmaktadır. Bu kısıtla amacımız bilgi 
    karmaşıklığını olabildikçe engellemek, hoca hakkındaki yorumları merak eden kullanıcılarımıza 
    az ama öz bilgi sunmaktır.</p>
    
    <br />
    <p style="font-weight:bold;">Eğer her hoca için sadece bir yorum hakkım varsa, yorumumu silersem veya 
    yorumum silinirse, aynı hocaya başka yorum yapamayacak mıyım?</p>
    <p>Yapabilirsin, her hoca için bir tane yorum hakkı görüntülenen yorumların için geçerlidir. Eğer yorumunu silersen 
    veya yorumun bizim tarafımızdan silinirse, aynı hoca için tekrar yorum yapma hakkın doğacaktır.</p>
    
    <br />
    <p style="font-weight:bold;">Sistemdeki bir okula daha önce yorum yapmıştım, şimdi tekrar yorum yapmak 
    istiyorum fakat, sadece bir önceki yorumumu güncelleyebiliyorum. Neden ?</p>
    <p>Sistemdeki her okul için sadece bir yorum hakkın bulunmaktadır. Bu kısıtla amacımız bilgi karmaşıklığını olabildikçe 
    engellemek, hoca hakkındaki yorumları merak eden kullanıcılarımıza az ama öz bilgi sunmaktır.</p>
    
    <br />
    <p style="font-weight:bold;">Eğer her okul için sadece bir yorum hakkım varsa, yorumumu silersem 
    veya yorumum silinirse, aynı okula başka yorum yapamayacak mıyım?</p>
    <p>Yapabilirsin, her okul için bir tane yorum hakkı görüntülenen yorumların için geçerlidir. Eğer yorumunu silersen veya 
    yorumun bizim tarafımızdan silinirse, aynı okul için tekrar yorum yapma hakkın olacaktır.</p>
    
    <br />
    <p style="font-weight:bold;">Yorum yapmak istediğim ders sistemde bulunmuyor. Ne yapmalıyım ?</p>
    <p>Hoca yorumu yaparken “Hangi ders(ler)e yönelik?” sorusunda aldığın dersi göremezsen, “Diğer” 
    seçip dersin ismini girebilirsin. Burada belirttiğin ders ismi tarafımızdan değerlendirilecek ve sisteme eklenecektir.
    <br />
    Bunun dışında, üşenmeyip iletisim@notverin.com adresinden bize bu konuda eposta atarsan süper olur.</p>
    
    <br />
    <p style="font-weight:bold;">Yorum yapmak istediğim hoca sistemde bulunmuyor. Ne yapmalıyım ?</p>
    <p>Ders yorumu yaparken “Hangi hocadan aldınız?” sorusunda dersi aldığınız hocayı göremezseniz, 
    “Diğer” seçip hocanın ismini girebilirsiniz. Burada belirttiğiniz hoca ismi tarafımızdan 
    değerlendirilecek ve sisteme eklenecektir.

Bunun dışında, üşenmeyip iletisim@notverin.com adresinden bize bu konuda eposta atarsan süper olur.</p>

    <br />
    <p style="font-weight:bold;">Yorumlarım yayınlanırken adım gözüksün istemiyorum, ne yapabilirim ?</p>
    <p>Sisteme üye olurken kullanıcı adı seçersen, yorumların kullanıcı adınla yayınlanır. Eğer kullanıcı 
    adı seçmezsen, yorumların ilk isminle yayınlanır (soyadın gözükmez). 
    <br />
    Eğer üye olurken almadıysan, iletisim@notverin.com ‘dan bize ulaşarak kullanıcı adı alabilirsin. 
    (İleride hesabım sayfasından da kullanıcı adını ayarlayabileceksin)</p>

    <br />
    <p style="font-weight:bold;">Yaptığım yorumlardan sonra kullanıcı bilgilerime yorum yaptığım hocalar ulaşabilir mi ? 
    Burada hocayı översem hoca bana daha yüksek not verir mi ?</p>
    <p>Hayır, kullanıcı bilgilerin tamamen gizlidir. Herhangi bir üçüncü şahsın bu bilgilere ulaşması mümkün değildir. </p>

    <br />
    <p style="font-weight:bold;">Ders arşivlerine nereden ulaşabileceğimi bulamadım. Ne yapmalıyım ? </p>
    <p>Ders arşivlerine ilgili dersin sayfasından ulaşabilirsin. 
    (Dersin sayfasında sağda yer alan “Ders arşivi için tıklayın” linki)</p>

    <br />
    <p style="font-weight:bold;">Yüklediğim dosyalar üzerinde hak talep ediyor musunuz? İleride benim 
    yüklediğim dosyaları satmaya kalkışırsanız çok ayıp edersiniz !</p>
    <p>NotVerin ticari amaçlarla değil, öğrencilerinin kendilerinin geliştirip yararlandığı bir kaynak olarak açılmıştır. 
    Buraya yüklediğin yorumlar ve dosyaların tüm hakkı ve sorumluluğu sana aittir ve yüklediğin içeriğin 
    satılması asla söz konusu değildir.</p>

    <br />
    <p style="font-weight:bold;">Benim bölümümü ne zaman ekleyeceksiniz sisteme ?</p>
    <p>Elimizdeki içeriği zenginleştirdikçe yeni bölümler ekleyeceğiz. Bu konuda ekibimize katılarak kendi bölümünün 
    NotVerin sorumlusu olabilirsin. Bunun için lütfen iletisim@notverin.com adresinden bize ulaş. </p>

    <br />
    <p style="font-weight:bold;">İyi hoş da benim başka sorum var !</p>
    <p>Üzgünüz sorun yeteri kadar sık sorulmuyormuş demek ki. iletisim@notverin.com ‘dan bize ulaşırsan cevaplarız. 
    Çok kazık bir soru sorarsan cevap veremeyebiliriz, ne sorduğuna bağlı.</p>
</div>
</asp:Content>