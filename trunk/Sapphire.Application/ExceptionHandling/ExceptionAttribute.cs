using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Sapphire.ExceptionHandling
{
  public class ExceptionAttribute : HandlerAttribute
  {
    #region Overrides of HandlerAttribute

    public override ICallHandler CreateHandler(IUnityContainer container)
    {
      return new ExceptionHandler();
    }

    #endregion
  }
}