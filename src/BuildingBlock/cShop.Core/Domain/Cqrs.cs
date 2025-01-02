using MediatR;

namespace cShop.Core.Domain;

public interface ICommand<TResponse> : IRequest<IResult>
    where TResponse : notnull
{
    
}

public interface IQuery<TResponse> : IRequest<ResultModel<TResponse>>
{
    public List<FilterModel> Filters { get; set; }
    public List<string> Includes { get; set; }
    
    public List<string> Sorts { get; set; }
    public int Page { get; set; }
    
    public int PageSize { get; set; }
    
    
}


public record FilterModel(string Field, string Operator, string Value);



public record ResultModel<TData>(TData Data, bool IsError, string Message)
{
    public static ResultModel<TData> Create(TData data, bool isError = false, string message = default) =>
        new(data, isError, message);
}
public record ListResultModel<TData>(List<TData> Items, int Total, int Page, int PageSize)
{
    public static ListResultModel<TData> Create(List<TData> items, int total, int page, int pageSize) =>
        new(items, total, page, pageSize);
}