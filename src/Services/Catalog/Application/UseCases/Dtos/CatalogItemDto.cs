namespace Application.UseCases.Dtos;

public record CatalogItemDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    
    public bool IsActive { get; set; }
    
    public Guid? CatalogTypeId { get; set; }
    
    public virtual CatalogTypeDto CatalogType { get; set; }
    
    public Guid? CatalogBrandId { get; set; }
    
    public virtual CatalogBrandDto CatalogBrand { get; set; }
}