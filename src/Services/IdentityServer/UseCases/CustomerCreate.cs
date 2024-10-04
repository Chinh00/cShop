using System.Text.Json;
using cShop.Contracts.Services.IdentityServer;
using IdentityServer.Data.Domain;
using MassTransit;

namespace IdentityServer.UseCases;

public class CustomerCreate
{
    public record Command(CustomerCreateModel Model) : ICommand<Guid>
    {
        public class Handler : IRequestHandler<Command, ResultModel<Guid>>
        {
            private readonly UserManager<User> _userManager;
            private readonly ITopicProducer<IntegrationEvent.CustomerCreatedIntegration> _integrationEventProducer;

            public Handler(UserManager<User> userManager, ITopicProducer<IntegrationEvent.CustomerCreatedIntegration> integrationEventProducer)
            {
                _userManager = userManager;
                _integrationEventProducer = integrationEventProducer;
            }

            public async Task<ResultModel<Guid>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = new User()
                {
                    UserName = request.Model.Username,
                    Email = $"{request.Model.Username}ascas@gmail.com"
                };
                var result = await _userManager.CreateAsync(user, request.Model.Password);
                if (!result.Succeeded)
                {
                    return ResultModel<Guid>.Create(Guid.NewGuid(), true, JsonSerializer.Serialize(result.Errors));
                }
                await _integrationEventProducer.Produce(new IntegrationEvent.CustomerCreatedIntegration(user.Id), cancellationToken);
                return ResultModel<Guid>.Create(user.Id);
            }
        }
    }
}