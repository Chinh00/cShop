using MediatR;
using GrpServices;
namespace Application.UseCases.Command;

public class CreateBasketCommand(Guid productId) : IRequest<Guid>
{

    private readonly Guid _productId = productId;
    public class Handler : IRequestHandler<CreateBasketCommand, Guid>
    {
        private readonly Catalog.CatalogClient _catalogClient;

        public Handler(Catalog.CatalogClient catalogClient)
        {
            _catalogClient = catalogClient;
        }

        public async Task<Guid> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var res = await _catalogClient.getProductByIdAsync(
                new GetProductByIdRequest() {Id = request._productId.ToString()});
            return Guid.Parse(res.ProductId);
        }
    }
}