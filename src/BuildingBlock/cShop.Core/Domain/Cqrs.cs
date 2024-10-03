using MediatR;

namespace cShop.Core.Domain;

public interface ICommand<TResponse> : IRequest<ResultModel<TResponse>>
    where TResponse : notnull
{
    
}

public interface ICreateCommand<TModel, TResponse> : ICommand<TResponse>   
{
    public TModel Model { get; }
}



public record ResultModel<TData>(TData Data, bool IsError, string Message)
{
    public static ResultModel<TData> Create(TData data, bool isError = false, string message = default) => new(data, isError, message);
}