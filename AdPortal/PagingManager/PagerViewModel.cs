using System.Collections.Generic;
using System.Linq;

namespace AdPortal
{
  public class PagerViewModel
  {
    #region Constructor
    public PagerViewModel()
      : base()
    {
      Init();
    }
    #endregion

    #region Public Properties
    public PMPager Pager { get; set; }
    public PMPagerItemCollection Pages { get; set; }
    #endregion

    #region Init Method
    public void Init()
    {
      Pager = new PMPager();

      SetPagerObject(11);
    }
    #endregion

    #region SetPagerObject Method
    private void SetPagerObject(int totalRecords)
    {
      // Set Pager Information
      Pager.TotalRecords = totalRecords;
      Pager.PageSize = 5;
      Pager.SetPagerProperties(string.Empty);

      // Build paging collection
      Pages = new PMPagerItemCollection(
        Pager.TotalRecords,
        Pager.PageSize, 
        Pager.PageIndex);

      // Set total pages
      Pager.TotalPages = Pages.PageCount;
    }
    #endregion
  }
}
