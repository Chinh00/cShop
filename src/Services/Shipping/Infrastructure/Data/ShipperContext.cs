using cShop.Infrastructure.Data;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ShipperContext(DbContextOptions options) : AppBaseContext(options)
{
    public DbSet<ShipperInfo> ShipperInfos { get; set; }
    public DbSet<ShipperOrder> ShipperOrders { get; set; }
}