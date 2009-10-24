using System;

namespace Sapphire.Environment.Runtime
{
  public interface ILanguagesFactory
  {
    ILanguage Create(string name);
  }

  public class LanguagesFactory : ILanguagesFactory
  {
    /// <exception cref="ArgumentException">name</exception>
    public ILanguage Create(string name)
    {
      if (name == "Python")
        return new PythonLanguage();

      throw new ArgumentException("name");
    }
  }
}