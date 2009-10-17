using System.Collections.Generic;
using Sapphire.Core.DomainBase;

namespace Sapphire.Core.Repositories
{
  public interface IRepository<T> where T : IAggregateRoot
  {
    int Add(T entity);

    T FindById(int id);

    T Find(string caml);

    IEnumerable<T> FindAll(string caml, int startRow, int rowCount);

    void Update(T entity);

    void Delete(int id);
  }
}