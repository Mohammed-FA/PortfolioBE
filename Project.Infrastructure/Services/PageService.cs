using AutoMapper;
using Project.Application.Dto;
using Project.Application.Interfaces;
using Project.Domain.Entities;

namespace Project.Infrastructure.Services
{
    public class PageService : IPageService
    {
        private readonly IRepository<PageModel> _pageRepo;
        private readonly IRepository<Websites> _websiteRepo;
        private readonly IMapper _mapper;

        public PageService(
            IRepository<PageModel> pageRepo,
            IRepository<Websites> websiteRepo,
            IMapper mapper)
        {
            _pageRepo = pageRepo;
            _websiteRepo = websiteRepo;
            _mapper = mapper;
        }

        private async Task<Websites> ValidateOwner(long websiteId, long userId)
        {
            var website = await _websiteRepo.GetByIdAsync(websiteId);

            if (website == null)
                throw new Exception("Website not found");

            if (website.UserId != userId)
                throw new UnauthorizedAccessException("You are not allowed to modify this website");

            return website;
        }

        public async Task<PageDto> CreatePageAsync(CreatePageDto dto, long userId)
        {
            await ValidateOwner(dto.WebsiteId, userId);

            var page = _mapper.Map<PageModel>(dto);
            await _pageRepo.AddAsync(page);
            await _pageRepo.SaveChangesAsync();

            return _mapper.Map<PageDto>(page);
        }

        public async Task<PageDto> UpdatePageAsync(long id, UpdatePageDto dto, long userId)
        {
            var page = await _pageRepo.GetByIdAsync(id);
            if (page == null)
                throw new Exception("Page not found");

            await ValidateOwner(page.WebsitesId, userId);

            _mapper.Map(dto, page);
            _pageRepo.Update(page);
            await _pageRepo.SaveChangesAsync();

            return _mapper.Map<PageDto>(page);
        }

        public async Task DeletePageAsync(long id, long userId)
        {
            var page = await _pageRepo.GetByIdAsync(id);
            if (page == null)
                throw new Exception("Page not found");

            await ValidateOwner(page.WebsitesId, userId);

            _pageRepo.Delete(page);
            await _pageRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<PageDto>> GetPagesAsync(long websiteId)
        {
            var pages = await _pageRepo.GetAllAsync(
                predicate: p => p.WebsitesId == websiteId
            );

            return _mapper.Map<IEnumerable<PageDto>>(pages);
        }

        public async Task<PageDto?> GetByIdAsync(long id)
        {
            var page = await _pageRepo.GetByIdAsync(id);

            if (page == null)
                return null;

            return _mapper.Map<PageDto>(page);
        }

    }
}
