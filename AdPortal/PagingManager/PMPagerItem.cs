
namespace AdPortal
{
  public class PMPagerItem
  {
    #region Constructors
    public PMPagerItem(int pageCount, int pageIndex, string tooltip)
    {
      Init();

      Text = (pageCount + 1).ToString();
      Argument = pageCount.ToString();
      IsSelected = (pageCount == pageIndex);
      Tooltip = tooltip;
    }

    public PMPagerItem(string text, string arg, bool disabled, string tooltip)
    {
      Init();

      Text = text;
      Argument = arg;
      Tooltip = tooltip;
      IsDisabled = disabled;
    }
    #endregion

    #region Public Properties
    public string Text { get; set; }
    public string Tooltip { get; set; }
    public string Argument { get; set; }
    public bool IsSelected { get; set; }
    public bool IsDisabled { get; set; }
    public string CssActiveClass { get; set; }
    public string CssDisabledClass { get; set; }
    public string CssClass
    {
      get
      {
        string result = string.Empty;
        if (IsSelected)
        {
          result = CssActiveClass;
        }
        else if (IsDisabled)
        {
          result = CssDisabledClass;
        }
        return result;
      }
    }
    #endregion

    #region Init Method
    public void Init()
    {
      Text = string.Empty;
      Argument = string.Empty;
      Tooltip = string.Empty;
      CssActiveClass = "active";
      CssDisabledClass = "disabled";
      IsSelected = false;
      IsDisabled = false;
    }
    #endregion

    #region ToString Override
    public override string ToString()
    {
      return Text;
    }
    #endregion
  }
}
