namespace Sapphire.Core.Commands
{
  public interface ICommand<T>
  {
    object Execute(T argument);
  }
}