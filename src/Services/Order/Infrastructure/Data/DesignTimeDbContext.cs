using cShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data;

public class DesignTimeDbContext : DesignTimeDbContextBase<OrderContext>
{
    
}