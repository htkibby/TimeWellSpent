using Microsoft.AspNetCore.Mvc;
using TimeWellSpent.Models;
using TimeWellSpent.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeWellSpent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserToActivityController : ControllerBase
    {
        private readonly IUserToActivityRepository _userToActivityRepository;
        public UserToActivityController(IUserToActivityRepository userToActivityRepository)
        {
            _userToActivityRepository = userToActivityRepository;
        }

        [HttpGet]
        public IActionResult GetAllUserToActivities()
        {
            return Ok(_userToActivityRepository.GetAllUserToActivities());
        }

        [HttpGet("{id}")]
        public IActionResult GetUserToActivityById(int id)
        {
            var userToActivity = _userToActivityRepository.GetUserToActivityById(id);

            if (userToActivity is null)
            {
                return NotFound();
            }

            return Ok(userToActivity);
        }

        [HttpPost]

        public IActionResult InsertUserToActivity(UserToActivity userToActivity)
        {
            if (userToActivity == null)
            {
                return BadRequest();
            }

            _userToActivityRepository.InsertUserToActivity(userToActivity);
            return Created("UserToActivity created" + userToActivity.Id, userToActivity);
        }

        [HttpPut("{id}")]

        public IActionResult UpdateUserToActivity(int id, UserToActivity userToActivity)
        {
            if (id != userToActivity.Id)
            {
                return BadRequest();
            }
            _userToActivityRepository.UpdateUserToActivity(userToActivity);
            return Ok(userToActivity);
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            var userToActivity = _userToActivityRepository.GetUserToActivityById(id);

            if (userToActivity is null)
            {
                return NotFound();
            }

            _userToActivityRepository.DeleteUserToActivity(userToActivity.Id);
            return NoContent();
        }
    }
}
