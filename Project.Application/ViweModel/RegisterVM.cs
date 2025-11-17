using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Project.Application.ViweModel
{
    public class RegisterVM
    {
        [Required]
        public string? Email { get; set; }
        [Required]

        public string? FullName { get; set; }
        [Required]

        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords entered do not match. Please try again.")]
        public string? ConfirmPass { get; set; }
        public IFormFile? imageurl { get; set; }
    }
}
