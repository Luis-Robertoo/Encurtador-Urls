using Encurtei.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Encurtei.Data.Mapping;

public class LinkMap : IEntityTypeConfiguration<Link>
{
    private readonly string _schema;
    public LinkMap()
    {
        //_schema = schema;
    }

    public void Configure(EntityTypeBuilder<Link> modelBuilder)
    {

        modelBuilder.ToTable(nameof(Link));
        modelBuilder.Property(l => l.Id).HasColumnName("ID");
        modelBuilder.HasKey(l => l.Id);

        modelBuilder.Property(l => l.UrlOriginal).HasColumnName("URL_ORIGINAL");
        modelBuilder.Property(l => l.UrlOriginal).HasMaxLength(255);

        modelBuilder.Property(l => l.UrlEncurtada).HasColumnName("URL_ENCURTADA");
        modelBuilder.Property(l => l.UrlEncurtada).HasMaxLength(127);

        modelBuilder.Property(l => l.CriadoEm).HasColumnName("DATA_CRIACAO");

        modelBuilder.Property(l => l.QrCode).HasColumnName("QR_CODE");
        modelBuilder.Property(l => l.QrCode).HasMaxLength(512);
    }
}
