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
            _service.AddMetric(data, User.Identity.Name);
            return RedirectToAction("Index", "Home");
        }
    }
}