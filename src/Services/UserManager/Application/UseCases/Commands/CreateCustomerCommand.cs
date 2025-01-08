
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

public record CreateCustomerCommand(string Name , string PhoneNumber, string Email, string Password) : ICommand<IResult>
{
    public class Validator : AbstractValidator<CreateCustomerCommand>
    {
        public Validator()
        {
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.PhoneNumber).NotEmpty();
            RuleFor(e => e.Email);
            RuleFor(e => e.Password).NotEmpty();
        }
    }
    
    internal class Handler(
        ISchemaRegistryClient schemaRegistryClient,
        IRepository<CustomerOutbox> repository,
        IRepository<Customer> userRepository) : OutboxHandler<CustomerOutbox>(schemaRegistryClient, repository),
        IRequestHandler<CreateCustomerCommand, IResult>
    {
        public async Task<IResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var (name, phoneNumber, email, password) = request;
            var user = new Customer()
            {
                Name = name,
                PhoneNumber = phoneNumber,
                Email = email,
            };
            var userCreatedIntegrationEvent = new CustomerCreatedIntegrationEvent()
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                PhoneNumber = phoneNumber,
                Email = email,
                Password = password
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