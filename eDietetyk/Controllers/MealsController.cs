using eDietetyk.Services;
using System;
using System.Web.Mvc;

namespace eDietetyk.Controllers
{
    public class MealsController : Controller
    {
        private MealsServices _services;
        public MealsController()
        {
            _services = new MealsServices();
        }

        public ActionResult GetMeals(string key)
        {
            var result = _services.GetMealsByName(key);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}