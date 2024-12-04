using cShop.Infrastructure.Data;
using Domain;
using Domain.Outboxs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class UserContext(DbContextOptions options) : AppBaseContext(options)
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerOutbox> CustomerOutboxes { get; set; }
    public DbSet<Shipper> Shippers { get; set; }
    public DbSet<ShipperOutbox> ShipperOutboxes { get; set; }
    
    
}