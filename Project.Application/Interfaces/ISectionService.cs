using Project.Application.Dto;

namespace Project.Application.Interfaces
{
    public interface ISectionService
    {
        Task<IEnumerable<SectionDto>> GetSectionsAsync(int pageId);
        Task<int> CreateAsync(SectionDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
