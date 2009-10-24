using Microsoft.SharePoint;

namespace Sapphire.Environment.Runtime
{
  public class SharePointContext
  {
    private readonly ILanguage _language;
    private readonly Console _console;

    public SharePointContext(SPContext context, ILanguage language, Console console)
    {
      _language = language;
      _console = console;
      language.SetVar("Console", _console);
      language.SetVar("__site__", context.Site);
      language.SetVar("__web__", context.Web);
    }

    public string Message
    {
      get { return _console.Message.Replace(System.Environment.NewLine, "<br />"); }
    }
  }
}