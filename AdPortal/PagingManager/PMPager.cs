
using System;
namespace AdPortal
{
  public class PMPager
  {
    #region Constructor
    public PMPager()
    {
      Init();
    }

    public PMPager(int pageSize)
    {
      Init();

      PageSize = pageSize;
    }
    #endregion

    #region Init PM
    public void Init()
    {
      PageSize = 15;
      PageIndex = 0;
      StartingRow = 1;
      TotalPages = 0;
      TotalRecords = 0;
    }
    #endregion

    #region Public Properties
    private int _pageSize = 0;
    public int PageSize
    {
      get { return _pageSize; }
      set
      {
        _pageSize = value;
        CalculateTotalPages();
      }
    }
    public int PageIndex { get; set; }
    public int StartingRow { get; set; }
    public int TotalPages { get; set; }
    private int _totalRecords = 0;
    public int TotalRecords
    {
      get { return _totalRecords; }
      set
      {
        _totalRecords = value;
        CalculateTotalPages();
      }
    }
    #endregion

    #region CalculateTotalPages PM
    public void CalculateTotalPages()
    {
      if (PageSize > 0)
      {
        TotalPages = Convert.ToInt32(
                      Math.Ceiling(
                         Convert.ToDecimal(TotalRecords) /
                         Convert.ToDecimal(PageSize)));       
      }
    }
    #endregion

    #region SetPagerProperties PM
    public void SetPagerProperties(string argument)
    {
      int page = -1;

      if (int.TryParse(argument, out page))
      {
        this.PageIndex = page;
      }
      else
      {
        switch (argument)
        {
          case PMPagerCommands.First:
            this.PageIndex = 0;
            break;

          case PMPagerCommands.Next:
            if (this.PageIndex < this.TotalPages)
            {
              this.PageIndex++;
            }
            break;

          case PMPagerCommands.Previous:
            if (this.PageIndex != 0)
            {
              this.PageIndex--;
            }
            break;

          case PMPagerCommands.Last:
            this.PageIndex = this.TotalPages - 1;
            break;
        }
      }

      StartingRow = (PageIndex * PageSize);
    }
    #endregion
  }
}
