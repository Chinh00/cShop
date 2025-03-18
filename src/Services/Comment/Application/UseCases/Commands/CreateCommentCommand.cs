using cShop.Core.Domain;
using cShop.Core.Repository;
using cShop.Infrastructure.IdentityServer;
using Domain;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Commands;

public record CreateCommentCommand(Guid ProductId, string Content) : ICommand<IResult>
{
    public class Validator : AbstractValidator<CreateCommentCommand>
    {
        public Validator()
        {
            
        }
    }
    
    internal class Handler : IRequestHandler<CreateCommentCommand, IResult>
    {
        private readonly IClaimContextAccessor _contextAccessor;
        private readonly IMongoCommandRepository<CommentLine> _mongoCommandRepository;
        public Handler(IClaimContextAccessor contextAccessor, IMongoCommandRepository<CommentLine> mongoCommandRepository)
        {
            _contextAccessor = contextAccessor;
            _mongoCommandRepository = mongoCommandRepository;
        }

        public async Task<IResult> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var commentLine = new CommentLine()
            {
                UserId = _contextAccessor.GetUserId(),
                ProductId = request.ProductId,
                Content = request.Content,
            };
            var result = await _mongoCommandRepository.AddAsync(commentLine, cancellationToken);
            return Results.Created();
        }
    }
}