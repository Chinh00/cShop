using cShop.Contracts.Events.DomainEvents;
using cShop.Contracts.Services.Catalog;
using cShop.Core.Domain;
using Domain.Enumrations;

namespace Domain.Entities;

public class Product : AggregateBase
{
    public string Name { get; set; }
    public float CurrentCost { get; set; }
    public string ImageSrc { get; set; }
    
    public ProductStatus Status { get; set; } 
    
    public Guid? CategoryId { get; set; }
    
    public virtual Category Category { get; set; }
    

    public void CreateProduct(Command.CreateCatalog createCatalog)
    {
        Name = createCatalog.Name;
        CurrentCost = createCatalog.Price;
        ImageSrc = createCatalog.ImageSrc;
        CategoryId = createCatalog.CategoryId;
        RaiseEvent(version => new ProductCreatedDomainEvent(Id, Name, CurrentCost, ImageSrc, CategoryId, version));
    }

    public void UpdateProductName(string name)
    {
        Name = name;
        RaiseEvent(version => new ProductNameUpdatedDomainEvent(Id, name, (int)version));
    }

    public void With(ProductCreatedDomainEvent @event)
    {
        (Id, Name, CurrentCost, ImageSrc, CategoryId, Version) = @event;
    }

    public void With(ProductNameUpdatedDomainEvent @event)
    {
        (Id, Name, Version) = @event;
    }
    
    public override void ApplyEvent(IDomainEvent @event) => With(@event as dynamic);   
    
    
}