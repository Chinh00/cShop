using cShop.Contracts.Services.Catalog;
using cShop.Core.Domain;
using Domain.Entities;

namespace Domain.Aggregate;

public class CatalogItem : AggregateBase
{
    
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    
    public bool IsActive { get; set; }
    
    public Guid? CategoryTypeId { get; set; }
    
    public virtual CatalogType CatalogType { get; set; }
    
    public Guid? CatalogBrandId { get; set; }
    
    public virtual CatalogBrand CatalogBrand { get; set; }
    
    
    
    public int AvailableStock { get; set; }


    public void RemoveStock(int quantity)
    {

        if (quantity <= 0) throw new DomainException("Quantity must be greater than zero");
        
        if (AvailableStock < quantity) throw new DomainException("Quantity cannot be less than available stock");
        
        if (AvailableStock - quantity < 0) throw new DomainException("Quantity cannot be less than available stock");
        
        AvailableStock -= quantity;
    }
    
    

    public void AssignCategory(CatalogType catalogType)
    {
        CategoryTypeId = catalogType.Id;
        RaiseEvent(version => new DomainEvents.CategoryAssigned(catalogType.Id, version));
    }
    

    public void CreateCatalog(Command.CreateCatalog command)
    {
        Name = command.Name;
        AvailableStock = command.Quantity;
        Price = command.Price;
        ImageUrl = command.ImageSrc;
        CategoryTypeId = command.CategoryId;
        IsActive = false;
        RaiseEvent(version => new DomainEvents.CatalogCreated(Id, Name, AvailableStock, Price, ImageUrl, CategoryTypeId , IsActive, version));
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
        AvailableStock = @event.Quantity;
        Price = @event.Price;
        ImageUrl = @event.ImageUrl;
        CategoryTypeId = @event.CategoryId;
        IsActive = @event.IsActive;
        
        Version = @event.Version;
    }
    
}