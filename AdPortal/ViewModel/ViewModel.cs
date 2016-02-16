using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace AdPortal.ViewModel
{
    public class ViewModelBase
    {
        #region Constructor

        public ViewModelBase()
        {
            Init();
        }

        #endregion

        #region Public Properties

        public PMPager Pager { get; set; }
        public PMPagerItemCollection Pages { get; set; }
        public SortDirection SortDirection { get; set; }
        public SortDirection SortDirectionNew { get; set; }
        public string SortExpression { get; set; }
        public string LastSortExpression { get; set; }
        public string EventCommand { get; set; }
        public string EventArgument { get; set; }
        public bool IsPagerVisible { get; set; }
        #endregion

        #region Init Method

        public void Init()
        {
            EventCommand = string.Empty;
            EventArgument = string.Empty;

            SortExpression = string.Empty;
            LastSortExpression = string.Empty;
            SortDirection = SortDirection.Ascending;
            SortDirectionNew = SortDirection.Ascending;

            Pager = new PMPager();
            IsPagerVisible = true;
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
            string orderby = SortExpression;

            // NOTE: Using System.Linq.Dynamic DLL
            list = list.OrderBy(SortExpression +
                                (SortDirection == SortDirection.Ascending ? " ASC" : " DESC"));

            return list.ToList();
        }

        #endregion

        #region HandleRequest Method

        public virtual void HandleRequest()
        {
        }

        #endregion

        #region SetPagerObject Method


        protected virtual void SetPagerObject(int totalRecords)
        {
            // Set Pager Information
            Pager.TotalRecords = totalRecords;
            Pager.SetPagerProperties(EventArgument);

            // Build paging collection
            Pages = new PMPagerItemCollection(Pager.TotalRecords, Pager.PageSize, Pager.PageIndex);
            // Set total pages
            Pager.TotalPages = Pages.PageCount;
        }

        #endregion
    }
}
