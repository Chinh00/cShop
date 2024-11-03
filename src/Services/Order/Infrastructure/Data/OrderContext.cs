using cShop.Infrastructure.Data;
using Domain;
using Domain.Outbox;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class OrderContext(DbContextOptions options) : AppBaseContext(options)
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<ProductInfo> ProductInfos { get; set; }
    public DbSet<CustomerInfo> CustomerInfos { get; set; }
    public DbSet<OrderOutbox> OrderOutboxes { get; set; }
}