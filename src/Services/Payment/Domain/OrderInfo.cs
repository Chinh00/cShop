using cShop.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Domain;
[Index(nameof(UserId))]
[Index(nameof(TransactionId))]
public class OrderInfo : EntityBase
{
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public Guid UserId { get; set; }
    public Guid? TransactionId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public DateTime? PaymentDate { get; set; }
    public PaymentStatus Status { get; set; }
}