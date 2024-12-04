
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

public record CreateCustomerCommand(string Name, string Email) : ICommand<IResult>
{
    public class Validator : AbstractValidator<CreateCustomerCommand>
    {
        public Validator()
        {
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.Email).NotEmpty();
        }
    }
    
    internal class Handler(ISchemaRegistryClient schemaRegistryClient, IRepository<CustomerOutbox> repository, IRepository<Customer> userRepository) : OutboxHandler<CustomerOutbox>(schemaRegistryClient, repository), IRequestHandler<CreateCustomerCommand, IResult>
    {
        
        
        
        
        
        public async Task<IResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var user = new Customer()
            {
                Name = request.Name,
                Email = request.Email,
            };
            var userCreatedIntegrationEvent = new CustomerCreatedIntegrationEvent()
            {
                Id = user.Id.ToString(),
                Name = user.Name
            };
            
            
            await userRepository.AddAsync(user, cancellationToken);
            await SendToOutboxAsync(user, () => (
                new CustomerOutbox(),
                userCreatedIntegrationEvent,
                "customer_cdc_events"
                ), cancellationToken);

            return Results.Ok(ResultModel<Customer>.Create(user));

        }
    }
}