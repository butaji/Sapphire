using System;

namespace Sapphire.Environment.Runtime
{
  public class LanguagesFactory
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