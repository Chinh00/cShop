using MediatR;

namespace Application.UseCases.Command;

public abstract record Commands
{
    public record CreateBasket() : IRequest<Guid>
    {
        
    }
}