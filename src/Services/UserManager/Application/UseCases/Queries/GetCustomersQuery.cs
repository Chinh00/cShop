using Application.UseCases.Dtos;
using Application.UseCases.Specs;
using AutoMapper;
using cShop.Core.Domain;
using cShop.Core.Repository;
using Domain;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Queries;

public record GetCustomersQuery : IQuery<ListResultModel<CustomerDto>>
{
    public List<FilterModel> Filters { get; set; } = [];
    public List<string> Includes { get; set; } = [];
    public List<string> Sorts { get; set; } = [];
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    
    
    public class Validator : AbstractValidator<GetCustomersQuery>
    {
        
    }
    internal class Handler(IMapper mapper, IListRepository<Customer> repository)
        : IRequestHandler<GetCustomersQuery, ResultModel<ListResultModel<CustomerDto>>>
    {
        public async Task<ResultModel<ListResultModel<CustomerDto>>> Handle(GetCustomersQuery request,
            CancellationToken cancellationToken)
        {
            var specs = new GetCustomersSpec(request);
            var customers = await repository.FindAsync(specs, cancellationToken);
            var listResultModel = ListResultModel<CustomerDto>.Create(mapper.Map<List<CustomerDto>>(customers),
                customers.Count, request.Page, request.PageSize);
            return ResultModel<ListResultModel<CustomerDto>>.Create(listResultModel);
        }
    }
}