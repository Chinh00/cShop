using Application.UseCases.Dtos;
using AutoMapper;
using Domain;

namespace Infrastructure.MapperConfig;

public sealed class UserConfigMapper : Profile
{
    public UserConfigMapper()
    {
        CreateMap<Customer, CustomerDto>();
        CreateMap<Shipper, ShipperDto>();
    }
}