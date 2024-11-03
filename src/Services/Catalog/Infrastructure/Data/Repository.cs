using cShop.Core.Domain;
using cShop.Infrastructure.Data;

namespace Infrastructure.Data;

public class Repository<TEntity>(DataContext context) : RepositoryBase<DataContext, TEntity>(context)
    where TEntity : EntityBase;