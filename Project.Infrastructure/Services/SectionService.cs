using AutoMapper;
using Project.Application.Dto;
using Project.Application.Interfaces;
using Project.Domain.Entities;

namespace Project.Infrastructure.Services
{
    public class SectionService : ISectionService
    {
        private readonly IRepository<SectionModel> _repo;
        private readonly IMapper _mapper;

        public SectionService(IRepository<SectionModel> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SectionDto>> GetSectionsAsync(int pageId)
        {
            var data = await _repo.GetAllAsync(x => x.PageId == pageId);
            return _mapper.Map<IEnumerable<SectionDto>>(data);
        }

        public async Task<int> CreateAsync(SectionDto dto)
        {
            var entity = _mapper.Map<SectionModel>(dto);
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
