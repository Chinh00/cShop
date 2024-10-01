using cShop.Infrastructure.Projection;
using Grpc.Core;
using GrpcServices;
using Projection;

namespace GrpcService.Implements;

public class CatalogGrpcService : Catalog.CatalogBase
{
    private readonly IProjectionRepository<CatalogProjection> _catalogProjectionRepository;

    public CatalogGrpcService(IProjectionRepository<CatalogProjection> catalogProjectionRepository)
    {
        _catalogProjectionRepository = catalogProjectionRepository;
    }

    public async override Task<GetCatalogByIdResponse> getProductById(GetCatalogByIdRequest request, ServerCallContext context)
    {
        var catalog = await _catalogProjectionRepository.FindByIdAsync(request.Id, default);
        
        
        return new GetCatalogByIdResponse()
        {
            ProductId = catalog.Id.ToString(),
            ProductName = catalog.Name,
            CurrentCost = catalog.Price
            
        };
    }
}