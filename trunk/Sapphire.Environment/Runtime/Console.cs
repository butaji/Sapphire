using System.Text;

namespace Sapphire.Environment.Runtime
{
  public class Console
  {
    private readonly StringBuilder _messageBuilder = new StringBuilder();

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