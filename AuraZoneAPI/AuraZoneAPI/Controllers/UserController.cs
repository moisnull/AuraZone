using Microsoft.AspNetCore.Mvc;
using AuraZoneAPI.DataAccess.Models;
using AuraZoneAPI.Services;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace AuraZoneAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAll();
            return Ok(users);
        }
        [HttpGet("{id:guid}")]
        public IActionResult GetUserById(Guid id)
        {
            if (!_userRepository.UserExists(id))
            {
                return NotFound("User not found.");
            }
            User user = _userRepository.GetById(id);
            return Ok(user);
        }
        [HttpGet("{username}")]
        public IActionResult GetUserByUsername(string username)
        {
            if (!_userRepository.UserExists(username))
            {
                return NotFound("User not found.");
            }
            var user = _userRepository.GetByUsername(username);
            return Ok(user);
        }
        [HttpPost("{username}, {email}, {password}")]
        public IActionResult AddUser(string username, string email, string password)
        {

            User newUser = new()
            {
                Id = Guid.NewGuid(),
                Username = username,
                Email = email,
                PasswordHashed = password,
                CreatedAt = DateTime.UtcNow
            };
            if (newUser == null)
            {
                return BadRequest("User is NULL");
            }
            _userRepository.AddUser(newUser);
            return Ok(newUser);
        }
        [HttpPatch("{id:guid}")]
        public IActionResult EditUser(Guid id, [FromBody] JsonPatchDocument<User> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest(ModelState);
            }

            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(user);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _userRepository.UpdateUser(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_userRepository.UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteUser(Guid id)
        {
            if (_userRepository.UserExists(id))
            {
                _userRepository.DeleteUser(id);
            }
            else
            {
                return NotFound("User not found.");
            }
            return Ok();
        }
    }
}
