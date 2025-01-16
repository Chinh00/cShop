using Application.UseCases.Dtos;
using AutoMapper;
using Domain;

namespace Infrastructure.ConfigMapper;

public class ConfigMapper : Profile
{
    public ConfigMapper()
    {
        CreateMap<Order, OrderDto>();
        CreateMap<OrderDetail, OrderDetailDto>();

    }
}