using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdPortal.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult About()
        {
            ViewBag.Message = "Applying Boostrap template to presenting data in tabels.";
            return View();
        }
    }
}