using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Dto;
using Project.Infrastructure.Services;

namespace Project.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebsiteController : ControllerBase
    {
        private readonly CreateWebsitServer _createWebsiteService;
        private readonly IWebHostEnvironment _environment;

        public WebsiteController(CreateWebsitServer createWebsiteService, IWebHostEnvironment environment)
        {
            _createWebsiteService = createWebsiteService;
            _environment = environment;
        }

        [HttpPost("upload-slot-image")]
        public async Task<IActionResult> UploadSlotImage(IFormFile file)
        {
            try
            {

                var results = await _createWebsiteService.UploadImge(file, HttpContext.Request.Host.Value);
                return Ok(new { url = results });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateWebsite")]
        [Authorize]
        public async Task<IActionResult> CreateWebsite([FromBody] CreateWebsiteRequest request)
        {
            if (request.Pages == null || request.Pages.Count == 0)
                return BadRequest("The Websit should have at less on page ");

            var email = User.FindFirstValue(ClaimTypes.Email)

            ?? User.FindFirstValue(JwtRegisteredClaimNames.Email);
            var result = await _createWebsiteService.CreateWebsiteWithPages(
                request.Name,
                email!,
                request.Pages,
                request.WebsieId
            );

            if (!result) return BadRequest("There is somthing be error ");

            return Ok(new { Success = true, Message = "Sucssfully" });
        }

        [HttpGet("GetAllByUser")]
        [Authorize]

        public async Task<IActionResult> GetAllWebsit()
        {
            var email = User.FindFirstValue(ClaimTypes.Email)
                ?? User.FindFirstValue(JwtRegisteredClaimNames.Email);
            try
            {
                var results = await _createWebsiteService.GetAllForUser(email!);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("DeleteWebsite/{id}")]
        [Authorize]

        public async Task<IActionResult> DeleteWebsite(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email)
                ?? User.FindFirstValue(JwtRegisteredClaimNames.Email);
            try
            {
                var results = await _createWebsiteService.DeleteWebsite(email!, id);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetPage/{websiteurl}/{pageurl}")]
        public IActionResult GetPage(string websiteurl, string pageurl)
        {
            var results = _createWebsiteService.GetPageToUser(websiteurl, pageurl);
            return Ok(results);
        }

        [HttpGet("GetWebsiteById")]
        public IActionResult GetWebsiteByID([FromQuery] int id, [FromQuery] string websiteName = "")
        {
            var results = _createWebsiteService.GetWebsiteById(id, websiteName);

            if (results == null)
            {
                return NotFound(new { message = $"Website with ID {id} not found." });
            }

            return Ok(results);
        }
    }
}
