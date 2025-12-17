using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Application.Dto;
using Project.Domain.Entities;
using Project.Domain.Entities.Enm;
using Project.Infrastructure.Data;

namespace Project.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AdminController(UserManager<UserModel> userManager, AppDbContext context, IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var totalUsers = await _userManager.Users.CountAsync(u => u.EmailConfirmed);
            var totalWebsites = await _context.Websites.CountAsync();
            var activeUsers = await _context.Users.CountAsync(u => u.IsActive);

            var recentActivityEntities = await _context.Users
                .Where(u => (u.Role != UserType.Admin && u.LoginTime != null))
                .OrderByDescending(a => a.LoginTime)
                .Take(5)
                .ToListAsync();

            var recentActivity = _mapper.Map<List<UserDto>>(recentActivityEntities);

            var dashboard = new DashboardStatsDto
            {
                TotalUsers = totalUsers,
                TotalWebsites = totalWebsites,
                ActiveUsers = activeUsers,
                RecentActivity = recentActivity,
            };

            return Ok(dashboard);
        }


        [HttpGet("getallusers")]
        public async Task<IActionResult> GetUsers([FromQuery] UserQueryParams query)
        {
            var usersQuery = _userManager.Users
                .Include(u => u.Websites)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                usersQuery = usersQuery.Where(u =>
                    u.Email!.Contains(query.Search) ||
                    u.UserName!.Contains(query.Search)
                );
            }

            if (query.Type is not null)
            {
                usersQuery = usersQuery.Where(u => u.Role == query.Type);
            }

            if (query.ShowBlock == 2 || query.ShowBlock == 3)
            {
                usersQuery = usersQuery.Where(u => query.ShowBlock == 2 ? u.IsBlock : !u.IsBlock);
            }
            var totalCount = await usersQuery.CountAsync();

            var users = await usersQuery
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            var response = new PaginatedResult<UserDto>
            {
                Page = query.Page,
                PageSize = query.PageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)query.PageSize),
                Data = _mapper.Map<List<UserDto>>(users)
            };

            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> ShowUserWebsit()
        {

            var users = await _userManager.Users

                .Include(w => w.Websites)
                .ToListAsync();

            var results = _mapper.Map<List<UserDto>>(users);
            return Ok(results);
        }
        [HttpPut("EditUserInfo")]
        public async Task<IActionResult> EditUser(EditUser model)
        {
            var user = await _userManager.Users
                .Include(u => u.Websites)
                .FirstOrDefaultAsync(u => u.Id == model.userId);

            if (user == null)
                return NotFound("User not found");

            user.FullName = model.FullName ?? user.FullName;
            user.Email = model.Email ?? user.Email;

            if (user.Role != model.Role)
            {
                user.Role = model.Role;

                var currentRoles = await _userManager.GetRolesAsync(user);
                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                    return BadRequest(removeResult.Errors);

                string roleName = model.Role == UserType.User ? "User" : "Admin";

                var addResult = await _userManager.AddToRoleAsync(user, roleName);
                if (!addResult.Succeeded)
                    return BadRequest(addResult.Errors);

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    return BadRequest(result.Errors);
            }

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(EditUser model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
                return BadRequest("Email already exists");

            if (String.IsNullOrEmpty(model.Password))
                return BadRequest("Password not enter");
            var user = new UserModel
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName ?? "",
                EmailConfirmed = true,
                Role = model.Role
            };


            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            string roleName = model.Role == UserType.User ? "User" : "Admin";

            var addRoleResult = await _userManager.AddToRoleAsync(user, roleName);
            if (!addRoleResult.Succeeded)
                return BadRequest(addRoleResult.Errors);

            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }



        [HttpGet("GetUserWebsite/{userId}")]
        public IActionResult GetUserWebsite(long userId)
        {
            var user = _userManager.Users
              .Include(u => u.Websites.Where(w => !w.IsDeleted))
                .FirstOrDefault(item => item.Id == userId);

            var results = _mapper.Map<UserDto>(user);
            return Ok(results);
        }


        [HttpDelete("BlockUser/{userId}")]
        public IActionResult BlockUser(long userId, bool isBlock = true)
        {
            var user = _userManager.Users
                .FirstOrDefault(item => item.Id == userId);
            if (user == null)
                throw new Exception("User not found");

            user.IsBlock = isBlock;

            _context.Update(user);
            _context.SaveChanges();

            var results = _mapper.Map<UserDto>(user);
            return Ok(results);
        }


    }
}
