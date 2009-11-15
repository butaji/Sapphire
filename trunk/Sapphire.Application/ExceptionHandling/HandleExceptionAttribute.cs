using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Sapphire.ExceptionHandling
{
  /// <summary>
  /// Don't forget that this attribute work only with virtual methods
  /// by my implementation. See more at <href>http://msdn.microsoft.com/en-us/library/dd140045.aspx</href>
  /// </summary>
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class HandleExceptionAttribute : HandlerAttribute
  {
    #region Overrides of HandlerAttribute

    public override ICallHandler CreateHandler(IUnityContainer container)
    {
      return new ExceptionHandler();
    }

    #endregion
  }
}