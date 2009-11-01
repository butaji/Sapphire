using System;
using Sapphire.ExceptionHandling;

namespace Sapphire.Web.UI
{
  public class UserControl : System.Web.UI.UserControl
  {
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      Sapphire.Application.BuildItemWithCurrentContext(this);
    }

    [Exception]
    public override void DataBind()
    {
      base.DataBind();
    }
  }
}