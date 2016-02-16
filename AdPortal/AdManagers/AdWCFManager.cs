using System;
using System.Collections.Generic;
using System.Linq;
using AdPortal.AdDataService;
using AdPortal.ViewModel;
using System.Linq.Dynamic;
using Ad = AdPortal.Models.Ad;

namespace AdPortal.AdManagers
{
    public class AdWCFManager : AdManager
    {
        public override void LoadAds(DateTime start, DateTime end)
        {
            var client = new AdDataServiceClient();
            var ads = client.GetAdDataByDateRange(start, end);
            var viewModelAds =
                ads.Select(a => Models.Ad.Create(a.AdId, a.Brand.BrandId, a.Brand.BrandName, a.NumPages, a.Position));
            Ads = viewModelAds.OrderBy(a=>a.BrandName).ToList();
        }

    }
}