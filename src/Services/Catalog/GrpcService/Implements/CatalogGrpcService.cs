using Grpc.Core;
using GrpcServices;

namespace GrpcService.Implements;

public class CatalogGrpcService : Catalog.CatalogBase
{

   

    public async override Task<GetCatalogByIdResponse> getProductById(GetCatalogByIdRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }
}