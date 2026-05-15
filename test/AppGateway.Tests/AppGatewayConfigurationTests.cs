using Microsoft.Extensions.Configuration;

namespace AppGateway.Tests;

[TestFixture]
public class AppGatewayConfigurationTests
{
    [Test]
    public void ReverseProxyRoutes_ShouldMapToDefinedClustersWithDestinationAddress()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "ProxyAppGateway.appsettings.json"), optional: false)
            .Build();

        var routes = configuration.GetSection("ReverseProxy:Routes").GetChildren().ToList();
        var clusters = configuration.GetSection("ReverseProxy:Clusters")
            .GetChildren()
            .ToDictionary(cluster => cluster.Key, cluster => cluster);

        Assert.That(routes, Is.Not.Empty, "No proxy routes were found.");

        foreach (var route in routes)
        {
            var clusterId = route["ClusterId"];
            Assert.That(clusterId, Is.Not.Null.And.Not.Empty, $"Route '{route.Key}' is missing a ClusterId.");
            Assert.That(clusters.ContainsKey(clusterId!), Is.True, $"Route '{route.Key}' points to missing cluster '{clusterId}'.");

            var destinationAddress = clusters[clusterId!]
                .GetSection("Destinations")
                .GetChildren()
                .FirstOrDefault()?["Address"];

            Assert.That(destinationAddress, Is.Not.Null.And.Not.Empty,
                $"Cluster '{clusterId}' must have at least one destination address.");
        }
    }
}
