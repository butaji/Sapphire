using Sapphire.Core.DomainBase;

namespace Sapphire.Core.UnitOfWork
{
  public interface IUnitOfWork
  {
    void RegisterAdded(IEntity entity, IUnitOfWorkRepository repository);
    void RegisterChanged(IEntity entity, IUnitOfWorkRepository repository);
    void RegisterRemoved(IEntity entity, IUnitOfWorkRepository repository);
    void Commit();
    object Key { get; }
  }
}