using Kitaplık.Utility;

namespace Kitaplık.Models
{
    public class KitapTuruRepository : Repository<KitapTur>, IKitapTuruRepository
    {
        public KitapTuruRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
        }

        public void Guncelle(KitapTur kitapTuru)
        {
            throw new NotImplementedException();
        }

        public void Kaydet()
        {
            throw new NotImplementedException();
        }
    }
}
