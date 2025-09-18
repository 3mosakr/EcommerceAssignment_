using Ecommerce.Entities.Models;
using Ecommerce.Services.Dto.User;
using Ecommerce.Services.Implementations;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;

        public UsersController(IUserService service, IPasswordService passwordService)
        {
            _userService = service;
            _passwordService = passwordService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            var response = users.Select(u => new UserReadDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email
            });
            return Ok(new ApiResponse<IEnumerable<UserReadDto>>(response, "Users retrieved successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound(new ApiResponse<string>("User not found"));
            var response = new UserReadDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
            return Ok(new ApiResponse<UserReadDto>(response, "User retrieved successfully"));
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>("Invalid user data"));

            // hash the password before storing
            var hashedPassword = _passwordService.HashPassword(dto.Password);

            var user = new User
            {
                UserName = dto.UserName,
                Password = hashedPassword, 
                Email = dto.Email
            };

            var created = await _userService.RegisterUserAsync(user);
            if (created is null)
                return BadRequest(new ApiResponse<string>("User registration failed"));
            // map created user to a UserReadDTO 
            var response = new UserReadDto
            {
                Id = created.Id,
                UserName = created.UserName,
                Email = created.Email
            };
            return Ok(new ApiResponse<UserReadDto>(response, "User registered successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>("Invalid user data"));

            var existingUser = await _userService.GetUserByIdAsync(id);
            if (existingUser == null)
                return NotFound(new ApiResponse<string>("User not found"));

            // Update fields
            existingUser.UserName = dto.UserName;
            existingUser.Email = dto.Email;

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                existingUser.Password = _passwordService.HashPassword(dto.Password);
            }

            var updated = await _userService.UpdateUserAsync(existingUser);

            var response = new UserReadDto
            {
                Id = updated.Id,
                UserName = updated.UserName,
                Email = updated.Email
            };

            return Ok(new ApiResponse<UserReadDto>(response, "User updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success) return NotFound(new ApiResponse<string>("User not found"));
            return Ok(new ApiResponse<string>("User deleted successfully"));
        }
    }
}
