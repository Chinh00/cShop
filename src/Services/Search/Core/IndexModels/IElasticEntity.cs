namespace Core.IndexModels;

public interface IElasticEntity<TEntityKey>
{
    TEntityKey Id { get; }
}