using MediatR;

namespace WebApi;

public class RequestDispatcher : IRequestHandler<IRequest<object>, object>
{
    private readonly ISender _mediator;

    public RequestDispatcher(ISender mediator)
    {
        _mediator = mediator;
    }

    

    public async Task<object> Handle(IRequest<object> request, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(request, cancellationToken));
    }
}