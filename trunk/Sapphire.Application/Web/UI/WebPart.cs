using System;

namespace Sapphire.Web.UI
{
  public class WebPart : System.Web.UI.WebControls.WebParts.WebPart
  {
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      Sapphire.Application.BuildItemWithCurrentContext(this);
    }
  }
}