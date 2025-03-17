using Core;
using Core.IndexModels;
using Nest;

namespace Infrastructure;

public class ElasticManager(IElasticClient elasticClient) : IElasticManager
{
    

    public async Task CreateIndexAsync<T, TKey>(string indexName) where T : ElasticEntity<TKey>
    {
        var indexExit = await elasticClient.Indices.ExistsAsync(indexName);
        if (indexExit.Exists) return;
        
        await elasticClient.Indices.CreateAsync(indexName,
            dd => dd.Index(indexName).Settings(s => s.NumberOfShards(1).NumberOfReplicas(0)));
    }

    public async Task AddOrUpdateAsync<T, TKey>(string indexName, T model) where T : ElasticEntity<TKey>
    {
        var entityExits = await elasticClient.DocumentExistsAsync(DocumentPath<T>.Id(model), dd => dd.Index(indexName));
        if (entityExits.Exists)
        {
            await elasticClient.UpdateAsync(DocumentPath<T>.Id(model), d => d.Index(indexName).Doc(model).RetryOnConflict(3));
        }
        else
        {
            var result = await elasticClient.IndexAsync(model, d => d.Index(indexName));
            Console.WriteLine(result);

            if (result.ServerError is null)
            {
                return;
            }


        }
    }

    public Task DeleteIndexAsync(string indexName)
    {
        throw new NotImplementedException();
    }
}