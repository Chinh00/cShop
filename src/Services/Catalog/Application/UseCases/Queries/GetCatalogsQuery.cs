using Application.UseCases.Queries.Specs;
using cShop.Core.Domain;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Queries;

public record GetCatalogsQuery : IQuery<IResult>
{
    public List<FilterModel> Filters { get; set; } = [];
    public List<string> Includes { get; set; } = [];
    public List<string> OrderBy { get; set; } = [];
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    
    public class Validator : AbstractValidator<GetCatalogsQuery>
    {
        public Validator()
        {
            
        }
    }
    
    
    internal class Handler : IRequestHandler<GetCatalogsQuery, IResult>
    {
        
        
        public Task<IResult> Handle(GetCatalogsQuery request, CancellationToken cancellationToken)
        {
            var specs = new GetCatalogsSpec(request);
            

            throw new NotImplementedException();
        }
    }
}