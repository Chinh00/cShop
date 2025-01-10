using cShop.Infrastructure.Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Infrastructure.Data;

public class PaymentContext(DbContextOptions options) : AppBaseContext(options)
{
    public DbSet<OrderInfo> OrderInfos { get; set; }
    public DbSet<Payment> Payments { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<OrderInfo>()
            .HasOne(o => o.Payment) 
            .WithOne(p => p.OrderInfo) 
            .HasForeignKey<Payment>(p => p.OrderInfoId);
    }
}