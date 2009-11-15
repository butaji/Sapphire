using System.Web.UI.WebControls.WebParts;
using WebPart = Sapphire.Web.UI.WebPart;

namespace Sapphire.Environment.UI.WebParts.Sapphire_Environment
{
  public class ReplWebPart : WebPart
  {
    public ReplWebPart()
    {
      ExportMode = WebPartExportMode.All;
    }

    public override string HostedControlVirtualPath
    {
      get { return "~/_controlTemplates/Sapphire/Environment/Repl.ascx"; }
    }
  }
}
