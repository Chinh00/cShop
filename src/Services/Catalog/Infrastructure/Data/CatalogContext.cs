using cShop.Infrastructure.Data;
using Domain.Aggregate;
using Domain.Entities;
using Domain.Outbox;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class CatalogContext(DbContextOptions options) : AppBaseContext(options)
{
    public DbSet<CatalogItem> CatalogItems { get; set; }
    public DbSet<CatalogPicture> CatalogPictures { get; set; }
    public DbSet<CatalogOutbox> CatalogOutboxes { get; set; }
    public DbSet<CatalogType> CatalogTypes { get; set; }
    public DbSet<CatalogBrand> CatalogBrands { get; set; }
}