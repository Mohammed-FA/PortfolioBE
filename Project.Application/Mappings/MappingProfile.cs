using AutoMapper;
using Project.Application.Dto;
using Project.Domain.Entities;

namespace Project.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserModel, UserDto>();
        }
    }
}
