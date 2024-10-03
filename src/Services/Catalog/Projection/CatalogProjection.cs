namespace Projection;

public class CatalogProjection : ProjectionBase
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string ImageSrc { get; set; }
    
    public bool IsActive { get; set; }
    
}

