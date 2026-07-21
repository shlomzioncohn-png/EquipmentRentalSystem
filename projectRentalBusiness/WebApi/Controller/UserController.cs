using AutoMapper;
using Core.Models;
using Core.Resources;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.SecurityNamespace;

namespace WebApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // 1. קבלת כל המשתמשים
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResource>>> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            if (users == null || !users.Any())
                return NotFound("No users found.");

            return Ok(users);
        }

        // 2. קבלת משתמש לפי ID
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResource>> GetById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound($"User with ID {id} not found.");

            return Ok(user);
        }

        // 3. יצירת משתמש חדש
        [HttpPost]
        public async Task<ActionResult<UserResource>> Create([FromBody] UserCreateResource userCreateResource)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.CreateUserAsync(userCreateResource);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // 4. עדכון משתמש
        [HttpPut("{id}")]
        public async Task<ActionResult<UserResource>> Update(Guid id, [FromBody] UserResource userResource)
        {
            var result = await _userService.UpdateUserAsync(id, userResource);
            if (result == null)
                return NotFound("Update failed. User not found.");

            return Ok(result);
        }

        // 5. מחיקת משתמש
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success)
                return BadRequest("Unable to delete user. Ensure user exists and has no associated businesses.");

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserResource>> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await _userService.LoginAsync(loginRequest.Email, loginRequest.Password);

            if (user == null)
                return Unauthorized("Invalid username or password.");

            return Ok(user);
        }
    
    

   }
    }