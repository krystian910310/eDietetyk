﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eDietetyk.Controllers
{
    [Authorize]
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult GetPanel()
        {
            return PartialView("Panel");
        }
    }
}