using AutoMapper;
using cShop.Core.Domain;
using cShop.Core.Repository;
using cShop.Infrastructure.IdentityServer;
using Domain;
using FluentValidation;
using Infrastructure.Dtos;
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
        private readonly IMapper _mapper;
        public Handler(IClaimContextAccessor contextAccessor, IMongoCommandRepository<CommentLine> mongoCommandRepository, IMapper mapper)
        {
            _contextAccessor = contextAccessor;
            _mongoCommandRepository = mongoCommandRepository;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var commentLine = new CommentLine()
            {
                UserId = _contextAccessor.GetUserId(),
                User = new UserInfo()
                {
                    Avatar = _contextAccessor.GetAvatar(),
                    Username = _contextAccessor.GetUsername()
                },
                ProductId = request.ProductId,
                Content = request.Content,
            };
            var result = await _mongoCommandRepository.AddAsync(commentLine, cancellationToken);
            return Results.Ok(ResultModel<CommentLineDto>.Create(_mapper.Map<CommentLineDto>(result)));
        }
    }
}