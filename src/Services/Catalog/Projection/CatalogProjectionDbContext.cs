using cShop.Infrastructure.Projection;
using Microsoft.Extensions.Options;

namespace Projection;

public class CatalogProjectionDbContext(IOptions<MongoDbOptions> options) : ProjectionDbContextBase(options);