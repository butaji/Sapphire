using Sapphire.Core.UI.Views;

namespace Sapphire.Core.UI.Presenters
{
  public interface IPresenter
  {
    IView View { get; }
  }

  public interface IService
  {
    string FetchDataFromExtremelySlowServiceCall();
  }
}