using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project.Application.ViweModel;
using Project.Domain.Entities;

namespace CleanTepm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment env;
        public AccountController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IConfiguration configuration, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            this.env = env;
        }

        [HttpPost("Create-account")]
        public async Task<IActionResult> Register([FromForm] RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var ValidateEmail = _userManager.FindByEmailAsync(model.Email!);
                if (ValidateEmail == null)
                {
                    return BadRequest($"The Email Is token {model.Email}");
                }
                if (model.Password != model.ConfirmPass)
                {
                    return BadRequest($"Password not match confirm Password");

                }
                var user = new UserModel
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName ?? "",
                };

                if (model.imageurl != null)
                {
                    if (!_configuration.GetSection("uploadFile:allowedFileExtension").Get<string[]>()!.Contains(Path.GetExtension(model.imageurl.FileName.ToLower())))
                    {
                        return BadRequest("File Extension error");
                    }
                    int maxFile = _configuration.GetSection("uploadFile:MaxSize").Get<int>();
                    if (maxFile * 1024 * 1024 < model.imageurl.Length)
                    {
                        return BadRequest($"File Length Extra Than {maxFile} MB");
                    }
                    string FileName = Guid.NewGuid().ToString() + Path.GetExtension(model.imageurl.FileName.ToLower());
                    string SubFile = _configuration.GetSection("uploadFile:subFile").Value!;
                    string path = Path.Combine(env.WebRootPath, SubFile, FileName);

                    using (var fs = new FileStream(path, FileMode.Create))
                    {
                        await model.imageurl.CopyToAsync(fs);
                    }
                    string urlImage = "https://" + HttpContext.Request.Host.Value + "/images/" + FileName;
                    user.ImageUrl = urlImage;
                }
                var results = await _userManager.CreateAsync(user, model.Password!);
                if (results.Succeeded)
                {
                    var r = await _userManager.AddToRoleAsync(user, "employee");
                    if (r.Succeeded)
                    {
                        return Ok(new { Name = model.FullName, Email = model.Email, Imageurl = user.ImageUrl, token = GenerateToken(user) });
                    }
                    return BadRequest("The Role Not Exsists");
                }
                else
                {
                    return BadRequest(results.Errors);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginVM model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var results = await _signInManager.PasswordSignInAsync(user, model.Password!, model.Rememberme ?? false, false);
                    if (results.Succeeded)
                    {
                        return Ok(new { Email = model.Email, token = GenerateToken(user) });
                    }
                }
                return BadRequest("Error in Email or Password");

            }
            return BadRequest(ModelState);
        }

        private async Task<string> GenerateToken(UserModel user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var Claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email!),
                new Claim(JwtRegisteredClaimNames.GivenName,user.FullName !),
                new Claim(ClaimTypes.Role, roles.FirstOrDefault() ?? "employee")
            };


            var key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(_configuration["JWT:skey"]!));

            var signCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = signCred,
                Issuer = _configuration["JWT:iss"],
                Audience = _configuration["JWT:aud"],
                Expires = DateTime.Now.AddDays(1),
                Subject = new ClaimsIdentity(Claims)
            };

            var TokenHandler = new JwtSecurityTokenHandler();
            var token = TokenHandler.CreateToken(tokenDiscriptor);

            return TokenHandler.WriteToken(token);
        }
    }
}
