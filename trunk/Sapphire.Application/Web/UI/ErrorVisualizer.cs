using System;

namespace Sapphire.Web.UI
{
  public class ErrorVisualizer : UserControl
  {
    public string Message { get; set; }

    public Exception Excetion { get; set; }

    public void Add(UserControl control)
    {
      control.ErrorVisualizer = this;
      Controls.Add(control);
    }
  }
}