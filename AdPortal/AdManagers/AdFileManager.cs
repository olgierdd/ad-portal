using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using AdPortal.Models;
using AdPortal.ViewModel;
using Microsoft.Ajax.Utilities;

namespace AdPortal.AdManagers
{
    internal class AdFileManager : AdManager
    {
        public override void LoadAds(DateTime start, DateTime end)
        {
            Ads = new List<Ad>();
            string physicalPath = HttpContext.Current.Request.MapPath(@"App_Data\AllAds.csv");
            var filePath = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, @"App_Data\AllAds.csv");
            if (Ads == null) return;
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] tk = line.Split(';');
                    Ads.Add(new Ad
                    {
                        AdId = int.Parse(tk[0]),
                        BrandId = int.Parse(tk[1]),
                        BrandName = tk[2],
                        NumPages = decimal.Parse(tk[3]),
                        Position = tk[4]
                    });
                }
                sr.Close();
            }
        }

    }
}