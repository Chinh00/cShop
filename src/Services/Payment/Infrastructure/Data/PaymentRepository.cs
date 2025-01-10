using cShop.Core.Domain;
using cShop.Infrastructure.Data;

namespace Infrastructure.Data;

public class PaymentRepository<TEntity>(PaymentContext context) : RepositoryBase<PaymentContext, TEntity>(context)
    where TEntity : EntityBase;