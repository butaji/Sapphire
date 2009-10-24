using System.Web.UI.WebControls.WebParts;
using Sapphire.Environment.UI.WebControls;

namespace Sapphire.Environment.UI.WebParts.Sapphire_Environment
{
  public class ReplWebPart : System.Web.UI.WebControls.WebParts.WebPart
  {
    public ReplWebPart()
    {
      this.ExportMode = WebPartExportMode.All;
    }

    protected override void CreateChildControls()
    {
      Repl repl = Page.LoadControl("~/_controlTemplates/Sapphire/Environment/Repl.ascx") as Repl;
      Controls.Add(repl);
      base.CreateChildControls();
    }
  }
}
