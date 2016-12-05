using eDietetyk.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        // GET: Meals
        public ActionResult GetMeals()
        {
            var result = _services.GetMealsByName(string.Empty);
            return PartialView(result);
        }
    }
}