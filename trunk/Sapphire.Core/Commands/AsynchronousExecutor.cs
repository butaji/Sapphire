using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace Sapphire.Core.Commands
{
  public class AsynchronousExecutor : ICommandExecutor
  {
    private readonly SynchronizationContext _synchronizationContext;
    private readonly List<BackgroundWorker> _workers =
      new List<BackgroundWorker>();
    public AsynchronousExecutor(SynchronizationContext
                                  synchronizationContext)
    {
      _synchronizationContext = synchronizationContext;
    }
    public void Execute(Action action)
    {
      // Exception handling is omitted, but this design
      // does allow for centralized exception handling
      ThreadPool.QueueUserWorkItem(o => action());
    }
    public void Execute(Func<Action> function)
    {
      ThreadPool.QueueUserWorkItem(o =>
                                     {
                                       Action continuation = function();
                                       _synchronizationContext.Send(x => continuation(), null);
                                     });
    }
  }
}