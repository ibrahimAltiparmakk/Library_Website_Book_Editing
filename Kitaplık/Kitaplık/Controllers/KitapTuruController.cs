using Kitaplık.Models;
using Kitaplık.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Kitaplık.Controllers
{
    public class KitapTuruController : Controller
    {
        private readonly UygulamaDbContext _uygulamaDbContext; // UygulamaDbContext tipinde bir özel alan tanımlanıyor. Bu, veritabanı işlemleri için kullanılacak olan Entity Framework Core context'ini temsil ediyor.

        public KitapTuruController(UygulamaDbContext context) // Bu constructor, UygulamaDbContext türünden bir context parametresi alır ve bu parametreyi _uygulamaDbContext özel alanına atar. Bu, dependency injection yoluyla context'in bu controller'a sağlanmasını sağlar.
        {
            _uygulamaDbContext = context; // veritabanınyla EntityFramework arasındaki köprü nesnesi
        }

        public IActionResult Index()
        {
            List<KitapTur> objKitapTuruList = _uygulamaDbContext.KitapTurleri.ToList(); // _uygulamaDbContext içindeki KitapTurleri DbSet'ini kullanarak tüm kitap türlerini bir liste olarak çeker ve bu listeyi objKitapTuruList adlı değişkene atar.
            return View(objKitapTuruList);// verileri view e gönderdik
        }

        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(KitapTur kitapTur)
        {
            if (ModelState.IsValid)// ModelState.IsValid özelliği, ASP.NET Core'da bir formdan gönderilen verilerin sunucu tarafındaki model doğrulama kurallarına uyup uymadığını kontrol etmek için kullanılır. Bu özellik, ModelState nesnesi içindeki doğrulama hatalarına bakarak, modelin doğrulama kurallarını geçip geçmediğini belirler.
            { //Ad propunun kurallarına uyarsa içeri girer eğer uymazsa dışarıdaki kısma gider
                _uygulamaDbContext.KitapTurleri.Add(kitapTur);
                _uygulamaDbContext.SaveChanges(); // SaveChanges yapmazsanız bilgiler veritabanına işlenmez
                TempData["basarili"] = "Yeni Kitap Türü Başarıyla Oluşturuldu";
                return RedirectToAction("Index", "KitapTuru");// hangi Actiona gitmesini istiyorsak onun ismini yazacağız , ikinci kısma da Controller ismini yazıyoruz ama aynı controller da olduğu için çok gerekli değil farklıda olsaydı gerekli olurdur
            }
            return View();
        }

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

        [HttpPost]
        public IActionResult Guncelle(KitapTur kitapTur)
        {
            if (ModelState.IsValid)// ModelState.IsValid özelliği, ASP.NET Core'da bir formdan gönderilen verilerin sunucu tarafındaki model doğrulama kurallarına uyup uymadığını kontrol etmek için kullanılır. Bu özellik, ModelState nesnesi içindeki doğrulama hatalarına bakarak, modelin doğrulama kurallarını geçip geçmediğini belirler.
            { //Ad propunun kurallarına uyarsa içeri girer eğer uymazsa dışarıdaki kısma gider
                _uygulamaDbContext.KitapTurleri.Update(kitapTur);// güncelleme
                _uygulamaDbContext.SaveChanges(); // SaveChanges yapmazsanız bilgiler veritabanına işlenmez
                TempData["basarili"] = "Kitap Türü Başarıyla Güncellendi";
                return RedirectToAction("Index", "KitapTuru");// hangi Actiona gitmesini istiyorsak onun ismini yazacağız , ikinci kısma da Controller ismini yazıyoruz ama aynı controller da olduğu için çok gerekli değil farklıda olsaydı gerekli olurdur
            }
            return View();
        }


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

        [HttpPost,ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            KitapTur? kitapTuru = _uygulamaDbContext.KitapTurleri.Find(id);
            if(kitapTuru == null)
            {
                return NotFound();
            }
            _uygulamaDbContext.KitapTurleri.Remove(kitapTuru);
            _uygulamaDbContext.SaveChanges();
            TempData["basarili"] = "Kitap Türü Başarıyla Silindi";
            return RedirectToAction("Index", "KitapTuru");// hangi Actiona gitmesini istiyorsak onun ismini yazacağız , ikinci kısma da Controller ismini yazıyoruz ama aynı controller da olduğu için çok gerekli değil farklıda olsaydı gerekli olurdur

        }

    }
}
