using System;
using System.Collections.Generic;
using Microsoft.SharePoint;
using Sapphire.Core.DomainBase;

namespace Sapphire.Core.Repositories
{
  public interface IListBasedRepository<T> where T : IAggregateRoot
  {
    string ListUrl { get; }

    int Add(T entity, SPWeb web);

    T FindById(int id, SPWeb web);

    T Find(string caml, SPWeb web);

    IEnumerable<T> FindAll(string caml, int startRow, int rowCount, SPWeb web);

    void Update(T entity, SPWeb web);

    void Delete(int id, SPWeb web);

    string GetFieldName(Guid key, SPWeb web);
  }
}