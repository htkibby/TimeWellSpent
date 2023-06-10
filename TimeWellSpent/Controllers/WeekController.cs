using Microsoft.AspNetCore.Mvc;
using TimeWellSpent.Models;
using TimeWellSpent.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeWellSpent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeekController : ControllerBase
    {
        private readonly IWeekRepository _weekRepository;
        public WeekController(IWeekRepository weekRepository)
        {
            _weekRepository = weekRepository;
        }

        [HttpGet]
        public IActionResult GetAllActivities()
        {
            return Ok(_weekRepository.GetAllActivities());
        }

        [HttpGet("{id}")]
        public IActionResult GetWeekById(int id)
        {
            var week = _weekRepository.GetWeekById(id);

            if (week is null)
            {
                return NotFound();
            }

            return Ok(week);
        }

        [HttpPost]

        public IActionResult InsertWeek(Week week)
        {
            if (week == null)
            {
                return BadRequest();
            }

            _weekRepository.InsertWeek(week);
            return Created("Week created" + week.Id, week);
        }

        [HttpPut("{id}")]

        public IActionResult UpdateWeek(int id, Week week)
        {
            if (id != week.Id)
            {
                return BadRequest();
            }
            _weekRepository.UpdateWeek(week);
            return Ok(week);
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            var week = _weekRepository.GetWeekById(id);

            if (week is null)
            {
                return NotFound();
            }

            _weekRepository.DeleteWeek(week.Id);
            return NoContent();
        }
    }
}
