using System;
using System.Web.UI;
using Sapphire.ExceptionHandling;
using Sapphire.Extensions;

namespace Sapphire.Web.UI
{
  /// <summary>
  /// WebPart with hosted UserControl
  /// </summary>
  [ResolveInsteadOfCallConstructor]
  [ControlBuilder(typeof(SapphireControlBuilder))]
  public class WebPart : System.Web.UI.WebControls.WebParts.WebPart,
                              IErrorVisualized
  {
    private UserControl _hostedControl;
    private ErrorVisualizer _errorVisualizer;

    [HandleException]
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
    }

    [HandleException]
    protected override void CreateChildControls()
    {
      Controls.Add(ErrorVisualizer);
      if (HostedControl != null)
        ErrorVisualizer.Add(HostedControl);
      base.CreateChildControls();
    }

    public ErrorVisualizer ErrorVisualizer
    {
      get
      {
        if (_errorVisualizer == null)
          _errorVisualizer = new ErrorVisualizer();

        return _errorVisualizer;
      }
    }

    public UserControl HostedControl
    {
      get
      {
        if (string.IsNullOrEmpty(HostedControlVirtualPath))
          return null;

        if (_hostedControl == null)
          _hostedControl = Page.LoadAndBuildUpControl(HostedControlVirtualPath);

        return _hostedControl;
      }
    }

    /// <summary>
    /// Override this method if you need to host control
    /// </summary>
    public virtual string HostedControlVirtualPath
    {
      get
      {
        return string.Empty;
      }
    }
  }
}