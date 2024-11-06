using cShop.Contracts.Services.Catalog;
using cShop.Core.Domain;
using Domain.Entities;

namespace Domain.Aggregate;

public class Product : AggregateBase
{
    
    public string Name { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string ImageUrl { get; set; }
    
    public bool IsActive { get; set; }
    
    public Guid? CategoryId { get; set; }
    
    public virtual Category Category { get; set; }


    public void AssignCategory(Category category)
    {
        CategoryId = category.Id;
        RaiseEvent(version => new DomainEvents.CategoryAssigned(category.Id, version));
    }
    

    public void CreateCatalog(Command.CreateCatalog command)
    {
        Name = command.Name;
        Quantity = command.Quantity;
        Price = command.Price;
        ImageUrl = command.ImageSrc;
        CategoryId = command.CategoryId;
        IsActive = false;
        RaiseEvent(version => new DomainEvents.CatalogCreated(Id, Name, Quantity, Price, ImageUrl, CategoryId , IsActive, version));
    }

    public void ActiveCatalog()
    {
        IsActive = true;
        RaiseEvent(version => new DomainEvents.CatalogActivated(Id, version));
    }
    
    public void InActiveCatalog()
    {
        IsActive = false;
        RaiseEvent(version => new DomainEvents.CatalogInactivated(Id, version));
    }



    public override void ApplyEvent(IDomainEvent @event) => With(@event as dynamic);


    void With(DomainEvents.CatalogActivated @event)
    {
        IsActive = true;
        Version = @event.Version;
    }
    void With(DomainEvents.CatalogInactivated @event)
    {
        IsActive = false;
        Version = @event.Version;
    }
    

    public void With(DomainEvents.CatalogCreated @event)
    {
        Id = @event.Id;
        Name = @event.Name;
        Quantity = @event.Quantity;
        Price = @event.Price;
        ImageUrl = @event.ImageUrl;
        CategoryId = @event.CategoryId;
        IsActive = @event.IsActive;
        
        Version = @event.Version;
    }
    
}