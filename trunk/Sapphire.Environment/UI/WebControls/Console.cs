using System.Text;

namespace Sapphire.Environment.UI.WebControls
{
  public class Console
  {
    private StringBuilder _messageBuilder = new StringBuilder();

    public void Write(object message)
    {
      _messageBuilder.Append(message);
    }

    public void WriteLine(object message)
    {
      _messageBuilder.AppendLine(message.ToString());
    }

    public string Message
    {
      get
      {
        return _messageBuilder.ToString();
      }
    }
  }
}