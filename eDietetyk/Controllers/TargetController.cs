using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eDietetyk.Services;

namespace eDietetyk.Controllers
{
    [Authorize]
    public class TargetController : Controller
    {
        // <summary>
        /// Serwis do zarządzania metrykami
        /// </summary>
        private MetricsServices _service;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public TargetController()
        {
            _service = new MetricsServices();
        }

        public ActionResult GetPanel()
        {
            var model = _service.GetCurrentTarget(User.Identity.Name);
            return PartialView("Panel", model);
        }

        public ActionResult Add()
        {
            var model = _service.GetTargetToAdd(User.Identity.Name);
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(Metrics data)
        {
            var success = _service.AddTarget(data, User.Identity.Name);
            if (success)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Errors = "Wprowadzono niepoprawną wartość";
            return View(data);
        }
    }
}