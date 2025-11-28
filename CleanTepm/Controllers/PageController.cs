using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Dto;
using Project.Application.Interfaces;

namespace Project.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class PageController : ControllerBase
    {
        private readonly IPageService _service;

        public PageController(IPageService service)
        {
            _service = service;
        }

        private long GetUserId()
        {
            return long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePageDto dto)
        {
            long userId = GetUserId();

            var result = await _service.CreatePageAsync(dto, userId);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, UpdatePageDto dto)
        {
            long userId = GetUserId();

            var result = await _service.UpdatePageAsync(id, dto, userId);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            long userId = GetUserId();

            await _service.DeletePageAsync(id, userId);
            return NoContent();
        }
    }
}
