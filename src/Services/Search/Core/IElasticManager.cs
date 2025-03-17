using Core.IndexModels;

namespace Core;

public interface IElasticManager
{
    Task CreateIndexAsync<T, TKey>(string indexName) where T : ElasticEntity<TKey>;
    Task AddOrUpdateAsync<T, TKey>(string indexName, T model) where T : ElasticEntity<TKey>;
    Task DeleteIndexAsync(string indexName);
    
}