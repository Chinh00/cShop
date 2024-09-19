using cShop.Core.Domain;

namespace Domain.Entities;

public class Category : AggregateBase
{
    
    public string Name { get; set; }
    public string PictureSrc { get; set; }
    public override void ApplyEvent(IDomainEvent @event)
    {
        throw new NotImplementedException();
    }
}