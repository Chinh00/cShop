using cShop.Infrastructure.Data;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ShipperContext(DbContextOptions options) : AppBaseContext(options)
{
    public DbSet<Shipper> Shippers { get; set; }
    public DbSet<ShipperOrder> ShipperOrders { get; set; }
}