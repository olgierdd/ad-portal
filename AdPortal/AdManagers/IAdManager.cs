using System;
using System.Collections.Generic;
using System.Data;
using System.Resources;
using AdPortal.Models;
using AdPortal.ViewModel;

namespace AdPortal.AdManagers
{
    internal interface IAdManager
    {
        List<Models.Ad> Ads { get; set; }
        int Count { get; }
        void LoadAds(DateTime start, DateTime end);
        List<Ad> GetAds(string field, int pageIndex, int pageSize,
            string sortOrder, SortDirection sortDirection);
        List<Ad> GetAdsInCover(string field, int pageIndex, int pageSize,
            string sortOrder, SortDirection sortDirection);
        void Reset();
    }
}