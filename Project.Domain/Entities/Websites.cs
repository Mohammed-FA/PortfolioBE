using System.ComponentModel.DataAnnotations.Schema;
using Project.Domain.Entities.Common;

namespace Project.Domain.Entities
{
    public class Websites : BaseEntity
    {
        public long UserId { get; set; }
        public string? Name { get; set; }

        public string? HostUrl { get; set; }
        public bool IsPublish { get; set; } = true;

        [ForeignKey(nameof(UserId))]
        public UserModel? UserModel { get; set; }

        public ICollection<PageModel>? Pages { get; set; }
    }
}
