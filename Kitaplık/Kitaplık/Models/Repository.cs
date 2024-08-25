using Kitaplık.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Kitaplık.Models
{
    public class Repository<T> :IRepository<T> where T : class
    {
        private readonly UygulamaDbContext _uygulamaDbContext;
        internal DbSet<T> dbSet; // dbSet adında, genel tür parametresi T olan bir DbSet<T> tipinde başka bir alan tanımlanmıştır. Bu DbSet<T>, belirtilen türdeki (T) veri tablosuna erişim sağlar ve LINQ sorguları, veri eklemeleri, güncellemeleri ve silmeleri gibi işlemleri gerçekleştirmek için kullanılır.

        public Repository(UygulamaDbContext uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;// uygulamaDbContext parametresini sınıfın _uygulamaDbContext alanına atar. Bu işlem, sınıf içerisinde veritabanı bağlamına kolayca erişilebilmesini sağlar.
            this.dbSet = _uygulamaDbContext.Set<T>();// ifadesiyle, T tipindeki veriler için DbSet nesnesi alınır ve dbSet alanına atanır. Bu, sınıf metodlarının, ilgili veri tablosu üzerinde çalışabilmesi için gerekli olan DbSet nesnesini sağlar.
        }

        public void Ekle(T entity)
        {
            dbSet.Add(entity);// ekle işlemi bu şekilde daha kolay 
        }

        public T Get(Expression<Func<T, bool>> filtre)// Bu imza, Get metodunun genel tür T olan bir nesneyi döndüreceğini ve bir lambda ifadesi alacağını belirtir. Expression<Func<T, bool>> türündeki filtre parametresi, bir T tipindeki nesne üzerinde çalışacak ve bool tipinde bir değer döndürecek bir fonksiyonu temsil eder. Bu fonksiyon, genellikle bir koşulu (filtreyi) ifade eder.
        {
            IQueryable<T> sorgu = dbSet;// Bu satırda, dbSet değişkeninden bir IQueryable<T> sorgu nesnesi oluşturulur. IQueryable<T> arayüzü, veritabanına LINQ sorguları göndermek ve sonuçları bir koleksiyon olarak almak için kullanılır.
            sorgu = sorgu.Where(filtre);// Where metodunu kullanarak, filtre parametresi olarak alınan lambda ifadesi sorgu üzerine uygulanır. Bu ifade, veritabanında belirli bir koşulu sağlayan nesneleri filtrelemek için kullanılır. Bu sayede, sorgu artık yalnızca bu koşulu sağlayan nesneleri içerecek şekilde güncellenir.
            return sorgu.FirstOrDefault(); // FirstOrDefault() metodu çağrılır. Bu metod, sorgu içerisindeki nesnelerden ilkini döndürür. Eğer sorgu boş ise, yani koşulu sağlayan hiçbir nesne bulunamazsa, null değeri döndürülür (veya değer türü T için varsayılan değer).
        }// Bu metot, veritabanından bir koşula göre tek bir nesne çekmek istediğinizde kullanışlıdır. Örneğin, belirli bir ID'ye sahip bir kullanıcıyı veya belirli bir kritere göre ilk ürünü getirmek için bu yapı kullanılabilir. Bu yaklaşım, veritabanı sorgularınızı dinamik ve esnek bir şekilde yönetmenizi sağlar.

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> sorgu = dbSet;// IQueryable<T> arayüzü, LINQ sorgularını temsil eder ve bu sorguların veritabanında çalıştırılmasını sağlar. Burada doğrudan dbSet'i sorgu değişkenine atayarak, veritabanındaki T tipindeki tüm nesneler üzerinde sorgulama yapılabilir hale getirilir.
            return sorgu.ToList();// ToList() metodu, IQueryable<T> sorgusunu çalıştırır ve sonuçları bir List<T> olarak döndürür.
        }

        public void Sil(T entity)
        {
            dbSet.Remove(entity);
        }

        public void SilAralik(IEnumerable<T> entities) // belli aralıktaki yeri silmek için
        {
            dbSet.RemoveRange(entities);
        }
    }
}
