namespace Kitaplık.Models
{
    public interface IKitapTuruRepository : IRepository<KitapTur> // Burada IKitapTuruRepository adlı bir arayüz (interface) tanımlanıyor ve bu arayüz, IRepository<KitapTur> arayüzünden kalıtım (inheritance) alıyor. Kalıtım sırasında KitapTur türü, IRepository<T> genel arayüzünün tür parametresi (T) olarak belirtiliyor. 
    {
        void Guncelle(KitapTur kitapTuru);
        void Kaydet();
    }
}
