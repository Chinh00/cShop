using Confluent.SchemaRegistry;
using cShop.Core.Domain;
using cShop.Core.Repository;
using cShop.Infrastructure.SchemaRegistry;
using FluentValidation;
using Identity.Api.Models;
using Identity.Api.Services;
using IntegrationEvents;
using MassTransit;
using MediatR;

namespace Identity.Api.Application.Commands;

public record CreateUserCommand(string Username, string Email, string Password) : ICommand<IResult>
{
    public class Validator : AbstractValidator<CreateUserCommand>
    {
        public Validator()
        {
            RuleFor(e => e.Username).NotEmpty();
            RuleFor(e => e.Email).NotEmpty();
            RuleFor(e => e.Password).NotEmpty();
        }
    }
    
    internal class Handler(UserManager userManager, ITopicProducer<UserCreatedIntegrationEvent> userCreatedIntegrationEvent) : IRequestHandler<CreateUserCommand, IResult>
    {

        public async Task<IResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser()
            {
                UserName = request.Username,
                Email = request.Email,
            };
            await userManager.CreateAsync(user, request.Password);
            await userCreatedIntegrationEvent.Produce(new { UserId = user.Id, Username = user.UserName }, cancellationToken);
            return Results.Ok(user);
        }
    }
    
}