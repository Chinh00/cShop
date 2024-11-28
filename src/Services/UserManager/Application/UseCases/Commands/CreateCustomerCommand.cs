
using Confluent.SchemaRegistry;
using cShop.Core.Domain;
using cShop.Core.Repository;
using cShop.Infrastructure.SchemaRegistry;
using Domain;
using Domain.Outboxs;
using FluentValidation;
using IntegrationEvents;
using MediatR;

namespace Application.UseCases.Commands;

public record CreateCustomerCommand(string UserName, string Email) : ICommand<IResult>
{
    public class Validator : AbstractValidator<CreateCustomerCommand>
    {
        public Validator()
        {
            RuleFor(e => e.UserName).NotEmpty();
            RuleFor(e => e.Email).NotEmpty();
        }
    }
    
    internal class Handler(ISchemaRegistryClient schemaRegistryClient, IRepository<UserOutbox> repository, IRepository<User> userRepository) : OutboxHandler<UserOutbox>(schemaRegistryClient, repository), IRequestHandler<CreateCustomerCommand, IResult>
    {
        
        
        
        
        
        public async Task<IResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                UserName = request.UserName,
                Email = request.Email,
            };
            var userCreatedIntegrationEvent = new CustomerCreatedIntegrationEvent()
            {
                Id = user.Id.ToString(),
                Name = user.UserName
            };
            
            
            await userRepository.AddAsync(user, cancellationToken);
            await SendToOutboxAsync(user, () => (
                new UserOutbox(),
                userCreatedIntegrationEvent,
                "user_cdc_events"
                ), cancellationToken);

            return Results.Ok(user);

        }
    }
}