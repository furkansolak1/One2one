using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

ESirketDbContext context = new ESirketDbContext();
#region Default Convention 
// her iki entity de Nav prop ile birbirlerine tekil referans kullanılarak 
// fiziksel bir ilişki olacağı ifade edilir 
// Dependent entity nin hangisi olduğunu belirlemek için dependenta CalisanId(fo) propu eklenir 
#endregion
//class Calisan

//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public CalisanAdresi CalisanAdresi { get; set; }
//}
//class CalisanAdresi
//{
//    public int Id { get; set; }
//    public int CalisanId { get; set; }// dependent taraf olduğunu ef anlıyor 
//    public string Name { get; set; }

//    public Calisan Calisan { get; set; } 
//}


#region Data Annotations 
// foreign key iççin ekstra default convention kolon oluşutrmaya gerek yok
// dependent ın ıdsini hem key hem de foreign olrak belirtiriz (maaliyet azaltma odaklı)
//class Calisan

//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public CalisanAdresi CalisanAdresi { get; set; }
//}
//class CalisanAdresi
//{
//    [Key,ForeignKey(nameof(Calisan))]
//    public int Id { get; set; }
//    public string Name { get; set; }

//    public Calisan Calisan { get; set; }
//}

#endregion

#region Fluent api
// ilişkiler context->onmodelcreating
class Calisan

{
    public int Id { get; set; }
    public string Name { get; set; }
    public CalisanAdresi CalisanAdresi { get; set; }
}
class CalisanAdresi
{
    
    public int Id { get; set; }
    public string Name { get; set; }

    public Calisan Calisan { get; set; }
}

#endregion



class ESirketDbContext : DbContext
{
    public DbSet<Calisan> Calisanlar { get; set; }
    public DbSet<CalisanAdresi> CalisanAdresleri { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("Server=DESKTOP-2IG9GVD\\SQLEXPRESS;Database=One2One;Trusted_Connection=True");

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CalisanAdresi>()
            .HasKey(c => c.Id);
        modelBuilder.Entity<Calisan>()
            .HasOne(c => c.CalisanAdresi)
            .WithOne(c => c.Calisan)
            .HasForeignKey<CalisanAdresi>(c => c.Id);
        
    }
}