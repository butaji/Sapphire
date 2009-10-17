namespace Sapphire.Environment.Runtime
{
  public interface ILanguage
  {
    string Name { get; }

    object Execute(params object[] arguments);
  }
}