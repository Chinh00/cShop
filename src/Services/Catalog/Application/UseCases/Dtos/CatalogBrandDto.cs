namespace Application.UseCases.Dtos;

public record CatalogBrandDto
{
    public Guid Id { get; init; }
    public string BrandName { get; set; }
    public string Description { get; set; }
}