using Project.Domain.Entities;
using Project.Domain.Entities.Enm;

namespace Project.Application.Dto
{

    public class UserDto
    {
        public string? token { get; set; }
        public long Id { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Imageurl { get; set; }
        public DateTime? LoginTime { get; set; }
        public UserType Role { get; set; } = UserType.User;
        public bool IsActive { get; set; } = true;
        public DateTime DateCreated { get; set; }
        public bool IsBlock { get; set; }

        public ICollection<Websites>? Websites { get; set; }
    }
    public class EditUser
    {
        public long userId { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public UserType Role { get; set; }
        public bool IsBlock { get; set; }
        public string? Password { get; set; }

    }
}
