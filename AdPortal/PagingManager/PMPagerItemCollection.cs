using System;
using System.Collections.Generic;

namespace AdPortal
{
    public class PMPagerItemCollection : List<PMPagerItem>
    {
        private const int MaxPages = 10;

        #region Constructor

        public PMPagerItemCollection(int rowCount, int pageSize, int pageIndex)
        {
            // Calculate total pages based on RowCount and PageSize
            int pageCount = 0;

            pageCount = Convert.ToInt32(
                Math.Ceiling(
                    Convert.ToDecimal(rowCount)/
                    Convert.ToDecimal(pageSize)));

            // Initialize the collection of pager items
            Init(pageCount, pageIndex);
        }

        #endregion

        #region Public Properties

        public int PageCount { get; set; }

        #endregion

        #region Init Method

        private void Init(int pageCount, int pageIndex)
        {
            int itemIndex = 0;

            PageCount = pageCount;

            Add(new PMPagerItem(PMPagerCommands.FirstText,
                PMPagerCommands.First,
                (pageIndex == 0), PMPagerCommands.FirstTooltipText));
            itemIndex++;
            Add(new PMPagerItem(PMPagerCommands.PreviousText,
                PMPagerCommands.Previous,
                (pageIndex == 0), PMPagerCommands.PreviousTooltipText));
            itemIndex++;

            if (PageCount < MaxPages)
            {
                for (int i = 0; i < PageCount; i++)
                {
                    Add(new PMPagerItem(i, pageIndex,
                        PMPagerCommands.PageText + " " + (i + 1).ToString()));
                    itemIndex++;
                }
            }
            else
            {
                var newPageIndex = pageIndex / 2;
                if (newPageIndex > 0 && newPageIndex != pageIndex - 2)
                {
                    Add(new PMPagerItem(newPageIndex, pageIndex,
                        PMPagerCommands.PageText + " " + (newPageIndex + 1).ToString()));
                    itemIndex++;
                }
                if (pageIndex - 2 > 0)
                {
                    Add(new PMPagerItem(pageIndex - 2, pageIndex,
                        PMPagerCommands.PageText + " " + (pageIndex - 1).ToString()));
                    itemIndex++;
                }
                Add(new PMPagerItem(pageIndex, pageIndex,
                        PMPagerCommands.PageText + " " + (pageIndex + 1).ToString()));
                itemIndex++;
                if (pageIndex + 2 < PageCount)
                {
                    Add(new PMPagerItem(pageIndex + 2, pageIndex,
                        PMPagerCommands.PageText + " " + (pageIndex + 3).ToString()));
                    itemIndex++;
                }
                newPageIndex = pageIndex + (PageCount - pageIndex) / 2;
                if (newPageIndex != pageIndex + 2 && newPageIndex != pageIndex)
                {
                    Add(new PMPagerItem(newPageIndex, pageIndex,
                        PMPagerCommands.PageText + " " + (newPageIndex + 1).ToString()));
                    itemIndex++;
                }
                
            }
            Add(new PMPagerItem(PMPagerCommands.NextText,
                PMPagerCommands.Next,
                (PageCount - 1 == pageIndex), PMPagerCommands.NextTooltipText));
            itemIndex++;
            Add(new PMPagerItem(PMPagerCommands.LastText,
                PMPagerCommands.Last,
                (PageCount - 1 == pageIndex), PMPagerCommands.LastTooltipText));
        }

        #endregion
    }
}
