using MbUnit.Framework;
using Sapphire.Environment.Runtime;

namespace Sapphire.Environment.Tests
{
  public class ReplWebPartTests
  {
    private LanguagesFactory _factory;

    [SetUp]
    void init_fields()
    {
      _factory = new LanguagesFactory();
    }

    [Test]
    private void should_have_console()
    {
      var python = _factory.Create("Python");
      python.SetVar("Console", new Console());
      python.Execute("Console.WriteLine('hello world')");
      var x = (Console)python.GetVar("Console");
      Assert.AreEqual(x.Message, "hello world\r\n");
    }
  }
}