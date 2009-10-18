using MbUnit.Framework;
using Microsoft.SharePoint;
using Sapphire.Environment.Runtime;
using TypeMock.ArrangeActAssert;

namespace Sapphire.Environment.Tests
{
  public class LanguagesTests
  {
    private LanguagesFactory _factory;

    [SetUp]
    void SetUp()
    {
      _factory = new LanguagesFactory();
    }

    [Test]
    private void language_factory_should_create_python()
    {
      var python = _factory.Create("Python");
      Assert.AreEqual(python.Name, "Python");
    }

    [Test]
    private void python_should_execute_simple_script()
    {
      var python = _factory.Create("Python");
      python.Execute("1 + 1");
    }

    [Test]
    private void python_should_execute_simple_script_and_return_value()
    {
      var python = _factory.Create("Python");
      var result = (int)python.Execute("1 + 1");
      Assert.AreEqual(result, 2);
    }

    [Test]
    private void python_should_assume_variables()
    {
      var python = _factory.Create("Python");
      python.SetVar("X", 2);
      var x = (int)python.GetVar("X");
      Assert.AreEqual(x, 2);
    }

    [Test]
    private void python_should_assume_sharepoint_variables()
    {
      SPWeb fakeWeb = Isolate.Fake.Instance<SPWeb>();
      Isolate.WhenCalled(() => fakeWeb.Title).WillReturn("I'm fake web");
      var python = _factory.Create("Python");
      python.SetVar("__x__", "123");
      python.SetVar("__web__", fakeWeb);
      python.Execute("__x__ = __web__.Title");
      var x = (string)python.GetVar("__x__");
      Assert.AreEqual(x, "I'm fake web");
    }
  }
}