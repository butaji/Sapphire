using MbUnit.Framework;
using Microsoft.SharePoint;
using Sapphire.Environment.Runtime;
using TypeMock.ArrangeActAssert;

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

    [Test]
    private void repl_webpart_should_fire_python_scripts()
    {
      SPWeb web = Isolate.Fake.Instance<SPWeb>();
      SPContext context = Isolate.Fake.Instance<SPContext>();
      Isolate.WhenCalled(() => web.Title).WillReturn("Fake Title");
      Isolate.WhenCalled(() => context.Web).WillReturn(web);
      var language = new LanguagesFactory().Create("Python");
      var sharepointContext = new SharePointContext(context, language, new Console());
      language.Execute("Console.Write(__web__.Title)");
      Assert.AreEqual(sharepointContext.Message, web.Title);
    }
  }
}