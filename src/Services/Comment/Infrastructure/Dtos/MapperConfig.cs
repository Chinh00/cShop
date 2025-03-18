using AutoMapper;
using Domain;

namespace Infrastructure.Dtos;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<CommentLine, CommentLineDto>();
    }
}