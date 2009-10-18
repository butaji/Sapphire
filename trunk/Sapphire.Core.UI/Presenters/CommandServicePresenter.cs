using Sapphire.Core.Commands;
using Sapphire.Core.UI.Views;

namespace Sapphire.Core.UI.Presenters
{
  public class CommandServicePresenter : IPresenter
  {
    private readonly IView _view;
    private readonly IService _service;
    private readonly ICommandExecutor _executor;
    public CommandServicePresenter(IView view, IService service,
      ICommandExecutor executor)
    {
      _view = view;
      _service = service;
      _executor = executor;
    }

    public void RefreshData()
    {
      _executor.Execute(() =>
                          {
                            var data = _service.FetchDataFromExtremelySlowServiceCall();
                            return () => _view.UpdateDisplay(data);
                          });
    }

    public IView View
    {
      get
      {
        return _view;
      }
    }
  }
}