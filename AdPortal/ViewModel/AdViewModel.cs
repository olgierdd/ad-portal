using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdPortal.Models;
using System.Linq.Dynamic;
using AdPortal.AdManagers;

namespace AdPortal.ViewModel
{
    public class AdViewModel : ViewModelBase
    {
        #region Public Property
        public List<Ad> Ads { get; set; }
        #endregion

        public bool AdsFileSource { get; set; }
        public AdsQueryType AdsQueryType { get; set; }
        private AdManager _mgr;
        public AdViewModel() : base()
        {
            Init();
        }
        #region HandleRequest Method
        public override void HandleRequest()
        {
            base.HandleRequest();

            if (EventCommand == "sort")
            {
                // Check to see if we need to change the sort order 
                // because the sort expression changed
                SetSortDirection();
            }

            switch (EventCommand)
            {
                case "page":
                case "sort":
                    // Setup Pager Object
                    SetPageObject();
                    // Get Ads
                    LoadAds();
                    break;
            }
        }
        #endregion

        #region Init Method
        public void Init()
        {
            Ads = new List<Ad>();
            AdsFileSource = true;
            EventCommand = "page";

            // Set initial sort expression
            SortExpression = "BrandName";
            SortDirection = SortDirection.Ascending;
            LastSortExpression = string.Empty;
            SortDirectionNew = SortDirection.Ascending;
            AdsQueryType = AdsQueryType.AllAds;
        }
        #endregion

        #region Private
        private AdManager GetManager(bool test)
        {
            if (test) return new AdFileManager();
            else return new AdWCFManager();
        }

        private void SetPageObject()
        {
            if (_mgr == null)
            {
                _mgr = GetManager(AdsFileSource);
                _mgr.LoadAds(Convert.ToDateTime("01/01/2011"), Convert.ToDateTime("02/01/2011"));
                Pager.TotalRecords = GetCount();
            }
            SetPagerObject(Pager.TotalRecords);
        }

        #endregion

        #region LoadAds Method
        public void LoadAds()
        {
            switch (AdsQueryType)
            {
                case AdsQueryType.AllAds:
                    Ads = _mgr.GetAds(SortExpression, Pager.PageIndex, Pager.PageSize,
                                 SortExpression,
                                 SortDirection);
                    break;
                case AdsQueryType.AdsInCover:
                    Ads = _mgr.GetAdsInCover(SortExpression, Pager.PageIndex, Pager.PageSize,
                        SortExpression,
                        SortDirection);
                    break;
                case AdsQueryType.Top5Ads:
                    Ads = _mgr.GetTop5Ads();
                    break;
                case AdsQueryType.Top5Brands:
                    Ads = null;
                    break;
            }
        }

        public List<BrandCoverage> GetTop5Brands()
        {
            return _mgr.GetTop5Brands();
        } 
        public int GetCount()
        {
            switch (AdsQueryType)
            {
                case AdsQueryType.AllAds:
                    return _mgr.Ads.Count;
                case AdsQueryType.AdsInCover:
                    return _mgr.GetAdsIncoverCount();
                case AdsQueryType.Top5Ads:
                case AdsQueryType.Top5Brands:
                default:
                    return 5;
            }
        }
        #endregion

        #region SetSortDirection Method
        protected virtual void SetSortDirection()
        {
            if (SortExpression == LastSortExpression)
            {
                SortDirection = SortDirection == SortDirection.Ascending 
                    ? SortDirection.Descending 
                    : SortDirection.Ascending;
            }
            else
            {
                SortDirection = SortDirectionNew;
            }
        }
        #endregion

        #region Sort Method
        protected virtual List<T> Sort<T>(IQueryable<T> list)
        {
            // NOTE: Using System.Linq.Dynamic DLL
            list = list.OrderBy(SortExpression +
              (SortDirection ==
                SortDirection.Ascending ? " ASC" : " DESC"));

            return list.ToList();
        }
        #endregion
    }
}