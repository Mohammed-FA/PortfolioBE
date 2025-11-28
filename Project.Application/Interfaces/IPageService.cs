using Project.Application.Dto;

namespace Project.Application.Interfaces
{
    public interface IPageService
    {
        Task<IEnumerable<PageDto>> GetPagesAsync(long websiteId);
        Task<PageDto?> GetByIdAsync(long id);

        Task<PageDto> CreatePageAsync(CreatePageDto dto, long userId);
        Task<PageDto> UpdatePageAsync(long id, UpdatePageDto dto, long userId);
        Task DeletePageAsync(long id, long userId);
    }
}
