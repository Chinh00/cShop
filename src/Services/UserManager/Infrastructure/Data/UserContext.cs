using cShop.Infrastructure.Data;
using Domain;
using Domain.Outboxs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class UserContext(DbContextOptions options) : AppBaseContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserOutbox> UserOutboxes { get; set; }
}