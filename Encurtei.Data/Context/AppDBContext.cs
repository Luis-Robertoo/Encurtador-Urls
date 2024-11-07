using Encurtei.Data.Entities;
using Encurtei.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Encurtei.Data.Context;

public class AppDBContext : DbContext
{

    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new LinkMap());


        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Link> Link { get; set;}
}

