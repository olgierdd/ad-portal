using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdPortal.Models
{
    public class Ad
    {
        public int AdId { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public decimal NumPages { get; set; }
        public string Position { get; set; }

        public static Ad Create(int adId, int brandId,
                    string brand, decimal numPages, string position)
        {
            return new Ad {AdId = adId, BrandId = brandId,
                BrandName = brand, NumPages = numPages,
                Position = position};
        }
    }
}