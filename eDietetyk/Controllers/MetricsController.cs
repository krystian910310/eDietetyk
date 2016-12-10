using eDietetyk.Models;
using eDietetyk.Services;
using System.Web.Mvc;

namespace eDietetyk.Controllers
{
    [Authorize]
    public class MetricsController : Controller
    {
        /// <summary>
        /// Serwis do zarządzania metrykami
        /// </summary>
        private MetricsServices _service;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public MetricsController()
        {
            _service = new MetricsServices();
        }

        /// <summary>
        /// Zwraca kafelek z metryką.
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPanel()
        {
            var model = _service.GetCurrentMetric(User.Identity.Name);
            return PartialView("Panel", model);
        }

        /// <summary>
        /// Wyświetla widok dodawania metryki
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Zapisuje metrykę
        /// </summary>
        /// <param name="data">Dane</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(Metrics data)
        {
            var success = _service.AddMetric(data, User.Identity.Name);
            if (success)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Errors = "Wprowadzono niepoprawną wartość";
            return View(data);
        }

        public ActionResult AddTarget()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTarget(Metrics data)
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