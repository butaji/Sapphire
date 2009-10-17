using System.Collections.Generic;
using Sapphire.Core.DomainBase;

namespace Sapphire.Core.Validation
{
  public interface IEntityValidator<T> where T : EntityBase
  {
    bool IsValid(T entityBase);

    IEnumerable<BrokenRule> GetBrokenRules(T entityBase);
  }
}