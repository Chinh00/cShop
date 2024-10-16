using Asp.Versioning.Builder;
using IdentityServer.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Apis;

public static class IdentityServerApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/identity";
    public static WebApplication MapIdentityServerApiV1(this WebApplication application, Action<WebApplication>? action = null)
    {
        var versionedApi = application.NewVersionedApi("IdentityServer");

        var group = versionedApi.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost("/register", async ([FromServices]ISender sender,[FromBody] CustomerCreateModel model) => Results.Ok(await sender.Send(new CustomerCreate.Command(model))));
        
        
        action?.Invoke(application);
        return application;
    } 
}