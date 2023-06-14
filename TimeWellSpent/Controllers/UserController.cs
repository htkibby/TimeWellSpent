using Microsoft.AspNetCore.Mvc;
using TimeWellSpent.Models;
using TimeWellSpent.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeWellSpent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetAllActivities()
        {
            return Ok(_userRepository.GetAllUsers());
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]

        public IActionResult InsertUser(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _userRepository.InsertUser(user);
            return Created("User created" + user.Id, user);
        }

        [HttpPut("{id}")]

        public IActionResult UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            _userRepository.UpdateUser(user);
            return Ok(user);
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            var user = _userRepository.GetUserById(id);

            if (user is null)
            {
                return NotFound();
            }

            _userRepository.DeleteUser(user.Id);
            return NoContent();
        }
    }
}
