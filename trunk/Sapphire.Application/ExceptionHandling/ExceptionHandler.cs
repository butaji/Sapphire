using Microsoft.Practices.Unity.InterceptionExtension;

namespace Sapphire.ExceptionHandling
{
  public class ExceptionHandler : ICallHandler
  {
    #region Implementation of ICallHandler

    /// <exception cref="SapphireUserFriendlyException"><c>SapphireUserFriendlyException</c>.</exception>
    public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
    {
      var result = getNext()(input, getNext);
      if (result.Exception == null)
        return result;
      throw new SapphireUserFriendlyException();
    }

    public int Order { get; set; }

    #endregion
  }
}