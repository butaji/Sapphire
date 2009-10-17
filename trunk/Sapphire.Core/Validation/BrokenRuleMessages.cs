using System.Collections.Generic;

namespace Sapphire.Core.Validation
{
  public abstract class BrokenRuleMessages
  {
    private readonly Dictionary<string, string> _messages;

    protected BrokenRuleMessages()
    {
      _messages = new Dictionary<string, string>();
      PopulateMessages();
    }

    protected Dictionary<string, string> Messages
    {
      get { return _messages; }
    }

    protected abstract void PopulateMessages();

    public string GetRuleDescription(string messageKey)
    {
      string description = string.Empty;
      if (_messages.ContainsKey(messageKey))
      {
        description = _messages[messageKey];
      }
      return description;
    }
  }
}