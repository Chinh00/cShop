using Microsoft.EntityFrameworkCore;

namespace cShop.Infrastructure.Data;

public class AppBaseContext(DbContextOptions options) : DbContext(options);