using System;

namespace Sapphire.Core.Commands
{
  public interface ICommandExecutor
  {
    // Execute an operation on a background thread that
    // does not update the user interface
    void Execute(Action action);

    // Execute an operation on a background thread and
    // update the user interface using the returned Action
    void Execute(Func<Action> function);
  }
}