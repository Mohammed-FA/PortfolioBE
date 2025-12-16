using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project.Application.Dto;
using Project.Domain.Entities;
using Project.Infrastructure.CommentCode;
using Project.Infrastructure.Data;

namespace Project.Infrastructure.Services
{
    public class CreateWebsitServer
    {
        private readonly AppDbContext _context;
        private readonly UserManager<UserModel> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public CreateWebsitServer(AppDbContext context, UserManager<UserModel> userManager, IMapper mapper, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
            _environment = environment;
        }

        public async Task<bool> CreateWebsiteWithPages(string name, string email, List<PageModel> pages)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            var exists = await _context.Websites
                .AnyAsync(w => w.UserId == user.Id && w.Name != null && w.Name.ToLower() == name.ToLower());

            if (exists)
            {
                return false;
            }

            var website = new Websites
            {
                UserId = user.Id,
                Name = name,
                IsPublish = true,
                HostUrl = $"{name}",
                Pages = pages.Select(p => new PageModel
                {
                    Name = p.Name,
                    Url = $"/{p.Name}",
                    Sections = p.Sections?.Select(s => new SectionModel
                    {
                        Type = s.Type,
                        Columns = s.Columns?.Select(c => new Columns
                        {
                            Slots = c.Slots?.Select(sl => new Slots
                            {
                                Type = sl.Type,
                                Url = sl.Url,
                                Label = sl.Label,
                                Content = sl.Content,
                                Href = sl.Href,
                                ListStyleType = sl.ListStyleType,
                                LinkType = sl.LinkType,
                                Target = sl.Target,
                                Orientation = sl.Orientation,
                                Thickness = sl.Thickness,
                                IconName = sl.IconName,
                                Poster = sl.Poster,
                                Volume = sl.Volume,
                                PlaybackRate = sl.PlaybackRate,
                                Controls = sl.Controls,
                                Muted = sl.Muted,
                                Loop = sl.Loop,
                                Autoplay = sl.Autoplay,
                                Items = sl.Items?.Select(i => new ListItems
                                {
                                    label = i.label,
                                    IconName = i.IconName,
                                    Href = i.Href,
                                    LinkType = i.LinkType,
                                    Target = i.Target
                                }).ToList()
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList()
            };

            _context.Websites.Add(website);
            await _context.SaveChangesAsync();

            return true;
        }



        public async Task<List<WebsitDto>> GetAllForUser(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new Exception("Email is null or empty");

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new Exception("User not found");

            var websites = await _context.Websites
                .Include(p => p.Pages)
                .Where(item => item.UserId == user.Id && item.IsDeleted == false)
                .ToListAsync();
            return _mapper.Map<List<WebsitDto>>(websites);

        }

        public PageModel? GetPageToUser(string HostUrl, string pageurl)
        {
            var results = _context.Pages
                .Include(w => w.websites)
                .Include(s => s.Sections)!
                .ThenInclude(c => c.Columns)!
                .ThenInclude(s => s.Slots)
                .FirstOrDefault(p => p.websites!.Name == HostUrl && p.Name == pageurl);


            return results;
        }
        public async Task<bool> DeleteWebsite(string email, int id)
        {

            if (string.IsNullOrEmpty(email))
                throw new Exception("Email is null or empty");

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new Exception("User not found");

            var results = _context.Websites.FirstOrDefault(website => website.Id == id && website.UserId == user.Id);
            if (results == null)
                throw new Exception("Some Thing be wrong");
            results.IsDeleted = true;
            _context.Update(results);
            _context.SaveChanges();
            return true;

        }

        public async Task<string> UploadImge(IFormFile file, string host)
        {
            string url = await Comment.CheckFile(file, _configuration, _environment, host);
            return url;
        }

    }
}
