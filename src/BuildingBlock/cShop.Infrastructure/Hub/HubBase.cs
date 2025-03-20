using Microsoft.AspNetCore.SignalR;

namespace cShop.Infrastructure.Hub;

public class HubBase : Microsoft.AspNetCore.SignalR.Hub
{
    public virtual void AddGroup(HubCallerContext context)
    {
    }
    
    
    
}