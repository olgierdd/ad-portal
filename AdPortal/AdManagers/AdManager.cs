using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdPortal.Models;
using System.Linq.Dynamic;
using AdPortal.ViewModel;

namespace AdPortal.AdManagers
{
    public abstract class AdManager : IAdManager
    {
        public List<Ad> Ads { get; set; }
        public int Count => Ads.Count;

        private IEnumerable<Ad> DefaultAds()
        {
            return new List<Ad>
            {
                new Ad {AdId = 1, BrandId = 10, BrandName = "Pepsi", NumPages = 0.5M, Position = "Page"},
                new Ad {AdId = 2, BrandId = 20, BrandName = "Nike", NumPages = 0.5M, Position = "Page"},
                new Ad {AdId = 3, BrandId = 44, BrandName = "IBM", NumPages = 0.2M, Position = "Page"},
                new Ad {AdId = 4, BrandId = 13, BrandName = "Sprite", NumPages = 0.8M, Position = "Cover"},
                new Ad {AdId = 18, BrandId = 110, BrandName = "Dell", NumPages = 0.5M, Position = "Page"},
                new Ad {AdId = 12, BrandId = 10, BrandName = "Pepsi", NumPages = 0.5M, Position = "Page"},
                new Ad {AdId = 23, BrandId = 220, BrandName = "Bob", NumPages = 0.5M, Position = "Page"},
                new Ad {AdId = 33, BrandId = 424, BrandName = "Tonsil", NumPages = 0.2M, Position = "Page"},
                new Ad {AdId = 43, BrandId = 13, BrandName = "EB", NumPages = 0.8M, Position = "Cover"},
                new Ad {AdId = 118, BrandId = 1110, BrandName = "Lech", NumPages = 0.5M, Position = "Page"},
                new Ad {AdId = 14, BrandId = 120, BrandName = "Warka", NumPages = 0.5M, Position = "Page"},
                new Ad {AdId = 24, BrandId = 203, BrandName = "Zywiec", NumPages = 0.5M, Position = "Page"},
                new Ad {AdId = 34, BrandId = 424, BrandName = "Kot", NumPages = 0.2M, Position = "Page"},
                new Ad {AdId = 44, BrandId = 123, BrandName = "SPies", NumPages = 0.8M, Position = "Cover"},
                new Ad {AdId = 118, BrandId = 1210, BrandName = "XPS", NumPages = 0.5M, Position = "Page"}

            };
        }


        public abstract void LoadAds(DateTime start, DateTime end);

        public void LoadAds(string start, string end)
        {
            LoadAds(Convert.ToDateTime(start), Convert.ToDateTime(end));
        }

        public List<Ad> GetAds(string field, int pageIndex, int pageSize, string sortOrder, SortDirection sortDirection)
        {
            var list = Ads.AsQueryable()
                .OrderBy(field + " " +
                         (sortDirection == SortDirection.Ascending ? "ASC" : "DESC"))
                .Skip(pageIndex*pageSize)
                .Take(pageSize);
            return list.ToList();
        }

        public List<Ad> GetAdsInCover(string field, int pageIndex, int pageSize, string sortOrder,
            SortDirection sortDirection)
        {
            var list = Ads
                .Where(a => a.Position == "Cover" && a.NumPages > 0.5M)
                .AsQueryable()
                .OrderBy(field + " " +
                         (sortDirection == SortDirection.Ascending ? "ASC" : "DESC"))
                .Skip(pageIndex*pageSize)
                .Take(pageSize);
            return list.ToList();
        }

        public List<Ad> GetTop5Ads()
        {
            var brands = new HashSet<int>();
            var list = Ads
                .OrderByDescending(a => a.NumPages)
                .ThenBy(a => a.BrandName)
                .Where(a => brands.Count < 5 && brands.Add(a.BrandId));
            return list.ToList();
        }

        public List<BrandCoverage> GetTop5Brands()
        {
            var brands = new HashSet<int>();
            var list = Ads
                .GroupBy(a=>a.BrandId,
                        a=>a,
                        (key,g) => new
                        {
                         BrandID = key,
                         Ads = g,
                         Cover = g.Sum(a=>a.NumPages)  
                        })
                .OrderByDescending(g=>g.Cover)
                .Where(a => brands.Count < 5 && brands.Add(a.BrandID))
                .Select(a=> new BrandCoverage {
                    BrandId = a.BrandID,
                    Name = a.Ads.First(b=>b.BrandId == a.BrandID).BrandName,
                    Coverage = a.Cover});
            return list.ToList();
        }
        public int GetAdsIncoverCount() => Ads.Count(a => a.Position == "Cover" && a.NumPages > 0.5M);

        public void Reset()
        {
            Ads = null;
        }
    }
}