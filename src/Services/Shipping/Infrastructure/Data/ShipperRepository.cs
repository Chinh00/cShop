using cShop.Core.Domain;
using cShop.Infrastructure.Data;

namespace Infrastructure.Data;

public class ShipperRepository<TEntity>(ShipperContext context) : RepositoryBase<ShipperContext, TEntity>(context)
    where TEntity : EntityBase;