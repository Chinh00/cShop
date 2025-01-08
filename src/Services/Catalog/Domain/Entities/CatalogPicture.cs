using cShop.Core.Domain;
using Domain.Aggregate;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;
[Index(nameof(PictureUrl))]
public class CatalogPicture : EntityBase
{
    public Guid CatalogItemId { get; set; }
    public virtual CatalogItem CatalogItem { get; set; }
    public string PictureUrl { get; set; }
    public string Description { get; set; }
}