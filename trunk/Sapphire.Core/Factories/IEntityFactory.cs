using Sapphire.Core.DomainBase;

namespace Sapphire.Core.Factories
{
  public interface IEntityFactory<T> where T : IEntity
  {
    T Create();
  }
}