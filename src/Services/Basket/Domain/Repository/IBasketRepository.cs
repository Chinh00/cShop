namespace Domain.Repository;

public interface IBasketRepository
{
    Task<T> AddBasketItemAsync<T>(T entity, CancellationToken cancellationToken);
}