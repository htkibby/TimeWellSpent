using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TimeWellSpent.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class UserRepository : Controller
    {
        // GET: UserRepository
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserRepository/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserRepository/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserRepository/Create
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

        // POST: UserRepository/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
