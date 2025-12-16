using AutoMapper;
using Project.Application.Dto;
using Project.Domain.Entities;

namespace Project.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserModel, UserDto>()
                    .ForMember(dest => dest.Imageurl, opt => opt.MapFrom(src => src.ImageUrl))
                    .ForMember(dest => dest.Websites, opt => opt.MapFrom(src => src.Websites));


            CreateMap<Websites, WebsitDto>();
        }
    }
}
