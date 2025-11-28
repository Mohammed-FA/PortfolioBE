using Microsoft.AspNetCore.Mvc;
using Project.Application.Dto;
using Project.Application.Interfaces;

namespace Project.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ISectionService _service;

        public SectionController(ISectionService service)
        {
            _service = service;
        }

        [HttpGet("page/{pageId}")]
        public async Task<IActionResult> GetSections(int pageId)
        {
            return Ok(await _service.GetSectionsAsync(pageId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(SectionDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return Ok(new { id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _service.DeleteAsync(id) ? Ok() : NotFound();
        }
    }
}
