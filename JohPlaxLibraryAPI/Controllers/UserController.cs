using JohPlaxLibraryAPI.Interfaces;
using JohPlaxLibraryAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JohPlaxLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UserController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync()
            => Ok(await _usersService.GetUsersAsync());

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> GetUserByIdAsync(string id)
        {
            var existingUser = await _usersService.GetUserByIdAsync(id);

            return existingUser is null ? NotFound() : Ok(existingUser);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserAsync(User user)
        {
            var createdUser = await _usersService.CreateUserAsync(user);

            return createdUser is null ? throw new Exception("Failed to create User") :
                CreatedAtAction(nameof(GetUserByIdAsync), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> UpdateUserByIdAsync(string id, User updatedUser)
        {
            var queryUser = await _usersService.GetUserByIdAsync(id);

            if (queryUser is null)
            {
                return NotFound();
            }

            await _usersService.UpdateUserByIdAsync(id, updatedUser);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> DeleteUserByIdAsync(string id)
        {
            var existingUser = await _usersService.GetUserByIdAsync(id);

            if (existingUser is null)
            {
                return NotFound();
            }

            await _usersService.DeleteUserByIdAsync(id);
            return NoContent();
        }
    }
}
