using System;
using Microsoft.SharePoint;
using Sapphire.Environment.Runtime;
using Sapphire.ExceptionHandling;
using Sapphire.Web.UI;
using Console = Sapphire.Environment.Runtime.Console;

namespace Sapphire.Environment.UI.WebControls
{
  public partial class Repl : UserControl
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [HandleException]
    protected virtual void RunButton_Click(object sender, EventArgs e)
    {
      var language = new LanguagesFactory().Create(PythonRadioButton.Text);
      var sharepointContext = new SharePointContext(SPContext.Current, language, new Console());
      language.Execute(ScriptTextBox.Text);
      ResultLabel.Text = sharepointContext.Message;
    }
  }
}