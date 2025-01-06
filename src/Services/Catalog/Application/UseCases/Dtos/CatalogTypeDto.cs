namespace Application.UseCases.Dtos;

public record CatalogTypeDto
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public string Description { get; set; }
}