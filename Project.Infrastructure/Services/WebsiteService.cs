using AutoMapper;
using Project.Application.Dto;
using Project.Application.Interfaces;
using Project.Domain.Entities;

namespace Project.Infrastructure.Services
{
    public class WebsiteService : IWebsiteService
    {
        private readonly IRepository<Websites> _repo;
        private readonly IMapper _mapper;

        public WebsiteService(IRepository<Websites> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WebsiteDto>> GetUserWebsitesAsync(long userId)
        {
            var data = await _repo.GetAllAsync(x => x.UserId == userId);
            return _mapper.Map<IEnumerable<WebsiteDto>>(data);
        }

        public async Task<WebsiteDto?> GetByIdAsync(int id)
        {
            var site = await _repo.GetByIdAsync(id);
            return _mapper.Map<WebsiteDto>(site);
        }

        public async Task<int> CreateAsync(WebsiteDto dto)
        {
            var entity = _mapper.Map<Websites>(dto);
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            _repo.Delete(entity);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
