using MediatR;

namespace WebApi.Application;

public static class ApplicationApi
{


    public static async Task<TResponse> Send<TRequest, TResponse>(ISender sender, TRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request, cancellationToken);
        return (TResponse)result;
    }
}