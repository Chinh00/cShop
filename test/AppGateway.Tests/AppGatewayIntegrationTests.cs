using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace AppGateway.Tests;

[TestFixture]
public class AppGatewayIntegrationTests
{
    [Test]
    public async Task ProxyRoute_ShouldForwardRequestAndApplyPathTransforms()
    {
        await using var downstreamApp = BuildDownstreamApp();
        await downstreamApp.StartAsync();

        var destinationAddress = downstreamApp.Urls.Single().TrimEnd('/') + "/";
        await using var factory = CreateFactory(destinationAddress);
        using var client = factory.CreateClient();

        using var response = await client.GetAsync("/proxy-test/catalog/items?id=42");
        response.EnsureSuccessStatusCode();

        var forwardedPath = await response.Content.ReadAsStringAsync();
        Assert.That(forwardedPath, Is.EqualTo("/catalog/items?id=42"));
    }

    private static WebApplication BuildDownstreamApp()
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            EnvironmentName = "Testing"
        });

        builder.WebHost.UseUrls("http://127.0.0.1:0");
        var app = builder.Build();
        app.Map("/{**catch-all}", (HttpRequest request) => Results.Text($"{request.Path}{request.QueryString}"));
        return app;
    }

    private static WebApplicationFactory<Program> CreateFactory(string destinationAddress)
    {
        return new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((_, configurationBuilder) =>
                {
                    configurationBuilder.Sources.Clear();
                    configurationBuilder.AddInMemoryCollection(new Dictionary<string, string?>
                    {
                        ["ReverseProxy:Routes:test-route:ClusterId"] = "test-cluster",
                        ["ReverseProxy:Routes:test-route:Match:Path"] = "/proxy-test/{**catch-all}",
                        ["ReverseProxy:Routes:test-route:Transforms:0:PathRemovePrefix"] = "/proxy-test",
                        ["ReverseProxy:Routes:test-route:Transforms:1:PathPrefix"] = "/",
                        ["ReverseProxy:Clusters:test-cluster:Destinations:destination1:Address"] = destinationAddress
                    });
                });
            });
    }
}
