using eDietetyk.Models;
using eDietetyk.Services;
using System;
using System.Web.Mvc;

namespace eDietetyk.Controllers
{
    [Authorize]
    public class UserMealsController : Controller
    {
        private UserMealsServices _service;
        public UserMealsController()
        {
            _service = new UserMealsServices();
        }
        // GET: Meals
        public ActionResult GetPanel()
        {
            var data = _service.GetMealsForDay(DateTime.Today, User.Identity.Name);
            return View("Panel",data);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(UserMealsViewModel data)
        {
            _service.AddUserMeals(data, User.Identity.Name);
            return RedirectToAction("Index","Home");
        }
    }
}