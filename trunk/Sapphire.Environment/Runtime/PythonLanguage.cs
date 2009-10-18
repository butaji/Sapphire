using System;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Microsoft.SharePoint;

namespace Sapphire.Environment.Runtime
{
  public class PythonLanguage : ILanguage
  {
    private readonly ScriptScope _scope;

    public string Name
    {
      get { return "Python"; }
    }

    public PythonLanguage()
    {
      _scope = Python.CreateEngine().CreateScope();
      _scope.Engine.Runtime.LoadAssembly(typeof(string).Assembly);
      _scope.Engine.Runtime.LoadAssembly(typeof(Uri).Assembly);
      _scope.Engine.Runtime.LoadAssembly(typeof(SPList).Assembly);
    }

    public object Execute(string input)
    {
      ScriptSource source = _scope.Engine.CreateScriptSourceFromString(input);
      return source.Execute(_scope);
    }

    public void SetVar(string name, object value)
    {
      _scope.SetVariable(name, value);
    }

    public object GetVar(string name)
    {
      return _scope.GetVariable(name);
    }
  }
}