using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data;

public class DesignTimeDbContext : IDesignTimeDbContextFactory<OrderContext>
{
    public OrderContext CreateDbContext(string[] args)
    {
        var option =
            new DbContextOptionsBuilder().UseSqlServer(
                "Server=localhost;Database=OrderDb;Encrypt=false;User Id=sa;Password=@P@ssw0rd02");
        return (OrderContext)Activator.CreateInstance(typeof(OrderContext), option.Options);
    }
}