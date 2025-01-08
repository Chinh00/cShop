namespace Application.UseCases.Dtos;

public record CatalogItemDto
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public List<CatalogPictureDto> Pictures { get; init; } = new();
    public string? Description { get; set; }
    
    public bool IsActive { get; set; }
    
    public Guid? CatalogTypeId { get; set; }
    
    public virtual CatalogTypeDto CatalogType { get; set; }
    
    public Guid? CatalogBrandId { get; set; }
    
    public virtual CatalogBrandDto CatalogBrand { get; set; }
}