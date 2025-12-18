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

        public async Task<bool> CreateWebsiteWithPages(string name, string email, List<PageModel> pages, int? websiteId = null)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            var exists = await _context.Websites
                .AnyAsync(w => w.UserId == user.Id && w.Name != null
                               && w.Name.ToLower() == name.ToLower()
                               && (!websiteId.HasValue || w.Id != websiteId.Value));


            if (exists)
            {
                return false;
            }

            Websites website;

            if (websiteId != 0)
            {
                website = await _context.Websites
                    .Include(w => w.Pages)
                        .ThenInclude(p => p.Sections)
                            .ThenInclude(s => s.Columns)
                                .ThenInclude(c => c.Slots)
                    .FirstOrDefaultAsync(w => w.Id == websiteId.Value && w.UserId == user.Id);

                if (website == null) return false;


                _context.Slots.RemoveRange(website.Pages.SelectMany(p => p.Sections)
                                                       .SelectMany(s => s.Columns)
                                                       .SelectMany(c => c.Slots));
                _context.Columns.RemoveRange(website.Pages.SelectMany(p => p.Sections)
                                                          .SelectMany(s => s.Columns));
                _context.Sections.RemoveRange(website.Pages.SelectMany(p => p.Sections));
                _context.Pages.RemoveRange(website.Pages);

                await _context.SaveChangesAsync();
                website.Name = name;
                website.HostUrl = name;
                website.IsPublish = true;
            }
            else
            {

                website = new Websites
                {
                    UserId = user.Id,
                    Name = name,
                    HostUrl = name,
                    IsPublish = true
                };
                _context.Websites.Add(website);
            }


            website.Pages = pages.Select(p => new PageModel
            {
                Id = 0,
                Name = p.Name,
                Url = $"/{p.Name}",
                Sections = p.Sections?.Select(s => CloneSectionWithZeroId(s)).ToList()
            }).ToList();

            await _context.SaveChangesAsync();
            return true;
        }


        public static Slots CloneWithZeroId(Slots source)
        {
            if (source == null) return null;

            var clone = new Slots { Id = 0 };

            var props = typeof(Slots).GetProperties()
                .Where(p => p.CanRead && p.CanWrite && p.Name != "Id");

            foreach (var prop in props)
            {
                var value = prop.GetValue(source);

                // Clone nested Items
                if (prop.Name == "Items" && value is ICollection<ListItems> items)
                {
                    clone.Items = items.Select(i => CloneListItemWithZeroId(i)).ToList();
                }
                else
                {
                    prop.SetValue(clone, value);
                }
            }

            return clone;
        }

        public static ListItems CloneListItemWithZeroId(ListItems source)
        {
            if (source == null) return null;

            var clone = new ListItems { Id = 0 };

            var props = typeof(ListItems).GetProperties()
                .Where(p => p.CanRead && p.CanWrite && p.Name != "Id");

            foreach (var prop in props)
            {
                prop.SetValue(clone, prop.GetValue(source));
            }

            return clone;
        }
        public static Columns CloneColumnWithZeroId(Columns source)
        {
            if (source == null) return null;

            var clone = new Columns { Id = 0 };

            var props = typeof(Columns).GetProperties()
                .Where(p => p.CanRead && p.CanWrite && p.Name != "Id");

            foreach (var prop in props)
            {
                var value = prop.GetValue(source);

                if (prop.Name == "Slots" && value is ICollection<Slots> slots)
                {
                    clone.Slots = slots.Select(s => CloneWithZeroId(s)).ToList();
                }
                else
                {
                    prop.SetValue(clone, value);
                }
            }

            return clone;
        }

        public static SectionModel CloneSectionWithZeroId(SectionModel source)
        {
            if (source == null) return null;

            var clone = new SectionModel { Id = 0 };

            var props = typeof(SectionModel).GetProperties()
                .Where(p => p.CanRead && p.CanWrite && p.Name != "Id");

            foreach (var prop in props)
            {
                var value = prop.GetValue(source);

                if (prop.Name == "Columns" && value is ICollection<Columns> columns)
                {
                    clone.Columns = columns.Select(c => CloneColumnWithZeroId(c)).ToList();
                }
                else
                {
                    prop.SetValue(clone, value);
                }
            }

            return clone;
        }

        public Websites? GetWebsiteById(int id, string websiteName = "")
        {
            var results = _context.Websites
                .Include(p => p.Pages)!
                    .ThenInclude(s => s.Sections)!
                        .ThenInclude(c => c.Columns)!
                            .ThenInclude(s => s.Slots)
                .FirstOrDefault(item =>
                    item.Id == id ||
                    (!string.IsNullOrWhiteSpace(websiteName) &&
                     item.Name.ToLower() == websiteName.ToLower())
                );

            return results;
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
                .OrderByDescending(item => item.Id)
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
                .FirstOrDefault(p => p.websites!.Name == HostUrl && p.Name == pageurl)
                ;


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
