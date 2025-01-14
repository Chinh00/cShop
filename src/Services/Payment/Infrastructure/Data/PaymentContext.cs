using cShop.Infrastructure.Data;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class PaymentContext(DbContextOptions options) : AppBaseContext(options)
{
    public DbSet<OrderInfo> OrderInfos { get; set; }
   
}