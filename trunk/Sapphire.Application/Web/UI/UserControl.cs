using System;
using System.Web.UI;
using Sapphire.ExceptionHandling;

namespace Sapphire.Web.UI
{
  [ControlBuilder(typeof(SapphireControlBuilder))]
  [ResolveInsteadOfCallConstructor]
  public class UserControl : System.Web.UI.UserControl,
                              IErrorVisualized
  {
    [HandleException]
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
    }

    [HandleException]
    public override void DataBind()
    {
      base.DataBind();
    }

    public ErrorVisualizer ErrorVisualizer { get; set; }
  }
}