using Project.Domain.Entities;

namespace Project.Application.Dto
{
    public class WebsitDto
    {
        public long UserId { get; set; }
        public string? Name { get; set; }
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? HostUrl { get; set; }
        public bool IsPublish { get; set; } = false;
        public List<PageModel>? Pages { get; set; }
    }
}
