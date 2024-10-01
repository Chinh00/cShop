using cShop.Infrastructure.Projection;

namespace Projection;

public class CatalogProjectionRepository(ILogger<CatalogProjection> logger, IProjectionDbContext projectionDbContext)
    : ProjectionRepositoryBase<CatalogProjection>(logger, projectionDbContext);