using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using project.Services;
using Project.Application.Dto;
using Project.Application.ViweModel;
using Project.Domain.Entities;

namespace CleanTepm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly EmailSenderService _emailSender;
        private readonly IMapper _mapper;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment env;
        public AccountController(EmailSenderService emailSender, UserManager<UserModel> userManager, IMapper mapper, SignInManager<UserModel> signInManager, IConfiguration configuration, IWebHostEnvironment env)
        {
            _emailSender = emailSender;

            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _configuration = configuration;
            this.env = env;
        }

        [HttpPost("Create-account")]
        public async Task<IActionResult> Register([FromForm] RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var ValidateEmail = await _userManager.FindByEmailAsync(model.Email!);
                if (ValidateEmail != null)
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
                    var r = await _userManager.AddToRoleAsync(user, "User");
                    if (r.Succeeded)
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationLink = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/ConfirmEmail?email={user.Email}&token={Uri.EscapeDataString(token)}";
                        var subject = "Confirm your email";
                        var message = $"Please confirm your account by clicking <a href='{confirmationLink}'>here</a>";

                        await _emailSender.SendEmailAsync(user.Email!, subject, message);

                        return Ok("Confirm The Email");

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

        [HttpGet("/ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest("Invalid email address.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            string baseurl = _configuration.GetSection("url:fronturl").Value!;
            if (result.Succeeded)
            {
                var token1 = await GenerateToken(user);
                var redirectUrl = $"{baseurl}/signin";
                return Redirect(redirectUrl);
            }

            return BadRequest("Email confirmation failed.");
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginVM model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    if (!user.EmailConfirmed)
                    {
                        return BadRequest("Email is not confirmed.");
                    }
                    var results = await _signInManager.PasswordSignInAsync(user, model.Password!, model.Rememberme ?? false, false);
                    if (results.Succeeded)
                    {
                        var userDto = _mapper.Map<UserDto>(user);
                        userDto.token = GenerateToken(user).Result;
                        return Ok(userDto);
                    }
                }
                return BadRequest("Error in Email or Password");

            }
            return BadRequest(ModelState);
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest("Invalid Email Address");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetLink = $"{_configuration.GetSection("url:fronturl").Value}/verify?email={email}&token={Uri.EscapeDataString(token)}";

            var subject = "Reset Your Password";
            var message = $"Click the link to reset your password: <a href='{resetLink}'>Reset Password</a>";

            await _emailSender.SendEmailAsync(email, subject, message);

            return Ok("Reset password link has been sent to your email.");
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest("Passwords do not match");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return Ok("Password has been reset successfully");
            }

            return BadRequest(result.Errors);
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
