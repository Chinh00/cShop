using cShop.Contracts.Events.DomainEvents;
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
    

    public void CreateProduct(string name, float currentCost, string imageSrc, Guid categoryId)
    {
        Name = name;
        CurrentCost = currentCost;
        ImageSrc = imageSrc;
        CategoryId = categoryId;
        
        RaiseEvent(version => new ProductCreatedDomainEvent(Id, name, currentCost, imageSrc, CategoryId, (int)version));
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