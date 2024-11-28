using cShop.Core.Domain;
using cShop.Infrastructure.Data;

namespace Infrastructure.Data;

public class UserRepository<TEntity>(UserContext context) : RepositoryBase<UserContext, TEntity>(context)
    where TEntity : EntityBase;