using Application.UseCases.Dtos;
using AutoMapper;
using Domain.Aggregate;
using Domain.Entities;

namespace Infrastructure.ConfigMapper;

public sealed class CatalogConfigMapper : Profile
{
    public CatalogConfigMapper()
    {
        CreateMap<CatalogItem, CatalogItemDto>();
        CreateMap<CatalogBrand, CatalogBrandDto>();
        CreateMap<CatalogType, CatalogTypeDto>();
        CreateMap<CatalogPicture, CatalogPictureDto>();

    }
}