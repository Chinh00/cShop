using cShop.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Domain;
[Index(nameof(PaymentId))]
[Index(nameof(UserId))]

public class OrderInfo : EntityBase
{
    public Guid OrderId { get; set; }
    public Guid PaymentId { get; set; }
    public virtual Payment Payment { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public Guid UserId { get; set; }
}