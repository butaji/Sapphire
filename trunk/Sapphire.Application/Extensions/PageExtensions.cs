using System.Web;
using System.Web.UI;
using Microsoft.Practices.Unity.InterceptionExtension;
using Sapphire.Web.UI;
using UserControl = Sapphire.Web.UI.UserControl;

namespace Sapphire.Extensions
{
  public static class PageExtensions
  {
    public static UserControl LoadAndBuildUpControl(this Page page, string virtualPath)
    {
      var control = page.LoadControl(virtualPath);
      return SapphireControlBuilder.Build<UserControl>(control.GetType());
    }
  }
}