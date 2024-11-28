using cShop.Core.Repository;
using Domain.Aggregate;
using Grpc.Core;
using GrpcServices;

namespace GrpcService.Implements;

public class CatalogGrpcService(IRepository<CatalogItem> productRepository) : Catalog.CatalogBase
{


   public override async Task<GetCatalogByIdResponse> getProductById(GetCatalogByIdRequest request, ServerCallContext context)
   {
       var product = await productRepository.FindByIdAsync(Guid.Parse(request.Id), default);
       return new GetCatalogByIdResponse()
       {
            ProductId = product.Id.ToString(),
            CurrentCost = (float) product.Price,
            ProductName = product.Name
       };

   }
}