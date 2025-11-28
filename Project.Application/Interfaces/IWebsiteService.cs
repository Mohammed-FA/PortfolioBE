using Project.Application.Dto;

namespace Project.Application.Interfaces
{
    public interface IWebsiteService
    {
        Task<IEnumerable<WebsiteDto>> GetUserWebsitesAsync(long userId);
        Task<WebsiteDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(WebsiteDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
