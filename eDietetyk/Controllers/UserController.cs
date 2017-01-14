using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eDietetyk.Services;

namespace eDietetyk.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private List<SelectListItem> _activityList = new List<SelectListItem>()
        {
            new SelectListItem {Text = "leżący lub siedzący tryb życia, brak aktywności fizycznej", Value = "10"},
            new SelectListItem {Text = "praca siedząca, aktywność fizyczna na niskim poziomie", Value = "12"},
            new SelectListItem {Text = "praca nie fizyczna, trening 2 razy w tygodniu", Value = "14"},
            new SelectListItem {Text = "lekka praca fizyczna, trening 3-4 razy w tygodniu", Value = "16"},
            new SelectListItem {Text = "praca fizyczna, trening 5 razy w tygodniu", Value = "18"},
            new SelectListItem {Text = "ciężka praca fizyczna, codzienny trening", Value = "20"}
        };

        public ActionResult GetPanel()
        {
            var model = new UserServices().GetData(User.Identity.Name);
            if (model != null)
            {
                ViewBag.ActivityName = GetActivity(model.Activity);
            }
            return PartialView("Panel", model);
        }

        public ActionResult Add()
        {
            ViewBag.ActivityList = _activityList;
            return View();
        }

        
        [HttpPost]
        public ActionResult Add(UserInfo data)
        {
            var success = new UserServices().Save(data, User.Identity.Name);
            if (string.IsNullOrEmpty(success))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ActivityList = _activityList;
            ViewBag.Errors = success;
            return View(data);
        }

        public ActionResult Edit()
        {
            var model = new UserServices().GetData(User.Identity.Name);
            ViewBag.ActivityList = _activityList;
            return View(model);
        }

       
        [HttpPost]
        public ActionResult Edit(UserInfo data)
        {
            var success = new UserServices().Save(data, User.Identity.Name);
            if (string.IsNullOrEmpty(success))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ActivityList = _activityList;
            ViewBag.Errors = success;
            return View(data);
        }

        private string GetActivity(int value)
        {
            return _activityList.First(x => x.Value == value.ToString()).Text;
        }

    }
}