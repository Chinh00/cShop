using cShop.Infrastructure.Data;
using Domain.Aggregate;
using Domain.Entities;
using Domain.Outbox;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext(DbContextOptions options) : AppBaseContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductOutbox> ProductOutboxes { get; set; }
    public DbSet<Category> Categories { get; set; }
}