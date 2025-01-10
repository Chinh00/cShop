using cShop.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Domain;

[Index(nameof(TransactionId))]
[Index(nameof(PaymentDate))]
public class Payment : EntityBase
{
    public Guid TransactionId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentStatus Status { get; set; }
    
    public string Description { get; set; }
    
    
    public Guid OrderInfoId { get; set; }
    public virtual OrderInfo OrderInfo { get; set; }
}