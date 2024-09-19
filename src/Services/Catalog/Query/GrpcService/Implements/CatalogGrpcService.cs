using Grpc.Core;
using GrpServices;
using Projections;

namespace GrpcService.Implements;

public class CatalogGrpcService : Catalog.CatalogBase
{
    private readonly IProjectionRepository<CatalogProjection> _catalogProjectionRepository;

    public CatalogGrpcService(IProjectionRepository<CatalogProjection> catalogProjectionRepository)
    {
        _catalogProjectionRepository = catalogProjectionRepository;
    }

    public override async Task<GetProductByIdResponse> getProductById(GetProductByIdRequest request, ServerCallContext context)
    {
        var product = await _catalogProjectionRepository.FindByIdAsync(request.Id, default);

        return new GetProductByIdResponse()
        {
            ProductId = product.Id.ToString(),
            ProductName = product.Name
        };
    }
}