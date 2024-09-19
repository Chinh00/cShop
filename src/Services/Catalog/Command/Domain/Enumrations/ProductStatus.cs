using System.ComponentModel;

namespace Domain.Enumrations;

public enum ProductStatus
{
    [Description("Product is active")]
    Active,
    [Description("Product is deleted")]
    Inactive,
}