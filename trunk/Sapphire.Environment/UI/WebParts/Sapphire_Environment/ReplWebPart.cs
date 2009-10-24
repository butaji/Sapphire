using System.Web.UI.WebControls.WebParts;
using Sapphire.Environment.UI.WebControls;

namespace Sapphire.Environment.UI.WebParts.Sapphire_Environment
{
  public sealed class ReplWebPart : WebPart
  {
    public ReplWebPart()
    {
      ExportMode = WebPartExportMode.All;
    }

    protected override void CreateChildControls()
    {
      Repl repl = Page.LoadControl("~/_controlTemplates/Sapphire/Environment/Repl.ascx") as Repl;
      Controls.Add(repl);
      base.CreateChildControls();
    }
  }
}
