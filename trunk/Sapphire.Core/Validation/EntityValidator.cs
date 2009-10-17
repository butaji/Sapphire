using System.Collections.Generic;
using Sapphire.Core.DomainBase;

namespace Sapphire.Core.Validation
{
  public abstract class EntityValidator<T> : IEntityValidator<T> where T : EntityBase
  {
    private readonly BrokenRuleMessages _brokenRuleMessages;
    private readonly List<BrokenRule> _brokenRules;

    protected List<BrokenRule> BrokenRules
    {
      get { return _brokenRules; }
    }

    #region IEntityValidator<T> Members

    public abstract bool IsValid(T entityBase);

    public IEnumerable<BrokenRule> GetBrokenRules(T entityBase)
    {
      IsValid(entityBase);
      return _brokenRules.AsReadOnly();
    }

    #endregion

    protected abstract BrokenRuleMessages GetBrokenRuleMessages();

    protected void AddBrokenRule(string messageKey)
    {
      _brokenRules.Add(new BrokenRule(messageKey,
                                      _brokenRuleMessages.GetRuleDescription(messageKey)));
    }
  }
}