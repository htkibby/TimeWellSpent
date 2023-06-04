using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserRepository/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserRepository/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserRepository/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}
