using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eDietetyk.Services;

namespace eDietetyk.Controllers
{
    [Authorize]
    public class ProgressController : Controller
    {
        // GET: Chart
        public ActionResult GetPanel()
        {
            var model = new ProgressServices().GetData(User.Identity.Name);
            return PartialView("Panel", model);
        }
    }
}