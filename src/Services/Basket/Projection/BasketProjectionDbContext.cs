using cShop.Infrastructure.Projection;
using Microsoft.Extensions.Options;

namespace Projection;

public class BasketProjectionDbContext(IOptions<MongoDbOptions> options) : ProjectionDbContextBase(options);