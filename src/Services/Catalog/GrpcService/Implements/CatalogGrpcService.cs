using cShop.Core.Repository;
using Domain.Aggregate;
using Grpc.Core;
using GrpcServices;

namespace GrpcService.Implements;

public class CatalogGrpcService(IRepository<Product> productRepository) : Catalog.CatalogBase
{


   public override async Task<GetCatalogByIdResponse> getProductById(GetCatalogByIdRequest request, ServerCallContext context)
   {
       var product = await productRepository.FindByIdAsync(Guid.Parse(request.Id));
       return new GetCatalogByIdResponse()
       {
            ProductId = product.Id.ToString(),
            CurrentCost = product.Price,
            ProductName = product.Name
       };

   }
}