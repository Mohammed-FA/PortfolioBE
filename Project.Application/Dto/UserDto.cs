using Project.Domain.Entities.Enm;

namespace Project.Application.Dto
{

    public class UserDto
    {
        public string token { get; set; }
        public long Id { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Imageurl { get; set; }
        public UserType Role { get; set; } = UserType.User;
        public bool IsActive { get; set; } = true;

    }
}
