using Application.UseCases.Specs;
using AutoMapper;
using cShop.Core.Domain;
using cShop.Core.Repository;
using Domain;
using FluentValidation;
using Infrastructure.Dtos;
using MediatR;

namespace Application.UseCases.Queries;

public record GetCommentsQuery : IQuery<ListResultModel<CommentLineDto>>
{
    public List<FilterModel> Filters { get; set; } = [];
    public List<string> Includes { get; set; } = [];
    public List<string> Sorts { get; set; } = [];
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    
    public class Validator : AbstractValidator<GetCommentsQuery>
    {
        public Validator()
        {
            
        }        
    }
    internal class Handler : IRequestHandler<GetCommentsQuery, ResultModel<ListResultModel<CommentLineDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IMongoQueryRepository<CommentLine> _commentsRepository;
        public Handler(IMapper mapper, IMongoQueryRepository<CommentLine> commentsRepository)
        {
            _mapper = mapper;
            _commentsRepository = commentsRepository;
        }

        public async Task<ResultModel<ListResultModel<CommentLineDto>>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetCommentsSpec(request);
            var comments = await _commentsRepository.FindAsync(spec, cancellationToken);
            var commentsTotalCount = await _commentsRepository.CountAsync(spec, cancellationToken);
            return ResultModel<ListResultModel<CommentLineDto>>.Create(ListResultModel<CommentLineDto>.Create(
                _mapper.Map<List<CommentLineDto>>(comments), commentsTotalCount, request.Page, request.PageSize));
        }
    }
    
}