using cShop.Infrastructure.Projection;

namespace Projection;

public class BasketProjectionRepository(ILogger<BasketProjection> logger, IProjectionDbContext projectionDbContext)
    : ProjectionRepositoryBase<BasketProjection>(logger, projectionDbContext);