namespace Kitaplık
{
    public class Not
    {
        /*
         * ilk önce paketleri yükleyerek başlıyalım ;
         * 
         * MicrosoftEntityFrameworkCore.SqlServer 
         * MicrosoftEntityFrameworkCore
         * MicrosoftEntityFrameworkCore.Tools
         * 
         * 
         * daha sonra Models klasörüe gidip ilk sınıfımızı oluşturuyoruz
         * bu oluşturdupumuz sınıf database de tablo olacak , class ı oluşturduktan sonra içini dolduralım
         * 
         * Database e bağlanmak için connection string oluşturacağız , ilk önce appsettings.json dosyasına gidiyoruz 
         * ve içinde gerekli işlemleri yapıyoruz , şu kodu yazıyoruz
         * 
         * "ConnectionStrings": {
             "DefaultConnection": "Server=DESKTOP-IGV354K\\SQLEXPRESS;Database=VTKitapSatis;Trusted_Connection=True;TrustServerCertificate=True"
         *   }
         * 
         * daha sonra dbcontext i tanımlamak için projede Utility adlı bi klasör oluşturup içine
         * UygulamaDbContext adlı sınıfı ekliyelim bu sınıfa DbContext den kalıtım yapalım ve belli bir kalıp olan şu kodu yazalım
         * " public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) { } " 
         * 
         * daha sonra gelip DbContext i sistemimize tanıtamız lazım o yüzden program.cs e gelip şu kodu yazıyoruz
         * 
         *             builder.Services.AddDbContext<UygulamaDbContext>(options =>
         *                   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
         *
         * bunları yaptıktan sonra tablo oluşturmak için UygulmaDbContext.cs e gidiyoruz ve KitapTuru sınıfının tablosunu oluşturmak için şu kodu yazıyoruz
         * "public DbSet<KitapTur> KitapTurleri { get; set; }"
         * 
         * daha sonra package manager console da add-migration deneme diyip ilk migration u oluşturuyoruz , sonrasında update-database diyoruz
         * 
         * şimdi de yeni bir controller ekleyeceğiz , Controller klasörüne sağ tıklayıp add Controller diyoruz ve Modelimizdeki class la aynı isme sahip olacak şekilde 
         * ve sonuna Controller yazacağımız şekilde KitapTuruController controller ı oluşturuyoruz 
         * 
         * daha sonra oluşturduğumuz controller n index inin üstüne gelip add view diyoruz ve boş Razor ekliyoruz viewimizi oluşturuyoruz
         * Shared klasöründe layout kısmına gidip kitapturu kısmını ekliyelim
         * 
         * şimdi de MSSQL a gidip KitapTuru tablosuna birkaç veri ekliyelim
         * 
         * database den verileri çekmek için KitapTuruController a gidelim ve gerekli işlemleri yapalım 
         * 
         * daha sonra çekeceğimiz verileri göstermek için index.cshtml e gidelim ve gerekli kodları yazalım , tablonun temelini oluşturdukan sonra controller da çektiğimiz verileri view e yansıtmak için 
         * Controller daki Index actionunun return kısmına yazalım "return View(objKitapTuruList);" , bunu yaptıktan sonra Index.cshtml e gelip return gönderidiğimiz verinin Tipini en yukarıya model olarak yazıyoruz
         * " @model List<KitapTur> " şeklinde yazdık daha sonra tablo işlemlerile devam ediyoruz
         * 
         * verileri yansıttıktan sonra bootstrap işlemleri için istediğimiz dosyayı indiriyoruz ve projenin
         * wwroot>lib>bootstrap>dist>css>bootstrapt.css dosyasına indirdiğimiz bootstrap.css dosyasını yapıştırıyoruz
         * daha sonra layout kısmına gelip yukarı kısımlardaki şu kodu 
         *     <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" /> 
         *     min kısmını silip kullanıyoruz
         *     <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.css" />
         * 
         * daha sonra layouttaki navbarın renklerini değiştirmek için önceden aşşağıdaki gibi olan kodu
         *         <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
         * şu şekilde değiştiriyoruz
         *         <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark bg-primary border-bottom box-shadow mb-3">
         *
         * daha sonra ul li etiketlerinin içindeki a etiketlerinin içindeki text-dark kısmını siliyoruz
         * 
         * şimdi Index.cshtml e gelip yazdığımzı kodu container içine alıyoruz , daha sonra ekle butonunu oluşturacağız , ilk önce göresel olarak butonumuzu oluşturalım
         * 
         * <div class="row pt-3 pb-2">
                <div>
                    <a asp-controller="" asp-action="" class="btn btn-lg btn-primary" type="button">Yeni Kitap Türü Ekle</a>
                </div> 
            </div>
         * 
         * butonu görsel olarak oluşturduktan sonra butonun işlevsel olarak çalışması için Actionunu yaratmamız lazım onun için  kitapTuruController a gidelim ve Action ekliyeli 
         * Ekle actionunu ekliyelim ve onun viewini ekliyelim , daha sonra eklediğimiz butonun Controller ve Index kısmını dolduralım 
         * 
         * daha sonra Ekle.cshtml i içini dolduralım , ilk önce KitapTur modelinin ekliyelim ve görüntü kısmını yazalım
         * şimdi de backend kısmını yapacağız , ilk önce input kısmının içerisine Ad olduğunu anlaması için asp-for="Ad" yazacağız
         * KitapTur modelinde nasıl yazdıysan öyle yazacaksın Ad yazdık biz ama Ad4 yazsan kabul olmaz , daha sonra Label için de aynını yapalım
         * <label asp-for="Ad"></label> bunu yaptıktan sonra ekranda sadece Ad olarak yazacak ama bunun böyle görünmesini istemiyorsanız KitapTur modeline gelip 
         * Ad prop unun üstüne [DisplayName("Kitap Türü Adı")]  yazıyoruz ve ekranda Ad propunun yazsının nasıl görünmesini istiyorsak onu yazıyoruz
         * 
         * bunları yaptıktan sonra Ekle.cshtml de Formumuz post methodunda olduğu için butona bastığımızda Controller da Post a sahip Ekle Actionu lazım Controller da Post Actiıonu oluşturalım
         * [HttpPost]
            public IActionResult Ekle(KitapTur kitapTur)
            {
                return View();
            }
         * bunu ekledik , daha sonra veritabanıyla iletşime geçmek ve değişiklikleri kaydetmek için şunları yazıyoruz
         * 
         *     _uygulamaDbContext.KitapTurleri.Add(kitapTur);
         *     _uygulamaDbContext.SaveChanges();
         *     
         * 
         * Kitap türü ekleme işleminden sonra hata uyarılırı içim işlemler yapacağız
         * maximum uzunluğu belirlemek için KitapTur modelimize gidiyoruz ve Ad propunun üstğne MaxLength(25) yazıyoruz
         * daha sonra Ad propunun üstünde yazan özelliklerin sağlanıp sağlanmadığını kontrol etmek için KitapTuruController a geliğ HttpPost Ekle Actionunda  if(ModelState.IsValid) diyerek o özellikleri sağlayıp sağlamadığını kontorl ediyoruz
         * şimdi de hatanın uyarılması için uyarı mesajı yapacağız Ekle.cshtml de inputun altına şunu yazıyoruz <span asp-validation-for="Ad" class="text-danger"></span> ve ekranda ne yazacağını belirtmek için modelimize gidip ad ın yukakarısındaki Required in içerisine ErrorMessage = "Lütfen Boş Alanı Doldurunuz") yazıyoruz ve mesajımızı burada belirtiyoruz
         * 
         * şimdi de sil ve güncelle kısmını yapacağız , ilk önce Index.cshtml kısmında tablomuza 2 tane daha (boş) th etiketi ekleyeceğiz ve 2 tane daha sutun oluşturacağız , daha sonra foreach döngüsünün içerisine girip td etiketiyle hücre oluşturmamız lazım o yüzden güncelle ve sil butonarı için döngü içerisine şunu yazıyoruz
         * 
         *     <td>
         *              <a asp-controller="KitapTuru" asp-action="Guncelle" type="button" class="btn btn-secondary">Güncelle</a>
         *     </td>
         * 
         * şimdi de güncelleme işlemleri için Controller a Güncelleme Actionlarını yazalım 
         *         
         
               public IActionResult Guncelle(int? id) // int id kısmı hangi id numaralı kitapın güncelleneceğini belirtmek için kullanılır
                {
                    if(id == null || id==0)// yukarıdaki ? işareti id null gelirse hata önlemek için
                    {
                        return NotFound();// id null gelirse veya 0 gelirse NotFound kısmını getir
                    }

                    KitapTur? kitapTuruVt = _uygulamaDbContext.KitapTurleri.Find(id);// id numaralı kitabı bul kitapTuruVt nesnesine at
                    // kitap id sini bulamazsa diye (null dönerse diye ? koyduk)
                    if (kitapTuruVt == null)
                    {
                        return NotFound(); // null dönerse NotFound a yönlendir
                    }
                    return View(kitapTuruVt);// bunu da view e gönderiyoruz
                }
         * 
         * actinunu yazdık , daha sonra Güncelleme butonunun bulunduğu cshtl kısmıına gidip Seçtiğimiz kitabın id sini contollerdaki Actiona göndermek için 
         *                         <a asp-controller="KitapTuru" asp-action="Guncelle" asp-route-id="@kitapTuru.Id" type="button" class="btn btn-secondary">Güncelle</a>
         * bu kısıma asp-route-id="@kitapTuru.Id" kısmını ekliyoruz ki id controller a gidebilsin
         * 
         * 
         * daha sonra Güncelle Actionunun Viewini oluşturalım aynı isimle , daha sonra Guncelle View inin içini Ekle Viewinden kodları kopyalayarak dolduralım yazılarda değişiklik yaptıktan sonra guncelle Actionunun HTTPOST kısmını yazmak için controller a dönüyoruz
         * 
                [HttpPost]
                public IActionResult Guncelle(KitapTur kitapTur)
                {
                    if (ModelState.IsValid)// ModelState.IsValid özelliği, ASP.NET Core'da bir formdan gönderilen verilerin sunucu tarafındaki model doğrulama kurallarına uyup uymadığını kontrol etmek için kullanılır. Bu özellik, ModelState nesnesi içindeki doğrulama hatalarına bakarak, modelin doğrulama kurallarını geçip geçmediğini belirler.
                    { //Ad propunun kurallarına uyarsa içeri girer eğer uymazsa dışarıdaki kısma gider
                        _uygulamaDbContext.KitapTurleri.Update(kitapTur);
                        _uygulamaDbContext.SaveChanges(); // SaveChanges yapmazsanız bilgiler veritabanına işlenmez
                        return RedirectToAction("Index", "KitapTuru");// hangi Actiona gitmesini istiyorsak onun ismini yazacağız , ikinci kısma da Controller ismini yazıyoruz ama aynı controller da olduğu için çok gerekli değil farklıda olsaydı gerekli olurdur
                    }
                    return View();
                }
         *  
         *  bu kodu yazıyoruz
         *  
         *  daha sonra silme işleminin controller kısmını yapacağız ilk Sil Actionumuzu aşşağıdaki gibi oluşturuyoruz
         *  
                 public IActionResult Sil(int? id) // int id kısmı hangi id numaralı kitapın güncelleneceğini belirtmek için kullanılır
                  {
                    if (id == null || id == 0)// yukarıdaki ? işareti id null gelirse hata önlemek için
                    {
                        return NotFound();// id null gelirse veya 0 gelirse NotFound kısmını getir
                    }

                    KitapTur? kitapTuruVt = _uygulamaDbContext.KitapTurleri.Find(id);// id numaralı kitabı bul kitapTuruVt nesnesine at
                    // kitap id sini bulamazsa diye (null dönerse diye ? koyduk)
                    if (kitapTuruVt == null)
                    {
                        return NotFound(); // null dönerse NotFound a yönlendir
                    }
                    return View(kitapTuruVt);// bunu da view e gönderiyoruz
                  }

            
         *
         *  daha sonra Index.cshtml deki Sil butonunun içindeki kısıma asp-route-id="@kitapTuru.Id" bunu ekliyoruz ki Actiona id gönderilebilsin
         *  Sil actionunun Viewini oluşturmamız lazım onu oluşturuyoruz Sil.cshtml adlı
         *  Güncelle.cshtml deki kodların aynısınnı bu viewe kopyalayalım ve içindeki textleri sil e çevirelim , bu koddaki input alanına kullanıcının bir şey yazmaması lazım o yüzden input alanını disabled yapmamız lazım şu şekilde
         *              
         *     <input asp-for="Ad" disabled class="form-control" /> 
         *     
         *  şimdide Sil actionunun POST kısmını yapacağız ve kodumuz bu şekilde 
         *  
         *    [HttpPost,ActionName("Sil")]       
              public IActionResult SilPOST(int? id)
                {
                    KitapTur? kitapTuru = _uygulamaDbContext.KitapTurleri.Find(id);
                    if(kitapTuru == null)
                    {
                        return NotFound();
                    }
                    _uygulamaDbContext.KitapTurleri.Remove(kitapTuru);
                    _uygulamaDbContext.SaveChanges();
                    return RedirectToAction("Index", "KitapTuru");// hangi Actiona gitmesini istiyorsak onun ismini yazacağız , ikinci kısma da Controller ismini yazıyoruz ama aynı controller da olduğu için çok gerekli değil farklıda olsaydı gerekli olurdur

                }
         *
         *  
         *  şimdi de silme güncelleme ve ekleme işlemi sonrası ekrana bilgilendirme mesajı yazmak için TempData kullnacağız , bunun için KitapTuruController da ekleme silme ve güncelleme işlemlerinin Post kısımlarında bilgilendirme işlemlerini kodlayalalım
         *  return kısmının hemen üstüne şu kodu yazıyoruz
         *  
         *   TempData["basarili"] = "Kitap Türü Başarıyla Oluşturuldu Silindi";
         *   hepsinin text kısmı farklı olacak tabiki
         *   
         *   daha sonra Index.cshtml e gidiyoruz ve modelin altına şu kodu yazıyoruz
         *   
         *     @if (TempData["basarili"] != null)
                  {
                    <h2>@TempData["basarili"]</h2>
                  }

         *
         *   şimdi projein basit kısmı bitti , sadece tek controller lı projelerde bu yaptıklarımız yeterli ama daha orta ve büyük projelerde Repository Design Pattern yöntemi kullanıyor
         *   
         *   
         *   -------------- Repository Design Pattern Nedir ? ------------------
         *   
         *   MVC (Model-View-Controller) mimarisinde, Repository Design Pattern, veri kaynağı ile iş mantığı arasında bir soyutlama katmanı sağlamak için kullanılır. Bu desen, veritabanı
         *   işlemlerinin uygulamanın geri kalanından izole edilmesini sağlar, böylece veri erişim mantığı daha düzenli ve yönetilebilir hale gelir.
         *   
         *   Repository Design Pattern'in Temel Özellikleri:
         *   -Soyutlama: Repository pattern, veri erişim katmanını uygulamanın geri kalanından soyutlar. Bu, veri kaynağı değişse bile uygulamanın geri kalanının etkilenmemesini sağlar.
         *   -Tekrar Kullanılabilirlik: Benzer veri erişim gereksinimleri olan farklı uygulama parçalarında repository'lerin yeniden kullanılmasını kolaylaştırır.
         *   -Test Edilebilirlik: Veri erişimi soyutlandığından, birim testleri sırasında gerçek veritabanı yerine sahte (mock) nesneler kullanılabilir, bu da test sürecini daha hızlı ve daha az karmaşık hale getirir.
         *   -Bakım Kolaylığı: Veri erişim kodu merkezi bir yerde toplandığı için, değişiklikler daha hızlı ve daha az hata ile yapılabilmektedir.
         *   
         *   Repository Pattern Nasıl Çalışır?
         *   1-Repository Interfaces: İlk olarak, her bir veri modeli türü için bir repository interface tanımlanır. Bu interface'ler, model nesnelerinin nasıl alınıp saklanacağını tanımlayan metotları içerir.
         *   2-Concrete Repositories: Interface'leri uygulayan somut sınıflar oluşturulur. Bu sınıflar, belirli bir veritabanı veya başka bir veri kaynağı ile etkileşimde bulunan gerçek veri erişim mantığını içerir.
         *   3-Service Layer: Uygulamanın iş mantığını içeren servis katmanı, repository interface'lerini kullanarak veri erişim işlemlerini gerçekleştirir. Bu sayede servis katmanı, veri kaynağının özel detaylarından bağımsız olarak işlevini yerine getirebilir.
         *   
         *   ---------------------------------------------------------------------------
         *   
         *   Repository Pattern için ilk olarak Models klsösrünün içerisinde IRepository adında bir interface oluşturuyoruz , models e sağ tıklayıp class ı seçip ordan interface i seçelim ve oluşturalım 
         *   oluşturduğumuz interface in içini şu şekilde dolduralım
         *   
         *   public interface IRepository<T> where T : class
                {
                    //T-> KitapTuru
                    IEnumerable<T> GetAll();
                    T Get(Expression<Func<T, bool>> filtre);
                    void Ekle(T entity);
                    void Sil(T entity);
                    void SilAralik(IEnumerable<T> entities);
        
                }
         *  
         *  
         *  şimdi de Models Klasörüne Repository class ımızı ekliyoruz Repository adından
         *  daha sonra bu repository sınıfını IRepository interface inden kalıtım alıyoruz ve interface üzerinde sağ tıklatıp implement edip aşşağıdaki gibi yapıyoruz
         *   
         *   
         *   
         *   public class Repository<T> :IRepository<T> where T : class
            {
                

                public void Ekle(T entity)
                {
                    throw new NotImplementedException();
                }

                public T Get(Expression<Func<T, bool>> filtre)
                {
                    throw new NotImplementedException();
                }

                public IEnumerable<T> GetAll()
                {
                    throw new NotImplementedException();
                }

                public void Sil(T entity)
                {
                    throw new NotImplementedException();
                }

                public void SilAralik(IEnumerable<T> entities)
                {
                    throw new NotImplementedException();
                }
            }
         * 
         * 
         *  daha sonra şu kısımları da sınıfımıza ekliyelim
         *  
         *  private readonly UygulamaDbContext _uygulamaDbContext;
            internal DbSet<T> dbSet;

            public Repository(UygulamaDbContext uygulamaDbContext)
            {
                _uygulamaDbContext = uygulamaDbContext;
                this.dbSet = _uygulamaDbContext.Set<T>();
            }
         * 
         * işlemleri (ekleme,silme,güncelleme vb) artık controller dan değil Repository sınıfından yapacağız
         * 
         * bunları yazdıktan sonra Ekle işlemin artık Repositoryde yapacağımız için ekle nin içini dolduralım
         * 
         * şimdi de Get metodunu dolduralım , sonra GetAll kısmını dolduralım
         * şimdi de sil ve SilAralık kısmını dolduralım
         * 
         * KitapTürüRepository oluşturacağız ama ilk önce bunun Interface ini oluşturmamız lazım o yüzden Model klasöründe IKitapTuruRepository adında bir interface oluşturacağız 
         * ve IRepository<KitapTur> kalıtım edeceğiz  ve içini şöyle dolduralım 
         * 
         *         void Guncelle(KitapTur kitapTuru);
                   void Kaydet();
           
           
         * şimdi de KitapTuruRepository sini oluşturalım , model klasörnün içine gelip sınıf olarak oluşturalım
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         */
    }
}
