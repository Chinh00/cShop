using cShop.Core.Domain;
using Microsoft.AspNetCore.Identity;

namespace Domain;

public class User : AggregateBase
{
    public string UserName { get; set; }
    public string Avatar { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    
    
    public override void ApplyEvent(IDomainEvent @event)
    {
        throw new NotImplementedException();
    }
}