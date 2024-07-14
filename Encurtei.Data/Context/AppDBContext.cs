using Encurtei.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Encurtei.Data.Context;

public class AppDBContext : DbContext
{

    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    /*
    protected AppDBContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=EncurtadoDB.mssql.somee.com;user id=LuisZR1_SQLLogin_1;pwd=pm8bstz99n;initial catalog=EncurtadoDB;TrustServerCertificate=True");
    }
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Link>().Property(l => l.Id).HasColumnName("ID");
        modelBuilder.Entity<Link>().HasKey(l => l.Id);

        modelBuilder.Entity<Link>().Property(l => l.UrlOriginal).HasColumnName("URL_ORIGINAL");
        modelBuilder.Entity<Link>().Property(l => l.UrlOriginal).HasMaxLength(255);

        modelBuilder.Entity<Link>().Property(l => l.UrlEncurtada).HasColumnName("URL_ENCURTADA");
        modelBuilder.Entity<Link>().Property(l => l.UrlEncurtada).HasMaxLength(127);

        modelBuilder.Entity<Link>().Property(l => l.CriadoEm).HasColumnName("DATA_CRIACAO");

        modelBuilder.Entity<Link>().Property(l => l.QrCode).HasColumnName("QR_CODE");
        modelBuilder.Entity<Link>().Property(l => l.QrCode).HasMaxLength(512);
    }

    public DbSet<Link> Link { get; set;}
}

