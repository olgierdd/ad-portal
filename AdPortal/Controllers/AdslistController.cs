using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdPortal.ViewModel;

namespace AdPortal.Controllers
{
    public class AdsListController : Controller
    {
        public ActionResult FullList()
        {
            AdViewModel vm = new AdViewModel();
            vm.EventCommand = "page";
            vm.HandleRequest();
            return View(vm);
        }

        [HttpPost]
        public ActionResult FullList(AdViewModel vm)
        {
            vm.HandleRequest();
            ModelState.Clear();
            return View(vm);
        }

        public ActionResult CoverList()
        {
            AdViewModel vm = new AdViewModel();
            vm.EventCommand = "page";
            vm.AdsQueryType = AdsQueryType.AdsInCover;
            vm.HandleRequest();
            return View(vm);
        }

        [HttpPost]
        public ActionResult CoverList(AdViewModel vm)
        {
            vm.AdsQueryType = AdsQueryType.AdsInCover;
            vm.HandleRequest();
            ModelState.Clear();
            return View(vm);
        }
    }
}