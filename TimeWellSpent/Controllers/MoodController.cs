using Microsoft.AspNetCore.Mvc;
using TimeWellSpent.Models;
using TimeWellSpent.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeWellSpent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoodController : ControllerBase
    {
        private readonly IMoodRepository _moodRepository;
        public MoodController(IMoodRepository moodRepository)
        {
            _moodRepository = moodRepository;
        }

        [HttpGet]

        public IActionResult GetAllActivities()
        {
            return Ok(_moodRepository.GetAllMoods());
        }

        [HttpGet("{id}")]

        public IActionResult GetMoodById(int id)
        {
            var mood = _moodRepository.GetMoodById(id);

            if (mood is null)
            {
                return NotFound();
            }

            return Ok(mood);
        }

        [HttpPost]

        public IActionResult InsertMood(Mood mood)
        {
            if (mood == null)
            {
                return BadRequest();
            }

            _moodRepository.InsertMood(mood);
            return Created("Mood created" + mood.Id, mood);
        }

        [HttpPut("{id}")]

        public IActionResult UpdateMood(int id, Mood mood)
        {
            if (id != mood.Id)
            {
                return BadRequest();
            }
            _moodRepository.UpdateMood(mood);
            return Ok(mood);
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            var mood = _moodRepository.GetMoodById(id);

            if (mood is null)
            {
                return NotFound();
            }

            _moodRepository.DeleteMood(mood.Id);
            return NoContent();
        }
    }
}
