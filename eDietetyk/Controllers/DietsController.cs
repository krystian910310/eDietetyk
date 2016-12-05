using eDietetyk.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eDietetyk.Controllers
{
    [Authorize]
    public class DietsController : Controller
    {
        private DietsServices _services;

        public DietsController()
        {
            _services = new DietsServices();
        }

        // GET: Diets
        public ActionResult GetPanel()
        {
            var data =_services.GetCurrentDiet(User.Identity.Name, DateTime.Today);
            return PartialView("Panel",data);
        }
    }
}