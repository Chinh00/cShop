using System.Text;
using Application.UseCases.Queries;
using Infrastructure.Catalog;
using Infrastructure.Constants;
using Nest;

namespace Search.Application.Tests;

[TestFixture]
public class GetCatalogsQueryHandlerTests
{
    [Test]
    public async Task Handle_ShouldReturnCatalogsAndMetadataFromElasticResponse()
    {
        var catalogId = Guid.NewGuid();
        var typeId = Guid.NewGuid();
        var brandId = Guid.NewGuid();

        var responseJson =
            $$"""
              {
                "took": 1,
                "timed_out": false,
                "_shards": { "total": 1, "successful": 1, "skipped": 0, "failed": 0 },
                "hits": {
                  "total": { "value": 1, "relation": "eq" },
                  "max_score": 1.0,
                  "hits": [
                    {
                      "_index": "catalogs",
                      "_id": "{{catalogId}}",
                      "_score": 1.0,
                      "_source": {
                        "id": "{{catalogId}}",
                        "catalogName": "Phone",
                        "price": 1200,
                        "description": "Smart phone",
                        "catalogTypeId": "{{typeId}}",
                        "catalogBrandId": "{{brandId}}",
                        "catalogTypeName": "Device",
                        "catalogBrandName": "Contoso",
                        "pictures": ["https://cdn/image-1.jpg"]
                      }
                    }
                  ]
                }
              }
            """;

        var requestBody = string.Empty;
        var handler = new GetCatalogsQueryHandler(CreateElasticClient(responseJson, body => requestBody = body));

        var query = new GetCatalogsQuery
        {
            q = "phone",
            Page = 1,
            PageSize = 10
        };

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.IsError, Is.False);
            Assert.That(result.Data.Total, Is.EqualTo(1));
            Assert.That(result.Data.Page, Is.EqualTo(1));
            Assert.That(result.Data.PageSize, Is.EqualTo(10));
            Assert.That(result.Data.Items, Has.Count.EqualTo(1));
            Assert.That(result.Data.Items[0].CatalogName, Is.EqualTo("Phone"));
            Assert.That(result.Data.Items[0].CatalogBrandName, Is.EqualTo("Contoso"));
            Assert.That(requestBody, Does.Contain("multi_match"));
            Assert.That(requestBody, Does.Contain("phone"));
        });
    }

    [Test]
    public async Task Handle_ShouldIncludeFiltersAndPaginationInElasticRequest()
    {
        var typeId = Guid.NewGuid();
        var brandId = Guid.NewGuid();

        var responseJson =
            """
            {
              "took": 1,
              "timed_out": false,
              "_shards": { "total": 1, "successful": 1, "skipped": 0, "failed": 0 },
              "hits": {
                "total": { "value": 0, "relation": "eq" },
                "max_score": null,
                "hits": []
              }
            }
            """;

        var requestBody = string.Empty;
        var requestPath = string.Empty;
        var handler = new GetCatalogsQueryHandler(CreateElasticClient(responseJson, body => requestBody = body, path => requestPath = path));

        var query = new GetCatalogsQuery
        {
            q = "laptop",
            CatalogTypeId = typeId,
            CatalogBrandId = brandId,
            Page = 2,
            PageSize = 5
        };

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.Data.Items, Is.Empty);
            Assert.That(requestPath, Does.Contain($"/{IndexConstants.CatalogIndex}/"));
            Assert.That(requestBody, Does.Contain("multi_match"));
            Assert.That(requestBody, Does.Contain("catalogTypeId.keyword"));
            Assert.That(requestBody, Does.Contain(typeId.ToString()));
            Assert.That(requestBody, Does.Contain("catalogBrandId.keyword"));
            Assert.That(requestBody, Does.Contain(brandId.ToString()));
            Assert.That(requestBody, Does.Contain("\"from\":10"));
            Assert.That(requestBody, Does.Contain("\"size\":5"));
        });
    }

    private static IElasticClient CreateElasticClient(string responseJson, Action<string> onRequestBody, Action<string>? onRequestPath = null)
    {
        var responseBytes = Encoding.UTF8.GetBytes(responseJson);
        var connection = new InMemoryConnection(responseBytes, 200, null);

        var settings = new ConnectionSettings(connection)
            .DefaultIndex(IndexConstants.CatalogIndex)
            .DisableDirectStreaming()
            .OnRequestCompleted(callDetails =>
            {
                if (callDetails.RequestBodyInBytes is not null)
                {
                    onRequestBody(Encoding.UTF8.GetString(callDetails.RequestBodyInBytes));
                }

                onRequestPath?.Invoke(callDetails.Uri.PathAndQuery);
            });

        return new ElasticClient(settings);
    }
}
