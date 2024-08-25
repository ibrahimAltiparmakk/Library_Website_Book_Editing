using Kitaplık.Models;
using Microsoft.EntityFrameworkCore;

namespace Kitaplık.Utility
{
    public class UygulamaDbContext : DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) { }
        
        public DbSet<KitapTur> KitapTurleri { get; set; } // kitap türü tablosunu oluşturma
    
    }
}
