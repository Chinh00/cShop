using System.Net.Http.Json;
using Application.UseCases.Queries;
using Asp.Versioning;
using cShop.Core.Domain;
using Infrastructure.Catalog;
using MediatR;
using WebApi.Apis;

namespace Search.WebApi.IntegrationTests;

[TestFixture]
public class SearchApiTests
{
    [Test]
    public async Task GetCatalogsEndpoint_ShouldBindQueryAndReturnSenderResult()
    {
        var sender = new FakeSender
        {
            Handler = request =>
            {
                var result = ResultModel<ListResultModel<CatalogIndexModel>>.Create(
                    ListResultModel<CatalogIndexModel>.Create(
                    [
                        new CatalogIndexModel
                        {
                            Id = Guid.NewGuid(),
                            CatalogName = "Headphone",
                            Description = "Wireless",
                            CatalogBrandName = "Contoso",
                            CatalogTypeName = "Audio",
                            CatalogTypeId = Guid.NewGuid(),
                            CatalogBrandId = Guid.NewGuid(),
                            Pictures = ["https://cdn/image.jpg"]
                        }
                    ], 1, 2, 5));
                return Task.FromResult((object?)result);
            }
        };

        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseUrls("http://127.0.0.1:0");
        builder.Services
            .AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1);
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        builder.Services.AddSingleton<ISender>(sender);

        await using var app = builder.Build();
        app.NewVersionedApi("Search").MapSearchApiV1();

        await app.StartAsync();

        try
        {
            using var client = new HttpClient { BaseAddress = new Uri(app.Urls.Single()) };

            var response = await client.GetAsync("/api/v1/search/catalogs?q=head&page=2&pageSize=5");
            var body = await response.Content.ReadFromJsonAsync<ResultModel<ListResultModel<CatalogIndexModel>>>();

            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccessStatusCode, Is.True);
                Assert.That(body, Is.Not.Null);
                Assert.That(body!.Data.Items, Has.Count.EqualTo(1));
                Assert.That(body.Data.Total, Is.EqualTo(1));
                Assert.That(body.Data.Page, Is.EqualTo(2));
                Assert.That(body.Data.PageSize, Is.EqualTo(5));
                Assert.That(sender.LastRequest, Is.TypeOf<GetCatalogsQuery>());
            });

            var request = (GetCatalogsQuery)sender.LastRequest!;
            Assert.Multiple(() =>
            {
                Assert.That(request.q, Is.EqualTo("head"));
                Assert.That(request.Page, Is.EqualTo(2));
                Assert.That(request.PageSize, Is.EqualTo(5));
            });
        }
        finally
        {
            await app.StopAsync();
        }
    }

    private sealed class FakeSender : ISender
    {
        public Func<object, Task<object?>> Handler { get; init; } = _ => Task.FromResult<object?>(null);
        public object? LastRequest { get; private set; }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            LastRequest = request;
            var response = await Handler(request);
            return (TResponse)response!;
        }

        public Task<object?> Send(object request, CancellationToken cancellationToken = default)
        {
            LastRequest = request;
            return Handler(request);
        }
    }
}
