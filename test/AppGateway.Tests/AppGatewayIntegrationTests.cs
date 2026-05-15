using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Yarp.ReverseProxy.Configuration;

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
        await using var proxyApp = await BuildProxyApp(destinationAddress);
        using var client = proxyApp.GetTestClient();

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

    private static async Task<WebApplication> BuildProxyApp(string destinationAddress)
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            EnvironmentName = "Testing"
        });

        builder.WebHost.UseTestServer();
        builder.Services
            .AddReverseProxy()
            .LoadFromMemory(
                [
                    new RouteConfig
                    {
                        RouteId = "test-route",
                        ClusterId = "test-cluster",
                        Match = new RouteMatch { Path = "/proxy-test/{**catch-all}" },
                        Transforms =
                        [
                            new Dictionary<string, string> { ["PathRemovePrefix"] = "/proxy-test" },
                            new Dictionary<string, string> { ["PathPrefix"] = "/" }
                        ]
                    }
                ],
                [
                    new ClusterConfig
                    {
                        ClusterId = "test-cluster",
                        Destinations = new Dictionary<string, DestinationConfig>
                        {
                            ["destination1"] = new() { Address = destinationAddress }
                        }
                    }
                ]);

        var app = builder.Build();
        app.UseWebSockets();
        app.UseRouting();
        app.MapReverseProxy();
        await app.StartAsync();
        return app;
    }
}
