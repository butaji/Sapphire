using System;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Sapphire.ExceptionHandling
{
  public class ExceptionHandler : ICallHandler
  {
    #region Implementation of ICallHandler

    public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
    {
      throw new NotImplementedException();
    }

    public int Order { get; set; }

    #endregion
  }
}