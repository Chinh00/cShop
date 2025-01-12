using cShop.Infrastructure.Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Infrastructure.Data;

public class PaymentContext(DbContextOptions options) : AppBaseContext(options)
{
    public DbSet<OrderInfo> OrderInfos { get; set; }
   
}