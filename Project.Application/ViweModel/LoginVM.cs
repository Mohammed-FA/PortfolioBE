using System.ComponentModel.DataAnnotations;

namespace Project.Application.ViweModel
{
    public class LoginVM
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool? Rememberme { get; set; } = false;
    }
}
