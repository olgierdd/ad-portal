using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdPortal.ViewModel;

namespace AdPortal.Controllers
{
    public class TopFiveController : Controller
    {
        public ActionResult Ads()
        {
            AdViewModel vm = new AdViewModel();
            vm.AdsQueryType = AdsQueryType.Top5Ads;
            vm.HandleRequest();
            return View(vm);
        }

        public ActionResult Brands()
        {
            AdViewModel vm = new AdViewModel();
            vm.AdsQueryType = AdsQueryType.Top5Brands;
            vm.HandleRequest();
            var topBrands = vm.GetTop5Brands();
            return View(topBrands);
        }
    }
}