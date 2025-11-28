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

            CreateMap<Websites, WebsiteDto>().ReverseMap();

            // Page
            CreateMap<PageModel, PageDto>().ReverseMap();

            // Section
            CreateMap<SectionModel, SectionDto>().ReverseMap();
        }
    }
}
