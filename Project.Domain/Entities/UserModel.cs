using Microsoft.AspNetCore.Identity;
using Project.Domain.Entities.Enm;

namespace Project.Domain.Entities
{
    public class UserModel : IdentityUser<long>
    {
        public string FullName { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public UserType Role { get; set; } = UserType.User;
        public bool IsActive { get; set; } = true;

        public bool IsBlock { get; set; } = false;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? LoginTime { get; set; }

        public ICollection<Websites>? Websites { get; set; }

    }
}
