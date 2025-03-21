using cShop.Contracts.Services.Catalog;
using cShop.Core.Domain;
using Domain.Entities;

namespace Domain.Aggregate;


public class CatalogItem : AggregateBase
{
    
    public string Name { get; set; }
    public decimal Price { get; set; }
    
    public virtual ICollection<CatalogPicture> Pictures { get; set; }
    
    public bool IsActive { get; set; }
    
    public string Description { get; set; }
    
    public Guid? CatalogTypeId { get; set; }
    
    public virtual CatalogType CatalogType { get; set; }
    
    public Guid? CatalogBrandId { get; set; }
    
    public virtual CatalogBrand CatalogBrand { get; set; }
    
    
    
    public int AvailableStock { get; set; }

    public bool IsAvailable(int quantity)
    {
        return AvailableStock >= quantity;
    }


    public void RemoveStock(int quantity)
    {

        if (quantity <= 0) throw new DomainException("Quantity must be greater than zero");
        
        if (AvailableStock < quantity) throw new DomainException("Quantity cannot be less than available stock");
        
        if (AvailableStock - quantity < 0) throw new DomainException("Quantity cannot be less than available stock");
        
        AvailableStock -= quantity;
    }
    
    

    public void AssignCatalogType(CatalogType catalogType)
    {
        CatalogType = catalogType;
    }
    

    
    
    public void AssignCatalogBrand(CatalogBrand catalogBrand)
    {
        CatalogBrand = catalogBrand;
    }
    

    public void CreateCatalog(string name, int quantity, decimal price, string description, List<string> pictures)
    {
        Name = name;
        AvailableStock = quantity;
        Price = price;
        Description = description;  
        Pictures = pictures.Select(e => new CatalogPicture()
        {
            PictureUrl = e,
            Description = e
        }).ToList();
        IsActive = false;
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
        CatalogTypeId = @event.CategoryTypeId;
        CatalogBrandId = @event.CategoryBrandId;
        IsActive = @event.IsActive;
        
        Version = @event.Version;
    }
    
}