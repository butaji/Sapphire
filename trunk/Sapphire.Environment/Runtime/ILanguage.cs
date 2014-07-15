namespace Sapphire.Environment.Runtime
{
  public interface ILanguage
  {
    string Name { get; }

    object Execute(string input);

    void SetVar(string name, object value);

    object GetVar(string name);
  }
}