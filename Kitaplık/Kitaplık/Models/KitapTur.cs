using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Kitaplık.Models
{
    public class KitapTur
    {
        [Key] // Id'yi Primary key yaptık
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen Boş Alanı Doldurunuz")] // ad propu null olamaz anlamında
        [MaxLength(25)]//max uzunluk
        [DisplayName("Kitap Türü Adı")] // Web kısmında ekranda nasıl gözükmesini istiyorsak öyle yazacağız
        public string Ad { get; set; }
    }
}
