using Microsoft.AspNetCore.Mvc;
using TimeWellSpent.Models;
using TimeWellSpent.Repositories;

namespace TimeWellSpent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : Controller
    {
        private readonly IActivityRepository _activityRepository;
        public ActivityController(IActivityRepository activityRepository)
        {
            _activityRepository= activityRepository;
        }

        [HttpGet]
        public IActionResult GetAllActivities()
        {
            return Ok(_activityRepository.GetAllActivities());
        }

        [HttpGet("{id}")]
        public IActionResult GetActivityById(int id)
        {
            var activity = _activityRepository.GetActivityById(id);

            if (activity is null)
            {
                return NotFound();
            }

            return Ok(activity);
        }

        [HttpPost]

        public IActionResult InsertActivity(Activity activity)
        {
            if (activity == null)
            {
                return BadRequest();
            }

            _activityRepository.InsertActivity(activity);
            return Created("Activity created" + activity.Id, activity);
        }

        [HttpPut("{id}")]

        public IActionResult UpdateActivity(int id, Activity activity)
        {
            if (id != activity.Id)
            {
                return BadRequest();
            }
            _activityRepository.UpdateActivity(activity);
            return Ok(activity);
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            var activity = _activityRepository.GetActivityById(id);

            if (activity is null)
            {
                return NotFound();
            }

            _activityRepository.DeleteActivity(activity.Id);
            return NoContent();
        }
    }
}
