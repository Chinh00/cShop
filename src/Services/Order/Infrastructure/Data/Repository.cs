using cShop.Core.Domain;
using cShop.Infrastructure.Data;

namespace Infrastructure.Data;

public class Repository<TEntity>(OrderContext context) : RepositoryBase<OrderContext, TEntity>(context)
    where TEntity : EntityBase;