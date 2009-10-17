using Sapphire.Core.DomainBase;

namespace Sapphire.Core.UnitOfWork
{
  public interface IUnitOfWorkRepository
  {
    void PersistNewItem(IEntity item);
    void PersistUpdatedItem(IEntity item);
    void PersistDeletedItem(IEntity item);
  }
}