using Microsoft.SharePoint;
using Sapphire.Core.DomainBase;
using Sapphire.Core.Mapping;

namespace Sapphire.Core.Repositories
{
  internal interface IListItemRepository<T> where T : IEntity, new()
  {
    SPListItem Add(SPWeb web, string listUrl, ListItemFieldMapper<T> mapper, T entity);
    SPListItem Get(SPWeb web, string listUrl, SPQuery query);
    SPListItemCollection GetAll(SPWeb web, string listUrl, SPQuery query);

    void Update(SPWeb web, string listUrl, int listItemId, ListItemFieldMapper<T> mapper, T entity);
    void Delete(SPWeb web, string listUrl, int listItemId);
  }
}