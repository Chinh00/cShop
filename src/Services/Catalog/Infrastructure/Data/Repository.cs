using cShop.Core.Domain;
using cShop.Infrastructure.Data;

namespace Infrastructure.Data;

public class Repository<TEntity>(CatalogContext context) : RepositoryBase<CatalogContext, TEntity>(context)
    where TEntity : EntityBase;