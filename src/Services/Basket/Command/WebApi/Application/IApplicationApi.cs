namespace WebApi.Application;

public interface IApplicationApi
{
    public Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default);
}