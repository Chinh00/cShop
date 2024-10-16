using System.Text.Json;
using cShop.Contracts.Services.IdentityServer;
using cShop.Core.Domain;
using IdentityServer.Data.Domain;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.UseCases;

public class CustomerCreate
{
    public record Command(CustomerCreateModel Model) : ICommand<IResult>
    {
        public class Handler : IRequestHandler<Command, IResult>
        {
            private readonly UserManager<User> _userManager;
            private readonly ITopicProducer<IntegrationEvent.CustomerCreatedIntegration> _integrationEventProducer;

            public Handler(UserManager<User> userManager, ITopicProducer<IntegrationEvent.CustomerCreatedIntegration> integrationEventProducer)
            {
                _userManager = userManager;
                _integrationEventProducer = integrationEventProducer;
            }

            public async Task<IResult> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = new User()
                {
                    UserName = request.Model.Username,
                    Email = $"{request.Model.Username}ascas@gmail.com"
                };
                var result = await _userManager.CreateAsync(user, request.Model.Password);
                if (!result.Succeeded)
                {
                    return Results.Ok(ResultModel<Guid>.Create(Guid.NewGuid(), true, JsonSerializer.Serialize(result.Errors)));
                }
                await _integrationEventProducer.Produce(new IntegrationEvent.CustomerCreatedIntegration(user.Id), cancellationToken);
                return Results.Ok(ResultModel<Guid>.Create(user.Id));
            }
        }
    }
}