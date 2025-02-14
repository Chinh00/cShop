using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using Google.Apis.Auth;
using MassTransit;

namespace Identity.Api;

public class ExternalGrantValidator(
    UserManager<ApplicationUser> userManager,
    ITopicProducer<UserCreatedIntegrationEvent> userCreatedIntegrationEventProducer)
    : IExtensionGrantValidator
{

    public async Task ValidateAsync(ExtensionGrantValidationContext context)
    {
        var provider = context.Request.Raw["provider"];
        var token = context.Request.Raw["token"];

        if (string.IsNullOrWhiteSpace(provider) || string.IsNullOrWhiteSpace(token))
        {
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid token");
            return;
        }
        // Verify id_token after client login google 
        var payload = await GoogleJsonWebSignature.ValidateAsync(token);
        if (payload == null)
        {
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid token");
            return;
        }
        // User already 
        var user = await userManager.FindByEmailAsync(payload.Email);
        if (user == null)
        {
            user = new ApplicationUser() { UserName = payload.Email, Email = payload.Email };
            var result = await userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User creation failed");
                return;
            }

            await userCreatedIntegrationEventProducer.Produce(new { UserId = user.Id });
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email.ToString())
        };

        context.Result = new GrantValidationResult(
            subject: user.Id.ToString(),
            authenticationMethod: provider,
            claims: claims,
            identityProvider: provider,
            customResponse: new Dictionary<string, object>()
        );
    }

    public string GrantType => "external";
}